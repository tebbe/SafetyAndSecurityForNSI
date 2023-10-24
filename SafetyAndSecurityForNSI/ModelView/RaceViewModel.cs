using System;
using System.ComponentModel.DataAnnotations;

namespace SafetyAndSecurityForNSI.ViewModel
{
    public class RaceViewModel
    {
        [Key]
        public Guid RaceId { get; set; }

        public string RaceCategories { get; set; }
   

    }
}