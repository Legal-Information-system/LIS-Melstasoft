using LegalSystemCore;
using LegalSystemCore.Common;
using LegalSystemCore.Controller;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataSource();
        }

        private void BindDataSource()
        {
            ICompanyController companyController = ControllerFactory.CreateCompanyController();

            companyList = companyController.GetCompanyList();
            GridView2.DataSource = companyController.GetCompanyList();
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
                btnSave.Text = "Save";
            }
            else
            {
                Company company = new Company();
                company.CompanyName = txtCompanyName.Text;
                company.CompanyAddress = txtCompanyAddress.Text;

                company.CompanyId = companyController.Save(company);
            }


            Clear();
            //BindDataSource();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;


            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            //if (listStudent == null && ViewState["listStudent"] != null)
            //    listStudent = (List<Student>)ViewState["listStudent"];
            //else
            //    listStudent = new List<Student>();


            txtCompanyName.Text = companyList[rowIndex].CompanyName;
            txtCompanyAddress.Text = companyList[rowIndex].CompanyAddress;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = companyList[rowIndex].CompanyId; ;
        }

        private void Clear()
        {
            txtCompanyName.Text = string.Empty;
            txtCompanyAddress.Text = string.Empty;

        }
    }
}