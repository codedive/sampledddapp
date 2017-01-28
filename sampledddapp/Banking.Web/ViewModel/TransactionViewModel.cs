using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Web.ViewModel
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter the account number to transfer money from.")]
        public int FromAccountId { get; set; }
        [Required(ErrorMessage = "Please enter the account number to transfer money to.")]
        public int ToAccountId { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please enter the amount.")]
        public double Amount { get; set; }
        public virtual BankAccountViewModel FromBankAccount { get; set; }
        public virtual BankAccountViewModel ToBankAccount { get; set; }
    }
}
