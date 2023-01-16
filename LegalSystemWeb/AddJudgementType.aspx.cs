using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class AddJudgementType : System.Web.UI.Page
    {
        List<JudgementType> judgementTypeList = new List<JudgementType>();
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 9).Any())
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
            IJudgementTypeController judgementTypeController = ControllerFactory.CreateJudgementTypeController();

            judgementTypeList = judgementTypeController.GetJudgementTypeList(false);
            gvJudgementType.DataSource = judgementTypeList;
            gvJudgementType.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            IJudgementTypeController judgementTypeController = ControllerFactory.CreateJudgementTypeController();

            if (btnSave.Text == "Update")
            {
                int rowIndex = (int)ViewState["updatedRowIndex"];
                JudgementType judgementType = new JudgementType();
                judgementType.JTypeId = rowIndex;
                judgementType.JTypeName = txtJType.Text;

                judgementTypeController.Update(judgementType);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Judgement Type Updated Succesfully!', 'success')", true);
                btnSave.Text = "Save";
            }
            else
            {
                JudgementType judgementType = new JudgementType();
                judgementType.JTypeName = txtJType.Text;
                judgementType.JTypeId = judgementTypeController.Save(judgementType);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Judgement Type Added Succesfully!', 'success')", true);
            }

            Clear();
            BindDataSource();

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = gvJudgementType.PageSize;
            int pageIndex = gvJudgementType.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            txtJType.Text = judgementTypeList[rowIndex].JTypeName;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = judgementTypeList[rowIndex].JTypeId;
        }

        private void Clear()
        {
            txtJType.Text = string.Empty;
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            IJudgementTypeController judgementTypeController = ControllerFactory.CreateJudgementTypeController();
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int pageSize = gvJudgementType.PageSize;
            int pageIndex = gvJudgementType.PageIndex;

            rowIndex = (pageSize * pageIndex) + rowIndex;

            JudgementType judgementType = new JudgementType();
            judgementType.JTypeId = judgementTypeList[rowIndex].JTypeId; ;

            judgementTypeController.Delete(judgementType);
            BindDataSource();
        }

        protected void gvJudgementType_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            gvJudgementType.PageIndex = e.NewPageIndex;
            this.BindDataSource();
        }
    }
}