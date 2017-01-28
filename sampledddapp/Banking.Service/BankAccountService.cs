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
    public interface IBankAccountService
    {
        BankAccount AccountDetails(BankAccount accountInfo);
        BankAccount AccountDetailsByUserName(string userName);
        BankAccount SetUserAccount(string userName);
        string DepositMoney(BankAccount accountInfo, Transaction transactionInfo);
        string WithdrawMoney(BankAccount accountInfo, Transaction transactionInfo);
        BankAccount ValidateAccount(BankAccount accountInfo);
    }
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IUnitOfWork _unitOfWork;
        //Controller
        public BankAccountService(IBankAccountRepository bankAccountRepository, IUnitOfWork unitOfWork)
        {
            this._bankAccountRepository = bankAccountRepository;
            this._unitOfWork = unitOfWork;
        }
        //Balance Enquiry
        public BankAccount AccountDetailsByUserName(string userName)
        {
            var bankAccountInfo = this._bankAccountRepository.GetAll().Where(x => x.UserName.ToLower() == userName.ToLower()).FirstOrDefault();
            return bankAccountInfo;
        }
        public BankAccount AccountDetails(BankAccount accountInfo)
        {
            var bankAccountInfo = this._bankAccountRepository.GetAll().Where(x => x.AccountNumber.ToLower() == accountInfo.AccountNumber.ToLower() && x.UserName.ToLower() == accountInfo.UserName.ToLower()).FirstOrDefault();
            return bankAccountInfo;
        }
        //Validate account
        public BankAccount ValidateAccount(BankAccount accountInfo)
        {
            var bankAccountInfo = this._bankAccountRepository.GetAll().Where(x => x.AccountNumber.ToLower() == accountInfo.AccountNumber.ToLower()).FirstOrDefault();
            return bankAccountInfo;
        }
        //Deposit money
        public string DepositMoney(BankAccount accountInfo, Transaction transactionInfo)
        {
            var bankAccount = this._bankAccountRepository.GetAll().Where(x => x.AccountNumber.ToLower() == accountInfo.AccountNumber.ToLower()).FirstOrDefault();
            bankAccount.Balance += transactionInfo.Amount;
            this._bankAccountRepository.Update(bankAccount);
            Save();
            return "Success";
        }
        //Withdraw money
        public string WithdrawMoney(BankAccount accountInfo, Transaction transactionInfo)
        {
            var bankAccount = this._bankAccountRepository.GetAll().Where(x => x.AccountNumber.ToLower() == accountInfo.AccountNumber.ToLower() && x.UserName.ToLower() == accountInfo.UserName.ToLower()).FirstOrDefault();
            if (bankAccount.Balance < transactionInfo.Amount)
                return "Not enough money to withdraw.";
            bankAccount.Balance -= transactionInfo.Amount;
            this._bankAccountRepository.Update(bankAccount);
            Save();
            return "Success";
        }
        public BankAccount SetUserAccount(string userName)
        {
            BankAccount bankAccount = new BankAccount();
            var accountNo = this._bankAccountRepository.GetAll().Select(x => x.AccountNumber).Count() > 0 ? (this._bankAccountRepository.GetAll().Select(x => Convert.ToInt32(x.AccountNumber)).Max() + 1).ToString()  : "100000000";
            bankAccount.AccountNumber = accountNo;
            bankAccount.Balance = 1000;
            bankAccount.UserName = userName;            
            this._bankAccountRepository.Add(bankAccount);
            Save();
            return bankAccount;
        }
        //Save bank account info
        public void Save()
        {
            this._unitOfWork.Commit();
        }


    }


}
