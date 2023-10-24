using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafetyAndSecurityForNSI.ViewModel
{
    public class TaxYearViewModel
    {
        [Key]
        public Guid TaxYear { get; set; }
        public int SocialSecurityNumber { get; set; }
        public int NID { get; set; }
        public string ITIN { get; set; }
        public string EIN { get; set; }

    }
}