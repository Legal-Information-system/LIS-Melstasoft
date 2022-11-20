using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static LegalSystemWeb.ViewPaymentMemo;

namespace LegalSystemWeb
{
    public partial class UpdateCaseActivity : System.Web.UI.Page
    {
        List<string> Cases = new List<string>(4);
        List<string> Lawyer = new List<string>(4);
        List<string> Action = new List<string>(4);


        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataSource();
        }

        private void BindDataSource()
        {

            Cases.Add("Case One");
            Cases.Add("Case Two");
            Cases.Add("Case Three");
            Cases.Add("Case Four");

            Lawyer.Add("Lawyer One");
            Lawyer.Add("Lawyer Two");
            Lawyer.Add("Lawyer Three");
            Lawyer.Add("Lawyer Four");

            Action.Add("Action One");
            Action.Add("Action Two");
            Action.Add("Action Three");
            Action.Add("Action Four");

            ddlCase.DataSource = Cases;
            ddlCase.DataBind();
            ddlAssignAttorney.DataSource = Lawyer;
            ddlAssignAttorney.DataBind();
            ddlCounselor.DataSource = Lawyer;
            ddlCounselor.DataBind();
            ddlActionTaken.DataSource = Action;
            ddlActionTaken.DataBind();
            ddlNextAction.DataSource = Action;
            ddlNextAction.DataBind();
        }

    }
}