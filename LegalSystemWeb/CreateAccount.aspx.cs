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
    public partial class CreateAccount : System.Web.UI.Page
    {
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        IUserPrivilegeController userPrivilegeController = ControllerFactory.CreateUserPrivilegeController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 15).Any()
                    && !(userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 15 && x.IsGrantRevoke == 0))) ||
                    userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 15 && x.IsGrantRevoke == 1)))
                {
                    Response.Redirect("404.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        BindUserRoles();
                        BindCompanyList();
                        BindCompanyUnitList();
                    }

                }
            }

        }


        private void BindUserRoles()
        {
            IUserRoleController userRoleController = ControllerFactory.CreateUserRoleController();
            ddlUserType.DataSource = userRoleController.GetUserRoleList();
            ddlUserType.DataValueField = "RoleId";
            ddlUserType.DataTextField = "RoleName";

            ddlUserType.DataBind();
        }

        private void BindCompanyList()
        {
            ICompanyController companyController = ControllerFactory.CreateCompanyController();
            ddlCompany.DataSource = companyController.GetCompanyList(false).OrderBy(x => x.CompanyName);
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataTextField = "CompanyName";

            ddlCompany.DataBind();
        }

        private void BindCompanyUnitList()
        {
            ICompanyUnitController companyUnitController = ControllerFactory.CreateCompanyUnitController();
            if (ddlCompany.SelectedValue != "")
            {
                ddlCompanyUnit.DataSource = companyUnitController.GetCompanyUnitListFilter(false, ddlCompany.SelectedValue).OrderBy(x => x.CompanyUnitName);
                ddlCompanyUnit.DataValueField = "CompanyUnitId";
                ddlCompanyUnit.DataTextField = "CompanyUnitName";

                ddlCompanyUnit.DataBind();

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            IUserLoginController userLoginController = ControllerFactory.CreateUserLoginController();

            List<UserLogin> userLoginList = new List<UserLogin>();
            userLoginList = userLoginController.GetUserLoginList(true);

            int flag = 0;

            UserLogin userLogin = new UserLogin();
            userLogin.UserName = txtUserName.Text;
            userLogin.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
            userLogin.CompanyId = ddlCompany.SelectedValue;
            userLogin.CompanyUnitId = ddlCompanyUnit.SelectedValue;
            userLogin.UserRoleId = Convert.ToInt32(ddlUserType.SelectedValue);

            foreach (var user in userLoginList)
            {
                if (user.UserName.ToLower() == userLogin.UserName.ToLower())
                {
                    flag = 1;
                }
            }

            if (flag == 0)
            {
                userLogin.UserId = userLoginController.Save(userLogin);

                Clear();
                lblErrorMsg.Text = "";

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "swal('Success!', 'User Created Succesfully!', 'success')", true);
                Clear();

            }
            else
            {
                Clear();
                lblSuccessMsg.Text = "";
                lblErrorMsg.Text = "User Already Exists!";
            }
        }

        private void Clear()
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;

        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCompanyUnitList();
        }
    }
}