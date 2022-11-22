using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class Activity
    {
        [DBField("activity_id")]
        public int ActivityId { get; set; }
        [DBField("activity_name")]
        public string ActivityName { get; set; }
    }
}
