using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podium_Technical_Test.Models
{
    public class MortgageResult
    {
        public int Id { get; set;}
        public string UniqueID { get; set; }
        public float PropertyValue { get; set; }
        public float DepositAmount { get; set; }
        public virtual Applicant Applicant { get; set; }
    }
}
