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
    public partial class UserPrivileges : System.Web.UI.Page
    {
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        IUserRoleController userRoleController = ControllerFactory.CreateUserRoleController();
        IUserPrivilegeController privilegeController = ControllerFactory.CreateUserPrivilegeController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                if (!((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 22).Any()
                    && !(privilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 22 && x.IsGrantRevoke == 0))) ||
                    privilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 22 && x.IsGrantRevoke == 1)))
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
                gvUserPrevilages.Visible = true;
                lblUserType.Text = userRoleController.GetUserRole(Convert.ToInt32(Session["User_Role_Id"])).RoleName;
                BindFunctionList();
            }
            else
            {
                lblUserType.Text = "";
                gvUserPrevilages.Visible = false;
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            List<Functions> autUserFunctionList = (List<Functions>)ViewState["functions"];

            UserPrivilege userPrivilege = new UserPrivilege();

            userPrivilege.FunctionId = autUserFunctionList[rowIndex].FunctionId;
            userPrivilege.UserLoginId = Convert.ToInt32(ddlUser.SelectedValue);
            List<UserPrivilege> userPrivileges = privilegeController.GetUserPrivilegeList(Convert.ToInt32(ddlUser.SelectedValue));
            if (autUserFunctionList[rowIndex].Status == "Yes")
            {
                userPrivilege.IsGrantRevoke = 0;
                if (userPrivileges.Any(x => x.FunctionId == autUserFunctionList[rowIndex].FunctionId))
                {
                    privilegeController.Update(userPrivilege);
                }
                else
                {
                    privilegeController.Save(userPrivilege);
                }
            }
            else
            {
                userPrivilege.IsGrantRevoke = 1;
                if (userPrivileges.Any(x => x.FunctionId == autUserFunctionList[rowIndex].FunctionId))
                {
                    privilegeController.Update(userPrivilege);
                }
                else
                {
                    privilegeController.Save(userPrivilege);
                }
            }
            BindFunctionList();

        }

        private void BindUser()
        {


            IUserLoginController userLoginController = ControllerFactory.CreateUserLoginController();
            List<UserLogin> userLogins = userLoginController.GetUserLoginList(true);
            ddlUser.DataSource = userLogins;
            ddlUser.DataValueField = "UserId";
            ddlUser.DataTextField = "UserName";
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem("-- select user --", ""));
            ViewState["userLogins"] = userLogins;
        }


        private void BindFunctionList()
        {

            List<UserLogin> userLogins = (List<UserLogin>)ViewState["userLogins"];
            UserLogin userLogin = userLogins.Where(x => x.UserId == Convert.ToInt32(ddlUser.SelectedValue)).Single();
            lblUserType.Text = userRoleController.GetUserRole(userLogin.UserRoleId).RoleName;
            IFunctionsController functionsController = ControllerFactory.CreateFunctionsController();
            IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
            IUserPrivilegeController userPrivilegeController = ControllerFactory.CreateUserPrivilegeController();
            List<Functions> functions = functionsController.GetFunctionList();

            List<UserRolePrivilege> userRolePrivileges = userRolePrivilegeController.GetUserRolePrivilegeListByRole(userLogin.UserRoleId.ToString());
            List<UserPrivilege> UserPrivileges = userPrivilegeController.GetUserPrivilegeList(userLogin.UserId);

            foreach (var item in functions)
            {
                if (userRolePrivileges.Any(x => x.FunctionId == item.FunctionId) || UserPrivileges.Any(x => x.FunctionId == item.FunctionId && x.IsGrantRevoke == 1))
                {
                    item.Status = "Yes";
                    if (UserPrivileges.Any(x => x.FunctionId == item.FunctionId && x.IsGrantRevoke == 0))
                    {
                        item.Status = "NO";
                    }
                }
                else
                {
                    item.Status = "NO";
                }
            }


            gvUserPrevilages.DataSource = functions;
            gvUserPrevilages.DataBind();

            ViewState["functions"] = functions;

        }
    }
}