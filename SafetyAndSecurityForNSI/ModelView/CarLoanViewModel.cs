using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace SafetyAndSecurityForNSI.ViewModel
{
    public class CarLoanViewModel
    {
        [Key]
        public Guid CarLoanId { get; set; }
        [Required]
        public string CitizenName { get; set; }
        public int NID { get; set; }
        public int BirthId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int Age { get; set; }
        public string Division { get; set; }
        public string Contact { get; set; }
        public string PresenrAddress { get; set; }
        public string RelativeContact { get; set; }
        public string PassportNo { get; set; }
        public string Email { get; set; }
        public int SSINumber { get; set; }
        public decimal LoanAmmount { get; set; }
        public string JobStatus { get; set; }
        public string MonthlyIncome { get; set; }
        public decimal YearlyIncome { get; set; }
        public string WorkingAddress { get; set; }
        public string PersonDetails { get; set; }
        public string FamilyMembers { get; set; }
        public string BankStatement { get; set; }
        public string PersonImage { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}