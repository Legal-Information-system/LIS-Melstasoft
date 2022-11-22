using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class Court
    {
        [DBField("court_id")]
        public int CourtId { get; set; }

        [DBField("court_name")]
        public string CourtName { get; set; }
    }
}
