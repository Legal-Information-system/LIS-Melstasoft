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


            BindDataSource();

        }
        private void BindDataSource()
        {
            ICaseNatureController caseNatureController = ControllerFactory.CreateCaseNatureController();
            casesList = caseNatureController.GetCaseNatureList();
            GridView2.DataSource = caseNatureController.GetCaseNatureList();
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
                btnSave.Text = "Add";
            }
            else
            {
                CaseNature caseNature = new CaseNature();

                caseNature.CaseNatureName = txtNatureName.Text;
                caseNature.CaseNatureId = caseNatureController.Save(caseNature);


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

            txtNatureName.Text = casesList[rowIndex].CaseNatureName;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = casesList[rowIndex].CaseNatureId;
        }
    }
}