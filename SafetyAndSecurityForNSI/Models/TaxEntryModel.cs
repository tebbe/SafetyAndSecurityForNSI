//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SafetyAndSecurityForNSI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaxEntryModel
    {
        public System.Guid TaxId { get; set; }
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
