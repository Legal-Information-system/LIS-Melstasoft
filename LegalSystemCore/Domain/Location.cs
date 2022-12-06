using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    [Serializable]
    public class Location
    {
        [DBField("locationc_id")]
        public int LocationId { get; set; }

        [DBField("locationc_name")]
        public string locationName { get; set; }

    }
}
