using LegalSystemCore;
using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace LegalSystemWeb
{
    public partial class CreateCase : System.Web.UI.Page
    {
        IUserPrivilegeController userPrivilegeController = ControllerFactory.CreateUserPrivilegeController();
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        int UserId, UserRoleId;
        string UserName;
        List<CourtLocation> courtlocation = new List<CourtLocation>();
        List<Court> courtList = new List<Court>();
        List<Lawyer> lawyerList = new List<Lawyer>();
        public static List<Lawyer> CounselorLawyerList = new List<Lawyer>();
        ICounselorController counselorController = ControllerFactory.CreateCounselorController();
        IPartyController partyControllerGlobal = ControllerFactory.CreatePartyController();
        IPartyCaseController partyCaseController = ControllerFactory.CreatePartyCaseController();
        public static List<Party> partyList = new List<Party>();
        Counselor counselor = new Counselor();
        public static List<Party> plaintif = new List<Party>();
        public static List<Party> defendent = new List<Party>();
        public static List<Counselor> counselorList = new List<Counselor>();
        public static string caseNumber;
        public static List<string> filePaths = new List<string>();
        public static List<ListItem> files = new List<ListItem>();
        public static List<HttpPostedFileBase> listUplodedFile = new List<HttpPostedFileBase>();
        public static List<DocumentCase> UplodedFilesList = new List<DocumentCase>();
        public static int documentIncrement = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 16).Any()
                    && !(userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 16 && x.IsGrantRevoke == 0))) ||
                    userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 16 && x.IsGrantRevoke == 1)))
                {
                    Response.Redirect("404.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        dropDownOption.Visible = false;
                        textOption.Visible = false;
                        plaintif.Clear();
                        defendent.Clear();
                        partyList.Clear();
                        counselorList.Clear();
                        CounselorLawyerList.Clear();
                        if (pageSwitch())
                        {
                            hTitle.InnerText = "Update Case";
                        }
                        else
                        {
                            hTitle.InnerText = "Create Case";


                        }
                        bindCaseList();
                        BindCompanyList();
                        BindCaseNatureList();
                        BindCourtList();
                        BindLawyerList();
                        partyList = partyControllerGlobal.GetPartyList();

                        if (pageSwitch())
                        {
                            pageUpdateSet();
                        }
                    }





                }
            }
        }

        private void bindCaseList()
        {
            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
            List<CaseMaster> caseMasters = caseMasterController.GetCaseMasterListAll();
            ddlPrevCase.DataSource = caseMasters;
            ddlPrevCase.DataValueField = "CaseNumber";
            ddlPrevCase.DataTextField = "CaseNumber";
            ddlPrevCase.DataBind();
            ddlPrevCase.Items.Insert(0, new ListItem("-- select Case --", ""));
        }
        private void pageUpdateSet()
        {
            btnBack.Visible = false;
            btnSave.Text = "Update";

            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
            List<CaseMaster> caseMasters = caseMasterController.GetCaseMasterListAll();
            CaseMaster caseMaster = new CaseMaster();
            caseMaster = caseMasterController.GetCaseMaster(Request.QueryString["casenumber"].ToString(), true);
            caseNumber = caseMaster.CaseNumber;
            ddlCompany.SelectedValue = caseMaster.CompanyId.ToString();
            BindCompanyUnitList();
            ddlCompanyUnit.SelectedValue = caseMaster.CompanyUnitId.ToString();
            ddlNatureOfCase.SelectedValue = caseMaster.CaseNatureId.ToString();
            txtCaseDescription.Text = caseMaster.CaseDescription;
            txtClaimAmount.Text = caseMaster.ClaimAmount.ToString();
            rbIsPlantiff.SelectedValue = caseMaster.IsPlentif.ToString();
            txtCaseOpenDate.Text = caseMaster.CaseOpenDate.ToString("yyyy-MM-dd");

            List<PartyCase> partyCaseList = new List<PartyCase>();
            partyCaseList = partyCaseController.GetPartyCaseList(caseMaster.CaseNumber);



            Party party = new Party();
            if (partyCaseList.Where(x => x.IsPlaintif == 1).Any())
            {
                foreach (PartyCase partyCase in partyCaseList.Where(x => x.IsPlaintif == 1))
                {
                    party = partyControllerGlobal.GetParty(partyCase.PartyId);
                    plaintif.Add(party);
                }
            }
            if (partyCaseList.Where(x => x.IsPlaintif == 0).Any())
            {
                foreach (PartyCase partyCase in partyCaseList.Where(x => x.IsPlaintif == 0))
                {
                    party = partyControllerGlobal.GetParty(partyCase.PartyId);
                    defendent.Add(party);
                }
            }
            BindPlaintifList();
            BindDefendentList();
            ddlCourt.SelectedValue = caseMaster.CourtId.ToString();
            BindDCourtLocationList();
            ddlLocation.SelectedValue = caseMaster.location.ToString();
            txtCaseNumber.Text = caseMaster.CaseNumber.ToString();
            if (caseMaster.PrevCaseNumber == null)
            {
                caseMaster.PrevCaseNumber = "";
            }
            else
            {
                if (caseMasters.Any(x => x.CaseNumber == caseMaster.PrevCaseNumber))
                {
                    rbPrevCase.SelectedValue = "1";
                    dropDownOption.Visible = true;
                    ddlPrevCase.SelectedValue = caseMaster.PrevCaseNumber.ToString();
                }
                else
                {
                    rbPrevCase.SelectedValue = "0";
                    textOption.Visible = true;
                    txtPreCaseNumber.Text = caseMaster.PrevCaseNumber.ToString();
                }
            }
            txtPreCaseNumber.Text = caseMaster.PrevCaseNumber.ToString();
            ddlAttorney.SelectedValue = caseMaster.AssignAttornerId.ToString();
            counselorList = counselorController.GetCounselorList(caseMaster.CaseNumber);
            ILawyerController lawyerController = ControllerFactory.CreateLawyerController();
            Lawyer lawyer = new Lawyer();
            foreach (Counselor counselor in counselorList)
            {
                lawyer = lawyerController.GetLawyer(counselor.LawyerId);
                CounselorLawyerList.Add(lawyer);
            }

            BindCounselorList();
        }

        private bool pageSwitch()
        {
            Dictionary<string, string> allRequestParamesDictionary = Request.Params.AllKeys.ToDictionary(x => x, x => Request.Params[x]);
            if (allRequestParamesDictionary.ContainsKey("update"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void BindCompanyList()
        {
            ICompanyController companyController = ControllerFactory.CreateCompanyController();
            List<Company> companyList = companyController.GetCompanyList(false);

            int companyId = Convert.ToInt32(Session["company_id"].ToString());
            UserId = Convert.ToInt32(Session["User_Role_Id"]);

            if (UserId == 4 || UserId == 5)
                companyList = companyList.Where(c => c.CompanyId == companyId).ToList();

            ddlCompany.DataSource = companyList;
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("-- select company --", ""));

        }
        private void BindCompanyUnitList()
        {
            ICompanyUnitController companyUnitController = ControllerFactory.CreateCompanyUnitController();
            if (ddlCompany.SelectedValue != "")
            {
                List<CompanyUnit> companyUnitList = companyUnitController.GetCompanyUnitListFilter(false, ddlCompany.SelectedValue);

                int companyUnitId = Convert.ToInt32(Session["company_unit_id"].ToString());
                UserId = Convert.ToInt32(Session["User_Role_Id"]);

                if (UserId == 5)
                    companyUnitList = companyUnitList.Where(c => c.CompanyUnitId == companyUnitId).ToList();

                ddlCompanyUnit.DataSource = companyUnitList;
                ddlCompanyUnit.DataValueField = "CompanyUnitId";
                ddlCompanyUnit.DataTextField = "CompanyUnitName";

                ddlCompanyUnit.DataBind();

            }
            else
            {
                ddlCompanyUnit.Items.Clear();
            }
        }
        private void BindCaseNatureList()
        {
            ICaseNatureController caseNatureController = ControllerFactory.CreateCaseNatureController();
            ddlNatureOfCase.DataSource = caseNatureController.GetCaseNatureList(false);
            ddlNatureOfCase.DataValueField = "CaseNatureId";
            ddlNatureOfCase.DataTextField = "CaseNatureName";
            ddlNatureOfCase.DataBind();
            ddlNatureOfCase.Items.Insert(0, new ListItem("-- select case nature --", ""));

        }

        private void BindCourtList()
        {

            ICourtController courtController = ControllerFactory.CreateCourtController();

            courtList = courtController.GetCourtList(false);
            ddlCourt.DataSource = courtList;
            ddlCourt.DataValueField = "CourtId";
            ddlCourt.DataTextField = "CourtName";
            ddlCourt.DataBind();
            ddlCourt.Items.Insert(0, new ListItem("-- select court --", ""));


        }
        private void BindDCourtLocationList()
        {
            ICourtLocationController courtlocationController = ControllerFactory.CreateCourtLocationController();

            if (ddlCourt.SelectedValue != "")
            {
                courtlocation = courtlocationController.GetCourtLocationListFilter(Convert.ToInt32(ddlCourt.SelectedValue));
                List<Location> listLocation = new List<Location>();
                foreach (CourtLocation courtLocation in courtlocation)
                {
                    listLocation.Add(courtLocation.location);
                }

                ddlLocation.DataSource = listLocation;
                ddlLocation.DataValueField = "LocationId";
                ddlLocation.DataTextField = "locationName";

                ddlLocation.DataBind();
            }
            else
            {
                ddlLocation.Items.Clear();
            }
        }

        private void BindLawyerList()
        {

            ILawyerController lawyerController = ControllerFactory.CreateLawyerController();
            lawyerList = lawyerController.GetLawyerList(false);

            ddlAttorney.DataSource = lawyerList;
            ddlAttorney.DataValueField = "LawyerId";
            ddlAttorney.DataTextField = "LawyerName";
            ddlAttorney.DataBind();
            ddlAttorney.Items.Insert(0, new ListItem("-- select Attorney --", ""));


            ddlCounselor.DataSource = lawyerList;
            ddlCounselor.DataValueField = "LawyerId";
            ddlCounselor.DataTextField = "LawyerName";
            ddlCounselor.DataBind();
            ddlCounselor.Items.Insert(0, new ListItem("-- select Counselor --", ""));

        }

        private string PrevCase()
        {
            if (rbPrevCase.SelectedValue == "0")
            {
                return " ";
            }
            else
            {
                return ddlPrevCase.SelectedValue.ToString();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (pageSwitch())
            {
                btnUpdate_Click();
            }
            else
            {
                int flag = 0;
                lblCounselor.Text = "";
                lblPlaintif.Text = "";
                lblDefendent.Text = "";
                lblAttorney.Text = "";
                if (CounselorLawyerList.Any() && ((plaintif.Any() && rbIsPlantiff.SelectedValue == "0") || (defendent.Any() && rbIsPlantiff.SelectedValue == "1")) && !CounselorLawyerList.Where(x => x.LawyerName == ddlAttorney.SelectedItem.Text).Any())
                {
                    ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
                    IPartyController partyController = ControllerFactory.CreatePartyController();
                    IPartyCaseController partyCaseController = ControllerFactory.CreatePartyCaseController();

                    CaseMaster caseMaster = new CaseMaster();

                    CultureInfo provider = new CultureInfo("en-US");

                    if (CheckAvailableCaseNum(false, txtCaseNumber.Text, caseMasterController))
                    {
                        if ((rbPrevCase.SelectedValue == "1" && ddlPrevCase.SelectedIndex != 0) || (rbPrevCase.SelectedValue == "0"))
                        {

                            caseMaster.CaseNumber = txtCaseNumber.Text;
                            if (rbPrevCase.SelectedValue == "1")
                            {
                                if (ddlPrevCase.SelectedValue != "")
                                {
                                    caseMaster.PrevCaseNumber = ddlPrevCase.SelectedValue;
                                }
                                else
                                {
                                    caseMaster.PrevCaseNumber = " ";
                                }
                            }
                            else
                            {
                                if (txtPreCaseNumber.Text == null || txtPreCaseNumber.Text == string.Empty)
                                {
                                    caseMaster.PrevCaseNumber = " ";
                                }
                                else
                                {
                                    caseMaster.PrevCaseNumber = txtPreCaseNumber.Text;
                                }
                            }

                            caseMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                            caseMaster.CompanyUnitId = Convert.ToInt32(ddlCompanyUnit.SelectedValue);
                            caseMaster.CaseNatureId = Convert.ToInt32(ddlNatureOfCase.SelectedValue);
                            string test = txtCaseOpenDate.Text;
                            caseMaster.CaseOpenDate = DateTime.Parse(txtCaseOpenDate.Text, provider, DateTimeStyles.AdjustToUniversal);
                            caseMaster.CaseDescription = txtCaseDescription.Text;
                            string clamount = txtClaimAmount.Text;
                            caseMaster.ClaimAmount = txtClaimAmount.Text;
                            caseMaster.IsPlentif = Convert.ToInt32(rbIsPlantiff.Text);

                            caseMaster.CourtId = Convert.ToInt32(ddlCourt.SelectedValue);
                            caseMaster.LocationId = Convert.ToInt32(ddlLocation.SelectedValue);
                            caseMaster.AssignAttornerId = Convert.ToInt32(ddlAttorney.SelectedValue);
                            counselor.CaseNumber = caseMaster.CaseNumber;


                            caseMaster.CreatedUserId = Convert.ToInt32(Session["User_Id"]);
                            caseMaster.CreatedDate = DateTime.Now;
                            caseMaster.CaseStatusId = 1;
                            caseMaster.CreatedUserId = Convert.ToInt32(Session["User_Id"]);
                            caseMaster.CreatedDate = DateTime.Now;
                            caseMaster.CaseStatusId = 1;

                            caseMasterController.Save(caseMaster);

                            foreach (Lawyer lawyer in CounselorLawyerList)
                            {
                                counselor.LawyerId = lawyer.LawyerId;
                                counselorController.Save(counselor);
                            }
                            PartyCase partyCase = new PartyCase();

                            foreach (Party party in plaintif)
                            {
                                if (partyList.Where(x => x.PartyName == party.PartyName).Any())
                                {
                                    partyCase.PartyId = partyList.Where(x => x.PartyName == party.PartyName).ElementAt(0).PartyId;
                                }
                                else
                                {
                                    partyCase.PartyId = partyController.Save(party);
                                }

                                partyCase.CaseNumber = caseMaster.CaseNumber;
                                partyCase.IsPlaintif = 1;
                                partyCaseController.Save(partyCase);
                            }


                            foreach (Party party in defendent)
                            {
                                if (partyList.Where(x => x.PartyName == party.PartyName).Any())
                                {
                                    partyCase.PartyId = partyList.Where(x => x.PartyName == party.PartyName).ElementAt(0).PartyId;
                                }
                                else
                                {
                                    partyCase.PartyId = partyController.Save(party);
                                }
                                partyCase.CaseNumber = caseMaster.CaseNumber;
                                partyCase.IsPlaintif = 0;
                                partyCaseController.Save(partyCase);
                            }

                            UploadFiles();
                            ClearDocuments();
                            Clear();
                            flag = 1;
                        }
                    }

                    Clear();
                    clearCounselor();
                    clearDefendent();
                    clearPlaintif();



                }
                else
                {

                }
                if (!CounselorLawyerList.Any() && flag == 0)
                {
                    dCounselor.Visible = true;
                    lblCounselor.Text = "Please Add Counselor";
                }
                if ((!plaintif.Any() && rbIsPlantiff.SelectedValue == "1") && flag == 0)
                {
                    dPlaintif.Visible = true;
                    lblPlaintif.Text = "Please Add Plaintif Side";
                }
                if ((!defendent.Any() && rbIsPlantiff.SelectedValue == "0") && flag == 0)
                {
                    dDefendent.Visible = true;
                    lblDefendent.Text = "Please Add Defendent Side";
                }
                if (CounselorLawyerList.Where(x => x.LawyerName == ddlAttorney.SelectedItem.Text).Any() && flag == 0)
                {
                    dAttorney.Visible = true;
                    lblAttorney.Text = "Cannot assign the Attorney as a Counselor!";
                }
                if (flag == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "swal('Success!', 'Case Created Succesfully!', 'success')", true);

                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Payment Approve Succesfully!', 'success');window.setTimeout(function(){window.location='CreateCase.aspx'},2500);", true);
                }

            }
        }

        protected void btnUpdate_Click()
        {
            int flag = 0;
            lblCounselor.Text = "";
            lblPlaintif.Text = "";
            lblDefendent.Text = "";
            lblAttorney.Text = "";
            if ((CounselorLawyerList.Any() && ((plaintif.Any() && rbIsPlantiff.SelectedValue == "0") || (defendent.Any() && rbIsPlantiff.SelectedValue == "1"))) && !CounselorLawyerList.Where(x => x.LawyerName == ddlAttorney.SelectedItem.Text).Any())
            {
                ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
                IPartyController partyController = ControllerFactory.CreatePartyController();
                IPartyCaseController partyCaseController = ControllerFactory.CreatePartyCaseController();

                CaseMaster caseMaster = new CaseMaster();

                CultureInfo provider = new CultureInfo("en-US");

                if (CheckAvailableCaseNum(false, txtCaseNumber.Text, caseMasterController) || txtCaseNumber.Text == caseNumber)
                {
                    if ((rbPrevCase.SelectedValue == "1" && ddlPrevCase.SelectedIndex != 0) || (rbPrevCase.SelectedValue == "0"))
                    {
                        caseMaster.PrevCaseNumberUpdate = caseNumber;
                        if (rbPrevCase.SelectedValue == "1")
                        {
                            if (ddlPrevCase.SelectedValue != "")
                            {
                                caseMaster.PrevCaseNumber = ddlPrevCase.SelectedValue;
                            }
                            else
                            {
                                caseMaster.PrevCaseNumber = " ";
                            }
                        }
                        else
                        {
                            if (txtPreCaseNumber.Text == null || txtPreCaseNumber.Text == string.Empty)
                            {
                                caseMaster.PrevCaseNumber = " ";
                            }
                            else
                            {
                                caseMaster.PrevCaseNumber = txtPreCaseNumber.Text;
                            }
                        }
                        caseMaster.CaseNumber = txtCaseNumber.Text;

                        caseMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        caseMaster.CompanyUnitId = Convert.ToInt32(ddlCompanyUnit.SelectedValue);
                        caseMaster.CaseNatureId = Convert.ToInt32(ddlNatureOfCase.SelectedValue);
                        string test = txtCaseOpenDate.Text;
                        caseMaster.CaseOpenDate = DateTime.Parse(txtCaseOpenDate.Text, provider, DateTimeStyles.AdjustToUniversal);
                        caseMaster.CaseDescription = txtCaseDescription.Text;
                        string clamount = txtClaimAmount.Text;
                        caseMaster.ClaimAmount = txtClaimAmount.Text;
                        caseMaster.IsPlentif = Convert.ToInt32(rbIsPlantiff.Text);

                        caseMaster.CourtId = Convert.ToInt32(ddlCourt.SelectedValue);
                        caseMaster.LocationId = Convert.ToInt32(ddlLocation.SelectedValue);
                        caseMaster.AssignAttornerId = Convert.ToInt32(ddlAttorney.SelectedValue);
                        counselor.CaseNumber = caseMaster.CaseNumber;



                        caseMaster.CreatedUserId = Convert.ToInt32(Session["User_Id"]);
                        caseMaster.CreatedDate = DateTime.Now;
                        caseMaster.CaseStatusId = 1;
                        caseMaster.CreatedUserId = Convert.ToInt32(Session["User_Id"]);
                        caseMaster.CreatedDate = DateTime.Now;
                        caseMaster.CaseStatusId = 1;

                        caseMasterController.Update(caseMaster);
                        List<Counselor> test1 = counselorController.GetCounselorList(caseMaster.CaseNumber);


                        if (test1.Any())
                        {
                            counselorController.DeletePermenent(caseMaster.CaseNumber);
                        }

                        counselorList.Clear();
                        foreach (Lawyer lawyer in CounselorLawyerList)
                        {
                            Counselor counselorTemp = new Counselor();
                            counselorTemp.CaseNumber = caseMaster.CaseNumber;
                            counselorTemp.LawyerId = lawyer.LawyerId;

                            counselorList.Add(counselorTemp);

                        }
                        test1 = counselorController.GetCounselorList(caseMaster.CaseNumber);
                        if (counselorList.Any())
                        {
                            foreach (Counselor counselor in counselorList)
                            {

                                counselorController.Save(counselor);
                            }
                        }

                        PartyCase partyCase = new PartyCase();
                        if (partyCaseController.GetPartyCaseList(caseMaster.CaseNumber).Any())
                        {
                            partyCaseController.DeletePermenent(caseMaster.CaseNumber);
                        }

                        foreach (Party party in plaintif)
                        {
                            if (partyList.Where(x => x.PartyName == party.PartyName).Any())
                            {
                                partyCase.PartyId = partyList.Where(x => x.PartyName == party.PartyName).ElementAt(0).PartyId;
                            }
                            else
                            {
                                partyCase.PartyId = partyController.Save(party);
                            }

                            partyCase.CaseNumber = caseMaster.CaseNumber;
                            partyCase.IsPlaintif = 1;
                            partyCaseController.Save(partyCase);
                        }


                        foreach (Party party in defendent)
                        {
                            if (partyList.Where(x => x.PartyName == party.PartyName).Any())
                            {
                                partyCase.PartyId = partyList.Where(x => x.PartyName == party.PartyName).ElementAt(0).PartyId;
                            }
                            else
                            {
                                partyCase.PartyId = partyController.Save(party);
                            }
                            partyCase.CaseNumber = caseMaster.CaseNumber;
                            partyCase.IsPlaintif = 0;
                            partyCaseController.Save(partyCase);
                        }


                        UploadFiles();
                        Clear();
                        lblSuccessMsg.Text = "Record Updated Successfully!";
                    }
                }

                Clear();
                clearCounselor();
                clearDefendent();
                clearPlaintif();
                flag = 1;
                lblSuccessMsg.Text = "Record Updated Successfully!";
            }
            else
            {

            }
            if (!counselorList.Any() && flag == 0)
            {
                dCounselor.Visible = true;
                lblCounselor.Text = "Please Add Counselor";
            }
            if ((!plaintif.Any() && rbIsPlantiff.SelectedValue == "1") && flag == 0)
            {
                dPlaintif.Visible = true;
                lblPlaintif.Text = "Please Add Plaintif Side";
            }
            if ((!defendent.Any() && rbIsPlantiff.SelectedValue == "0") && flag == 0)
            {
                dDefendent.Visible = true;
                lblDefendent.Text = "Please Add Defendent Side";
            }
            if (CounselorLawyerList.Where(x => x.LawyerName == ddlAttorney.SelectedItem.Text).Any())
            {
                dAttorney.Visible = true;
                lblAttorney.Text = "Cannot assign the Attorney as a Counselor!";
            }
        }



        protected void btnDocUpload_Click1(object sender, EventArgs e)
        {
            Response.Redirect("UploadDocument.aspx");
        }


        protected void UploadFiles()
        {
            IDocumentController documentController = ControllerFactory.CreateDocumentController();
            IDocumentCaseController documentCaseController = ControllerFactory.CreateDocumentCaseController();

            Document document = new Document();
            DocumentCase documentCase = new DocumentCase();

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
                document.DocumentType = "case";
                item.DocumentId = documentController.Save(document);

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

                        uploadFile.SaveAs(Server.MapPath("~/SystemDocuments/CaseMaster/") + uploadFile.FileName);

                        DocumentCase document = new DocumentCase
                        {
                            DocumentName = uploadFile.FileName,
                            CaseNumber = txtCaseNumber.Text,
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

            DocumentCase documentCase = UplodedFilesList[rowIndex];
            string path = Server.MapPath("~/SystemDocuments/CaseMaster/");
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



        protected void uploadData(object sender, EventArgs e)
        {

        }



        protected void ddlCourt_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDCourtLocationList();
        }


        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCompanyUnitList();
        }

        private void Clear()
        {

            txtCaseDescription.Text = string.Empty;
            txtCaseNumber.Text = string.Empty;
            txtClaimAmount.Text = string.Empty;
            txtPreCaseNumber.Text = string.Empty;
            ddlCompany.SelectedIndex = 0;
            ddlNatureOfCase.SelectedIndex = 0;
            ddlCourt.SelectedIndex = 0;
            ddlAttorney.SelectedIndex = 0;
            ddlCounselor.SelectedIndex = 0;
            ddlCompanyUnit.Items.Clear();
            ddlLocation.Items.Clear();
            txtCaseOpenDate.Text = string.Empty;
            rbIsPlantiff.Items[0].Selected = false;
            rbIsPlantiff.Items[1].Selected = false;
            rbPrevCase.Items[0].Selected = false;
            rbPrevCase.Items[1].Selected = false;
            lblCaseNumberError.Text = string.Empty;
            lblPrevCaseNumberError.Text = string.Empty;
            lblCounselor.Text = string.Empty;
            lblDefendent.Text = string.Empty;
            lblPlaintif.Text = string.Empty;
            txtPlaintif.Text = string.Empty;
            txtDefendent.Text = string.Empty;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private bool CheckAvailableCaseNum(bool isPrev, string Number, ICaseMasterController c)
        {
            CaseMaster caseMaster = c.GetCaseMaster(Number, false);



            if (caseMaster.CaseNumber == null)
            {
                if (isPrev)
                {
                    lblSuccessMsg.Text = string.Empty;
                    lblCaseNumberError.Text = string.Empty;
                    lblPrevCaseNumberError.Text = "Not a Valid Case Number";
                    return false;
                }
                else
                {
                    lblCaseNumberError.Text = string.Empty;
                    lblSuccessMsg.Text = string.Empty;
                    lblPrevCaseNumberError.Text = string.Empty;
                    return true;
                }
            }
            else
            {
                if (isPrev)
                {
                    lblPrevCaseNumberError.Text = string.Empty;
                    lblSuccessMsg.Text = string.Empty;
                    lblCaseNumberError.Text = string.Empty;
                    return true;
                }
                else
                {
                    lblCaseNumberError.Text = "Case Number Already Exsists!";
                    lblPrevCaseNumberError.Text = string.Empty;
                    lblSuccessMsg.Text = string.Empty;
                    return false;
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ILawyerController lawyerController = ControllerFactory.CreateLawyerController();
            Lawyer lawyer = new Lawyer();

            lawyer.LawyerName = ddlCounselor.SelectedItem.Text;
            string lawyerName = ddlCounselor.SelectedItem.Text;
            if (CounselorLawyerList.Where(x => x.LawyerName == lawyerName).Any())
            {
                dCounselor.Visible = true;
                lblCounselor.Text = "Counselor already exists in list!";
            }
            if (ddlCounselor.SelectedIndex != 0)
            {
                lawyer.LawyerId = Convert.ToInt32(ddlCounselor.SelectedValue);

                if (!(CounselorLawyerList.Where(x => x.LawyerName == lawyerName).Any()) && lawyerName != ddlAttorney.SelectedItem.Text)
                {
                    CounselorLawyerList.Add(lawyer);
                    BindCounselorList();
                    dCounselor.Visible = false;


                }
                if (lawyerName == ddlAttorney.SelectedItem.Text)
                {
                    dCounselor.Visible = true;
                    lblCounselor.Text = "Cannot assign the Attorney as a Counselor!";
                }

            }


        }

        protected void btnAddPlaintif_Click(object sender, EventArgs e)
        {
            Party party = new Party();
            party.PartyName = txtPlaintif.Text;
            if (plaintif.Where(x => x.PartyName == party.PartyName).Any())
            {
                dPlaintifError.Visible = true;
                lblPlaintifError.Text = "Party already exists in Plaintiff Side";
            }
            if (!(plaintif.Where(x => x.PartyName == party.PartyName).Any()) && party.PartyName != "" && !(defendent.Where(x => x.PartyName == party.PartyName).Any()))
            {
                plaintif.Add(party);
                BindPlaintifList();


                dPlaintifError.Visible = false;
                txtPlaintif.Text = string.Empty;
            }
            if (defendent.Where(x => x.PartyName == party.PartyName).Any())
            {
                dPlaintifError.Visible = true;
                lblPlaintifError.Text = "Party already exists in Defendent Side";
            }

        }

        protected void btnAddDefendent_Click(object sender, EventArgs e)
        {
            Party party = new Party();
            party.PartyName = txtDefendent.Text;
            if (defendent.Where(x => x.PartyName == party.PartyName).Any())
            {
                dDefendentError.Visible = true;
                lblDefendentError.Text = "Party already exists in Defendent Side";
            }
            if (!(defendent.Where(x => x.PartyName == party.PartyName).Any()) && party.PartyName != "" && !(plaintif.Where(x => x.PartyName == party.PartyName).Any()))
            {
                defendent.Add(party);
                BindDefendentList();

                dDefendentError.Visible = false;
                txtDefendent.Text = string.Empty;
            }
            if (plaintif.Where(x => x.PartyName == party.PartyName).Any())
            {
                dDefendentError.Visible = true;
                lblDefendentError.Text = "Party already exists in Plaintiff Side";
            }

        }




        protected void GridView2_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            gvCounselor.PageIndex = e.NewPageIndex;
            gvCounselor.DataSource = CounselorLawyerList;
            gvCounselor.DataBind();
        }

        private void BindCounselorList()
        {
            gvCounselor.DataSource = CounselorLawyerList;
            gvCounselor.DataBind();

        }

        private void BindPlaintifList()
        {
            gvPlaintif.DataSource = plaintif;
            gvPlaintif.DataBind();
        }

        private void BindDefendentList()
        {
            gvDefendent.DataSource = defendent;
            gvDefendent.DataBind();
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = gvCounselor.PageSize;
            int pageIndex = gvCounselor.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;
            CounselorLawyerList.RemoveAll(x => x.LawyerName == CounselorLawyerList[rowIndex].LawyerName);


            BindCounselorList();

        }



        protected void btndelete_ClickPlaintif(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = gvPlaintif.PageSize;
            int pageIndex = gvPlaintif.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            plaintif.RemoveAll(x => x.PartyName == plaintif[rowIndex].PartyName);
            BindPlaintifList();

        }

        protected void btnEdit_ClickPlaintiff(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = gvPlaintif.PageSize;
            int pageIndex = gvPlaintif.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;
            txtPlaintif.Text = plaintif[rowIndex].PartyName;
            plaintif.RemoveAll(x => x.PartyName == plaintif[rowIndex].PartyName);
            BindPlaintifList();
        }

        protected void btndelete_ClickDefendent(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = gvDefendent.PageSize;
            int pageIndex = gvDefendent.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            defendent.RemoveAll(x => x.PartyName == defendent[rowIndex].PartyName);
            BindDefendentList();

        }

        protected void btnEdit_ClickDefendent(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = gvDefendent.PageSize;
            int pageIndex = gvDefendent.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;
            txtDefendent.Text = defendent[rowIndex].PartyName;
            defendent.RemoveAll(x => x.PartyName == defendent[rowIndex].PartyName);
            BindDefendentList();

        }

        private void clearCounselor()
        {
            CounselorLawyerList.Clear();
            BindCounselorList();
        }
        private void clearPlaintif()
        {
            plaintif.Clear();
            BindPlaintifList();
        }

        private void clearDefendent()
        {
            defendent.Clear();
            BindDefendentList();
        }



        public static string ConvertNumbertoWords(long number)
        {
            var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            if (number == 0)
                return "Zero";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000000) + " Billion ";
                number %= 1000000000;
            }
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000) + " Million ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " Thousand ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {

                //if (words != "")
                //    words += "AND ";
                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            //if (number > 0)
            //{
            //    if (words != "")
            //        words += "AND ";
            //    if (number < 20)
            //        words += unitsMap[number];
            //    else
            //    {
            //        words += tensMap[number / 10];
            //        if ((number % 10) > 0)
            //            words += " " + unitsMap[number % 10];
            //    }
            //    words += " Cents";
            //}
            return words;
        }
        protected void claimAmountInWords(object sender, EventArgs e)
        {
            if ((txtClaimAmount.Text.All(x => ".0123456789".Contains(x)) || txtClaimAmount.Text.All(x => "0123456789".Contains(x))) && ((txtClaimAmount.Text.Count(x => x == '.') == 1) || (txtClaimAmount.Text.Count(x => x == '.') == 0)))
            {
                lblClaimAmountInWords.Text = "Claim Amount In Words : ";
                string number = txtClaimAmount.Text.Split('.')[0];


                //long claimAmount = long.Parse(txtClaimAmount.Text);

                lblClaimAmountInWords.Text += ConvertNumbertoWords(long.Parse(number));
                if (txtClaimAmount.Text.Contains('.'))
                {
                    string decimalNumber = txtClaimAmount.Text.Split('.')[1].Substring(0, 2);
                    lblClaimAmountInWords.Text += " And " + ConvertNumbertoWords(long.Parse(decimalNumber)) + " Cents";
                }
            }
            else
            {
                lblClaimAmountInWords.Text = "Enter Valid Amount";
            }
        }
        protected void rbPrevCaseChanged(object sender, EventArgs e)
        {
            if (rbPrevCase.SelectedValue == "1")
            {
                dropDownOption.Visible = true;
                textOption.Visible = false;
            }
            else
            {
                dropDownOption.Visible = false;
                textOption.Visible = true;
            }
        }
    }
}
