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
    
    public partial class HomeLoanModel
    {
        public System.Guid HomeLoanId { get; set; }
        public string CitizenName { get; set; }
        public long NID { get; set; }
        public long BirthId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int Age { get; set; }
        public string ParmanentAddress { get; set; }
        public Nullable<int> Contact { get; set; }
        public string PresenrAddress { get; set; }
        public Nullable<int> RelativeContact { get; set; }
        public string PassportNo { get; set; }
        public string Email { get; set; }
        public long SSINumber { get; set; }
        public decimal LoanAmmount { get; set; }
        public string JobStatus { get; set; }
        public Nullable<decimal> MonthlyIncome { get; set; }
        public Nullable<decimal> YearlyIncome { get; set; }
        public string WorkingAddress { get; set; }
        public string PersonDetails { get; set; }
        public int FamilyMembers { get; set; }
        public string Gender { get; set; }
        public string PersonImage { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
