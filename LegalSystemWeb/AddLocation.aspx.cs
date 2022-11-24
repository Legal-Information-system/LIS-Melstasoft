using LegalSystemCore;
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
    public partial class AddLocation : System.Web.UI.Page
    {
        List<Location> locationList = new List<Location>();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataSource();
        }

        private void BindDataSource()
        {
            ILocationController locationController = ControllerFactory.CreateLocationController();

            locationList = locationController.GetLocationList();
            GridView2.DataSource = locationController.GetLocationList();
            GridView2.DataBind();

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            ILocationController locationController = ControllerFactory.CreateLocationController();

            if (btnSave.Text == "Update")
            {
                int rowIndex = (int)ViewState["updatedRowIndex"];
                Location location = new Location();
                location.LocationId = rowIndex;
                location.locationName = txtLocationName.Text;


                locationController.Update(location);
                btnSave.Text = "Save";
            }
            else
            {
                Location location = new Location();
                location.locationName = txtLocationName.Text;

                location.LocationId = locationController.Save(location);
            }

            BindDataSource();
            Clear();

        }

        private void Clear()
        {
            txtLocationName.Text = string.Empty;

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ILocationController locationController = ControllerFactory.CreateLocationController();

            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;

            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            txtLocationName.Text = locationList[rowIndex].locationName;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = locationList[rowIndex].LocationId;
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            ILocationController locationController = ControllerFactory.CreateLocationController();
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            Location location = new Location();
            location.LocationId = locationList[rowIndex].LocationId; ;

            locationController.Delete(location);
            BindDataSource();
        }
    }
}