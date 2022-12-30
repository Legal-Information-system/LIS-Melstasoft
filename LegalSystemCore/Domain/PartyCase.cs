using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class PartyCase
    {
        [DBField("party_id")]
        public int PartyId { get; set; }

        [DBField("case_number")]
        public string CaseNumber { get; set; }

        [DBField("is_plaintif")]

        public int IsPlaintif { get; set; }
    }
}
