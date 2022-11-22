using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class JudgementType
    {
        [DBField("judgement_type_id")]
        public int JTypeId { get; set; }

        [DBField("judgement_type_name")]
        public string JTypeName { get; set; }
    }
}
