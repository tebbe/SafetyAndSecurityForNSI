using System;
using System.ComponentModel.DataAnnotations;

namespace SafetyAndSecurityForNSI.ViewModel
{
    public class EthnicityViewModel
    {
        [Key]
        public Guid EthnicityId { get; set; }
        public string EthnicityCategory { get; set; }
    }
}