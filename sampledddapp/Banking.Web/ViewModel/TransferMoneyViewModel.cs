using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banking.Web.ViewModel
{
    public class TransferMoneyViewModel
    {
        public BankAccountViewModel fromBankAccountViewModel { get; set; }
        public BankAccountViewModel toBankAccountViewModel { get; set; }
        public TransactionViewModel transactionViewModel { get; set; }
        public string TransferStatus { get; set; }
    }
}