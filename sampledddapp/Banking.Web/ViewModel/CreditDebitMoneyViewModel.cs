using Banking.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banking.Web.ViewModel
{
    public class CreditDebitMoneyViewModel
    {
        public BankAccountViewModel bankAccountViewModel { get; set; }
        public TransactionViewModel transactionViewModel { get; set; }
        public string Status { get; set; }
    }
}