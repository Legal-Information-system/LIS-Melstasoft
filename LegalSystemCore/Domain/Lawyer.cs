using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class Lawyer
    {
        [DBField("lawyer_id")]
        public int LawyerId { get; set; }

        [DBField("lawyer_name")]
        public string LawyerName { get; set; }

        [DBField("l_email")]
        public string LawyerEmail { get; set; }

        [DBField("l_contact")]
        public string LawyerContact { get; set; }
    }
}
