using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wompi.Core;
using Wompi.Core.IRepositories;

namespace Wompi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGeLinkRepository GeLink => throw new NotImplementedException();

        public IGeTransactionRepository GeTransaction => throw new NotImplementedException();

        public Task<int> CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
