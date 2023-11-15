using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Wompi.Core.EntityModels.API.Request;
using Wompi.Core.EntityModels;
using Wompi.Core.IRepositories;
using Newtonsoft.Json.Linq;
using Wompi.Data.Context;

namespace Wompi.Business.Services
{
    public class GeLinkService : IGeLinkRepository
    {
        private readonly HttpClient _httpClient;
        private readonly WompiDevDbContext wompiDevDbContext;
        public GeLinkService(HttpClient httpClient, WompiDevDbContext context)
        {
            wompiDevDbContext = context;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "prv_test_H2tLsaR84CB8UojpYfAQu1h0yzE9GKXQ");
        }

        public GeLink GetLinkById(string idLinkWompi)
        {
            return wompiDevDbContext.GeLinks.FirstOrDefault(link => link.IdLinkWompi == idLinkWompi);
        }


        public async Task<GeLink> CreateWompiLink(WompiLinkRequest wompiLinkRequest)
        {
            string apiUrl = "https://sandbox.wompi.co/v1/payment_links";
            var response = await _httpClient.PostAsJsonAsync(apiUrl, wompiLinkRequest);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {

                var jObject = JsonConvert.DeserializeObject<JObject>(result);
                var idLinkWompi = jObject["data"]?["id"]?.ToString();
                var geLink = new GeLink
                {
                    IdLinkWompi = idLinkWompi,
                    JsonRequest = JsonConvert.SerializeObject(wompiLinkRequest),
                    JsonWompiResponse = result, 
                    TransactionState = "PENDING"
                };
               wompiDevDbContext.GeLinks.Add(geLink);
               await wompiDevDbContext.SaveChangesAsync();
               return geLink;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
        }
    }
}