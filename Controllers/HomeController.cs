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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private class Applicant
        {
            public string UniqueID { get; set; }
            public string Forname { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Email { get; set; }

        }
        private class Applicants
        {
            public List<Applicant> ApplicantList { get; set; }

            public Applicants()
            {
                ApplicantList = new List<Applicant>();
            }
        }
        
        public IActionResult Index(string UniqueId)
        {
            ViewBag.UniqueId = UniqueId ?? "No id";
            return View();
        }
        [HttpPost]
        public JsonResult ApplicantRequest(string Forname,string Surname,DateTime DateOfBirth,string Email)
        {
            Applicants Applicants = new Applicants();
            var RandomisedUniqueID = Guid.NewGuid().ToString();
            var Applicant = new Applicant()
            {
                UniqueID = RandomisedUniqueID,
                Forname = Forname,
                Surname = Surname,
                DateOfBirth = DateOfBirth,
                Email = Email
            };
            Applicants.ApplicantList.Add(Applicant);
            return Json(RandomisedUniqueID);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
