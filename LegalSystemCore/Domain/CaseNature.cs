using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    [Serializable]
    public class CaseNature
    {
        [DBField("case_nature_id")]
        public int CaseNatureId { get; set; }
        [DBField("case_nature_name")]
        public string CaseNatureName { get; set; }
    }
}
