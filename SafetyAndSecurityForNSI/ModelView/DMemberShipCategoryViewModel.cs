using System;
using System.ComponentModel.DataAnnotations;

namespace SafetyAndSecurityForNSI.ViewModel
{
    public class DMemberShipCategoryViewModel { 
        [Key]
        public Guid DMembershipId { get; set; }
        public string MemberCategory { get; set; }

    }

}