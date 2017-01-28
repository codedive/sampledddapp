using Banking.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Data.Configuration
{
    class TransactionConfiguration : EntityTypeConfiguration<Transaction>
    {
        public TransactionConfiguration() 
        {
            this.HasOptional(m=>m.FromBankAccount)
                                 .WithMany().HasForeignKey(m => m.FromAccountId).WillCascadeOnDelete(false);
            this.HasOptional(m => m.ToBankAccount)
                                 .WithMany().HasForeignKey(m => m.ToAccountId).WillCascadeOnDelete(false);
        }
    }
}
