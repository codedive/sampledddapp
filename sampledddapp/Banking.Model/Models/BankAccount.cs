using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Model.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string UserName { get; set; }
        public double Balance { get; set; }
        
        [InverseProperty("FromBankAccount")]
        public virtual ICollection<Transaction> TransactionFrom { get; set; }
        [InverseProperty("ToBankAccount")]
        public virtual ICollection<Transaction> TransactionTo { get; set; }
    }
}
