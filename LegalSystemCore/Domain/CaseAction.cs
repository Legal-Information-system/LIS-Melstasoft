using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class CaseAction
    {
        [DBField("case_action_id")]
        public int ActionId { get; set; }

        [DBField("case_action_name")]
        public string ActionName { get; set; }
    }
}
