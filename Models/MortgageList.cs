using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podium_Technical_Test.Models
{
    public class MortgageList
    {
        public int Id { get; set; }

        public string Bank { get; set; }

        public decimal InterestRate { get; set; }

        public string Type { get; set; }

        public decimal LTV { get; set; }

    }
}
