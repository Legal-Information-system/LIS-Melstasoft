using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class ViewCaseActivity : System.Web.UI.Page
    {
        CaseActivity CaseActivity = new CaseActivity();
        UserLogin userLogin = new UserLogin();
        DocumentCaseActivity documentCaseActivity = new DocumentCaseActivity();
        string caseNumber;
        int UserId, companyId;
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        IUserPrivilegeController userPrivilegeController = ControllerFactory.CreateUserPrivilegeController();
        ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
        ICaseActivityController caseActivityController = ControllerFactory.CreateCaseActivityController();
        IUserLoginController userLoginController = ControllerFactory.CreateUserLoginController();
        protected List<UserRolePrivilege> rolePrivileges = new List<UserRolePrivilege>();
        protected List<UserPrivilege> userPrivileges = new List<UserPrivilege>();
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
                    rolePrivileges = userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString());
                    userPrivileges = userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"]));
                    if (!((rolePrivileges.Where(x => x.FunctionId == 34).Any()
                    && !(userPrivileges.Any(x => x.FunctionId == 34 && x.IsGrantRevoke == 0))) ||
                    userPrivileges.Any(x => x.FunctionId == 34 && x.IsGrantRevoke == 1)))
                        Response.Redirect("404.aspx");
                    else
                    {
                        SetCaseMasterData();
                        BindDocumentList(caseNumber);
                    }
                }
                //}
            }
        }

        private void SetCaseMasterData()
        {
            try
            {
                CaseActivity = caseActivityController.GetCaseActivity(Request.QueryString["CaseActivityNumber"].ToString(), true);
                List<UserRolePrivilege> userRolePrivileges = userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString());

                UserId = Convert.ToInt32(Session["User_Role_Id"]);
                companyId = Convert.ToInt32(Session["company_id"].ToString());
                int companyUnitId = Convert.ToInt32(Session["company_unit_id"].ToString());


                lblCaseNumber.Text = CaseActivity.CaseNumber;
                lblActivityId.Text = CaseActivity.CaseActivitId.ToString();

                lblAssignedAttorney.Text = CaseActivity.assignAttorney.LawyerName;
                lblCreatedDate.Text = CaseActivity.ActivityDate.ToString("dd/MM/yyyy");
                lblCounsellor.Text = CaseActivity.counsilor.LawyerName;
                lblOppositeSideLawyer.Text = CaseActivity.OtherSideLawyer;
                lblJudgeName.Text = CaseActivity.JudgeName;

                lblCompanyReperesentative.Text = CaseActivity.CompanyRep;
                lblActionTaken.Text = CaseActivity.actionTaken.ActionName;
                lblNextDate.Text = CaseActivity.NextDate.ToString("dd/MM/yyyy");

                lblRemarks.Text = CaseActivity.Remarks;
                if (lblRemarks.Text == "")
                {
                    lblRemarks.Text = "N/A";
                }
                lblNextAction.Text = CaseActivity.nextAction.ActionName;
                userLogin.UserId = CaseActivity.CreateUserId;

                lblCreatedBy.Text = userLoginController.GetUserLogin(CaseActivity.CreateUserId.ToString()).UserName;


            }
            catch (Exception)
            {
                Response.Redirect("500.aspx");
            }

        }



        private void BindDocumentList(string casenumber)
        {
            IDocumentCaseActivityController documentController = ControllerFactory.CreateDocumentCaseActivityController();
            documentCaseActivity.CaseActivityId = Request.QueryString["CaseActivityNumber"].ToString();
            List<DocumentCaseActivity> documentList = documentController.GetDocumentList(documentCaseActivity, false);


            gvDocuments.DataSource = documentList;
            gvDocuments.DataBind();
        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            caseNumber = Request.QueryString["CaseActivityNumber"].ToString();
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            IDocumentCaseActivityController documentController = ControllerFactory.CreateDocumentCaseActivityController();
            documentCaseActivity.CaseActivityId = caseNumber;
            List<DocumentCaseActivity> documentList = documentController.GetDocumentList(documentCaseActivity, false);


            string fileName = documentList[rowIndex].DocumentName;
            if (fileName != "" && fileName != null)
            {
                string filePathe = Server.MapPath("~/SystemDocuments/CaseMaster/CaseActivity/" + fileName);

                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename = " + fileName);
                Response.TransmitFile(filePathe);
                Response.End();
            }


        }
    }
}
