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
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 6).Any())
                {
                    if (!IsPostBack)
                    {
                        BindCompanyList();
                    }
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
            ICompanyUnitController companyUnitController = ControllerFactory.CreateCompanyUnitController();

            companyUnitList = companyUnitController.GetCompanyUnitList(false, true);
            GridView2.DataSource = companyUnitList;
            GridView2.DataBind();

        }

        private void BindCompanyList()
        {
            ICompanyController companyController = ControllerFactory.CreateCompanyController();
            ddlCompany.DataSource = companyController.GetCompanyList(false);
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
                companyUnit.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                companyUnit.CompanyUnitId = rowIndex;



                companyUnitController.Update(companyUnit);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Company Unit Updated Succesfully!', 'success')", true);
                btnSave.Text = "Save";
            }
            else
            {
                CompanyUnit companyUnit = new CompanyUnit();
                companyUnit.CompanyUnitName = txtCompanyUnitName.Text;
                string test = ddlCompany.SelectedValue;
                companyUnit.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Company Unit Added Succesfully!', 'success')", true);
                companyUnit.CompanyUnitId = companyUnitController.Save(companyUnit);

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

            txtCompanyUnitName.Text = companyUnitList[rowIndex].CompanyUnitName;
            ddlCompany.SelectedValue = Convert.ToString(companyUnitList[rowIndex].CompanyId);
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = companyUnitList[rowIndex].CompanyUnitId; ;
        }

        private void Clear()
        {
            txtCompanyUnitName.Text = string.Empty;
            ddlCompany.ClearSelection();

        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            ICompanyUnitController companyUnitController = ControllerFactory.CreateCompanyUnitController();
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = GridView2.PageSize;
            int pageIndex = GridView2.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            CompanyUnit companyUnit = new CompanyUnit();
            companyUnit.CompanyUnitId = companyUnitList[rowIndex].CompanyUnitId;
            companyUnitController.Delete(companyUnit);
            BindDataSource();
        }

        protected void GridView2_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            this.BindDataSource();
        }
    }
}