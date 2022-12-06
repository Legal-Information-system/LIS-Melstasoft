using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore
{
    [Serializable]
    public class Company
    {
        [DBField("company_id")]
        public int CompanyId { get; set; }

        [DBField("company_name")]
        public string CompanyName { get; set; }

        [DBField("company_address")]
        public string CompanyAddress { get; set; }
    }
}
