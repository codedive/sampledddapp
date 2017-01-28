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
    public class BalanceEnquiryController : Controller
    {
        private IBankAccountService _bankAccountService;
        private ITransactionService _transactionService;

        public BalanceEnquiryController(IBankAccountService bankAccountService, ITransactionService transactionService)
        {
            this._bankAccountService = bankAccountService;
            this._transactionService = transactionService;
        }

        #region Check Account Balance
        // GET: BalanceEnquiry
        public ActionResult CheckAccountBalance()
        {

            BankAccount accountBalanceInfo = this._bankAccountService.AccountDetailsByUserName(User.Identity.Name);
            BankAccountViewModel bankAccountViewModel = Mapper.Map<BankAccount, BankAccountViewModel>(accountBalanceInfo);           
            return View(bankAccountViewModel);
        }

        //Post
        [HttpPost]
        public ActionResult CheckAccountBalance(BankAccountViewModel bankAccountViewModel)
        {
            BankAccount bankAccount = Mapper.Map<BankAccountViewModel, BankAccount>(bankAccountViewModel);
            BankAccount accountBalanceInfo = this._bankAccountService.AccountDetails(bankAccount);
            if (accountBalanceInfo == null)
            {
                ViewBag.NoAccount = "NotFound";
                ViewBag.AccountInfo = new BankAccount();
            }
            else
                ViewBag.AccountInfo = accountBalanceInfo;
            return View();
        }
        #endregion

        #region Mini statement
        public ActionResult MiniStatement()
        {
            BankAccount accountBalanceInfo = this._bankAccountService.AccountDetailsByUserName(User.Identity.Name);
            BankAccountViewModel bankAccountViewModel = Mapper.Map<BankAccount, BankAccountViewModel>(accountBalanceInfo);
            return View(bankAccountViewModel);
        }
        [HttpPost]
        public ActionResult MiniStatement(BankAccountViewModel bankAccountViewModel)
        {
            BankAccount bankAccount = Mapper.Map<BankAccountViewModel, BankAccount>(bankAccountViewModel);
            var accountDetails = this._bankAccountService.AccountDetails(bankAccount);
            if (accountDetails != null)
            {
                ViewBag.LoggedAccount = accountDetails.Id;
                List<Transaction> result = this._transactionService.GetMiniStatement(accountDetails.Id);
                List<TransactionViewModel> miniStatement = Mapper.Map<List<Transaction>, List<TransactionViewModel>>(result);
                ViewBag.Transactions = miniStatement;
            }
            else
            {
                ViewBag.NoAccount = "NotFound";
                ViewBag.Transactions = new List<Transaction>();
            }
            return View();
        }
        #endregion
    }
}