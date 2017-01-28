using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        BankingEntities Get();
    }
}
