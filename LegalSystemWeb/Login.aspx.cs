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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            IUserLoginController userLoginController = ControllerFactory.CreateUserLoginController();
            UserLogin userLogin = new UserLogin();
            userLogin.UserName = txtUserName.Text;
            userLogin.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1"); ;
            userLogin = userLoginController.GetUserLogin(userLogin);
            if (userLogin.UserId == 0)
            {
                txtPassword.Text = string.Empty;
            }
            else
            {
                Session["User_Id"] = userLogin.UserId;
                Session["User_Role_Id"] = userLogin.UserRoleId;
                Session["User_Name"] = userLogin.UserName;
                Response.Redirect("Dashboard.aspx");
            }
        }


    }
}