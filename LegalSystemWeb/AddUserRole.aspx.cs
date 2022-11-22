﻿using LegalSystemCore;
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
    public partial class AddUserRole : System.Web.UI.Page
    {
        List<UserRole> userRoles = new List<UserRole>();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataSource();
        }
        private void BindDataSource()
        {
            IUserRoleController userRoleController = ControllerFactory.CreateUserRoleController();

            userRoles = userRoleController.GetUserRoleList();
            gvUserRole.DataSource = userRoleController.GetUserRoleList();
            gvUserRole.DataBind();

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            IUserRoleController userRoleController = ControllerFactory.CreateUserRoleController();

            if (btnSave.Text == "Update")
            {
                int rowIndex = (int)ViewState["updatedRowIndex"];
                UserRole userRole = new UserRole();
                userRole.RoleId = rowIndex;
                userRole.RoleName = txtRoleName.Text;


                userRoleController.Update(userRole);
                btnSave.Text = "Save";
            }
            else
            {
                UserRole userRole = new UserRole();
                userRole.RoleName = txtRoleName.Text;
                userRole.RoleId = userRoleController.Save(userRole);
            }


            Clear();
            BindDataSource();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;

            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            txtRoleName.Text = userRoles[rowIndex].RoleName;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = userRoles[rowIndex].RoleId; ;
        }


        private void Clear()
        {
            txtRoleName.Text = string.Empty;
        }
    }
}