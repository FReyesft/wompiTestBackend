using Microsoft.AspNetCore.Mvc;
using Wompi.Business.Services;
using Wompi.Core.EntityModels;
using Wompi.Core.EntityModels.API.Request;

[ApiController]
[Route("api")]
public class ReceiverWompiEventController : ControllerBase
{
    private readonly ReceiverWompiEventService wompiEventService;
    private readonly GeLinkService geLinkService;
    public ReceiverWompiEventController(ReceiverWompiEventService wompiEventService, GeLinkService geLinkService)
    {
        this.wompiEventService = wompiEventService;
        this.geLinkService = geLinkService;
    }

    [HttpGet]
    [Route("get-event")]
    public async Task<ActionResult> GetEventTransactionState(string idLinkWompi)
    {
        try
        {
            var linkRecord = geLinkService.GetLinkById(idLinkWompi);

            if (linkRecord != null)
            {
                return Ok(new
                {
                    JsonWompiResponse = linkRecord.JsonWompiResponse,
                    IdLinkWompi = linkRecord.IdLinkWompi,
                    TransactionState = linkRecord.TransactionState
                });
            }
            else
            {
                return NotFound($"No se encontró ningún registro con IdLinkWompi: {idLinkWompi}");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al obtener el registro: {ex.Message}");
        }
    }


    [HttpPost("event")]
    public async Task<ActionResult> ReceiveEvent([FromBody] WompiTransactionEventRequest request)
    {
        try
        {
            var eventResponse = await wompiEventService.ReceiveEvent(request);
            if (eventResponse != null)
            {
                return Ok(new GeTransaction
                {
                    JsonWompiResponse = eventResponse.JsonWompiResponse,
                    IdLinkWompi = eventResponse.IdLinkWompi,
                    IdTransactionWompi = eventResponse.IdTransactionWompi,
                    TransactionState = eventResponse.TransactionState,
                }); 
            }
            else
            {
                return BadRequest(new { ErrorMessage = "GeLink Response es Nulo" });
            }
        }
        catch(Exception ex) { 
            return BadRequest(ex.Message);
        }
    }
}
