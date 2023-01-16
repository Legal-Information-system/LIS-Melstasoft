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
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 7).Any())
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
            ICourtController courtController = ControllerFactory.CreateCourtController();

            courtList = courtController.GetCourtList(false);
            GridView2.DataSource = courtList;
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
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Court Details Updated Succesfully!', 'success')", true);
                btnSave.Text = "Save";
            }
            else
            {
                Court court = new Court();
                court.CourtName = txtCourtName.Text;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Court Added Succesfully!', 'success')", true);
                court.CourtId = courtController.Save(court);
            }

            BindDataSource();
            Clear();

        }

        private void Clear()
        {
            txtCourtName.Text = string.Empty;

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ICourtController courtController = ControllerFactory.CreateCourtController();

            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;

            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = GridView2.PageSize;
            int pageIndex = GridView2.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            txtCourtName.Text = courtList[rowIndex].CourtName;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = courtList[rowIndex].CourtId;
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            ICourtController courtController = ControllerFactory.CreateCourtController();
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = GridView2.PageSize;
            int pageIndex = GridView2.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            Court court = new Court();
            court.CourtId = courtList[rowIndex].CourtId;
            courtController.Delete(court);
            BindDataSource();
        }

        protected void GridView2_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            this.BindDataSource();
        }
    }
}