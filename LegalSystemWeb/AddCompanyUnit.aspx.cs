using LegalSystemCore.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LegalSystemCore.Common;

namespace LegalSystemWeb
{
    public partial class AddCompanyUnit : System.Web.UI.Page
    {
        List<Orders> order = new List<Orders>();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataSource();
        }

        private void BindDataSource()
        {
            ICompanyController companyController = ControllerFactory.CreateCompanyController();


            this.GridView2.DataSource = companyController.GetCompanyList();
            this.GridView2.DataBind();
        }

        [Serializable]
        public class Orders
        {

            public Orders()
            {

            }
            public Orders(long OrderId, string CustomerId, int EmployeeId, double Freight, string ShipCity, string ShipName,
                DateTime OrderDate, string ShipCountry, string ShipPostalCode, bool Verified)
            {
                this.OrderID = OrderId;
                this.CustomerID = CustomerId;
                this.EmployeeID = EmployeeId;
                this.Freight = Freight;
                this.ShipCity = ShipCity;
                this.ShipName = ShipName;
                this.OrderDate = OrderDate;
                this.ShipCountry = ShipCountry;
                this.ShipPostalCode = ShipPostalCode;
                this.Verified = Verified;
            }
            public long OrderID { get; set; }
            public string CustomerID { get; set; }
            public int EmployeeID { get; set; }
            public double Freight { get; set; }
            public string ShipCity { get; set; }
            public string ShipName { get; set; }
            public DateTime OrderDate { get; set; }
            public string ShipCountry { get; set; }
            public string ShipPostalCode { get; set; }
            public bool Verified { get; set; }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}