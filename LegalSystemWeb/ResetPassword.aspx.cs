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
    public partial class ResetPassword : System.Web.UI.Page
    {
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        IUserRoleController userRoleController = ControllerFactory.CreateUserRoleController();
        IUserPrivilegeController privilegeController = ControllerFactory.CreateUserPrivilegeController();
        IUserLoginController userLoginController = ControllerFactory.CreateUserLoginController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 31).Any()
                    && !(privilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 31 && x.IsGrantRevoke == 0))) ||
                    privilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 31 && x.IsGrantRevoke == 1)))
                    Response.Redirect("404.aspx");
                else
                {
                    BindUser();
                }
            }
        }
        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUser.SelectedValue != "")
            {

                lblUserType.Text = userRoleController.GetUserRole(userLoginController.GetUserLoginById(ddlUser.SelectedValue).UserRoleId).RoleName;
            }
            else
            {
                lblUserType.Text = "";
            }
        }

        private void BindUser()
        {



            List<UserLogin> userLogins = userLoginController.GetUserLoginList(true);
            ddlUser.DataSource = userLogins;
            ddlUser.DataValueField = "UserId";
            ddlUser.DataTextField = "UserName";
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem("-- select user --", ""));
            ViewState["userLogins"] = userLogins;
        }

        private bool CheckPass()
        {
            if (txtPassword.Text == txtRetypePassword.Text)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckPass())
            {
                lblError.Text = string.Empty;
                UserLogin userLogin = new UserLogin();
                userLogin.UserId = Convert.ToInt32(ddlUser.SelectedValue);
                userLogin.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
                userLoginController.UpdatePassword(userLogin);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "swal('Success!', 'User " + ddlUser.SelectedItem.Text + " Password Changed Succesfully!', 'success')", true);
            }
            else
            {
                lblError.Text = "Passwords Does not match! ";
            }
        }
    }
}