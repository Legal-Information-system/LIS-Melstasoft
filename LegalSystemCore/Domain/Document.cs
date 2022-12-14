using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    [Serializable]
    public class Document
    {
        [DBField("document_id")]
        public int DocumentId { get; set; }

        [DBField("document_type")]
        public string DocumentType { get; set; }

        [DBField("is_active")]
        public int IsActive { get; set; }
    }
}
