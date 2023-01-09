using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class ViewPaymentMemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                BindDataSource();
            }
        }

        List<Payment> listGloabalPayment = new List<Payment>();

        private void BindDataSource()
        {
            IPaymentController paymentController = ControllerFactory.CreatePaymentController();
            List<Payment> listPayment = paymentController.GetPaymentList(true, true, true, true);

            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
            List<CaseMaster> caseMasterList = caseMasterController.GetCaseMasterListAll();
            CaseMaster caseMaster = new CaseMaster();

            foreach (Payment payment in listPayment)
            {
                if (caseMasterList.Any(x => x.CaseNumber == payment.CaseNumber))
                {
                    caseMaster = caseMasterList.Where(x => x.CaseNumber == payment.CaseNumber).Single();
                    caseMaster.payableAmount = Convert.ToDouble(caseMaster.ClaimAmount) - caseMaster.totalPaidAmoutToPresent;
                    payment.caseMaster = caseMaster;
                }
            }

            int UserRoleId = Convert.ToInt32(Session["User_Role_Id"]);
            int companyId = Convert.ToInt32(Session["company_id"].ToString());
            int companyUnitId = Convert.ToInt32(Session["company_unit_id"].ToString());

            if (UserRoleId == 4 || UserRoleId == 5)
            {
                if (listPayment.Any((c => c.caseMaster.CompanyId == companyId)))
                {
                    listPayment = listPayment.Where(c => c.caseMaster.CompanyId == companyId).ToList();
                }
                else
                {
                    listPayment.Clear();
                }
            }

            if (UserRoleId == 5)
            {
                listPayment = listPayment.Where(c => c.caseMaster.CompanyUnitId == companyUnitId).ToList();
            }

            this.GridView1.DataSource = listPayment;
            listGloabalPayment = listPayment;
            this.GridView1.DataBind();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = GridView1.PageSize;
            int pageIndex = GridView1.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            Response.Redirect("ApprovePaymentMemo.aspx?PaymentId=" + listGloabalPayment[rowIndex].PaymentId.ToString());
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindDataSource();
        }
    }
}