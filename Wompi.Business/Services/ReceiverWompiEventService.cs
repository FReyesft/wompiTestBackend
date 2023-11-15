using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Wompi.Core.EntityModels;
using Wompi.Core.EntityModels.API.Request;
using Wompi.Core.IRepositories;
using Wompi.Data.Context;

namespace Wompi.Business.Services
{
    public class ReceiverWompiEventService : IGeTransactionRepository
    {
        private readonly HttpClient _httpClient;
        private readonly WompiDevDbContext _wompiDevDbContext;

        public ReceiverWompiEventService(HttpClient httpClient, WompiDevDbContext context)
        {
            _wompiDevDbContext = context;
            _httpClient = httpClient;
        }



        public async Task<GeTransaction> ReceiveEvent(WompiTransactionEventRequest wompiEventRequest)
        {
            string apiUrl = "https://run.mocky.io/v3/27966b2d-34b8-4a6c-90b9-d04359c8cbc0";
            var response = await _httpClient.PostAsJsonAsync(apiUrl, wompiEventRequest);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                //Procesar los datos :D
                var jObject = JsonConvert.DeserializeObject<JObject>(result);
                var idLinkWompi = jObject["data"]?["transaction"]?["payment_link_id"]?.ToString();
                var idTransactionWompi = jObject["data"]?["transaction"]?["id"]?.ToString();
                var transactionState = jObject["data"]?["transaction"]?["status"]?.ToString();

                // Buscar el registro en la tabla Link :D
                var existingLink = _wompiDevDbContext.GeLinks.FirstOrDefault(link => link.IdLinkWompi == idLinkWompi);

                if (existingLink != null)
                {
                    // Actualizar el estado de la transacción :D
                    existingLink.TransactionState = transactionState;
                    _wompiDevDbContext.SaveChanges();
                }

                var geTransaction = new GeTransaction
                {
                    JsonWompiResponse = result,
                    IdLinkWompi = idLinkWompi,
                    IdTransactionWompi = idTransactionWompi,
                    TransactionState = transactionState,
                };

                _wompiDevDbContext.GeTransactions.Add(geTransaction);
                await _wompiDevDbContext.SaveChangesAsync();

                return geTransaction;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
        }
    }
}