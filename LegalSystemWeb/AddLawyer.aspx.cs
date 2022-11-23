using LegalSystemCore;
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
    public partial class AddLawyer : System.Web.UI.Page
    {
        List<Lawyer> lawyerList = new List<Lawyer>();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataSource();
        }

        private void BindDataSource()
        {
            ILawyerController lawyerController = ControllerFactory.CreateLawyerController();


            lawyerList = lawyerController.GetLawyerList();
            GridView2.DataSource = lawyerController.GetLawyerList(); ;
            GridView2.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ILawyerController lawyerController = ControllerFactory.CreateLawyerController();

            if (btnSave.Text == "Update")
            {
                int rowIndex = (int)ViewState["updatedRowIndex"];
                Lawyer lawyer = new Lawyer();
                lawyer.LawyerId = rowIndex;
                lawyer.LawyerName = txtName.Text;
                lawyer.LawyerEmail = txtEmail.Text;
                lawyer.LawyerContact = txtContact.Text;


                lawyerController.Update(lawyer);
                btnSave.Text = "Add";
            }
            else
            {
                Lawyer lawyer = new Lawyer();
                lawyer.LawyerName = txtName.Text;
                lawyer.LawyerEmail = txtEmail.Text;
                lawyer.LawyerContact = txtContact.Text;

                lawyer.LawyerId = lawyerController.Save(lawyer);
            }


            Clear();
            BindDataSource();
        }
        private void Clear()
        {
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtContact.Text = string.Empty;

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            txtName.Text = lawyerList[rowIndex].LawyerName;
            txtEmail.Text = lawyerList[rowIndex].LawyerEmail;
            txtContact.Text = lawyerList[rowIndex].LawyerContact;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = lawyerList[rowIndex].LawyerId;

        }


    }
}
