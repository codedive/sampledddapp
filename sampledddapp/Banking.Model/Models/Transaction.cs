using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Model.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        
        public int? FromAccountId { get; set; }
        public virtual BankAccount FromBankAccount { get; set; }
        public int? ToAccountId { get; set; }
        public virtual BankAccount ToBankAccount { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        
    }
}
