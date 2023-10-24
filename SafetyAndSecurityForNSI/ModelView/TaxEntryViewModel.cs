using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafetyAndSecurityForNSI.ViewModel
{
    public class TaxEntryViewModel
    {
        [Key]
        public Guid TaxId { get; set; }
        public Nullable<int> SocialSecurityNumber { get; set; }
        public Nullable<int> NID { get; set; }
        public string ITIN { get; set; }
        public string EIN { get; set; }
        public string PassportNo { get; set; }
        public string TaxImageUrl { get; set; }
        public string TaxYear { get; set; }
        public string Gender { get; set; }
        public string PersonImage { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}