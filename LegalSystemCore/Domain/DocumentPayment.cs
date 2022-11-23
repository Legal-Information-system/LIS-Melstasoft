using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class DocumentPayment
    {
        [DBField("payment_document_id")]
        public int DocumentPaymentId { get; set; }

        [DBField("document_type_id")]
        public int DocumentId { get; set; }

        [DBField("document_name")]
        public string DocumentName { get; set; }

        [DBField("document_description")]
        public string DocumentDescription { get; set; }

        [DBField("payment_id")]
        public int PaymentId { get; set; }

        [DBField("is_active")]
        public int IsActive { get; set; }
    }
}
