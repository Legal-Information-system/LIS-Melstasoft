using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace LegalSystemWeb
{
    public partial class AddCaseNature : System.Web.UI.Page
    {
        List<CaseNature> casesList = new List<CaseNature>();
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
            ICaseNatureController caseNatureController = ControllerFactory.CreateCaseNatureController();
            casesList = caseNatureController.GetCaseNatureList(false);
            GridView2.DataSource = casesList;
            GridView2.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            ICaseNatureController caseNatureController = ControllerFactory.CreateCaseNatureController();

            if (btnSave.Text == "Update")
            {
                int rowIndex = (int)ViewState["updatedRowIndex"];
                CaseNature caseNature = new CaseNature();

                caseNature.CaseNatureId = rowIndex;

                caseNature.CaseNatureName = txtNatureName.Text;

                caseNatureController.Update(caseNature);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Case Nature Updated Succesfully!', 'success')", true);
                btnSave.Text = "Add";
            }
            else
            {
                CaseNature caseNature = new CaseNature();

                caseNature.CaseNatureName = txtNatureName.Text;
                caseNature.CaseNatureId = caseNatureController.Save(caseNature);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Case Nature Added Succesfully!', 'success')", true);

            }


            Clear();
            BindDataSource();
        }
        private void Clear()
        {
            txtNatureName.Text = string.Empty;


        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;

            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = GridView2.PageSize;
            int pageIndex = GridView2.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            txtNatureName.Text = casesList[rowIndex].CaseNatureName;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = casesList[rowIndex].CaseNatureId;
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            ICaseNatureController caseNatureController = ControllerFactory.CreateCaseNatureController();
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = GridView2.PageSize;
            int pageIndex = GridView2.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            CaseNature caseNature = new CaseNature();
            caseNature.CaseNatureId = casesList[rowIndex].CaseNatureId;

            caseNatureController.Delete(caseNature);
            BindDataSource();
        }

        protected void GridView2_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            this.BindDataSource();
        }
    }
}