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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (Session["User_Role_Id"].ToString() == "1" || Session["User_Role_Id"].ToString() == "2")
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

            caseActionList = caseActionController.GetCaseActionList();
            gvCaseAction.DataSource = caseActionController.GetCaseActionList();
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
                btnSave.Text = "Save";
            }
            else
            {
                CaseAction caseAction = new CaseAction();
                caseAction.ActionName = txtAction.Text;
                caseAction.ActionId = caseActionController.Save(caseAction);
            }

            Clear();
            BindDataSource();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

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

            CaseAction caseAction = new CaseAction();
            caseAction.ActionId = caseActionList[rowIndex].ActionId;

            caseActionController.Delete(caseAction);
            BindDataSource();
        }
    }
}