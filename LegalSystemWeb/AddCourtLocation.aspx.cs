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
    public partial class AddCourt : System.Web.UI.Page
    {
        List<CourtLocation> courtlocation = new List<CourtLocation>();
        List<Court> courtList = new List<Court>();
        List<Location> locationList = new List<Location>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCourtList();
                BindLocationList();
            }
            BindDataSource();
        }

        private void BindDataSource()
        {
            ICourtLocationController courtlocationController = ControllerFactory.CreateCourtLocationController();

            courtlocation = courtlocationController.GetCourtLocationList();
            GridView2.DataSource = courtlocationController.GetCourtLocationList();
            GridView2.DataBind();
        }

        private void BindCourtList()
        {

            ICourtController courtController = ControllerFactory.CreateCourtController();

            courtList = courtController.GetCourtList();
            ddlCourt.DataSource = courtController.GetCourtList();
            ddlCourt.DataValueField = "CourtId";
            ddlCourt.DataTextField = "CourtName";
            ddlCourt.DataBind();

        }

        private void BindLocationList()
        {

            ILocationController locationController = ControllerFactory.CreateLocationController();

            locationList = locationController.GetLocationList();
            ddlLocation.DataSource = locationController.GetLocationList();
            ddlLocation.DataValueField = "LocationId";
            ddlLocation.DataTextField = "location";
            ddlLocation.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            ICourtLocationController courtLocationController = ControllerFactory.CreateCourtLocationController();

            if (btnSave.Text == "Update")
            {
                int courtId = (int)ViewState["updatedRowIndex1"];
                int locationId = (int)ViewState["updatedRowIndex2"];
                CourtLocation courtlocation = new CourtLocation();
                courtlocation.CourtId = Convert.ToInt32(ddlCourt.SelectedValue);
                courtlocation.LocationId = Convert.ToInt32(ddlLocation.SelectedValue);

                courtLocationController.Update(courtlocation, courtId, locationId);
                btnSave.Text = "Save";
            }
            else
            {
                CourtLocation courtlocation = new CourtLocation();
                courtlocation.CourtId = Convert.ToInt32(ddlCourt.SelectedValue);
                courtlocation.LocationId = Convert.ToInt32(ddlLocation.SelectedValue);

                courtlocation.CourtId = courtLocationController.Save(courtlocation);
            }

            BindDataSource();

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ICourtLocationController courLocationtController = ControllerFactory.CreateCourtLocationController();

            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;

            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            ddlCourt.SelectedValue = Convert.ToString(courtlocation[rowIndex].CourtId);
            ddlLocation.SelectedValue = Convert.ToString(courtlocation[rowIndex].LocationId);
            btnSave.Text = "Update";
            ViewState["updatedRowIndex1"] = courtlocation[rowIndex].CourtId;
            ViewState["updatedRowIndex2"] = courtlocation[rowIndex].LocationId;
        }
    }
}