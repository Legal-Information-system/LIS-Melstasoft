using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    [Serializable]
    public class PaymentStatus
    {
        [DBField("payment_status_id")]
        public int StatusId { get; set; }

        [DBField("payment_status_name")]
        public string StatusName { get; set; }
    }
}
