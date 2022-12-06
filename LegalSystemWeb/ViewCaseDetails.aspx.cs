using LegalSystemCore;
using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class ViewCaseDetails : System.Web.UI.Page
    {
        CaseMaster caseMaster = new CaseMaster();
        string caseNumber;
        int UserId, companyId;

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
                    SetCaseMasterData();
                    caseNumber = Request.QueryString["CaseNumber"].ToString();
                    BindCaseActivityList(caseNumber);
                    BindDocumentList(caseNumber);
                }
                //}
            }
        }

        private void SetCaseMasterData()
        {
            try
            {
                ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
                caseMaster = caseMasterController.GetCaseMaster(Request.QueryString["CaseNumber"].ToString());

                UserId = Convert.ToInt32(Session["User_Role_Id"]);
                companyId = Convert.ToInt32(Session["company_id"].ToString());
                int companyUnitId = Convert.ToInt32(Session["company_unit_id"].ToString());

                if (UserId == 4)
                {
                    if (caseMaster.CompanyId != companyId)
                    {
                        Response.Redirect("404.aspx");
                    }
                }

                if (UserId == 5)
                {
                    if (caseMaster.CompanyUnitId != companyUnitId)
                    {
                        Response.Redirect("404.aspx");
                    }
                }

                ICompanyController companyController = ControllerFactory.CreateCompanyController();
                ICompanyUnitController companyUnitController = ControllerFactory.CreateCompanyUnitController();
                ICaseNatureController caseNatureController = ControllerFactory.CreateCaseNatureController();
                ICourtController courtController = ControllerFactory.CreateCourtController();
                ILocationController locationController = ControllerFactory.CreateLocationController();
                ILawyerController lawyerController = ControllerFactory.CreateLawyerController();
                IUserLoginController userLoginController = ControllerFactory.CreateUserLoginController();
                IJudgementTypeController judgementTypeController = ControllerFactory.CreateJudgementTypeController();

                Company company = new Company();
                company = companyController.GetCompany(caseMaster.CompanyId);

                CompanyUnit companyUnit = new CompanyUnit();
                companyUnit = companyUnitController.GetCompanyUnit(caseMaster.CompanyUnitId);

                CaseNature caseNature = new CaseNature();
                caseNature = caseNatureController.GetCaseNature(caseMaster.CaseNatureId);

                List<Court> courtList = courtController.GetCourtList(true);
                courtList = courtList.Where(c => c.CourtId == caseMaster.CaseStatusId).ToList();

                List<Location> locationList = locationController.GetLocationList(true);
                locationList = locationList.Where(l => l.LocationId == caseMaster.LocationId).ToList();

                List<Lawyer> lawyerList = lawyerController.GetLawyerList(true);
                List<Lawyer> assignList = lawyerList.Where(l => l.LawyerId == caseMaster.AssignAttornerId).ToList();

                List<UserLogin> userClosedList = userLoginController.GetUserLoginList(true);
                List<UserLogin> userCreatedList = userClosedList.Where(l => l.UserId == caseMaster.CreatedUserId).ToList();


                if (caseMaster.JudgementTypeId > 0)
                {
                    userClosedList = userClosedList.Where(l => l.UserId == caseMaster.CreatedUserId).ToList();
                    List<JudgementType> judgementTypesList = judgementTypeController.GetJudgementTypeList(true);
                    judgementTypesList = judgementTypesList.Where(l => l.JTypeId == caseMaster.JudgementTypeId).ToList();

                    lblJudgement.Text = judgementTypesList[0].JTypeName;
                    lblCloseOutcome.Text = caseMaster.CaseOutcome;
                    if (caseMaster.ClosedRemarks != null)
                        if (caseMaster.ClosedRemarks != "")
                            lblCLoseRemarks.Text = caseMaster.ClosedRemarks;
                    lblCloseUser.Text = userClosedList[0].UserName;
                    lblCloseDate.Text = caseMaster.ClosedDate.ToString("dd/MM/yyyy");
                }



                lblCaseNumber.Text = Request.QueryString["CaseNumber"].ToString();
                lblCompany.Text = company.CompanyName;
                lblCompanyUnit.Text = companyUnit.CompanyUnitName;
                lblClaimAmount.Text = caseMaster.ClaimAmount.ToString();
                lblCreateDate.Text = caseMaster.CreatedDate.ToString("dd/MM/yyyy");
                lblDescription.Text = caseMaster.CaseDescription;
                lblNature.Text = caseNature.CaseNatureName;

                lblCourt.Text = courtList[0].CourtName;
                lblLocationi.Text = locationList[0].locationName;
                lblAttorney.Text = assignList[0].LawyerName;
                lblUser.Text = userCreatedList[0].UserName;

                if (caseMaster.CounsilorId > 0)
                {
                    List<Lawyer> counsilorList = lawyerList.Where(l => l.LawyerId == caseMaster.CounsilorId).ToList();
                    lblCounsilor.Text = counsilorList[0].LawyerName;
                }

                if (caseMaster.PrevCaseNumber != null)
                    lblPrevCase.Text = caseMaster.PrevCaseNumber;


                if (caseMaster.CaseStatusId == 1)
                    lblStatus.Text = "Ongoing";
                else
                    lblStatus.Text = "Closed";

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
            catch (Exception)
            {
                Response.Redirect("500.aspx");
            }

        }


        private void BindCaseActivityList(string casenumber)
        {
            ICaseActivityController caseActivityController = ControllerFactory.CreateCaseActivityController();
            List<CaseActivity> caseActivityList = caseActivityController.GetUpdateCaseList(true);
            caseActivityList = caseActivityList.Where(x => x.CaseNumber == casenumber).ToList();

            foreach (var activity in caseActivityList)
            {

                activity.ActivityDateString = activity.ActivityDate.ToString("dd/MM/yyyy");
                if ((activity.NextDate).ToString("dd/MM/yyyy") != "01/01/0001")
                    activity.NextDateString = activity.NextDate.ToString("dd/MM/yyyy");
                else
                    activity.NextDateString = "N/A";
            }

            gvCaseActivity.DataSource = caseActivityList;
            gvCaseActivity.DataBind();
        }

        private void BindDocumentList(string casenumber)
        {
            IDocumentCaseController documentController = ControllerFactory.CreateDocumentCaseController();
            List<DocumentCase> documentList = documentController.GetDocumentList();

            gvDocuments.DataSource = documentList.Where(x => x.CaseNumber == casenumber).ToList();
            gvDocuments.DataBind();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            caseNumber = Request.QueryString["CaseNumber"].ToString();
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            IDocumentCaseController documentController = ControllerFactory.CreateDocumentCaseController();
            List<DocumentCase> documentList = documentController.GetDocumentList();
            documentList = documentList.Where(x => x.CaseNumber == caseNumber).ToList();

            string fileName = documentList[rowIndex].DocumentName;
            if (fileName != "" && fileName != null)
            {
                string filePathe = Server.MapPath("~/SystemDocuments/CaseMaster/" + fileName);

                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename = " + fileName);
                Response.TransmitFile(filePathe);
                Response.End();
            }


        }
    }
}
