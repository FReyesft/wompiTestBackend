using System;
using System.Collections.Generic;

namespace Wompi.Core.EntityModels;

public partial class GeTransaction
{
    public int IdGeTransaction { get; set; }

    public string? JsonWompiResponse { get; set; }

    public string? IdLinkWompi { get; set; }

    public string? IdTransactionWompi { get; set; }

    public string? TransactionState { get; set; }
}
