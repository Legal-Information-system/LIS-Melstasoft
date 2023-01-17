using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static LegalSystemWeb.ViewPaymentMemo;

namespace LegalSystemWeb
{
    public partial class AddAction : System.Web.UI.Page
    {
        List<CaseAction> caseActionList = new List<CaseAction>();
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
                if (((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 1).Any()
                    && !(userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 1 && x.IsGrantRevoke == 0)))) ||
                    userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 1 && x.IsGrantRevoke == 1))
                {
                    BindDataSource();
                }
                else
                {
                    Response.Redirect("404.aspx");
                }
            }
        }

        private void BindDataSource()
        {
            ICaseActionController caseActionController = ControllerFactory.CreateCaseActionController();

            caseActionList = caseActionController.GetCaseActionList(false);
            gvCaseAction.DataSource = caseActionList;
            gvCaseAction.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ICaseActionController caseActionController = ControllerFactory.CreateCaseActionController();

            if (btnSave.Text == "Update")
            {
                int rowIndex = (int)ViewState["updatedRowIndex"];
                CaseAction caseAction = new CaseAction();
                caseAction.ActionId = rowIndex;
                caseAction.ActionName = txtAction.Text;
                caseActionController.Update(caseAction);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Action Updated Succesfully!', 'success')", true);
                btnSave.Text = "Save";
            }
            else
            {
                CaseAction caseAction = new CaseAction();
                caseAction.ActionName = txtAction.Text;
                caseAction.ActionId = caseActionController.Save(caseAction);

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Action Added Succesfully!', 'success')", true);
            }

            Clear();
            BindDataSource();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = gvCaseAction.PageSize;
            int pageIndex = gvCaseAction.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            txtAction.Text = caseActionList[rowIndex].ActionName;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = caseActionList[rowIndex].ActionId;
        }

        private void Clear()
        {
            txtAction.Text = string.Empty;
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            ICaseActionController caseActionController = ControllerFactory.CreateCaseActionController();
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = gvCaseAction.PageSize;
            int pageIndex = gvCaseAction.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            CaseAction caseAction = new CaseAction();
            caseAction.ActionId = caseActionList[rowIndex].ActionId;

            caseActionController.Delete(caseAction);
            BindDataSource();
        }


        protected void gvCaseAction_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            gvCaseAction.PageIndex = e.NewPageIndex;
            this.BindDataSource();
        }
    }
}