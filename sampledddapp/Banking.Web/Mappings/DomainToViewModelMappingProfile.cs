using AutoMapper;
using Banking.Model.Models;
using Banking.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banking.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<BankAccount , BankAccountViewModel>();
            Mapper.CreateMap<Transaction , TransactionViewModel>();
        }
    }
}