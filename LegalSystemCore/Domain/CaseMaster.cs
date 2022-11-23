using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class CaseMaster
    {
        [DBField("case_number")]
        public string CaseNumber { get; set; }

        [DBField("company_id")]
        public int CompanyId { get; set; }

        [DBField("company_unit_id")]
        public int CompanyUnitId { get; set; }

        [DBField("case_nature_id")]
        public int CaseNatureId { get; set; }

        [DBField("case_description")]
        public string CaseDescription { get; set; }

        [DBField("claim_amount")]
        public float ClaimAmount { get; set; }

        [DBField("is_plaintif")]
        public int IsPlentif { get; set; }

        [DBField("other_party")]
        public string OtherParty { get; set; }

        [DBField("court_id")]
        public int CourtId { get; set; }

        [DBField("locationc_id")]
        public int LocationId { get; set; }

        [DBField("prev_case_number")]
        public string PrevCaseNumber { get; set; }

        [DBField("assign_attorney_id")]
        public int AssignAttornerId { get; set; }

        [DBField("counsilor_id")]
        public int CounsilorId { get; set; }

        [DBField("created_by_user")]
        public int CreatedUserId { get; set; }

        [DBField("created_date")]
        public DateTime CreatedDate { get; set; }

        [DBField("case_status_id")]
        public int CaseStatusId { get; set; }

        [DBField("judgement_type")]
        public int JudgementTypeId { get; set; }

        [DBField("case_outcome")]
        public string CaseOutcome { get; set; }

        [DBField("closed_remarks")]
        public string ClosedRemarks { get; set; }

        [DBField("closed_date")]
        public DateTime ClosedDate { get; set; }

        [DBField("close_by_user")]
        public int ClosedUserId { get; set; }

        [DBField("is_active")]
        public int IsActive { get; set; }


    }
}
