using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafetyAndSecurityForNSI.ModelView
{
    public class ApplicationCategoryViewModel
    {
        [Key]
        public Guid ApplicationCategoryId { get; set; }
        public string ApplicationName { get; set; }
    }
}