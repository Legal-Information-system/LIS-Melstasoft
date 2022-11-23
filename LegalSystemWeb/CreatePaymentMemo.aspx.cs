using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class CreatePaymentMemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindActivityList();
                BindLawyerList();
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

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            IPaymentController paymentController = ControllerFactory.CreatePaymentController();

            Payment payment = new Payment();

            payment.CaseNumber = ddlCaseNo.SelectedValue;
            payment.LawyerId = ddlLawyerName;


            //userLogin.UserName = txtUserName.Text;
            //userLogin.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
            //userLogin.CompanyId = ddlCompany.SelectedValue;
            //userLogin.CompanyUnitId = ddlCompanyUnit.SelectedValue;
            //userLogin.UserRoleId = ddlUserType.SelectedValue;
            //userLogin.UserId = userLoginController.Save(userLogin);




            Clear();

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        private void Clear()
        {
            txtTotalPayableAmount.Text = string.Empty;
            ddlCaseNo.ClearSelection();
        }
    }
}