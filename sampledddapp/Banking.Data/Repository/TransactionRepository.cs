using Banking.Data.Infrastructure;
using Banking.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Data.Repository
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
        }
    }
    public interface ITransactionRepository : IRepository<Transaction>
    {

    }
}
