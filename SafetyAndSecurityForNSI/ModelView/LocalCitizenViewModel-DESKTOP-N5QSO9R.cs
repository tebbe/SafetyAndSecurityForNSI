using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafetyAndSecurityForNSI.ViewModel
{
    public class LocalCitizenViewModel
    {
        [Key]
        public Guid CitizenId { get; set; }
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