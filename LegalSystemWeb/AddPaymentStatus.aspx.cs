using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class AddPaymentStatus : System.Web.UI.Page
    {
        List<PaymentStatus> paymentStatusList = new List<PaymentStatus>();

        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataSource();
        }

        private void BindDataSource()
        {
            IPaymentStatusController paymentStatusController = ControllerFactory.CreatePaymentStatusController();

            paymentStatusList = paymentStatusController.GetPaymentStatusList();
            gvPaymentStatus.DataSource = paymentStatusController.GetPaymentStatusList();
            gvPaymentStatus.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            IPaymentStatusController paymentStatusController = ControllerFactory.CreatePaymentStatusController();

            if (btnSave.Text == "Update")
            {
                int rowIndex = (int)ViewState["updatedRowIndex"];
                PaymentStatus paymentStatus = new PaymentStatus();
                paymentStatus.StatusId = rowIndex;
                paymentStatus.StatusName = txtPStatus.Text;

                paymentStatusController.Update(paymentStatus);
                btnSave.Text = "Save";
            }
            else
            {
                PaymentStatus paymentStatus = new PaymentStatus();
                paymentStatus.StatusName = txtPStatus.Text;
                paymentStatus.StatusId = paymentStatusController.Save(paymentStatus);
            }

            Clear();
            BindDataSource();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            txtPStatus.Text = paymentStatusList[rowIndex].StatusName;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = paymentStatusList[rowIndex].StatusId;
        }
        private void Clear()
        {
            txtPStatus.Text = string.Empty;
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {

            IPaymentStatusController paymentStatusController = ControllerFactory.CreatePaymentStatusController();
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            PaymentStatus paStatus = new PaymentStatus();
            paStatus.StatusId = paymentStatusList[rowIndex].StatusId;
            paymentStatusController.Delete(paStatus);
            BindDataSource();
        }
    }
}