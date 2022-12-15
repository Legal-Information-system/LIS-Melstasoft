using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    [Serializable]
    public class CourtLocation
    {
        [DBField("court_id")]
        public int CourtId { get; set; }

        [DBField("locationc_id")]
        public int LocationId { get; set; }

        public Location location { get; set; }

        public Court court { get; set; }
    }
}
