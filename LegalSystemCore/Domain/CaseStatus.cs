using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public class CaseStatus
    {
        [DBField("case_status_id")]
        public int StatusId { get; set; }

        [DBField("case_status_name")]
        public string StatusName { get; set; }
    }
}
