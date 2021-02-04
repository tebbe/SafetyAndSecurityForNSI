using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafetyAndSecurityForNSI.ViewModel
{
    public class DoctorsEntryViewModel
    {
        [Key]
        public Guid DoctorsId { get; set; }
        public string DoctorName { get; set; }
        public string DegreeOne { get; set; }
        public string DegreeTwo { get; set; }
        public string DegreeThree { get; set; }
        public string TrainingInstitute { get; set; }
        public string TrainingPeriod { get; set; }
        public string SpecialTraining { get; set; }
        public Nullable<long> BMAMembershipNo { get; set; }
        public string BranceCode { get; set; }
        public string MembershipCategory { get; set; }
        public string BcsRegNo { get; set; }
        public long NID { get; set; }
        public string DoctorImage { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }

    }

}