using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafetyAndSecurityForNSI.ModelView
{
    public class ApplicationViewModel
    {
        [Key]
        public Guid ApplicationId { get; set; }
        [Required(ErrorMessage ="*")]
        [StringLength(17, MinimumLength = 17)]
        [DisplayName("NID")]

        public int NID { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Birth Id")]
        [StringLength(17, MinimumLength = 17)]
        public int BirthId { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Applicant Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Phone Number.")]
        [StringLength(11, MinimumLength = 11)]
        public string Contact { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Application Type.")]
        public string ApplicationType { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Full Name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Descriptions")]
        [StringLength(500, MinimumLength = 20)]
        public string Details { get; set; }

    }
}