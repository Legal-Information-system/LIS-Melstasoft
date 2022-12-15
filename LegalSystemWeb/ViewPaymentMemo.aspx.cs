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
            List<Payment> listPayment = paymentController.GetPaymentList(true);


            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
            List<CaseMaster> caseMasterList = caseMasterController.GetCaseMasterList(true);
            CaseMaster caseMaster = new CaseMaster();

            ILawyerController lawyerController = ControllerFactory.CreateLawyerController();
            List<Lawyer> lawyerList = lawyerController.GetLawyerList(true);

            IActivityController activityController = ControllerFactory.CreateActivityController();
            List<Activity> activityList = activityController.GetActivityList(true);

            IPaymentStatusController paymentStatusController = ControllerFactory.CreatePaymentStatusController();
            List<PaymentStatus> paymentStatusList = paymentStatusController.GetPaymentStatusList();

            foreach (Payment payment in listPayment)
            {
                caseMaster = caseMasterList.Where(x => x.CaseNumber == payment.CaseNumber).Single();
                payment.lawyer = lawyerList.Where(x => x.LawyerId == payment.LawyerId).Single();
                caseMaster.payableAmount = caseMaster.ClaimAmount - caseMaster.totalPaidAmoutToPresent;
                payment.caseMaster = caseMaster;

                payment.paymentStatus = paymentStatusList.Where(x => x.StatusId == payment.PaymentStatusId).Single();

                foreach (var paymentActivity in payment.listPaymentActivity)
                {

                    if (payment.Actions == null)
                    {
                        payment.Actions = paymentActivity.activity.ActivityName;
                    }
                    else
                    {
                        payment.Actions = payment.Actions + " , " + paymentActivity.activity.ActivityName;
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