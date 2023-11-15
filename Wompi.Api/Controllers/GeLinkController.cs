using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Wompi.Business.Services;
using Wompi.Core.EntityModels;
using Wompi.Core.EntityModels.API.Request;

namespace Wompi.Api.Controllers
{
    public class GeLinkController : ControllerBase
    {
        private readonly GeLinkService geLinkService;

        public GeLinkController(GeLinkService geLinkService)
        {
            this.geLinkService=geLinkService;
        }

        [HttpPost]
        [Route("api/create-link")]
        public async Task<ActionResult> CreateWompiLink([FromBody] WompiLinkRequest wompiLinkRequest)
        {
            try
            {
                var geLinkResponse = await geLinkService.CreateWompiLink(wompiLinkRequest);
                if (geLinkResponse != null)
                {
                    return Ok(new GeLink
                    {
                        IdLinkWompi = geLinkResponse.IdLinkWompi,
                        JsonRequest = geLinkResponse.JsonRequest,
                        JsonWompiResponse = geLinkResponse.JsonWompiResponse,
                        TransactionState = "PENDING"
                    });
                }
                else
                {
                    return BadRequest(new { ErrorMessage = "GeLink Response es Nulo" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorMessage = $"Error al crear un nuevo enlace: {ex}" });
            }
        } 
    }
}
