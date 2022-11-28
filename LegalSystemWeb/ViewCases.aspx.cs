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
                if (Session["User_Role_Id"].ToString() == "3")
                    Response.Redirect("404.aspx");
                else
                {
                    if (!IsPostBack)
                    {
                        BindCaseStatus();
                    }
                }
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

            UserId = Convert.ToInt32(Session["User_Role_Id"]);
            companyId = Convert.ToInt32(Session["company_id"].ToString());
            int companyUnitId = Convert.ToInt32(Session["company_unit_id"].ToString());

            if (UserId == 4 || UserId == 5)
            {
                caseMasterListO = caseMasterListO.Where(c => c.CompanyId == companyId).ToList();
                caseMasterListC = caseMasterListC.Where(c => c.CompanyId != companyId).ToList();
            }

            if (UserId == 5)
            {
                caseMasterListO = caseMasterListO.Where(c => c.CompanyUnitId == companyUnitId).ToList();
                caseMasterListC = caseMasterListC.Where(c => c.CompanyUnitId != companyUnitId).ToList();
            }
            caseMasterListC = caseMasterList.Where(x => x.CaseStatusId == 2).ToList();

            datatablesSimple.DataSource = caseMasterListO;
            datatablesSimple.DataBind();

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

        protected void btnView_Click(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;


            BindCaseList();

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