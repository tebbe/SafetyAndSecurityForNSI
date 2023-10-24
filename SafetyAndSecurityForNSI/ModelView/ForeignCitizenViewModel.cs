using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafetyAndSecurityForNSI.ViewModel
{
    public class ForeignCitizenViewModel
    {
        [Key]
        public Guid ForignCitizenId { get; set; }
        public string CitizenName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string PassportNo { get; set; }
        public string PassportImagCopy { get; set; }
        public string CountryName { get; set; }
        public decimal Age { get; set; }
        public string State { get; set; }
        public string Contact { get; set; }
        public string PresenrAddress { get; set; }
        public string RelativeContact { get; set; }
        public string PoliceClearImg { get; set; }
        public string Email { get; set; }
        public string DrivingLicenceNo { get; set; }
        public string DLicenceCopy { get; set; }
        public string PoliceEntryNo { get; set; }
        public string PersonImage { get; set; }
        public Nullable<int> PostCode { get; set; }
        public string City { get; set; }
        public string LivingTown { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}