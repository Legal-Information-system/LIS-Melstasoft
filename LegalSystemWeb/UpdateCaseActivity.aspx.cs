using LegalSystemCore;
using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class UpdateCaseActivity : System.Web.UI.Page
    {
        List<JudgementType> judgementTypeList = new List<JudgementType>(4);
        List<Lawyer> lawyerList = new List<Lawyer>(4);
        List<CaseAction> caseActionList = new List<CaseAction>(4);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User_Id"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    BindCaseList();
                    BindLawyerList();
                    BindActionList();
                    BindJudgementList();
                }
            }

        }

        private void BindCaseList()
        {
            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();

            ddlCase.DataSource = caseMasterController.GetCaseMasterList();
            ddlCase.DataValueField = "CaseNumber";
            ddlCase.DataTextField = "CaseNumber";
            ddlCase.DataBind();
            ddlCase.Items.Insert(0, new ListItem("-- select case --", ""));

        }

        protected void BindCaseDeatils(object sender, EventArgs e)
        {
            if (ddlCase.SelectedValue != "")
            {

                ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
                ICompanyController companyController = ControllerFactory.CreateCompanyController();
                ICompanyUnitController companyUnitController = ControllerFactory.CreateCompanyUnitController();
                ICaseNatureController caseNatureController = ControllerFactory.CreateCaseNatureController();

                CaseMaster caseMaster = new CaseMaster();
                caseMaster = caseMasterController.GetCaseMaster(ddlCase.SelectedValue);

                Company company = new Company();
                company = companyController.GetCompany(caseMaster.CompanyId);

                CompanyUnit companyUnit = new CompanyUnit();
                companyUnit = companyUnitController.GetCompanyUnit(caseMaster.CompanyUnitId);

                CaseNature caseNature = new CaseNature();
                caseNature = caseNatureController.GetCaseNature(caseMaster.CaseStatusId);

                lblCompany.Text = company.CompanyName;
                lblCompanyUnit.Text = companyUnit.CompanyUnitName;
                lblDescription.Text = caseMaster.CaseDescription;
                lblNature.Text = caseNature.CaseNatureName;
                if (caseMaster.IsPlentif == 1)
                {
                    lblPlaintiff.Text = company.CompanyName;
                    lblDefendant.Text = caseMaster.OtherParty; ;
                }
                else
                {
                    lblPlaintiff.Text = caseMaster.OtherParty;
                    lblDefendant.Text = company.CompanyName;
                }
            }
            else
            {
                lblCompany.Text = "N/A";
                lblCompanyUnit.Text = "N/A";
                lblDescription.Text = "N/A";
                lblNature.Text = "N/A";
                lblPlaintiff.Text = "N/A";
                lblDefendant.Text = "N/A";
            }
        }

        private void BindLawyerList()
        {

            ILawyerController lawyerController = ControllerFactory.CreateLawyerController();
            lawyerList = lawyerController.GetLawyerList();

            ddlAssignAttorney.DataSource = lawyerList;
            ddlAssignAttorney.DataValueField = "LawyerId";
            ddlAssignAttorney.DataTextField = "LawyerName";
            ddlAssignAttorney.DataBind();
            ddlAssignAttorney.Items.Insert(0, new ListItem("-- select attorney --", ""));


            ddlCounselor.DataSource = lawyerList;
            ddlCounselor.DataValueField = "LawyerId";
            ddlCounselor.DataTextField = "LawyerName";
            ddlCounselor.DataBind();
            ddlCounselor.Items.Insert(0, new ListItem("-- select counsilor --", ""));

        }

        private void BindActionList()
        {

            ICaseActionController caseActionController = ControllerFactory.CreateCaseActionController();
            caseActionList = caseActionController.GetCaseActionList();

            ddlActionTaken.DataSource = caseActionList;
            ddlActionTaken.DataValueField = "ActionId";
            ddlActionTaken.DataTextField = "ActionName";
            ddlActionTaken.DataBind();
            ddlActionTaken.Items.Insert(0, new ListItem("-- select action taken --", ""));

            ddlNextAction.DataSource = caseActionList;
            ddlNextAction.DataValueField = "ActionId";
            ddlNextAction.DataTextField = "ActionName";
            ddlNextAction.DataBind();
            ddlNextAction.Items.Insert(0, new ListItem("-- select next action --", ""));

        }

        private void BindJudgementList()
        {

            IJudgementTypeController judgementTypeController = ControllerFactory.CreateJudgementTypeController();
            judgementTypeList = judgementTypeController.GetJudgementTypeList();

            ddlJudgement.DataSource = judgementTypeList;
            ddlJudgement.DataValueField = "JTypeId";
            ddlJudgement.DataTextField = "JTypeName";
            ddlJudgement.DataBind();
            ddlJudgement.Items.Insert(0, new ListItem("-- select judgement type --", ""));

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void btnUpdateActivity_Click(object sender, EventArgs e)
        {
            ICaseActivityController caseActivityController = ControllerFactory.CreateCaseActivityController();
            CaseActivity caseActivity = new CaseActivity();

            CultureInfo provider = new CultureInfo("en-US");

            caseActivity.CaseNumber = ddlCase.SelectedValue;
            caseActivity.CreateUserId = Convert.ToInt32(Session["User_Id"]);
            caseActivity.ActivityDate = DateTime.Parse(txtDate.Text, provider, DateTimeStyles.AdjustToUniversal);
            caseActivity.AssignAttorneyId = Convert.ToInt32(ddlAssignAttorney.SelectedValue);
            caseActivity.CounsilorId = Convert.ToInt32(ddlCounselor.SelectedValue);
            caseActivity.OtherSideLawyer = txtOtherLawyer.Text;
            caseActivity.JudgeName = txtJudgeName.Text;
            caseActivity.CompanyRep = txtCompanyRepresenter.Text;
            caseActivity.ActionTakenId = Convert.ToInt32(ddlActionTaken.SelectedValue);
            caseActivity.NextActionId = Convert.ToInt32(ddlNextAction.SelectedValue);
            caseActivity.NextDate = DateTime.Parse(txtNextDate.Text, provider, DateTimeStyles.AdjustToUniversal);
            caseActivity.Remarks = txtRemarks.Text;

            caseActivityController.Save(caseActivity);

            Clear();
        }


        private void Clear()
        {
            txtDate.Text = string.Empty;
            txtJudgeName.Text = string.Empty;
            txtCompanyRepresenter.Text = string.Empty;
            txtNextDate.Text = string.Empty;
            txtOtherLawyer.Text = string.Empty;
            txtOutcome.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            ddlCase.SelectedIndex = 0;
            ddlAssignAttorney.SelectedIndex = 0;
            ddlCounselor.SelectedIndex = 0;
            ddlActionTaken.SelectedIndex = 0;
            ddlNextAction.SelectedIndex = 0;
            ddlJudgement.Items.Clear();

            lblCompany.Text = "N/A";
            lblCompanyUnit.Text = "N/A";
            lblDescription.Text = "N/A";
            lblNature.Text = "N/A";
            lblPlaintiff.Text = "N/A";
            lblDefendant.Text = "N/A";

        }
    }
}