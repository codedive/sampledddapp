using AutoMapper;
using Banking.Model.Models;
using Banking.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banking.Web.Mappings
{
    public class ViewModelToDomainMappingProfile :Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<BankAccountViewModel, BankAccount>();
            Mapper.CreateMap<TransactionViewModel, Transaction>();
        }
    }
}