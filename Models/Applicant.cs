using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podium_Technical_Test.Models
{
    public class Applicant
    {
        public string UniqueID { get; set; }
        public string Forname { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public virtual ICollection<MortgageResult> MortgageResult { get; set; }
    }
}
