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
        List<UserRole> userRoleList = new List<UserRole>();
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
                if (((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 13).Any()
                    && !(userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 13 && x.IsGrantRevoke == 0))) ||
                    userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 13 && x.IsGrantRevoke == 1)))
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
            IUserRoleController userRoleController = ControllerFactory.CreateUserRoleController();

            userRoleList = userRoleController.GetUserRoleList();
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
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Role Updated Succesfully!', 'success')", true);
                btnSave.Text = "Save";
            }
            else
            {
                UserRole userRole = new UserRole();
                userRole.RoleName = txtRoleName.Text;
                userRole.RoleId = userRoleController.Save(userRole);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Role Created Succesfully!', 'success')", true);
            }


            Clear();
            BindDataSource();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            //GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;

            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = gvUserRole.PageSize;
            int pageIndex = gvUserRole.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            txtRoleName.Text = userRoleList[rowIndex].RoleName;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = userRoleList[rowIndex].RoleId; ;
        }


        private void Clear()
        {
            txtRoleName.Text = string.Empty;
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            IUserRoleController userRoleController = ControllerFactory.CreateUserRoleController();

            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = gvUserRole.PageSize;
            int pageIndex = gvUserRole.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            UserRole userRole = new UserRole();
            userRole.RoleId = userRoleList[rowIndex].RoleId;

            userRoleController.Delete(userRole);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Role Deleted Succesfully!', 'success')", true);
            BindDataSource();
        }

        protected void gvUserRole_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            gvUserRole.PageIndex = e.NewPageIndex;
            this.BindDataSource();
        }
    }
}