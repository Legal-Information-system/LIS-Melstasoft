using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class AddCaseStatus : System.Web.UI.Page
    {
        List<CaseStatus> caseStatusList = new List<CaseStatus>();

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
            ICaseStatusController caseStatusController = ControllerFactory.CreateCaseStatusController();

            caseStatusList = caseStatusController.GetCaseStatusList();
            gvCaseStatus.DataSource = caseStatusController.GetCaseStatusList();
            gvCaseStatus.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ICaseStatusController caseStatusController = ControllerFactory.CreateCaseStatusController();

            if (btnSave.Text == "Update")
            {
                int rowIndex = (int)ViewState["updatedRowIndex"];
                CaseStatus caseStatus = new CaseStatus();
                caseStatus.StatusId = rowIndex;
                caseStatus.StatusName = txtCase.Text;


                caseStatusController.Update(caseStatus);
                btnSave.Text = "Save";
            }
            else
            {
                CaseStatus caseStatus = new CaseStatus();
                caseStatus.StatusName = txtCase.Text;
                caseStatus.StatusId = caseStatusController.Save(caseStatus);
            }

            Clear();
            BindDataSource();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            txtCase.Text = caseStatusList[rowIndex].StatusName;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = caseStatusList[rowIndex].StatusId;
        }

        private void Clear()
        {
            txtCase.Text = string.Empty;
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            ICaseStatusController caseStatusController = ControllerFactory.CreateCaseStatusController();
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            CaseStatus caStatus = new CaseStatus();
            caStatus.StatusId = caseStatusList[rowIndex].StatusId;
            caseStatusController.Delete(caStatus);
            BindDataSource();
        }
    }
}