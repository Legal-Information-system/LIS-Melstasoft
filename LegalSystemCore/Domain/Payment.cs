using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class Payment
    {
        [DBField("payment_id")]
        public int PaymentId { get; set; }

        [DBField("case_number")]
        public string CaseNumber { get; set; }

        [DBField("lawyer_id")]
        public int LawyerId { get; set; }

        [DBField("amount")]
        public double Amount { get; set; }

        [DBField("remarks")]
        public string Remarks { get; set; }

        [DBField("payment_status_id")]
        public int PaymentStatusId { get; set; }

        [DBField("created_date")]
        public DateTime CreatedDate { get; set; }

        [DBField("create_user_id")]
        public int CreateUserId { get; set; }

        [DBField("action_taken_date")]
        public DateTime ActionTakenDate { get; set; }

        [DBField("action_remarks")]
        public string ActionRemarks { get; set; }

        [DBField("action_taken_user_id")]
        public int ActionTakenUserId { get; set; }

        [DBField("is_active")]
        public int IsActive { get; set; }

        public CaseMaster caseMaster { get; set; }

        public PaymentStatus paymentStatus { get; set; }

        public Lawyer lawyer { get; set; }

        public string Actions { get; set; }

    }
}
