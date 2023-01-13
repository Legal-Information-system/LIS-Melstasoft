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
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (Session["User_Role_Id"].ToString() == "1" || Session["User_Role_Id"].ToString() == "2")
                {
                    BindDataSource();
                }
                else
                {
                    Response.Redirect("404.aspx");
                }
            }
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
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Payment Status Updated Succesfully!', 'success')", true);
                btnSave.Text = "Save";
            }
            else
            {
                PaymentStatus paymentStatus = new PaymentStatus();
                paymentStatus.StatusName = txtPStatus.Text;
                paymentStatus.StatusId = paymentStatusController.Save(paymentStatus);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Payment Status Created Succesfully!', 'success')", true);
            }

            Clear();
            BindDataSource();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = gvPaymentStatus.PageSize;
            int pageIndex = gvPaymentStatus.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

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
            int pageSize = gvPaymentStatus.PageSize;
            int pageIndex = gvPaymentStatus.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            PaymentStatus paStatus = new PaymentStatus();
            paStatus.StatusId = paymentStatusList[rowIndex].StatusId;
            paymentStatusController.Delete(paStatus);
            BindDataSource();
        }

        protected void gvPaymentStatus_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            gvPaymentStatus.PageIndex = e.NewPageIndex;
            this.BindDataSource();
        }
    }
}