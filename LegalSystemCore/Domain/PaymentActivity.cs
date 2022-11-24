using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class PaymentActivity
    {
        [DBField("activity_id")]
        public int ActivityId { get; set; }

        [DBField("payment_id")]
        public int PaymentId { get; set; }

        [DBField("is_active")]
        public int IsActive { get; set; }
    }
}
