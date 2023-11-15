namespace Wompi.Core.EntityModels;

public partial class GeLink
{
    public int IdGeLink { get; set; }

    public string? IdLinkWompi { get; set; }

    public string? JsonRequest { get; set; }

    public string? JsonWompiResponse { get; set; }

    public string? TransactionState { get; set; }
}
