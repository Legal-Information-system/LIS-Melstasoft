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
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 10).Any())
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
            ILawyerController lawyerController = ControllerFactory.CreateLawyerController();

            lawyerList = lawyerController.GetLawyerList(false);
            GridView2.DataSource = lawyerList;
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
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Lawyer Updated Succesfully!', 'success')", true);
                btnSave.Text = "Add";
            }
            else
            {
                Lawyer lawyer = new Lawyer();
                lawyer.LawyerName = txtName.Text;
                lawyer.LawyerEmail = txtEmail.Text;
                lawyer.LawyerContact = txtContact.Text;

                lawyer.LawyerId = lawyerController.Save(lawyer);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Lawyer Added Succesfully!', 'success')", true);
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
            int pageSize = GridView2.PageSize;
            int pageIndex = GridView2.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            txtName.Text = lawyerList[rowIndex].LawyerName;
            txtEmail.Text = lawyerList[rowIndex].LawyerEmail;
            txtContact.Text = lawyerList[rowIndex].LawyerContact;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = lawyerList[rowIndex].LawyerId;

        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            ILawyerController lawyerController = ControllerFactory.CreateLawyerController();
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = GridView2.PageSize;
            int pageIndex = GridView2.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            Lawyer lawyer = new Lawyer();
            lawyer.LawyerId = lawyerList[rowIndex].LawyerId; ;

            lawyerController.Delete(lawyer);
            BindDataSource();
        }


        protected void GridView2_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            this.BindDataSource();
        }
    }
}
