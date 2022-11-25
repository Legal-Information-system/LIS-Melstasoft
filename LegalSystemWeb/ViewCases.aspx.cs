using LegalSystemCore;
using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class ViewCases : System.Web.UI.Page
    {

        List<CaseMaster> caseMasterListO = new List<CaseMaster>();
        List<CaseMaster> caseMasterListC = new List<CaseMaster>();
        List<CaseMaster> caseMasterList = new List<CaseMaster>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCaseStatus();
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