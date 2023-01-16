using LegalSystemCore;
using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class AddComapny : System.Web.UI.Page
    {
        List<Company> companyList = new List<Company>();
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 5).Any())
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
            ICompanyController companyController = ControllerFactory.CreateCompanyController();

            companyList = companyController.GetCompanyList(false);
            GridView2.DataSource = companyList;
            GridView2.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ICompanyController companyController = ControllerFactory.CreateCompanyController();

            if (btnSave.Text == "Update")
            {
                int rowIndex = (int)ViewState["updatedRowIndex"];
                Company company = new Company();
                company.CompanyId = rowIndex;
                company.CompanyName = txtCompanyName.Text;
                company.CompanyAddress = txtCompanyAddress.Text;


                companyController.Update(company);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Company Updated Succesfully!', 'success')", true);
                btnSave.Text = "Save";
            }
            else
            {
                Company company = new Company();
                company.CompanyName = txtCompanyName.Text;
                company.CompanyAddress = txtCompanyAddress.Text;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Company Added Succesfully!', 'success')", true);
                company.CompanyId = companyController.Save(company);
            }


            Clear();
            BindDataSource();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;


            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = GridView2.PageSize;
            int pageIndex = GridView2.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            txtCompanyName.Text = companyList[rowIndex].CompanyName;
            txtCompanyAddress.Text = companyList[rowIndex].CompanyAddress;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = companyList[rowIndex].CompanyId;
        }

        private void Clear()
        {
            txtCompanyName.Text = string.Empty;
            txtCompanyAddress.Text = string.Empty;

        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            ICompanyController companyController = ControllerFactory.CreateCompanyController();
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = GridView2.PageSize;
            int pageIndex = GridView2.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            Company company = new Company();
            company.CompanyId = companyList[rowIndex].CompanyId;
            companyController.Delete(company);
            BindDataSource();
        }

        protected void GridView2_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            this.BindDataSource();
        }
    }
}