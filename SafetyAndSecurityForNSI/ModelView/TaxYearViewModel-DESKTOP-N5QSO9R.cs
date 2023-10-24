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
        public long SocialSecurityNumber { get; set; }
        public Nullable<long> NID { get; set; }
        public Nullable<long> ITIN { get; set; }
        public string EIN { get; set; }

    }
}