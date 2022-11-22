using LegalSystemCore.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore;

namespace LegalSystemWeb
{
    public partial class AddCompanyUnit : System.Web.UI.Page
    {
        List<CompanyUnit> companyUnitList = new List<CompanyUnit>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCompanyList();
                BindDataSource();
            }
        }

        private void BindDataSource()
        {
            ICompanyUnitController companyUnitController = ControllerFactory.CreateCompanyUnitController();

            companyUnitList = companyUnitController.GetCompanyUnitList(true);
            GridView2.DataSource = companyUnitController.GetCompanyUnitList(true);
            GridView2.DataBind();

        }

        private void BindCompanyList()
        {
            ICompanyController companyController = ControllerFactory.CreateCompanyController();
            ddlCompany.DataSource = companyController.GetCompanyList();
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataTextField = "CompanyName";

            ddlCompany.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ICompanyUnitController companyUnitController = ControllerFactory.CreateCompanyUnitController();

            if (btnSave.Text == "Update")
            {
                int rowIndex = (int)ViewState["updatedRowIndex"];
                CompanyUnit companyUnit = new CompanyUnit();
                companyUnit.CompanyUnitName = txtCompanyUnitName.Text;



                companyUnitController.Update(companyUnit);
                btnSave.Text = "Save";
            }
            else
            {
                CompanyUnit companyUnit = new CompanyUnit();
                companyUnit.CompanyUnitName = txtCompanyUnitName.Text;
                string test = ddlCompany.SelectedValue;
                companyUnit.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                companyUnit.CompanyUnitId = companyUnitController.Save(companyUnit);
            }


            Clear();
            BindDataSource();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;


            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            //if (listStudent == null && ViewState["listStudent"] != null)
            //    listStudent = (List<Student>)ViewState["listStudent"];
            //else
            //    listStudent = new List<Student>();

            txtCompanyUnitName.Text = companyUnitList[rowIndex].CompanyUnitName;
            ddlCompany.SelectedValue = Convert.ToString(companyUnitList[rowIndex].CompanyId);
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = companyUnitList[rowIndex].CompanyId; ;
        }

        private void Clear()
        {
            txtCompanyUnitName.Text = string.Empty;
            ddlCompany.ClearSelection();

        }
    }
}