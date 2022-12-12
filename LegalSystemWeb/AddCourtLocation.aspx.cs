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
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (Session["User_Role_Id"].ToString() == "1" || Session["User_Role_Id"].ToString() == "2")
                {
                    if (!IsPostBack)
                    {
                        BindCourtList();
                        BindLocationList();
                    }
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
            ICourtLocationController courtlocationController = ControllerFactory.CreateCourtLocationController();

            courtlocation = courtlocationController.GetCourtLocationList(false);
            GridView2.DataSource = courtlocation;
            GridView2.DataBind();
        }

        private void BindCourtList()
        {

            ICourtController courtController = ControllerFactory.CreateCourtController();

            courtList = courtController.GetCourtList(false);
            ddlCourt.DataSource = courtList;
            ddlCourt.DataValueField = "CourtId";
            ddlCourt.DataTextField = "CourtName";
            ddlCourt.DataBind();

        }

        private void BindLocationList()
        {

            ILocationController locationController = ControllerFactory.CreateLocationController();

            locationList = locationController.GetLocationList(false);
            ddlLocation.DataSource = locationList;
            ddlLocation.DataValueField = "LocationId";
            ddlLocation.DataTextField = "locationName";
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
            int pageSize = GridView2.PageSize;
            int pageIndex = GridView2.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            ddlCourt.SelectedValue = Convert.ToString(courtlocation[rowIndex].CourtId);
            ddlLocation.SelectedValue = Convert.ToString(courtlocation[rowIndex].LocationId);
            btnSave.Text = "Update";
            ViewState["updatedRowIndex1"] = courtlocation[rowIndex].CourtId;
            ViewState["updatedRowIndex2"] = courtlocation[rowIndex].LocationId;
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            ICourtLocationController courLocationtController = ControllerFactory.CreateCourtLocationController();

            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = GridView2.PageSize;
            int pageIndex = GridView2.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            CourtLocation cl = new CourtLocation();
            cl.CourtId = courtlocation[rowIndex].CourtId; ;
            cl.LocationId = courtlocation[rowIndex].LocationId;

            courLocationtController.Delete(cl);
            BindDataSource();
        }

        protected void GridView2_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            this.BindDataSource();
        }
    }
}