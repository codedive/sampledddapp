using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Web.ViewModel
{
    public class BankAccountViewModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Please enter the account number")]
        public string AccountNumber { get; set; }
        [Required (ErrorMessage = "Please enter the username")]
        public string UserName { get; set; }
        public double Balance { get; set; }
        public virtual ICollection<TransactionViewModel> TransactionFrom { get; set; }
        public virtual ICollection<TransactionViewModel> TransactionTo { get; set; }
    }
}
