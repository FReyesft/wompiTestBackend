using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wompi.Core.IRepositories;

namespace Wompi.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IGeLinkRepository GeLink {  get; }
        IGeTransactionRepository GeTransaction { get; }
        Task<int> CommitAsync();
    }
}
