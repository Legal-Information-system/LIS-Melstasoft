using LegalSystemCore;
using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using File = System.IO.File;

namespace LegalSystemWeb
{
    public partial class UpdateCaseActivity : System.Web.UI.Page
    {
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        IUserPrivilegeController userPrivilegeController = ControllerFactory.CreateUserPrivilegeController();
        ICaseActivityController caseActivityController = ControllerFactory.CreateCaseActivityController();
        CaseActivity caseActivityGlobal = new CaseActivity();
        List<JudgementType> judgementTypeList = new List<JudgementType>();
        List<Lawyer> lawyerList = new List<Lawyer>();
        List<CaseAction> caseActionList = new List<CaseAction>();
        int UserId;
        public static List<string> filePaths = new List<string>();
        public static List<ListItem> files = new List<ListItem>();
        public static List<HttpPostedFileBase> listUplodedFile = new List<HttpPostedFileBase>();
        public static List<DocumentCaseActivity> UplodedFilesList = new List<DocumentCaseActivity>();
        public static int documentIncrement = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 21).Any()
                    && !(userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 21 && x.IsGrantRevoke == 0))) ||
                    userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 21 && x.IsGrantRevoke == 1)))
                    Response.Redirect("404.aspx");
                else
                {
                    if (!IsPostBack)
                    {
                        BindCaseList();
                        BindLawyerList();
                        BindActionList();
                        BindJudgementList();
                        if (pageSwitch())
                        {
                            caseActivityGlobal = caseActivityController.GetCaseActivity(Request.QueryString["CaseActivityNumber"].ToString(), true);
                            hTitle.InnerText = "Update Case Activity - " + caseActivityGlobal.CaseNumber + " ( " + caseActivityGlobal.CaseActivitId + " )";
                            caseActivityGlobal = caseActivityController.GetCaseActivity(Request.QueryString["CaseActivityNumber"].ToString(), true);
                            pageUpdateSet();
                        }
                        else
                        {
                            hTitle.InnerText = "Update Case Activity";


                        }
                    }
                }

            }

        }

        private void pageUpdateSet()
        {
            ddlCase.SelectedValue = caseActivityGlobal.CaseNumber;
            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
            ICompanyController companyController = ControllerFactory.CreateCompanyController();
            ICompanyUnitController companyUnitController = ControllerFactory.CreateCompanyUnitController();
            ICaseNatureController caseNatureController = ControllerFactory.CreateCaseNatureController();
            dvCaseNumber.Visible = false;
            CaseMaster caseMaster = new CaseMaster();
            caseMaster = caseMasterController.GetCaseMaster(ddlCase.SelectedValue, true);

            Company company = new Company();
            company = companyController.GetCompany(caseMaster.CompanyId);

            CompanyUnit companyUnit = new CompanyUnit();
            companyUnit = companyUnitController.GetCompanyUnit(caseMaster.CompanyUnitId);

            CaseNature caseNature = new CaseNature();
            caseNature = caseNatureController.GetCaseNature(caseMaster.CaseNatureId);

            lblCompany.Text = company.CompanyName;
            lblCompanyUnit.Text = companyUnit.CompanyUnitName;
            lblDescription.Text = caseMaster.CaseDescription;
            lblNature.Text = caseNature.CaseNatureName;
            BindDocumentList();
            if (caseMaster.IsPlentif == 1)
            {
                lblPlaintiff.Text = company.CompanyName;
                //lblDefendant.Text = caseMaster.OtherParty; ;
            }
            else
            {
                //lblPlaintiff.Text = caseMaster.OtherParty;
                lblDefendant.Text = company.CompanyName;
            }

            txtDate.Text = caseActivityGlobal.ActivityDate.ToString("yyyy-MM-dd");
            ddlAssignAttorney.SelectedValue = caseActivityGlobal.AssignAttorneyId.ToString();
            ddlCounselor.SelectedValue = caseActivityGlobal.CounsilorId.ToString();
            txtOtherLawyer.Text = caseActivityGlobal.OtherSideLawyer.ToString();
            txtJudgeName.Text = caseActivityGlobal.JudgeName.ToString();
            txtCompanyRepresenter.Text = caseActivityGlobal.CompanyRep.ToString();
            ddlActionTaken.SelectedValue = caseActivityGlobal.actionTaken.ActionId.ToString();
            ddlNextAction.SelectedValue = caseActivityGlobal.NextActionId.ToString();
            txtNextDate.Text = caseActivityGlobal.NextDate.ToString("yyyy-MM-dd");
            txtRemarks.Text = caseActivityGlobal.Remarks.ToString();
        }

        private bool pageSwitch()
        {
            Dictionary<string, string> allRequestParamesDictionary = Request.Params.AllKeys.ToDictionary(x => x, x => Request.Params[x]);
            if (allRequestParamesDictionary.ContainsKey("update") && allRequestParamesDictionary.ContainsKey("CaseActivityNumber"))
            {
                if (Request.QueryString["CaseActivityNumber"] != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void BindCaseList()
        {
            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
            List<CaseMaster> caseMasterList = caseMasterController.GetCaseMasterList(true);

            int companyUnitId = Convert.ToInt32(Session["company_unit_id"].ToString());
            int companyId = Convert.ToInt32(Session["company_id"].ToString());
            UserId = Convert.ToInt32(Session["User_Role_Id"]);


            if (UserId == 4)
                caseMasterList = caseMasterList.Where(c => c.CompanyId == companyId).ToList();
            if (UserId == 5)
                caseMasterList = caseMasterList.Where(c => c.CompanyUnitId == companyUnitId).ToList();

            ddlCase.DataSource = caseMasterList.OrderBy(x => x.CaseNumber);
            ddlCase.DataValueField = "CaseNumber";
            ddlCase.DataTextField = "CaseNumber";
            ddlCase.DataBind();
            ddlCase.Items.Insert(0, new ListItem("-- select case --", ""));

        }

        protected void BindCaseDetails(object sender, EventArgs e)
        {
            if (ddlCase.SelectedValue != "")
            {

                ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
                ICompanyController companyController = ControllerFactory.CreateCompanyController();
                ICompanyUnitController companyUnitController = ControllerFactory.CreateCompanyUnitController();
                ICaseNatureController caseNatureController = ControllerFactory.CreateCaseNatureController();

                CaseMaster caseMaster = new CaseMaster();
                caseMaster = caseMasterController.GetCaseMaster(ddlCase.SelectedValue, true);

                Company company = new Company();
                company = companyController.GetCompany(caseMaster.CompanyId);

                CompanyUnit companyUnit = new CompanyUnit();
                companyUnit = companyUnitController.GetCompanyUnit(caseMaster.CompanyUnitId);

                CaseNature caseNature = new CaseNature();
                caseNature = caseNatureController.GetCaseNature(caseMaster.CaseNatureId);

                lblCompany.Text = company.CompanyName;
                lblCompanyUnit.Text = companyUnit.CompanyUnitName;
                lblDescription.Text = caseMaster.CaseDescription;
                lblNature.Text = caseNature.CaseNatureName;
                if (caseMaster.IsPlentif == 1)
                {
                    lblPlaintiff.Text = company.CompanyName;
                    //lblDefendant.Text = caseMaster.OtherParty; ;
                }
                else
                {
                    //lblPlaintiff.Text = caseMaster.OtherParty;
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
            lawyerList = lawyerController.GetLawyerList(false);

            ddlAssignAttorney.DataSource = lawyerList.OrderBy(x => x.LawyerName);
            ddlAssignAttorney.DataValueField = "LawyerId";
            ddlAssignAttorney.DataTextField = "LawyerName";
            ddlAssignAttorney.DataBind();
            ddlAssignAttorney.Items.Insert(0, new ListItem("-- select attorney --", ""));


            ddlCounselor.DataSource = lawyerList.OrderBy(x => x.LawyerName);
            ddlCounselor.DataValueField = "LawyerId";
            ddlCounselor.DataTextField = "LawyerName";
            ddlCounselor.DataBind();
            ddlCounselor.Items.Insert(0, new ListItem("-- select counselor --", ""));

        }

        private void BindActionList()
        {

            ICaseActionController caseActionController = ControllerFactory.CreateCaseActionController();
            caseActionList = caseActionController.GetCaseActionList(false);

            ddlActionTaken.DataSource = caseActionList.OrderBy(x => x.ActionName);
            ddlActionTaken.DataValueField = "ActionId";
            ddlActionTaken.DataTextField = "ActionName";
            ddlActionTaken.DataBind();
            ddlActionTaken.Items.Insert(0, new ListItem("-- select action taken --", ""));

            ddlNextAction.DataSource = caseActionList.OrderBy(x => x.ActionName);
            ddlNextAction.DataValueField = "ActionId";
            ddlNextAction.DataTextField = "ActionName";
            ddlNextAction.DataBind();
            ddlNextAction.Items.Insert(0, new ListItem("-- select next action --", ""));

        }

        private void BindJudgementList()
        {

            IJudgementTypeController judgementTypeController = ControllerFactory.CreateJudgementTypeController();
            judgementTypeList = judgementTypeController.GetJudgementTypeList(false);

            ddlJudgement.DataSource = judgementTypeList.OrderBy(x => x.JTypeName);
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

            CaseActivity caseActivity = new CaseActivity();

            CultureInfo provider = new CultureInfo("en-US");

            bool nextdate = false;

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
            if (txtNextDate.Text != "")
            {
                caseActivity.NextDate = DateTime.Parse(txtNextDate.Text, provider, DateTimeStyles.AdjustToUniversal);
                nextdate = true;
            }
            int caseActivityNumber;
            caseActivity.Remarks = txtRemarks.Text;
            if (pageSwitch())
            {
                caseActivityNumber = Convert.ToInt32(Request.QueryString["CaseActivityNumber"].ToString());
                caseActivity.CaseActivitId = caseActivityNumber;
                caseActivityController.Update(caseActivity, nextdate);
            }
            else
            {
                caseActivityNumber = caseActivityController.Save(caseActivity, nextdate);
            }
            UploadFiles(caseActivityNumber);
            ClearDocuments();

            if (ddlJudgement.SelectedValue != "")
            {
                ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
                CaseMaster caseMaster = new CaseMaster();

                caseMaster.CaseStatusId = 2;
                caseMaster.JudgementTypeId = Convert.ToInt32(ddlJudgement.SelectedValue);
                caseMaster.CaseOutcome = txtOutcome.Text;
                caseMaster.ClosedRemarks = txtCaseCloseRemarks.Text;
                caseMaster.ClosedDate = DateTime.Now;
                caseMaster.ClosedUserId = Convert.ToInt32(Session["User_Id"]);
                caseMaster.CaseNumber = ddlCase.SelectedValue;

                caseMasterController.CaseClose(caseMaster);

                BindCaseList();
            }

            Clear();

            if (pageSwitch())
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Case Activity Updated Succesfully!', 'success');window.setTimeout(function(){window.location='ViewCaseActivity.aspx?CaseActivityNumber=" + Request.QueryString["CaseActivityNumber"].ToString() + "'},2500);", true);

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Case Activity Updated Succesfully!', 'success')", true);
            }
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
            ddlJudgement.SelectedIndex = 0;

            lblCompany.Text = "N/A";
            lblCompanyUnit.Text = "N/A";
            lblDescription.Text = "N/A";
            lblNature.Text = "N/A";
            lblPlaintiff.Text = "N/A";
            lblDefendant.Text = "N/A";

        }

        protected void UploadFiles(int caseActivityId)
        {
            IDocumentController documentController = ControllerFactory.CreateDocumentController();
            IDocumentCaseActivityController documentCaseController = ControllerFactory.CreateDocumentCaseActivityController();

            Document document = new Document();
            DocumentCaseActivity documentCase = new DocumentCaseActivity();
            documentCase.CaseActivityId = ddlCase.SelectedValue.ToString();
            if (pageSwitch())
            {
                List<DocumentCaseActivity> documentCases = documentCaseController.GetDocumentList(documentCase, false);

                documentCaseController.DeleteDocuments(documentCase);
                foreach (DocumentCaseActivity doc in documentCases)
                {
                    document.DocumentId = doc.DocumentId;
                    documentController.DeletePermenent(document);
                }
            }

            //int i = 0;
            //foreach (HttpPostedFileBase file in listUplodedFile)
            //{
            //    if (file.ContentLength > 0)
            //    {
            //        file.SaveAs(Server.MapPath("~/SystemDocuments/CaseMaster/") + caseNumber + filePaths[i]);
            //        //lblListOfUploadedFiles.Text += String.Format("{0}<br />", uploadFile.FileName);


            //        document.DocumentType = "case";
            //        documentCase.DocumentId = documentController.Save(document);

            //        documentCase.DocumentName = caseNumber + filePaths[i];
            //        documentCase.CaseNumber = txtCaseNumber.Text;
            //        documentCase.DocumentDescription = "";
            //        documentCaseController.Save(documentCase);
            //    }
            //}

            foreach (var item in UplodedFilesList)
            {
                document.DocumentType = "case Activity";
                item.DocumentId = documentController.Save(document);
                item.CaseActivityId = caseActivityId.ToString();
                documentCaseController.Save(item);
            }

            listUplodedFile.Clear();
            files.Clear();
            filePaths.Clear();
            BindDocuments();
        }

        protected void AddFiles(object sender, EventArgs e)
        {


            if (Uploader.PostedFile != null)
            {
                //listUplodedFile.Add(new HttpPostedFileWrapper(HttpContext.Current.Request.Files[0]));
                //string fileName = Path.GetFileName(Uploader.PostedFile.FileName);
                //HttpPostedFileBase uploadFile = listUplodedFile.Last();

                //if (uploadFile.ContentLength > 0)
                //{
                //    filePaths.Add(documentIncrement++ + uploadFile.FileName);
                //    //lblListOfUploadedFiles.Text += String.Format("{0}<br />", uploadFile.FileName);
                //}


                HttpFileCollection uploadFiles = Request.Files;
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    HttpPostedFile uploadFile = uploadFiles[i];
                    if (uploadFile.ContentLength > 0)
                    {

                        uploadFile.SaveAs(Server.MapPath("~/SystemDocuments/CaseMaster/CaseActivity/") + uploadFile.FileName);

                        DocumentCaseActivity document = new DocumentCaseActivity
                        {
                            DocumentName = uploadFile.FileName,

                            DocumentDescription = ""

                        };

                        UplodedFilesList.Add(document);
                    }
                }
                BindDocuments();

            }
        }

        protected void ClearDocuments()
        {
            UplodedFilesList.Clear();
            filePaths.Clear();
            BindDocuments();
        }




        protected void DeleteFiles(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = fileGridview.PageSize;
            int pageIndex = fileGridview.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;
            //FileInfo file = new FileInfo(filePaths[rowIndex]);

            //filePaths.RemoveAt(rowIndex);
            //listUplodedFile.RemoveAt(rowIndex);

            DocumentCaseActivity documentCase = UplodedFilesList[rowIndex];
            string path = Server.MapPath("~/SystemDocuments/CaseMaster/CaseActivity/");
            string filePath = path + documentCase.DocumentName;

            if (File.Exists(filePath))
            {
                // If file found, delete it    
                File.Delete(filePath);
                UplodedFilesList.RemoveAt(rowIndex);
            }

            BindDocuments();
        }

        protected void BindDocuments()
        {
            fileGridview.DataSource = UplodedFilesList;
            fileGridview.DataBind();
        }

        private void BindDocumentList()
        {
            IDocumentCaseActivityController documentController = ControllerFactory.CreateDocumentCaseActivityController();
            DocumentCaseActivity documentCaseActivity = new DocumentCaseActivity();
            documentCaseActivity.CaseActivityId = Request.QueryString["CaseActivityNumber"].ToString();
            UplodedFilesList.Clear();
            UplodedFilesList = documentController.GetDocumentList(documentCaseActivity, false);

            BindDocuments();
        }
    }
}