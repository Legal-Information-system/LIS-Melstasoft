using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class Party
    {
        [DBField("party_id")]
        public int PartyId { get; set; }

        [DBField("party_name")]
        public string PartyName { get; set; }
    }
}
