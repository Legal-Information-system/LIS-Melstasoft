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
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        IUserPrivilegeController userPrivilegeController = ControllerFactory.CreateUserPrivilegeController();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 26).Any()
                     && !(userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 26 && x.IsGrantRevoke == 0))) ||
                     userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 26 && x.IsGrantRevoke == 1)))
                {
                    Response.Redirect("404.aspx");
                }
                else
                {
                    BindDataSource();
                }
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
            List<UserRolePrivilege> userRolePrivileges = userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString());

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

            if (!((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 29 || x.FunctionId == 30 || x.FunctionId == 28).Any()
                    && !(userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 29 || x.FunctionId == 30 || x.FunctionId == 28 && x.IsGrantRevoke == 0))) ||
                    userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 29 || x.FunctionId == 30 || x.FunctionId == 28 && x.IsGrantRevoke == 1))
                && ((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 20).Any()
                    && !(userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 20 && x.IsGrantRevoke == 0))) ||
                    userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 20 && x.IsGrantRevoke == 1)))
            {
                Response.Redirect("404.aspx");
            }

            if (((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 29 || x.FunctionId == 30 || x.FunctionId == 28).Any()
                    && !(userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 29 || x.FunctionId == 30 || x.FunctionId == 28 && x.IsGrantRevoke == 0))) ||
                    userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 29 || x.FunctionId == 30 || x.FunctionId == 28 && x.IsGrantRevoke == 1)))
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

            if (((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 30).Any()
                    && !(userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 30 && x.IsGrantRevoke == 0))) ||
                    userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 30 && x.IsGrantRevoke == 1)))
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