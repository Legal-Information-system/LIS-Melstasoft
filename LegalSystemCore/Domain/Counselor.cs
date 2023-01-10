using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    [Serializable]
    public class Counselor
    {
        [DBField("case_number")]

        public string CaseNumber { get; set; }

        [DBField("lawyer_id")]

        public int LawyerId { get; set; }
    }
}
