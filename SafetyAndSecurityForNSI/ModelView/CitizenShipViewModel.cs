using System;
using System.ComponentModel.DataAnnotations;

namespace SafetyAndSecurityForNSI.ViewModel
{
    public class CitizenShipViewModel
    {
        [Key]
        public Guid CitizenshipId { get; set; }
        public string CitizenShipCategories { get; set; }
    }
}