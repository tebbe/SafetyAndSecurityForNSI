using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafetyAndSecurityForNSI.ModelView
{
    public class CriminalRecordEntryModelView
    {
        public System.Guid CrimeId { get; set; }
        public string Name { get; set; }
        public long? Nid { get; set; }
        public long? BirthId { get; set; }
        public long? SSN { get; set; }
        public long? ParentsNid { get; set; }
        public string PassportNo { get; set; }
        public string CrimeType { get; set; }
        public string CrimeDetails { get; set; }
        public int? Status { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}