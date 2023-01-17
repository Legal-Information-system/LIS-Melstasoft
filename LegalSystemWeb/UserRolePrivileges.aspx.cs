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
    public partial class UserRolePrivileges : System.Web.UI.Page
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
                if (!((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 23).Any()
                    && !(privilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 23 && x.IsGrantRevoke == 0))) ||
                    privilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 23 && x.IsGrantRevoke == 1)))
                {


                    Response.Redirect("404.aspx");
                }
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
                BindFunctionList();
            }
            else
            {
                gvUserPrevilages.Visible = false;
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            List<Functions> autUserFunctionList = (List<Functions>)ViewState["functions"];

            UserRolePrivilege userPrivilege = new UserRolePrivilege();

            userPrivilege.FunctionId = autUserFunctionList[rowIndex].FunctionId;
            userPrivilege.UserRoleId = Convert.ToInt32(ddlUser.SelectedValue);
            List<UserPrivilege> userPrivileges = privilegeController.GetUserPrivilegeList(Convert.ToInt32(ddlUser.SelectedValue));
            if (autUserFunctionList[rowIndex].Status == "Yes")
            {
                userRolePrivilegeController.Delete(userPrivilege);
            }
            else
            {
                userRolePrivilegeController.Save(userPrivilege);
            }
            BindFunctionList();

        }

        private void BindUser()
        {

            IUserRoleController userRoleController = ControllerFactory.CreateUserRoleController();
            List<UserRole> userRoles = userRoleController.GetUserRoleList();
            ddlUser.DataSource = userRoles;
            ddlUser.DataValueField = "RoleId";
            ddlUser.DataTextField = "RoleName";
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem("-- select user --", ""));
            ViewState["userLogins"] = userRoles;
        }


        private void BindFunctionList()
        {

            List<UserRole> userLogins = (List<UserRole>)ViewState["userLogins"];
            UserRole userLogin = userLogins.Where(x => x.RoleId == Convert.ToInt32(ddlUser.SelectedValue)).Single();
            IFunctionsController functionsController = ControllerFactory.CreateFunctionsController();
            IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
            List<Functions> functions = functionsController.GetFunctionList();

            List<UserRolePrivilege> userRolePrivileges = userRolePrivilegeController.GetUserRolePrivilegeListByRole(userLogin.RoleId.ToString());

            foreach (var item in functions)
            {
                if (userRolePrivileges.Any(x => x.FunctionId == item.FunctionId))
                {
                    item.Status = "Yes";

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