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
        List<JudgementType> judgementType = new List<JudgementType>();

        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataSource();
        }

        private void BindDataSource()
        {
            IJudgementTypeController judgementTypeController = ControllerFactory.CreateJudgementTypeController();

            judgementType = judgementTypeController.GetJudgementTypeList();
            gvJudgementType.DataSource = judgementTypeController.GetJudgementTypeList();
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
                btnSave.Text = "Save";
            }
            else
            {
                JudgementType judgementType = new JudgementType();
                judgementType.JTypeName = txtJType.Text;
                judgementType.JTypeId = judgementTypeController.Save(judgementType);
            }

            Clear();
            BindDataSource();

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            txtJType.Text = judgementType[rowIndex].JTypeName;
            btnSave.Text = "Update";
            ViewState["updatedRowIndex"] = judgementType[rowIndex].JTypeId;
        }

        private void Clear()
        {
            txtJType.Text = string.Empty;
        }
    }
}