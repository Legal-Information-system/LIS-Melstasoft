﻿using LegalSystemCore.Common;
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
            IFunctionsController functionsController = ControllerFactory.CreateFunctionsController();
            IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
            IUserPrivilegeController userPrivilegeController = ControllerFactory.CreateUserPrivilegeController();
            UserLogin userLogin = new UserLogin();
            userLogin.UserName = txtUserName.Text;
            userLogin.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1"); ;
            userLogin = userLoginController.GetUserLogin(userLogin);
            if (userLogin.UserId == 0)
            {
                txtPassword.Text = string.Empty;
                lblErrorMsg.Text = "Incorrect Username or Password!";
            }
            else
            {
                Session["User_Id"] = userLogin.UserId;
                Session["User_Role_Id"] = userLogin.UserRoleId;
                Session["User_Name"] = userLogin.UserName;
                Session["company_id"] = userLogin.CompanyId;
                Session["company_unit_id"] = userLogin.CompanyUnitId;
                if (!functionsController.GetFunctionList().Any())
                {
                    functionsController.Init();
                }
                if (!userRolePrivilegeController.GetUserRolePrivilegeList().Any())
                {
                    userRolePrivilegeController.Init();
                }
                if (!((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 20).Any()
                    && !(userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 20 && x.IsGrantRevoke == 0))) ||
                    userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 20 && x.IsGrantRevoke == 1)))
                {

                    Response.Redirect("ViewPaymentMemo.aspx");
                }
                else
                {
                    Response.Redirect("Dashboard.aspx");
                }

            }
        }


    }
}