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

namespace LegalSystemWeb
{
    public partial class CreateCase : System.Web.UI.Page
    {

        int UserId, UserRoleId;
        string UserName;
        List<CourtLocation> courtlocation = new List<CourtLocation>();
        List<Court> courtList = new List<Court>();
        List<Lawyer> lawyerList = new List<Lawyer>();
        public static List<Lawyer> CounselorList = new List<Lawyer>();
        ICounselorController counselorController = ControllerFactory.CreateCounselorController();
        IPartyController partyControllerGlobal = ControllerFactory.CreatePartyController();
        public static List<Party> partyList = new List<Party>();
        Counselor counselor = new Counselor();
        public static List<Party> plaintif = new List<Party>();
        public static List<Party> defendent = new List<Party>();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (Session["User_Role_Id"].ToString() == "3" || Session["User_Role_Id"].ToString() == "2")
                    Response.Redirect("404.aspx");
                else
                {
                    if (!IsPostBack)
                    {
                        BindCompanyList();
                        BindCaseNatureList();
                        BindCourtList();
                        BindLawyerList();
                        partyList = partyControllerGlobal.GetPartyList();
                    }
                }
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


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CounselorList.Any() && ((plaintif.Any() && rbIsPlantiff.SelectedValue == "0") || (defendent.Any() && rbIsPlantiff.SelectedValue == "1")))
            {
                ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
                IPartyController partyController = ControllerFactory.CreatePartyController();
                IPartyCaseController partyCaseController = ControllerFactory.CreatePartyCaseController();

                CaseMaster caseMaster = new CaseMaster();

                CultureInfo provider = new CultureInfo("en-US");

                caseMaster.CaseNumber = txtCaseNumber.Text;
                caseMaster.PrevCaseNumber = txtPreCaseNumber.Text;
                caseMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                caseMaster.CompanyUnitId = Convert.ToInt32(ddlCompanyUnit.SelectedValue);
                caseMaster.CaseNatureId = Convert.ToInt32(ddlNatureOfCase.SelectedValue);
                caseMaster.CaseOpenDate = DateTime.Parse(txtCaseOpenDate.Text, provider, DateTimeStyles.AdjustToUniversal);
                caseMaster.CaseDescription = txtCaseDescription.Text;
                string clamount = txtClaimAmount.Text;
                caseMaster.ClaimAmount = Convert.ToDouble(txtClaimAmount.Text);
                caseMaster.IsPlentif = Convert.ToInt32(rbIsPlantiff.Text);

                caseMaster.CourtId = Convert.ToInt32(ddlCourt.SelectedValue);
                caseMaster.LocationId = Convert.ToInt32(ddlLocation.SelectedValue);
                caseMaster.AssignAttornerId = Convert.ToInt32(ddlAttorney.SelectedValue);
                counselor.CaseNumber = caseMaster.CaseNumber;

                caseMaster.CreatedUserId = Convert.ToInt32(Session["User_Id"]);
                caseMaster.CreatedDate = DateTime.Now;
                caseMaster.CaseStatusId = 1;

                caseMasterController.Save(caseMaster);
                foreach (Lawyer lawyer in CounselorList)
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
                Clear();
                clearCounselor();
                clearDefendent();
                clearPlaintif();
                lblSuccessMsg.Text = "Record Updated Successfully!";
            }
            else
            {

                if (!CounselorList.Any())
                {
                    lblCounselor.Text = "Please Add Counselor";
                }
                if (!(plaintif.Any() && rbIsPlantiff.SelectedValue == "0"))
                {

                    lblPlaintif.Text = "Please Add Plaintif Side";
                }
                if (!(defendent.Any() && rbIsPlantiff.SelectedValue == "1"))
                {
                    lblDefendent.Text = "Please Add Defendent Side";
                }
            }
        }


        protected void btnDocUpload_Click1(object sender, EventArgs e)
        {
            Response.Redirect("UploadDocument.aspx");
        }


        private void UploadFiles()
        {
            IDocumentController documentController = ControllerFactory.CreateDocumentController();
            IDocumentCaseController documentCaseController = ControllerFactory.CreateDocumentCaseController();

            Document document = new Document();
            DocumentCase documentCase = new DocumentCase();

            if (Uploader.HasFile)
            {
                HttpFileCollection uploadFiles = Request.Files;
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    HttpPostedFile uploadFile = uploadFiles[i];
                    if (uploadFile.ContentLength > 0)
                    {
                        uploadFile.SaveAs(Server.MapPath("~/SystemDocuments/CaseMaster/") + uploadFile.FileName);
                        //lblListOfUploadedFiles.Text += String.Format("{0}<br />", uploadFile.FileName);

                        document.DocumentType = "case";
                        documentCase.DocumentId = documentController.Save(document);

                        documentCase.DocumentName = uploadFile.FileName;
                        documentCase.CaseNumber = txtCaseNumber.Text;
                        documentCase.DocumentDescription = "";
                        documentCaseController.Save(documentCase);
                    }
                }
            }
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
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ILawyerController lawyerController = ControllerFactory.CreateLawyerController();
            Lawyer lawyer = new Lawyer();

            lawyer.LawyerName = ddlCounselor.SelectedItem.Text;
            if (ddlCounselor.SelectedIndex != 0)
            {
                lawyer.LawyerId = Convert.ToInt32(ddlCounselor.SelectedValue);
                string lawyerName = ddlCounselor.SelectedItem.Text;
                if (!(CounselorList.Where(x => x.LawyerName == lawyerName).Any()))
                {
                    CounselorList.Add(lawyer);
                    BindCounselorList();
                    if (lblCounselor.Text != "")
                    {
                        lblCounselor.Text = "";
                    }
                }
            }


        }

        protected void btnAddPlaintif_Click(object sender, EventArgs e)
        {
            Party party = new Party();
            party.PartyName = txtPlaintif.Text;
            if (!(plaintif.Where(x => x.PartyName == party.PartyName).Any()) && party.PartyName != "" && !(defendent.Where(x => x.PartyName == party.PartyName).Any()))
            {
                plaintif.Add(party);
                BindPlaintifList();
                if (lblPlaintif.Text != "")
                {
                    lblPlaintif.Text = "";
                }
            }
        }

        protected void btnAddDefendent_Click(object sender, EventArgs e)
        {
            Party party = new Party();
            party.PartyName = txtDefendent.Text;
            if (!(defendent.Where(x => x.PartyName == party.PartyName).Any()) && party.PartyName != "" && !(plaintif.Where(x => x.PartyName == party.PartyName).Any()))
            {
                defendent.Add(party);
                BindDefendentList();
                if (lblDefendent.Text != "")
                {
                    lblDefendent.Text = "";
                }
            }
        }

        protected void GridView2_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            gvCounselor.PageIndex = e.NewPageIndex;
            gvCounselor.DataSource = CounselorList;
            gvCounselor.DataBind();
        }

        private void BindCounselorList()
        {
            gvCounselor.DataSource = CounselorList;
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

            CounselorList.RemoveAll(x => x.LawyerName == CounselorList[rowIndex].LawyerName);
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

        protected void btndelete_ClickDefendent(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = gvDefendent.PageSize;
            int pageIndex = gvDefendent.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            defendent.RemoveAll(x => x.PartyName == defendent[rowIndex].PartyName);
            BindDefendentList();

        }

        private void clearCounselor()
        {
            CounselorList.Clear();
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
    }
}