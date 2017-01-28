using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Data.Infrastructure
{
    public class DatabaseFactory :  IDatabaseFactory
    {
        private BankingEntities dataContext;

        public void Dispose()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }

        public BankingEntities Get()
        {
            return dataContext ?? (dataContext = new BankingEntities());
        }
    }
}
