using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafetyAndSecurityForNSI.ViewModel
{
    public class SSIViewModel
    {
        [Key]
        public Guid SSIId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string BirthFirstName { get; set; }
        public string BirthMiddleName { get; set; }
        public string BirthLastName { get; set; }
        public int ThanaCode { get; set; }
        public int PostalCode { get; set; }
        public int NidLastdigit { get; set; }
        public string PlaceOfBirth { get; set; }
        public string City { get; set; }
        public string Thana { get; set; }
        public string DateOfBirth { get; set; }
        public string CitizenShip { get; set; }
        public string Ethnicity { get; set; }
        public string Race { get; set; }
        public string ParentsFirstName { get; set; }
        public string ParentsMiddleName { get; set; }
        public string ParentsLastName { get; set; }
        public string ParentsSSINo { get; set; }
        public string PhoneNumber { get; set; }
        public string MailingAddress { get; set; }
        public int NID { get; set; }
        public string PassportNo { get; set; }
        public string Gender { get; set; }
        public string Division { get; set; }
        public string Address { get; set; }
        public string PersonImage { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }

    }
}