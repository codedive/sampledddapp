using Banking.Data.Infrastructure;
using Banking.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Banking.Data.Repository
{
    public class BankAccountRepository : BaseRepository<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
        }
    }

    public interface IBankAccountRepository : IRepository<BankAccount>
    {

    }
}
