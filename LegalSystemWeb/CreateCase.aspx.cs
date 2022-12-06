using LegalSystemCore;
using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
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
            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
            CaseMaster caseMaster = new CaseMaster();

            caseMaster.CaseNumber = txtCaseNumber.Text;
            caseMaster.PrevCaseNumber = txtPreCaseNumber.Text;
            caseMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            caseMaster.CompanyUnitId = Convert.ToInt32(ddlCompanyUnit.SelectedValue);
            caseMaster.CaseNatureId = Convert.ToInt32(ddlNatureOfCase.SelectedValue);
            caseMaster.CaseDescription = txtCaseDescription.Text;
            string clamount = txtClaimAmount.Text;
            caseMaster.ClaimAmount = Convert.ToDouble(txtClaimAmount.Text);
            caseMaster.IsPlentif = Convert.ToInt32(rbIsPlantiff.Text);
            caseMaster.OtherParty = txtOtherside.Text;
            caseMaster.CourtId = Convert.ToInt32(ddlCourt.SelectedValue);
            caseMaster.LocationId = Convert.ToInt32(ddlLocation.SelectedValue);
            caseMaster.AssignAttornerId = Convert.ToInt32(ddlAttorney.SelectedValue);

            if (ddlCounselor.SelectedValue != "")
                caseMaster.CounsilorId = Convert.ToInt32(ddlCounselor.SelectedValue);

            caseMaster.CreatedUserId = Convert.ToInt32(Session["User_Id"]);
            caseMaster.CreatedDate = DateTime.Now;
            caseMaster.CaseStatusId = 1;

            caseMasterController.Save(caseMaster);

            UploadFiles();
            Clear();

            lblSuccessMsg.Text = "Record Updated Successfully!";
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
            txtOtherside.Text = string.Empty;
            txtPreCaseNumber.Text = string.Empty;
            ddlCompany.SelectedIndex = 0;
            ddlNatureOfCase.SelectedIndex = 0;
            ddlCourt.SelectedIndex = 0;
            ddlAttorney.SelectedIndex = 0;
            ddlCounselor.SelectedIndex = 0;
            ddlCompanyUnit.Items.Clear();
            ddlLocation.Items.Clear();
            //rbIsPlantiff.SelectedItem.Clear();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Clear();
        }


    }
}