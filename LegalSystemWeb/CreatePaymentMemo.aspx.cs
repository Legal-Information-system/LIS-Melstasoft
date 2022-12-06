using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class CreatePaymentMemo : System.Web.UI.Page
    {
        Activity activity = new Activity();
        List<Activity> listActivity = new List<Activity>();
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
                    if (Session["User_Role_Id"].ToString() == "3" || Session["User_Role_Id"].ToString() == "2")
                    {
                        Response.Redirect("404.aspx");
                    }
                    else
                    {
                        BindActivityList();
                        BindLawyerList();
                        BindCaseList();
                    }
                }
            }
        }

        private void BindActivityList()
        {
            IActivityController activityController = ControllerFactory.CreateActivityController();
            cblActivity.DataSource = activityController.GetActivityList(false);
            cblActivity.DataValueField = "ActivityId";
            cblActivity.DataTextField = "ActivityName";
            cblActivity.DataBind();
        }

        private void BindLawyerList()
        {
            ILawyerController LawyerController = ControllerFactory.CreateLawyerController();
            ddlLawyerName.DataSource = LawyerController.GetLawyerList(false);
            ddlLawyerName.DataValueField = "LawyerId";
            ddlLawyerName.DataTextField = "LawyerName";
            ddlLawyerName.DataBind();
            ddlLawyerName.Items.Insert(0, new ListItem("-- select lawyer --", ""));

        }

        private void BindCaseList()
        {
            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
            List<CaseMaster> caseMasterList = caseMasterController.GetCaseMasterList(true);

            int companyUnitId = Convert.ToInt32(Session["company_unit_id"].ToString());
            int companyId = Convert.ToInt32(Session["company_id"].ToString());
            int UserRoleId = Convert.ToInt32(Session["User_Role_Id"].ToString());

            if (UserRoleId == 4)
                caseMasterList = caseMasterList.Where(c => c.CompanyId == companyId).ToList();
            if (UserRoleId == 5)
                caseMasterList = caseMasterList.Where(c => c.CompanyUnitId == companyUnitId).ToList();

            ddlCaseNo.DataSource = caseMasterList;
            ddlCaseNo.DataValueField = "CaseNumber";
            ddlCaseNo.DataTextField = "CaseNumber";
            ddlCaseNo.DataBind();

            ddlCaseNo.Items.Insert(0, new ListItem("-- select case --", ""));

        }



        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtTotalPayableAmount.Text = string.Empty;
            ddlCaseNo.ClearSelection();
            ddlLawyerName.ClearSelection();
            txtRemarks.Text = string.Empty;

            //for (int i = 0; i < cblActivity.Items.Count; i++)
            //{
            //    cblActivity.Items[i].Value; 
            //}
        }





        protected void btnSave_Click(object sender, EventArgs e)
        {
            IPaymentController paymentController = ControllerFactory.CreatePaymentController();
            IPaymentActivityController paymentActivityController = ControllerFactory.CreatePaymentActivityController();

            Payment payment = new Payment();

            PaymentActivity paymentActivity = new PaymentActivity();

            payment.CaseNumber = Convert.ToString(ddlCaseNo.SelectedValue);
            payment.LawyerId = Convert.ToInt32(ddlLawyerName.SelectedValue);
            payment.Amount = Convert.ToInt32(txtTotalPayableAmount.Text);
            payment.CreatedDate = System.DateTime.Now;
            payment.CreateUserId = Convert.ToInt32(Session["User_Id"]);
            payment.Remarks = txtRemarks.Text;
            payment.PaymentStatusId = 1;

            int count = 0;

            for (int i = 0; i < cblActivity.Items.Count; i++)
            {
                if (cblActivity.Items[i].Selected) { count++; }
            }

            if (count > 0)
            {
                payment.PaymentId = paymentController.Save(payment);

                for (int i = 0; i < cblActivity.Items.Count; i++)
                {
                    if (cblActivity.Items[i].Selected)
                    {
                        paymentActivity.ActivityId = Convert.ToInt32(cblActivity.Items[i].Value);
                        paymentActivity.PaymentId = payment.PaymentId;
                        paymentActivityController.Save(paymentActivity);
                    }
                }

                UploadFiles(payment.PaymentId);
                lblCheckRequired.Text = "";
                Clear();
                lblSuccessMsg.Text = "Record Updated Successfully!";
            }
            else
            {
                lblCheckRequired.Text = "*";
            }

        }

        private void UploadFiles(int paymentId)
        {
            IDocumentController documentController = ControllerFactory.CreateDocumentController();

            IDocumentPaymentController documentPaymentController = ControllerFactory.CreateDocumentPaymentController();

            Document document = new Document();
            DocumentPayment documentPayment = new DocumentPayment();


            if (Uploader.HasFile)
            {
                HttpFileCollection uploadFiles = Request.Files;
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    HttpPostedFile uploadFile = uploadFiles[i];
                    if (uploadFile.ContentLength > 0)
                    {
                        uploadFile.SaveAs(Server.MapPath("~/SystemDocuments/PaymentMemo/RequestPayments/") + uploadFile.FileName);
                        //lblListOfUploadedFiles.Text += String.Format("{0}<br />", uploadFile.FileName);

                        document.DocumentType = "payment";
                        documentPayment.DocumentId = documentController.Save(document);

                        documentPayment.DocumentName = uploadFile.FileName;
                        documentPayment.PaymentId = paymentId;
                        documentPayment.DocumentDescription = "";
                        documentPaymentController.Save(documentPayment);
                    }
                }
            }
        }
    }
}