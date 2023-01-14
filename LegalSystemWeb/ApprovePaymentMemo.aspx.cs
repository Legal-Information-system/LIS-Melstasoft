using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class ApprovePaymentMemo : System.Web.UI.Page
    {
        int paymentId;
        string paymentIdS;
        int userId;
        string roleId;
        Payment payment = new Payment();
        IPaymentController paymentController = ControllerFactory.CreatePaymentController();
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
                    userId = (int)Session["User_Id"];
                    roleId = Session["User_Role_Id"].ToString();
                    paymentId = Convert.ToInt32(Request.QueryString["PaymentId"]);
                    BindData();
                    BindDocumentList(payment.PaymentId);
                }

            }


        }

        private void BindData()
        {

            ILawyerController lawyerController = ControllerFactory.CreateLawyerController();
            IPaymentStatusController paymentStatusController = ControllerFactory.CreatePaymentStatusController();

            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
            CaseMaster caseMaster = new CaseMaster();


            payment = paymentController.GetPayment(paymentId);

            if (payment.PaymentId == 0)
            {
                Response.Redirect("404.aspx");
            }
            else
            {
                IUserLoginController userLoginController = ControllerFactory.CreateUserLoginController();
                lblPaymentId.Text = payment.PaymentId.ToString();
                lblCreatedDate.Text = payment.CreatedDate.ToString();
                lblCaseNumber.Text = payment.CaseNumber.ToString();
                lblCreatedBy.Text = userLoginController.GetUserLoginById(payment.CreateUserId.ToString()).UserName;
                payment.lawyer = lawyerController.GetLawyer(payment.LawyerId);
                lblLawyerName.Text = payment.lawyer.LawyerName.ToString();
                payment.paymentStatus = paymentStatusController.GetPaymentStatus(payment.PaymentStatusId);
                lblPaymentStatus.Text = payment.paymentStatus.StatusName;
                lblRemarks.Text = payment.Remarks.ToString();
                if (lblRemarks.Text == string.Empty || lblRemarks.Text == "")
                {
                    lblRemarks.Text = "N/A";
                }
                lblRequestedPaymentAmount.Text = payment.Amount.ToString();

                caseMaster = caseMasterController.GetCaseMasterWithPaid(payment.CaseNumber);
                payment.caseMaster = caseMaster;


                int UserRoleId = Convert.ToInt32(Session["User_Role_Id"]);
                int companyId = Convert.ToInt32(Session["company_id"].ToString());
                int companyUnitId = Convert.ToInt32(Session["company_unit_id"].ToString());

                if (UserRoleId == 4)
                {
                    if (payment.caseMaster.CompanyId != companyId)
                    {
                        Response.Redirect("404.aspx");
                    }
                }

                if (UserRoleId == 5)
                {
                    if (payment.caseMaster.CompanyUnitId != companyUnitId)
                    {
                        Response.Redirect("404.aspx");
                    }
                }
            }


        }

        private void BindDocumentList(int paymentId)
        {
            IDocumentPaymentController documentPaymentController = ControllerFactory.CreateDocumentPaymentController();
            IDocumentController documentController = ControllerFactory.CreateDocumentController();

            List<DocumentPayment> listDocumentPayment = documentPaymentController.GetDocumentList();

            gvDocuments.DataSource = listDocumentPayment.Where(x => x.PaymentId == paymentId).ToList();
            gvDocuments.DataBind();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            payment.PaymentId = Convert.ToInt32(Request.QueryString["PaymentId"]);
            payment.ActionRemarks = txtRemarks.Text;
            payment.ActionTakenUserId = Convert.ToInt32(Session["User_Id"].ToString());
            payment.ActionTakenDate = DateTime.Now;
            payment.PaymentStatusId = 2;
            paymentController.UpdatePaymentStatus(payment);

            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
            CaseMaster caseMaster = new CaseMaster();
            caseMaster.CaseNumber = lblCaseNumber.Text;
            caseMaster.payableAmount = Convert.ToDouble(lblRequestedPaymentAmount.Text);
            caseMasterController.UpdateCasePaidAmount(caseMaster);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Payment Approve Succesfully!', 'success');window.setTimeout(function(){window.location='ApprovePaymentMemo.aspx?PaymentId=" + Request.QueryString["PaymentId"] + "'},2500);", true);

        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            payment.PaymentId = Convert.ToInt32(Request.QueryString["PaymentId"]);
            payment.ActionRemarks = txtRemarks.Text;
            payment.ActionTakenUserId = Convert.ToInt32(Session["User_Id"].ToString());
            payment.ActionTakenDate = DateTime.Now;
            payment.PaymentStatusId = 3;
            paymentController.UpdatePaymentStatus(payment);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Payment Reject Succesfully!', 'success');window.setTimeout(function(){window.location='ApprovePaymentMemo.aspx?PaymentId=" + Request.QueryString["PaymentId"] + "'},2500);", true);
        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            paymentId = Convert.ToInt32(Request.QueryString["PaymentId"]);
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            IDocumentPaymentController documentPaymentController = ControllerFactory.CreateDocumentPaymentController();
            List<DocumentPayment> documentPaymentList = documentPaymentController.GetDocumentList();
            documentPaymentList = documentPaymentList.Where(x => x.PaymentId == paymentId).ToList();

            string fileName = documentPaymentList[rowIndex].DocumentName;
            if (fileName != "" && fileName != null)
            {
                string filePathe = Server.MapPath("~/SystemDocuments/PaymentMemo/RequestPayments/" + fileName);

                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename = " + fileName);
                Response.TransmitFile(filePathe);
                Response.End();
            }


        }
    }
}