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
                BindActivityList();
                BindLawyerList();
                BindCaseList();
            }
        }

        private void BindActivityList()
        {
            IActivityController activityController = ControllerFactory.CreateActivityController();
            cblActivity.DataSource = activityController.GetActivityList();
            cblActivity.DataValueField = "ActivityId";
            cblActivity.DataTextField = "ActivityName";
            cblActivity.DataBind();
        }

        private void BindLawyerList()
        {
            ILawyerController LawyerController = ControllerFactory.CreateLawyerController();
            ddlLawyerName.DataSource = LawyerController.GetLawyerList();
            ddlLawyerName.DataValueField = "LawyerId";
            ddlLawyerName.DataTextField = "LawyerName";
            ddlLawyerName.DataBind();
        }

        private void BindCaseList()
        {
            ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
            ddlCaseNo.DataSource = caseMasterController.GetCaseMasterList(true);
            ddlCaseNo.DataValueField = "CaseNumber";
            ddlCaseNo.DataTextField = "CaseNumber";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            IPaymentController paymentController = ControllerFactory.CreatePaymentController();
            IPaymentActivityController paymentActivityController = ControllerFactory.CreatePaymentActivityController();

            Payment payment = new Payment();

            PaymentActivity paymentActivity = new PaymentActivity();

            payment.CaseNumber = ddlCaseNo.SelectedValue;
            payment.LawyerId = Convert.ToInt32(ddlLawyerName.SelectedValue);
            payment.Amount = Convert.ToInt32(txtTotalPayableAmount);
            payment.PaymentId = paymentController.Save(payment);
            foreach (Activity activity in listActivity)
            {
                paymentActivity.ActivityId = activity.ActivityId;
                paymentActivity.PaymentId = payment.PaymentId;
                paymentActivityController.Save(paymentActivity);
            }

            Clear();

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtTotalPayableAmount.Text = string.Empty;
            ddlCaseNo.ClearSelection();
        }

        public void Check_Clicked(Object sender, EventArgs e)
        {
            listActivity.Clear();
            for (int i = 0; i < cblActivity.Items.Count; i++)
            {
                if (cblActivity.Items[i].Selected)
                {
                    activity.ActivityId = Convert.ToInt32(cblActivity.Items[i].Value);
                    listActivity.Add(activity);
                }
            }
        }
    }
}