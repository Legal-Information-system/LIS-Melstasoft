using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Domain
{
    public class CompanyUnit
    {
        public int CompanyUnitId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyUnitName { get; set; }
    }
}
