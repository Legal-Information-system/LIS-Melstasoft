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
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (Session["User_Role_Id"].ToString() == "1" || Session["User_Role_Id"].ToString() == "2")
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
            int pageSize = GridView2.PageSize;
            int pageIndex = GridView2.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            txtLocationName.Text = locationList[rowIndex].locationName;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = locationList[rowIndex].LocationId;
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            ILocationController locationController = ControllerFactory.CreateLocationController();
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = GridView2.PageSize;
            int pageIndex = GridView2.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            Location location = new Location();
            location.LocationId = locationList[rowIndex].LocationId; ;

            locationController.Delete(location);
            BindDataSource();
        }

        protected void GridView2_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            this.BindDataSource();
        }
    }
}