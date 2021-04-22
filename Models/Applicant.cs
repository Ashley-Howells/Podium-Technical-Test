using System;

namespace Podium_Technical_Test.Models
{
    public class Applicant
    {
        public string UniqueID { get; set; }
        public string Forname { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
    }
}
