using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class CaseActivity
    {
        [DBField("case_activity_id")]
        public int CaseActivitId { get; set; }

        [DBField("case_number")]
        public string CaseNumber { get; set; }
        [DBField("activity_date")]
        public DateTime ActivityDate { get; set; }
        public string ActivityDateString { get; set; }

        [DBField("assign_attorney_id")]
        public int AssignAttorneyId { get; set; }

        [DBField("counsilor_id")]
        public int CounsilorId { get; set; }
        [DBField("other_side_lawyer")]
        public string OtherSideLawyer { get; set; }

        [DBField("judge_name")]
        public string JudgeName { get; set; }

        [DBField("company_rep")]
        public string CompanyRep { get; set; }

        [DBField("action_taken_id")]
        public int ActionTakenId { get; set; }

        [DBField("next_date")]
        public DateTime NextDate { get; set; }
        public string NextDateString { get; set; }

        [DBField("remarks")]
        public string Remarks { get; set; }

        [DBField("next_action_id")]
        public int NextActionId { get; set; }
        [DBField("create_user_id")]
        public int CreateUserId { get; set; }


        [DBField("is_active")]
        public int IsActive { get; set; }


        public Lawyer assignAttorney { get; set; }
        public Lawyer counsilor { get; set; }
        public CaseAction actionTaken { get; set; }
        public CaseAction nextAction { get; set; }

    }
}
