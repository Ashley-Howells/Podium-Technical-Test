using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Podium_Technical_Test.Models;

namespace Podium_Technical_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static List<Applicant> ApplicantsList = new List<Applicant>();
        public static List<MortgageList> MortgagesList = new List<MortgageList>();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            PopulateMortgageList();
            return View();
        }
        private int GetAge(DateTime DateOfBirth)
        {
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int dob = int.Parse(DateOfBirth.ToString("yyyyMMdd"));
            int age = (now - dob) / 10000;
            return age;
        }
        [HttpGet]
        public JsonResult MortgageSearch(decimal PropertyValue, decimal DepositAmount, string UniqueId)
        {
            decimal UserLTV = (DepositAmount / PropertyValue) * 100;
            var Applicant = ApplicantsList.Where(a => a.UniqueID == UniqueId).FirstOrDefault();
            if(GetAge(Applicant.DateOfBirth) < 18 )
            {
                return Json("Age");
            }
            if(UserLTV > 90)
            {
                return Json("LTV");
            }
            var MortgageList = MortgagesList.Where(a => (100 - UserLTV) < a.LTV).ToList();
            return Json(MortgageList);
        }
        [HttpGet]
        public JsonResult ApplicantRequest(string Forname,string Surname,DateTime DateOfBirth,string Email)
        {
            var RandomisedUniqueID = Guid.NewGuid().ToString();
            var Applicant = new Applicant()
            {
                UniqueID = RandomisedUniqueID,
                Forname = Forname,
                Surname = Surname,
                DateOfBirth = DateOfBirth,
                Email = Email
            };
            ApplicantsList.Add(Applicant);
            return Json(RandomisedUniqueID);
        }
        private void PopulateMortgageList()
        {
            var List = "Bank A,2,Variable,60&Bank B,3,Fixed,60&Bank C,4,Variable,90";
            foreach(var Item in List.Split("&"))
            {
                var ItemSplit = Item.Split(",");
                var Mortgage = new MortgageList()
                {
                    Bank = ItemSplit[0],
                    InterestRate = decimal.Parse(ItemSplit[1]),
                    Type = ItemSplit[2],
                    LTV = decimal.Parse(ItemSplit[3])
                };
                MortgagesList.Add(Mortgage);
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
