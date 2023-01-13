using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.IO;
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
        UserPrivilege activity = new UserPrivilege();
        List<UserPrivilege> listActivity = new List<UserPrivilege>();
        ILawyerController LawyerController = ControllerFactory.CreateLawyerController();
        ICounselorController counselorController = ControllerFactory.CreateCounselorController();
        ICaseMasterController masterController = ControllerFactory.CreateCaseMasterController();
        public static List<string> filePaths = new List<string>();
        public static List<ListItem> files = new List<ListItem>();
        public static List<HttpPostedFileBase> listUplodedFile = new List<HttpPostedFileBase>();
        public static List<DocumentPayment> UplodedFilesList = new List<DocumentPayment>();
        public static int documentIncrement = 0;
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
                    if (Session["User_Role_Id"].ToString() == "3")
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
            List<Counselor> counselors = new List<Counselor>();
            List<Lawyer> counselorLawyer = new List<Lawyer>();
            counselors = counselorController.GetCounselorList(ddlCaseNo.SelectedValue);
            foreach (Counselor counselor in counselors)
            {
                counselorLawyer.Add(LawyerController.GetLawyer(counselor.LawyerId));
            }

            counselorLawyer.Add(LawyerController.GetLawyer(masterController.GetCaseMaster(ddlCaseNo.SelectedValue, false).AssignAttornerId));

            ddlLawyerName.DataSource = counselorLawyer;
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

        protected void btnCaseNo_IndexChange(object sender, EventArgs e)
        {
            BindLawyerList();
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

            cblActivity.Items.Clear();
            BindActivityList();
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
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Payment Created Succesfully!', 'success')", true);
            }
            else
            {
                lblCheckRequired.Text = "*";
            }

        }

        protected void UploadFiles(int paymentId)
        {
            IDocumentController documentController = ControllerFactory.CreateDocumentController();
            IDocumentPaymentController documentPaymentController = ControllerFactory.CreateDocumentPaymentController();

            Document document = new Document();
            DocumentPayment documentPayment = new DocumentPayment();

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
                item.PaymentId = paymentId;
                documentPaymentController.Save(item);
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

                        uploadFile.SaveAs(Server.MapPath("~/SystemDocuments/PaymentMemo/RequestPayments/") + uploadFile.FileName);

                        DocumentPayment document = new DocumentPayment
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




        protected void DeleteFiles(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = fileGridview.PageSize;
            int pageIndex = fileGridview.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;
            //FileInfo file = new FileInfo(filePaths[rowIndex]);

            //filePaths.RemoveAt(rowIndex);
            //listUplodedFile.RemoveAt(rowIndex);

            DocumentPayment documentPayment = UplodedFilesList[rowIndex];
            string path = Server.MapPath("~/SystemDocuments/PaymentMemo/RequestPayments/");
            string filePath = path + documentPayment.DocumentName;

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

    }
}