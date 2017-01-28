using Banking.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Banking.Web.Controllers
{
    public class HomeController : Controller
    {
       
       // private readonly IBankAccountService _bankAccountService;
        public HomeController(IBankAccountService bankAccountService)
        {
           // _bankAccountService = bankAccountService;
        }
        [Authorize]
        public ActionResult Index()
        {
            //_bankAccountService.BankAccount();
            return View();
        }

        
    }
}