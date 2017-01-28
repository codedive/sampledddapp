using AutoMapper;
using Banking.Model.Models;
using Banking.Service;
using Banking.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Banking.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private IBankAccountService _bankAccountService;
        private ITransactionService _transactionService;

        public TransactionController(IBankAccountService bankAccountService, ITransactionService transactionService)
        {
            this._bankAccountService = bankAccountService;
            this._transactionService = transactionService;
        }
        // GET: Transaction
        public ActionResult DepositMoney()
        {
            CreditDebitMoneyViewModel creditDebitMoneyViewModel = new CreditDebitMoneyViewModel();
            BankAccount accountBalanceInfo = this._bankAccountService.AccountDetailsByUserName(User.Identity.Name);
            creditDebitMoneyViewModel.bankAccountViewModel = Mapper.Map<BankAccount, BankAccountViewModel>(accountBalanceInfo);
            return View(creditDebitMoneyViewModel);
        }

        //Post
        //Deposit money
        [HttpPost]
        public ActionResult DepositMoney(CreditDebitMoneyViewModel model)
        {
            var bankAccount = Mapper.Map<BankAccountViewModel, BankAccount>(model.bankAccountViewModel);
            var transaction = Mapper.Map<TransactionViewModel, Transaction>(model.transactionViewModel);
            BankAccount accountDetails = this._bankAccountService.AccountDetails(bankAccount);
            if (accountDetails != null)
            {
                model.Status = this._bankAccountService.DepositMoney(bankAccount, transaction);
                transaction.ToAccountId = accountDetails.Id;
                this._transactionService.CreateTransaction(transaction);
                var bankInfoView = Mapper.Map<BankAccount, BankAccountViewModel>(accountDetails);
                model.bankAccountViewModel = bankInfoView;
            }
            else
                model.Status = "Please provide valid credentials to deposit money.";
            ViewBag.TransactionInfo = model;
            return View();
        }
        //Get
        public ActionResult TransferMoney()
        {
            TransferMoneyViewModel transferMoneyViewModel = new TransferMoneyViewModel();
            BankAccount accountBalanceInfo = this._bankAccountService.AccountDetailsByUserName(User.Identity.Name);
            transferMoneyViewModel.fromBankAccountViewModel = Mapper.Map<BankAccount, BankAccountViewModel>(accountBalanceInfo);
            return View(transferMoneyViewModel);           
        }

        //Transfer money
        [HttpPost]
        public ActionResult TransferMoney(TransferMoneyViewModel model)
        {
            if (model.fromBankAccountViewModel.AccountNumber.ToLower() != model.toBankAccountViewModel.AccountNumber.ToLower())
            {
                var fromBankAccount = Mapper.Map<BankAccountViewModel, BankAccount>(model.fromBankAccountViewModel);
                var toBankAccount = Mapper.Map<BankAccountViewModel, BankAccount>(model.toBankAccountViewModel);
                var transaction = Mapper.Map<TransactionViewModel, Transaction>(model.transactionViewModel);
                BankAccount fromBankAccountDetails = this._bankAccountService.AccountDetails(fromBankAccount);
                BankAccount toBankAccountDetails = this._bankAccountService.ValidateAccount(toBankAccount);


                if (fromBankAccountDetails == null)
                {
                    model.TransferStatus = "Please provide valid credentials.";

                }
                else if (toBankAccountDetails == null)
                {
                    model.TransferStatus = "Please provide a valid account details to transfer money to.";
                }
                else if (fromBankAccountDetails.Balance < transaction.Amount)
                {
                    model.TransferStatus = "You don't have enough balance to transfer this amount.";
                }
                else
                {
                    string withdrawStatus = this._bankAccountService.WithdrawMoney(fromBankAccount, transaction);
                    string depositedStatus = this._bankAccountService.DepositMoney(toBankAccount, transaction);

                    transaction.ToAccountId = toBankAccountDetails.Id;
                    transaction.FromAccountId = fromBankAccountDetails.Id;
                    this._transactionService.CreateTransaction(transaction);
                    if (withdrawStatus == "Success" && depositedStatus == "Success")
                        model.TransferStatus = "Success";
                    var fromBankInfoView = Mapper.Map<BankAccount, BankAccountViewModel>(fromBankAccountDetails);
                    var toBankInfoView = Mapper.Map<BankAccount, BankAccountViewModel>(toBankAccountDetails);
                    model.fromBankAccountViewModel = fromBankInfoView;
                    model.toBankAccountViewModel = toBankInfoView;
                }
            }
            else
                model.TransferStatus = "Account numbers cannot be same.";
            ViewBag.TransactionInfo = model;
            return View();
        }

        //Withdraw money
        public ActionResult WithdrawMoney()
        {
            CreditDebitMoneyViewModel creditDebitMoneyViewModel = new CreditDebitMoneyViewModel();
            BankAccount accountBalanceInfo = this._bankAccountService.AccountDetailsByUserName(User.Identity.Name);
            creditDebitMoneyViewModel.bankAccountViewModel = Mapper.Map<BankAccount, BankAccountViewModel>(accountBalanceInfo);
            return View(creditDebitMoneyViewModel);            
        }
        //Withdraw money
        [HttpPost]
        public ActionResult WithdrawMoney(CreditDebitMoneyViewModel model)
        {
            var bankAccount = Mapper.Map<BankAccountViewModel, BankAccount>(model.bankAccountViewModel);
            var transaction = Mapper.Map<TransactionViewModel, Transaction>(model.transactionViewModel);
            BankAccount accountDetails = this._bankAccountService.AccountDetails(bankAccount);
            if (accountDetails != null)
            {
                string result = this._bankAccountService.WithdrawMoney(bankAccount, transaction);
                if (result == "Success")
                {
                    transaction.FromAccountId = accountDetails.Id;
                    this._transactionService.CreateTransaction(transaction);
                    var bankInfoView = Mapper.Map<BankAccount, BankAccountViewModel>(accountDetails);
                    model.bankAccountViewModel = bankInfoView;
                }
                //model.Status = "Not enough money to withdraw.";
                model.Status = result;
            }
            else
                model.Status = "Please provide valid credentials to withdraw money.";
            ViewBag.TransactionInfo = model;
            return View();
        }

    }
}