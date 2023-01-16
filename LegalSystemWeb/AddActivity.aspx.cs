using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Activity = LegalSystemCore.Domain.Activity;

namespace LegalSystemWeb
{
    public partial class AddActivity : System.Web.UI.Page
    {
        List<Activity> activitiesList = new List<Activity>();
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 2).Any())
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
            IActivityController activityController = ControllerFactory.CreateActivityController();
            activitiesList = activityController.GetActivityList(false);
            GridView2.DataSource = activitiesList;
            GridView2.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            IActivityController activityController = ControllerFactory.CreateActivityController();

            if (btnSave.Text == "Update")
            {
                int rowIndex = (int)ViewState["updatedRowIndex"];
                Activity activity = new Activity();
                activity.ActivityId = rowIndex;
                activity.ActivityName = txtAddActivity.Text;



                activityController.Update(activity);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Activity Updated Succesfully!', 'success')", true);
                btnSave.Text = "Add";
            }
            else
            {
                Activity activity = new Activity();
                activity.ActivityName = txtAddActivity.Text;
                activity.ActivityId = activityController.Save(activity);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Activity Added Succesfully!', 'success')", true);
            }


            Clear();
            BindDataSource();
        }
        private void Clear()
        {
            txtAddActivity.Text = string.Empty;
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {

            IActivityController activityController = ControllerFactory.CreateActivityController();
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            if (GridView2.PageIndex != 0)
            {
                rowIndex = (GridView2.PageSize + rowIndex) * (GridView2.PageIndex);
            }
            Activity activity = new Activity();
            activity.ActivityId = activitiesList[rowIndex].ActivityId;

            activityController.Delete(activity);
            BindDataSource();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            if (GridView2.PageIndex != 0)
            {
                rowIndex = (GridView2.PageSize + rowIndex) * (GridView2.PageIndex);
            }
            txtAddActivity.Text = activitiesList[rowIndex].ActivityName;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = activitiesList[rowIndex].ActivityId;
        }

        protected void GridView2_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            this.BindDataSource();
        }
    }
}