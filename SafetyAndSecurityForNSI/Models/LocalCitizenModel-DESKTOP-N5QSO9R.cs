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
    
    public partial class LocalCitizenModel
    {
        public System.Guid CitizenId { get; set; }
        public string CitizenName { get; set; }
        public long NID { get; set; }
        public long BirthId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int Age { get; set; }
        public string Division { get; set; }
        public Nullable<int> Contact { get; set; }
        public string PresentAddress { get; set; }
        public Nullable<int> RelativeContact { get; set; }
        public string PassportNo { get; set; }
        public string Email { get; set; }
        public string DrivingLicenceNo { get; set; }
        public string DLicenceCopy { get; set; }
        public string CreatedBy { get; set; }
        public string Gender { get; set; }
        public string Zilla { get; set; }
        public string Thana { get; set; }
        public string Village { get; set; }
        public string PostOffice { get; set; }
        public string PersonImg { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}