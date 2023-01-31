using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    [Serializable]
    public class DocumentCaseActivity
    {
        [DBField("case_activity_document_id")]
        public int DocumentCaseActivityId { get; set; }

        [DBField("document_type_id")]
        public int DocumentId { get; set; }

        [DBField("document_name")]
        public string DocumentName { get; set; }

        [DBField("document_description")]
        public string DocumentDescription { get; set; }

        [DBField("case_activity_id")]
        public string CaseActivityId { get; set; }

        [DBField("is_active")]
        public int IsActive { get; set; }
    }
}
