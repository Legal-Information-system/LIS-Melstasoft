using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class ViewPaymentMemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                BindDataSource();
            }
        }

        List<Payment> listGloabalPayment = new List<Payment>();

        private void BindDataSource()
        {
            IPaymentController paymentController = ControllerFactory.CreatePaymentController();
            List<Payment> listPayment = paymentController.GetPaymentList();

            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
            CaseMaster caseMaster = new CaseMaster();

            ILawyerController lawyerController = ControllerFactory.CreateLawyerController();

            IPaymentActivityController paymentActivityController = ControllerFactory.CreatePaymentActivityController();
            List<PaymentActivity> listPaymentActivity = new List<PaymentActivity>();

            IActivityController activityController = ControllerFactory.CreateActivityController();

            IPaymentStatusController paymentStatusController = ControllerFactory.CreatePaymentStatusController();

            foreach (Payment payment in listPayment)
            {
                caseMaster = caseMasterController.GetCaseMasterWithPaid(payment.CaseNumber);
                payment.lawyer = lawyerController.GetLawyer(payment.LawyerId);
                caseMaster.payableAmount = caseMaster.ClaimAmount - caseMaster.totalPaidAmoutToPresent;
                payment.caseMaster = caseMaster;
                listPaymentActivity = paymentActivityController.GetPaymentActivityList(payment.PaymentId);
                payment.paymentStatus = paymentStatusController.GetPaymentStatus(payment.PaymentStatusId);
                foreach (PaymentActivity paymentActivity in listPaymentActivity)
                {

                    if (payment.Actions == null)
                    {
                        payment.Actions = activityController.GetActivity(paymentActivity.ActivityId).ActivityName;
                    }
                    else
                    {
                        payment.Actions = payment.Actions + " , " + activityController.GetActivity(paymentActivity.ActivityId).ActivityName;
                    }
                }

            }

            int UserRoleId = Convert.ToInt32(Session["User_Role_Id"]);
            int companyId = Convert.ToInt32(Session["company_id"].ToString());
            int companyUnitId = Convert.ToInt32(Session["company_unit_id"].ToString());

            if (UserRoleId == 4 || UserRoleId == 5)
            {
                listPayment = listPayment.Where(c => c.caseMaster.CompanyId == companyId).ToList();
            }

            if (UserRoleId == 5)
            {
                listPayment = listPayment.Where(c => c.caseMaster.CompanyUnitId == companyUnitId).ToList();
            }

            this.GridView1.DataSource = listPayment;
            listGloabalPayment = listPayment;
            this.GridView1.DataBind();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = GridView1.PageSize;
            int pageIndex = GridView1.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            Response.Redirect("ApprovePaymentMemo.aspx?PaymentId=" + listGloabalPayment[rowIndex].PaymentId.ToString());
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindDataSource();
        }
    }
}