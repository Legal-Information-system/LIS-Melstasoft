using LegalSystemCore;
using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
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
                    BindPaymentDetailsList(caseNumber);
                }
                //}
            }
        }

        private void SetCaseMasterData()
        {
            try
            {
                ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
                caseMaster = caseMasterController.GetCaseMaster(Request.QueryString["CaseNumber"].ToString(), true);

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


                if (caseMaster.JudgementTypeId > 0)
                {

                    lblJudgement.Text = caseMaster.judgementType.JTypeName;
                    lblCloseOutcome.Text = caseMaster.CaseOutcome;
                    if (caseMaster.ClosedRemarks != null)
                        if (caseMaster.ClosedRemarks != "")
                            lblCLoseRemarks.Text = caseMaster.ClosedRemarks;
                    lblCloseUser.Text = caseMaster.userClose.UserName;
                    lblCloseDate.Text = caseMaster.ClosedDate.ToString("dd/MM/yyyy");
                }

                lblCaseNumber.Text = Request.QueryString["CaseNumber"].ToString();
                lblCompany.Text = caseMaster.company.CompanyName;
                lblCompanyUnit.Text = caseMaster.companyUnit.CompanyUnitName;
                lblClaimAmount.Text = caseMaster.ClaimAmount.ToString();
                lblCreateDate.Text = caseMaster.CreatedDate.ToString("dd/MM/yyyy");
                lblCaseOpenDate.Text = caseMaster.CaseOpenDate.ToString("dd/MM/yyyy");
                lblDescription.Text = caseMaster.CaseDescription;
                lblNature.Text = caseMaster.caseNature.CaseNatureName;

                lblCourt.Text = caseMaster.court.CourtName;
                lblLocationi.Text = caseMaster.location.locationName;
                lblAttorney.Text = caseMaster.AssignAttorner.LawyerName;
                lblUser.Text = caseMaster.userCreate.UserName;

                //if (caseMaster.CounsilorId > 0)
                //{

                //    lblCounsilor.Text = caseMaster.Counsilor.LawyerName;
                //}

                ICounselorController counselorController = ControllerFactory.CreateCounselorController();
                List<Counselor> listCounselor = counselorController.GetCounselorList(caseMaster.CaseNumber);
                Lawyer lawyer = new Lawyer();
                ILawyerController lawyerController = ControllerFactory.CreateLawyerController();
                IPartyController partyController = ControllerFactory.CreatePartyController();
                IPartyCaseController partyCaseControler = ControllerFactory.CreatePartyCaseController();
                List<PartyCase> partyCaseList = partyCaseControler.GetPartyCaseList(Request.QueryString["CaseNumber"].ToString());
                Party party = new Party();
                if (caseMaster.IsPlentif == 1)
                {
                    foreach (PartyCase partyCase in partyCaseList.Where(x => x.IsPlaintif == 1))
                    {
                        StringBuilder cstextCard = new StringBuilder();
                        party = partyController.GetParty(partyCase.PartyId);


                        cstextCard.Append("<div class=\"row\">\r\n                        <div class=\"col-sm-6\">\r\n                            <p>Plaintiff Side</p>\r\n                        </div>\r\n                        <div class=\"col-md-6\">\r\n                            <label>");
                        cstextCard.Append(party.PartyName);
                        cstextCard.Append("</label>\r\n\r\n                        </div>\r\n                    </div>");


                        ltPlaintifParty.Text += cstextCard;
                    }
                    foreach (PartyCase partyCase in partyCaseList.Where(x => x.IsPlaintif == 0))
                    {
                        StringBuilder cstextCard = new StringBuilder();
                        party = partyController.GetParty(partyCase.PartyId);


                        cstextCard.Append("<div class=\"row mb-1\">\r\n                        <div class=\"col-sm-5\">\r\n                            <p>Defendent Side</p>\r\n                        </div>\r\n                        <div class=\"col-md-7\">\r\n                            <label>");
                        cstextCard.Append(party.PartyName);
                        cstextCard.Append("</label>\r\n\r\n                        </div>\r\n                    </div>");


                        ltDefendentParty.Text += cstextCard;
                    }
                }
                else
                {
                    foreach (PartyCase partyCase in partyCaseList.Where(x => x.IsPlaintif == 1))
                    {
                        StringBuilder cstextCard = new StringBuilder();
                        party = partyController.GetParty(partyCase.PartyId);


                        cstextCard.Append("<div class=\"row\">\r\n                        <div class=\"col-sm-6\">\r\n                            <p>Plaintiff Side</p>\r\n                        </div>\r\n                        <div class=\"col-md-6\">\r\n                            <label>");
                        cstextCard.Append(party.PartyName);
                        cstextCard.Append("</label>\r\n\r\n                        </div>\r\n                    </div>");


                        ltDefendentParty.Text += cstextCard;
                    }
                    foreach (PartyCase partyCase in partyCaseList.Where(x => x.IsPlaintif == 0))
                    {
                        StringBuilder cstextCard = new StringBuilder();
                        party = partyController.GetParty(partyCase.PartyId);


                        cstextCard.Append("<div class=\"row mb-1\">\r\n                        <div class=\"col-sm-5\">\r\n                            <p>Defendent Side</p>\r\n                        </div>\r\n                        <div class=\"col-md-7\">\r\n                            <label>");
                        cstextCard.Append(party.PartyName);
                        cstextCard.Append("</label>\r\n\r\n                        </div>\r\n                    </div>");


                        ltPlaintifParty.Text += cstextCard;
                    }
                }
                foreach (Counselor counselor in listCounselor)
                {
                    StringBuilder cstextCard = new StringBuilder();
                    lawyer = lawyerController.GetLawyer(counselor.LawyerId);

                    cstextCard.Append("<div class=\"row mb-1\">\r\n                        <div class=\"col-sm-5\">\r\n                            <p>Counsilor</p>\r\n                        </div>\r\n                        <div class=\"col-md-7\">\r\n                            <label>");
                    cstextCard.Append(lawyer.LawyerName);
                    cstextCard.Append("</label>\r\n\r\n                        </div>\r\n                    </div>");


                    ltCounselor.Text += cstextCard;
                }



                if (caseMaster.PrevCaseNumber != null)
                    lblPrevCase.Text = caseMaster.PrevCaseNumber;


                if (caseMaster.CaseStatusId == 1)
                    lblStatus.Text = "Ongoing";
                else
                    lblStatus.Text = "Closed";

                if (caseMaster.IsPlentif == 1)
                {
                    lblPlaintiff.Text = "Plaintiff";
                    //lblDefendant.Text = caseMaster.OtherParty; 
                }
                else
                {
                    lblPlaintiff.Text = "Defendent";
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


        private void BindPaymentDetailsList(string casenumber)
        {
            IPaymentController paymentController = ControllerFactory.CreatePaymentController();
            List<Payment> paymentList = paymentController.GetPaymentList(true, true, true, true);
            paymentList = paymentList.Where(x => x.CaseNumber == casenumber).ToList();

            foreach (var payment in paymentList)
            {
                payment.CreatedDateString = payment.CreatedDate.ToString("dd/MM/yyyy");
            }

            gvPayments.DataSource = paymentList;
            gvPayments.DataBind();
        }


        private void BindDocumentList(string casenumber)
        {
            IDocumentCaseController documentController = ControllerFactory.CreateDocumentCaseController();
            List<DocumentCase> documentList = documentController.GetDocumentList();

            gvDocuments.DataSource = documentList.Where(x => x.CaseNumber == casenumber).ToList();
            gvDocuments.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
            caseMaster = caseMasterController.GetCaseMaster(Request.QueryString["CaseNumber"].ToString(), true);
            caseMasterController.Delete(caseMaster);
            Response.Redirect("ViewCases.aspx?name=All");

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateCase.aspx?casenumber=" + Request.QueryString["CaseNumber"].ToString() + "&update=true");
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
