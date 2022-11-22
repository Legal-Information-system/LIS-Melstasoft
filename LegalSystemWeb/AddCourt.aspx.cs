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
    public partial class AddCourt1 : System.Web.UI.Page
    {
        List<Court> courtList = new List<Court>();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataSource();
        }

        private void BindDataSource()
        {
            ICourtController courtController = ControllerFactory.CreateCourtController();

            courtList = courtController.GetCourtList();
            GridView2.DataSource = courtController.GetCourtList();
            GridView2.DataBind();

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            ICourtController courtController = ControllerFactory.CreateCourtController();

            if (btnSave.Text == "Update")
            {
                int rowIndex = (int)ViewState["updatedRowIndex"];
                Court court = new Court();
                court.CourtId = rowIndex;
                court.CourtName = txtCourtName.Text;


                courtController.Update(court);
                btnSave.Text = "Save";
            }
            else
            {
                Court court = new Court();
                court.CourtName = txtCourtName.Text;

                court.CourtId = courtController.Save(court);
            }

            BindDataSource();
            Clear();

        }

        private void Clear()
        {
            txtCourtName.Text = string.Empty;

        }
    }
}