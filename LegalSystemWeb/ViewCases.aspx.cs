using LegalSystemCore;
using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class ViewCases : System.Web.UI.Page
    {
        int companyId, UserId;
        string name;
        List<CaseMaster> caseMasterListO = new List<CaseMaster>();
        List<CaseMaster> caseMasterListC = new List<CaseMaster>();
        List<CaseMaster> caseMasterList = new List<CaseMaster>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                //if (Session["User_Role_Id"].ToString() == "3")
                //    Response.Redirect("404.aspx");
                //else
                //{
                if (!IsPostBack)
                {
                    name = Request.QueryString["name"].ToString();
                    BindCaseStatus();
                }
                //}
            }

        }
        private void BindCaseStatus()
        {
            ICaseStatusController caseStatusController = ControllerFactory.CreateCaseStatusController();

            ddlCaseStatus.DataSource = caseStatusController.GetCaseStatusList();
            ddlCaseStatus.DataValueField = "StatusId";
            ddlCaseStatus.DataTextField = "StatusName";
            ddlCaseStatus.DataBind();

            BindCaseList();
        }
        private void BindCaseList()
        {
            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();

            caseMasterListO = caseMasterController.GetCaseMasterList(true);
            caseMasterList = caseMasterController.GetCaseMasterList(false);
            caseMasterListC = caseMasterList.Where(x => x.CaseStatusId == 2).ToList();



            UserId = Convert.ToInt32(Session["User_Role_Id"]);
            companyId = Convert.ToInt32(Session["company_id"].ToString());
            int companyUnitId = Convert.ToInt32(Session["company_unit_id"].ToString());

            if (UserId == 4 || UserId == 5)
            {
                caseMasterListO = caseMasterListO.Where(c => c.CompanyId == companyId).ToList();
                caseMasterListC = caseMasterListC.Where(c => c.CompanyId == companyId).ToList();
            }

            if (UserId == 5)
            {
                caseMasterListO = caseMasterListO.Where(c => c.CompanyUnitId == companyUnitId).ToList();
                caseMasterListC = caseMasterListC.Where(c => c.CompanyUnitId == companyUnitId).ToList();
            }


            if (name != "All" & name != null)
            {
                if (UserId != 5 && UserId != 4)
                {
                    caseMasterListO = caseMasterListO.Where(c => c.company.CompanyName == name).ToList();
                    caseMasterListC = caseMasterListC.Where(c => c.company.CompanyName == name).ToList();
                }
                if (UserId == 4)
                {
                    caseMasterListO = caseMasterListO.Where(c => c.companyUnit.CompanyUnitName == name).ToList();
                    caseMasterListC = caseMasterListC.Where(c => c.companyUnit.CompanyUnitName == name).ToList();
                }
            }

            datatablesSimple.DataSource = caseMasterListO;
            datatablesSimple.DataBind();

            ViewState["ListCaseO"] = caseMasterListO;
            ViewState["ListCaseC"] = caseMasterListC;

        }

        protected void ddlCaseStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCaseList();
            if (ddlCaseStatus.SelectedValue == "1")
                datatablesSimple.DataSource = caseMasterListO;
            else
                datatablesSimple.DataSource = caseMasterListC;

            datatablesSimple.DataBind();
        }

        protected void datatablesSimple_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            datatablesSimple.PageIndex = e.NewPageIndex;
            this.BindCaseList();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = datatablesSimple.PageSize;
            int pageIndex = datatablesSimple.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            caseMasterListO = (List<CaseMaster>)ViewState["ListCaseO"];
            caseMasterListC = (List<CaseMaster>)ViewState["ListCaseC"];

            string caseNumber;

            if (ddlCaseStatus.SelectedValue == "1")
            {
                caseNumber = caseMasterListO[rowIndex].CaseNumber;
            }
            else
            {
                caseNumber = caseMasterListC[rowIndex].CaseNumber;
            }

            Response.Redirect("ViewCaseDetails.aspx?CaseNumber=" + caseNumber);
        }
    }
}