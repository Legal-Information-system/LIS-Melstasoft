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

        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataSource();
        }

        private void BindDataSource()
        {
            IActivityController activityController = ControllerFactory.CreateAddActivity();

            GridView2.DataSource = activityController.GetActivityList();
            GridView2.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            IActivityController activityController = ControllerFactory.CreateAddActivity();

            if (btnSave.Text == "Update")
            {
                int rowIndex = (int)ViewState["updatedRowIndex"];
                Activity activity = new Activity();
                activity.ActivityId = rowIndex;
                activity.ActivityName = txtAddActivity.Text;



                //lawyerController.Update(lawyer);
                btnSave.Text = "Add";
            }
            else
            {
                Activity activity = new Activity();
                activity.ActivityName = txtAddActivity.Text;


                activity.ActivityId = activityController.Save(activity);
            }


            Clear();
        }
        private void Clear()
        {
            txtAddActivity.Text = string.Empty;


        }
    }
}