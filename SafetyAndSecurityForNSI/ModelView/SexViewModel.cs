using System;
using System.ComponentModel.DataAnnotations;

namespace SafetyAndSecurityForNSI.ViewModel
{
    public class SexViewModel
    {
        [Key]
        public Guid SexId { get; set; }

        public string  SexCategory { get; set; }
   
    }
}