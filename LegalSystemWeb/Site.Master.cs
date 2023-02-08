﻿using LegalSystemCore.Common;
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
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        IUserPrivilegeController UserPrivilegeController = ControllerFactory.CreateUserPrivilegeController();
        protected List<UserRolePrivilege> userRolePrivileges = new List<UserRolePrivilege>();
        protected List<UserPrivilege> userPrivileges = new List<UserPrivilege>();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUserName.Text = Session["User_Name"].ToString();
            userRolePrivileges = userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString());
            userPrivileges = UserPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"]));

        }

        protected void btnLogut_Click(object sender, EventArgs e)
        {
            string test = Session["User_Id"].ToString();
            if (Session["User_Id"] != null)
            {
                Session.Abandon();
                Response.Redirect("Login.aspx");

            }
        }

        protected void btnNotification_Click(object sender, EventArgs e)
        {
            Response.Redirect("Notification.aspx");
        }
    }
}
