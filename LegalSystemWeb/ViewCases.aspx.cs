using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class ViewCases : System.Web.UI.Page
    {
        
        List<Cases> order = new List<Cases>();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataSource();
        }

        private void BindDataSource()
        {
            int code = 10000;
            for (int i = 1; i < 10; i++)
            {
                order.Add(new Cases("EM/000/000", "Melstasoft", "Melstacorp", "Plaintiff", "Toms Spezialitäten", "Toms Spezialitäten", "Ongoing"));
               
                code += 5;
            }
            this.datatablesSimple.DataSource = order;
            this.datatablesSimple.DataBind();
        }

        [Serializable]
        public class Cases
        {
            public Cases()
            {

            }
            public Cases(string CaseNo,string CompanyName, string CompanyUnit, string PartyType, string Plentiff, string Defendent, string Status)
            {
                this.CaseNo= CaseNo;    
                this.CompanyName= CompanyName;
                this.CompanyUnit = CompanyUnit;
                this.PartyType= PartyType;
                this.Plentiff= Plentiff;
                this.Defendent= Defendent;
                this.Status= Status;
            }
            public string CaseNo { get; set; }
            public string CompanyName { get; set; }
            public string CompanyUnit { get; set; }
            public string PartyType { get; set; }
            public string Plentiff { get; set; }
            public string Defendent { get; set; }
            public string Status { get; set; }

        }
    }
}