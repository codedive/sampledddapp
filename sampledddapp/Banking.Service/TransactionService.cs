using Banking.Data.Infrastructure;
using Banking.Data.Repository;
using Banking.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Service
{
    public interface ITransactionService
    {
        List<Transaction> GetMiniStatement(int accountId);
        void CreateTransaction(Transaction transaction);
    }
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        //Controller
        public TransactionService(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            this._transactionRepository = transactionRepository;
            this._unitOfWork = unitOfWork;
        }
        //Get Mini Statement
        public List<Transaction> GetMiniStatement(int accountId)
        {
            var transactions = this._transactionRepository.GetAll().Where(a => a.FromAccountId == accountId || a.ToAccountId == accountId).OrderByDescending(x => x.Date).Take(5).ToList();
            return transactions;
        }
        //Create transacation
        public void CreateTransaction(Transaction transaction)
        {
            transaction.Date = DateTime.Now;
            if(transaction.FromAccountId == 0)
                transaction.FromAccountId = null;
            if (transaction.ToAccountId == 0)
                transaction.ToAccountId = null;
            this._transactionRepository.Add(transaction);
            SaveTransaction();
        }
        //Save transaction
        public void SaveTransaction()
        {
            this._unitOfWork.Commit();
        }
    }
}
