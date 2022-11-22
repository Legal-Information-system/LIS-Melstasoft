using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class CompanyUnit
    {
        [DBField("company_unit_id")]
        public int CompanyUnitId { get; set; }

        [DBField("company_id")]
        public int CompanyId { get; set; }

        [DBField("company_unit_name")]
        public string CompanyUnitName { get; set; }

        public Company company { get; set; }
     

    }
}
