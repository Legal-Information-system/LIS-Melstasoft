using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUserName.Text = Session["User_Name"].ToString();
        }

        protected void btnLogut_Click(object sender, EventArgs e)
        {
            if (Session["User_Id"] != null)
            {
                Session.Abandon();
                Response.Redirect("Login.aspx");

            }
        }
    }
}
