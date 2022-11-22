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
        protected void Page_Load(object sender, EventArgs e)
        {


            BindDataSource();

        }
        private void BindDataSource()
        {
            ICaseNatureController caseNatureController = ControllerFactory.CreateCaseNatureController();
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

                //lawyerController.Update(lawyer);
                btnSave.Text = "Save";
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
    }
}