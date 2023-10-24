using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SafetyAndSecurityForNSI.ViewModel
{
    public class PoliceClearanceViewModel
    {
        [Key]
        public Guid ThanaId { get; set; }
        public string ThanaName { get; set; }
        public string TokenNumber { get; set; }
        public string ThanaDetails { get; set; }
        public string OfficerName { get; set; }
        public int NID { get; set; }
        public string PassportNo { get; set; }
        public string PoliceClearImg { get; set; }
        public string Gender { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}