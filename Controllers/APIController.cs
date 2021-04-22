using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Podium_Technical_Test.Models;

namespace Podium_Technical_Test.Controllers
{
    public class APIController : Controller
    {
        private static List<Applicant> ApplicantsList = new List<Applicant>();

        private bool CheckAge(DateTime DateOfBirth)
        {
            int TodaysDate = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int DateOfBirthConverted = int.Parse(DateOfBirth.ToString("yyyyMMdd"));
            int Age = (TodaysDate - DateOfBirthConverted) / 10000;
            if(Age < 18)
            {
                return false;
            }
            else
            {
                return true;
            }   
        }
        [HttpGet]
        public JsonResult MortgageSearch(decimal PropertyValue, decimal DepositAmount, string UniqueId)
        {
            var Applicant = ApplicantsList.Where(a => a.UniqueID == UniqueId).FirstOrDefault() ?? null;
            if (Applicant == null)
            {
                return Json("Applicant Not Found");
            }
            decimal UserLTV = (DepositAmount / PropertyValue) * 100;
            if((100 - UserLTV) > 90)
            {
                return Json("Deposit To Low For Loan Amount");
            }
            var MortgagesList = PopulateMortgageList();
            return Json(MortgagesList.Where(a => (100 - UserLTV) < a.LTV).ToList());
        }
        [HttpGet]
        public JsonResult ApplicantRequest(string Forname,string Surname,DateTime? DateOfBirth,string Email)
        {
            if(Forname == null || Surname == null || Email == null || DateOfBirth == null)
            {
                return Json("Details Are Not Correct");
            }
            if (!CheckAge((DateTime)DateOfBirth))
            {
                return Json("Age Below 18");
            }
            var Applicant = new Applicant()
            {
                UniqueID = Guid.NewGuid().ToString(),
                Forname = Forname,
                Surname = Surname,
                DateOfBirth = (DateTime)DateOfBirth,
                Email = Email
            };
            ApplicantsList.Add(Applicant);
            return Json(Applicant.UniqueID);
        }
        private List<MortgageList> PopulateMortgageList()
        {
            var MortgagesList = new List<MortgageList>
            {
                new MortgageList
                {
                    Bank = "Bank A",
                    InterestRate = 2,
                    Type = "Variable",
                    LTV = 60
                },
                new MortgageList
                {
                    Bank = "Bank B",
                    InterestRate = 3,
                    Type = "Fixed",
                    LTV = 60
                },
                new MortgageList
                {
                    Bank = "Bank C",
                    InterestRate = 4,
                    Type = "Variable",
                    LTV = 90
                }
            };

            return MortgagesList;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
