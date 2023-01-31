using LegalSystemCore;
using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Xsl;

namespace LegalSystemWeb
{
    public partial class Dashboard : System.Web.UI.Page
    {
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        IUserPrivilegeController userPrivilegeController = ControllerFactory.CreateUserPrivilegeController();
        public DataTable dashboardCardList, progressTable, claimAmoutTable, DailyCaseList, MonthCaseList;
        public string dates, caseCount, caseNumber, per, CompanyUnitName;
        public int DailyTotal, MonthlyTotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                List<UserRolePrivilege> userRolePrivileges = userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString());

                if (!((userRolePrivileges.Where(x => x.FunctionId == 20).Any()) && userRolePrivileges.Where(x => x.FunctionId == 28 || x.FunctionId == 29 || x.FunctionId == 30).Any() &&
                !(userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => (x.FunctionId == 28 || x.FunctionId == 29 || x.FunctionId == 30) && x.IsGrantRevoke == 0)) ||
                userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => (x.FunctionId == 28 || x.FunctionId == 29 || x.FunctionId == 30) && x.IsGrantRevoke == 1)))
                {
                    Response.Redirect("ViewPaymentMemo.aspx");
                }
                if (!IsPostBack)
                {

                    if (Request.QueryString["name"] == null)
                    {
                        DashboardView.Visible = true;
                        BindCompanyList();
                        ViewCompany.Visible = false;
                    }
                    else
                    {
                        CompanyUnitName = Request.QueryString["name"].ToString();
                        DashboardView.Visible = false;
                        ViewCompany.Visible = true;
                        BindCompanyUnitList();

                    }
                }

            }
        }
        private void BindCompanyUnitList()
        {
            IDashboardCardController dashboardCardController = ControllerFactory.CreateDashboardCardController();
            ICompanyController companyController = ControllerFactory.CreateCompanyController();
            List<UserRolePrivilege> userRolePrivileges = userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString());
            List<UserPrivilege> UserPrivileges = userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"]));
            Company company = new Company();
            company = companyController.GetCompanyByName(CompanyUnitName);
            if (((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 28).Any()
                    && !(UserPrivileges.Any(x => x.FunctionId == 28 && x.IsGrantRevoke == 0))) ||
                    UserPrivileges.Any(x => x.FunctionId == 28 && x.IsGrantRevoke == 1)))
            {


                dashboardCardList = dashboardCardController.GetCardDetailsCompanyUnit(company.CompanyId);
                progressTable = dashboardCardController.GetMonthProgressUnit(false, company.CompanyId);
                claimAmoutTable = dashboardCardController.GetClaimAmountPercentageUnit(false, company.CompanyId);

            }
            StringBuilder cstextCard = new StringBuilder();
            cstextCard.Append("<div id=\"DashboardView\" runat=\"server\">\r\n        <h1 class=\"mt-4\">");
            cstextCard.Append(CompanyUnitName);
            cstextCard.Append("</h1> <br/> <br/>\r\n <div class=\"row\">");
            foreach (DataRow row in dashboardCardList.Rows)
            {


                cstextCard.Append("<div class=\"col-xl-3 col-md-6\">    <div class=\"card bg-primary text-white mb-4\"> <div class=\"card-body\">   <div class=\"text-center\">");
                cstextCard.Append(row["company_name"].ToString());
                cstextCard.Append("</div>   <div class=\"text-center\">");
                cstextCard.Append(row["case_count"].ToString());
                cstextCard.Append("</div>   <a class=\"small text-white stretched-link\" href=\"ViewCases.aspx?name=");
                cstextCard.Append(row["company_name"].ToString());
                string text = row["company_name"].ToString();
                cstextCard.Append("\"></a>  </div>  </div>  </div> ");

            }
            cstextCard.Append("</div> </div>");
            ltCompanyUnit.Text += cstextCard;
        }

        private void BindCompanyList()
        {
            IDashboardCardController dashboardCardController = ControllerFactory.CreateDashboardCardController();
            List<UserRolePrivilege> userRolePrivileges = userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString());
            List<UserPrivilege> UserPrivileges = userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"]));
            if (((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 28).Any()
                    && !(UserPrivileges.Any(x => x.FunctionId == 28 && x.IsGrantRevoke == 0))) ||
                    UserPrivileges.Any(x => x.FunctionId == 28 && x.IsGrantRevoke == 1)))
            {
                dashboardCardList = dashboardCardController.GetCardDetails();
                progressTable = dashboardCardController.GetMonthProgress();
                claimAmoutTable = dashboardCardController.GetClaimAmountPercentage();
                foreach (DataRow row in dashboardCardList.Rows)
                {
                    StringBuilder cstextCard = new StringBuilder();

                    cstextCard.Append("<div class=\"col-xl-3 col-md-6\">    <div class=\"card bg-primary text-white mb-4\"> <div class=\"card-body\">   <div class=\"text-center\">");
                    cstextCard.Append(row["company_name"].ToString());
                    cstextCard.Append("</div>   <div class=\"text-center\">");
                    cstextCard.Append(row["case_count"].ToString());
                    cstextCard.Append("</div>   <a class=\"small text-white stretched-link\" href=\"Dashboard.aspx?name=");
                    cstextCard.Append(row["company_name"].ToString());
                    string text = row["company_name"].ToString();
                    cstextCard.Append("\"></a>  </div>  </div>  </div> ");

                    ltCompanyStatus.Text += cstextCard;
                }
            }
            else
            {


                if (((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 29).Any()
                    && !(UserPrivileges.Any(x => x.FunctionId == 29 && x.IsGrantRevoke == 0))) ||
                    UserPrivileges.Any(x => x.FunctionId == 29 && x.IsGrantRevoke == 1)))
                {
                    dashboardCardList = dashboardCardController.GetCardDetailsCompanyUnit(Convert.ToInt32(Session["company_id"].ToString()));
                    progressTable = dashboardCardController.GetMonthProgressUnit(false, Convert.ToInt32(Session["company_id"].ToString()));
                    claimAmoutTable = dashboardCardController.GetClaimAmountPercentageUnit(false, Convert.ToInt32(Session["company_id"].ToString()));
                }
                else
                {

                    if (((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 30).Any()
                    && !(UserPrivileges.Any(x => x.FunctionId == 30 && x.IsGrantRevoke == 0))) ||
                    UserPrivileges.Any(x => x.FunctionId == 30 && x.IsGrantRevoke == 1)))
                    {
                        dashboardCardList = dashboardCardController.GetCardDetailsUnit(Convert.ToInt32(Session["company_unit_id"].ToString()));
                        progressTable = dashboardCardController.GetMonthProgressUnit(true, Convert.ToInt32(Session["company_unit_id"].ToString()));
                        claimAmoutTable = dashboardCardController.GetClaimAmountPercentageUnit(true, Convert.ToInt32(Session["company_unit_id"].ToString()));
                    }

                }
                foreach (DataRow row in dashboardCardList.Rows)
                {
                    StringBuilder cstextCard = new StringBuilder();

                    cstextCard.Append("<div class=\"col-xl-3 col-md-6\">    <div class=\"card bg-primary text-white mb-4\"> <div class=\"card-body\">   <div class=\"text-center\">");
                    cstextCard.Append(row["company_name"].ToString());
                    cstextCard.Append("</div>   <div class=\"text-center\">");
                    cstextCard.Append(row["case_count"].ToString());
                    cstextCard.Append("</div>   <a class=\"small text-white stretched-link\" href=\"ViewCases.aspx?name=");
                    cstextCard.Append(row["company_name"].ToString());
                    string text = row["company_name"].ToString();
                    cstextCard.Append("\"></a>  </div>  </div>  </div> ");

                    ltCompanyStatus.Text += cstextCard;
                }
            }



            foreach (DataRow row in progressTable.Rows)
            {
                if (caseCount == null) caseCount = row["case_count"].ToString();
                else caseCount = caseCount + "," + row["case_count"].ToString();

                if (dates == null) dates = row["month_day"].ToString();
                else dates = dates + "," + row["month_day"].ToString();
            }

            int c = 0;
            foreach (DataRow row in claimAmoutTable.Rows)
            {
                if (c >= 4) break;
                c++;

                if (per == null)
                {
                    if (row["per"].ToString() == "") per = "0";
                    else per = row["per"].ToString();
                }
                else
                {
                    if (row["per"].ToString() == "") per = per + "," + "0";
                    else per = per + "," + row["per"].ToString();
                }

                if (caseNumber == null) caseNumber = "'" + row["case_number"].ToString() + "'";
                else caseNumber = caseNumber + ",'" + row["case_number"].ToString() + "'";

            }




            ClientScriptManager cs1 = Page.ClientScript;

            String csname1 = "cases";
            Type cstype1 = this.GetType();
            cs1 = Page.ClientScript;

            if (!cs1.IsStartupScriptRegistered(cstype1, csname1))
            {
                StringBuilder cstext1 = new StringBuilder();
                cstext1.Append("<script type=\"text/javascript\">");
                cstext1.Append("Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,\"Segoe UI\",Roboto,\"Helvetica Neue\",Arial,sans-serif';");
                cstext1.Append("Chart.defaults.global.defaultFontColor = '#292b2c';");
                cstext1.Append("var ctx = document.getElementById(\"progressChart\");");
                cstext1.Append("var myLineChart = new Chart(ctx, {");
                cstext1.Append("type: 'line',");
                cstext1.Append("data: {");
                cstext1.Append("labels: [" + dates + "],");
                cstext1.Append("datasets: [{");
                cstext1.Append("label: \"Cases\",");
                cstext1.Append("backgroundColor: \"rgba(0, 181, 204, 1)\",");
                cstext1.Append("borderColor: \"rgba(0, 181, 204, 1)\",");
                cstext1.Append("data: [" + caseCount + "],");
                cstext1.Append("}],");
                cstext1.Append("},");
                cstext1.Append("options: {");
                cstext1.Append("scales: {");
                cstext1.Append("xAxes: [{");
                cstext1.Append("stacked: true,");
                cstext1.Append("time: {");
                cstext1.Append("unit: 'date'");
                cstext1.Append("},");
                cstext1.Append("gridLines: {");
                cstext1.Append("display: true");
                cstext1.Append("},");
                cstext1.Append("ticks: {");
                cstext1.Append("maxTicksLimit: 31");
                cstext1.Append("}");
                cstext1.Append("}],");
                cstext1.Append("yAxes: [{");
                cstext1.Append("stacked: true,");
                cstext1.Append("ticks: {");
                cstext1.Append("min: 0,");
                cstext1.Append("max: 10,");
                cstext1.Append("maxTicksLimit: 5");
                cstext1.Append("}");
                cstext1.Append("}],");
                cstext1.Append("},");
                cstext1.Append("legend: {");
                cstext1.Append("display: true");
                cstext1.Append(" }");
                cstext1.Append("}");
                cstext1.Append("});");
                cstext1.Append("</script>\");");

                cstext1.Append("<script type=\"text/javascript\">");
                cstext1.Append("Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,\"Segoe UI\",Roboto,\"Helvetica Neue\",Arial,sans-serif';");
                cstext1.Append("Chart.defaults.global.defaultFontColor = '#292b2c';");
                cstext1.Append("var ctx = document.getElementById(\"amountChart\");");
                cstext1.Append("var myLineChart = new Chart(ctx, {");
                cstext1.Append("type: 'bar',");
                cstext1.Append("data: {");
                cstext1.Append("labels: [" + caseNumber + "],");
                cstext1.Append("datasets: [{");
                cstext1.Append("label: \"Percentage\",");
                cstext1.Append("backgroundColor: \"rgba(0, 181, 204, 1)\",");
                cstext1.Append("borderColor: \"rgba(0, 181, 204, 1)\",");
                cstext1.Append("data: [" + per + "],");
                cstext1.Append("}],");
                cstext1.Append("},");
                cstext1.Append("options: {");
                cstext1.Append("scales: {");
                cstext1.Append("xAxes: [{");
                cstext1.Append("gridLines: {");
                cstext1.Append("display: true");
                cstext1.Append("},");
                cstext1.Append("ticks: {");
                cstext1.Append("maxTicksLimit: 10");
                cstext1.Append("}");
                cstext1.Append("}],");
                cstext1.Append("yAxes: [{");
                cstext1.Append("stacked: true,");
                cstext1.Append("ticks: {");
                cstext1.Append("min: 0,");
                cstext1.Append("max: 100,");
                cstext1.Append("maxTicksLimit: 5");
                cstext1.Append("}");
                cstext1.Append("}],");
                cstext1.Append("},");
                cstext1.Append("legend: {");
                cstext1.Append("display: true");
                cstext1.Append(" }");
                cstext1.Append("}");
                cstext1.Append("});");
                cstext1.Append("</script>\");");

                cs1.RegisterStartupScript(cstype1, csname1, cstext1.ToString());

            }

            DailyCaseList = dashboardCardController.GeCompanyListDailyMonth(true, false);
            DailyTotal = 0;

            foreach (DataRow row in DailyCaseList.Rows)
            {
                StringBuilder cstextCard = new StringBuilder();

                cstextCard.Append(" <div class=\"row\"> <div class=\"col-8\"><h4 class=\"small\">");
                cstextCard.Append(row["company_name"].ToString());
                cstextCard.Append("</h4> </div> <div class=\"col-4\"> <h4 class=\"small d-flex flex-row-reverse\">");
                cstextCard.Append(row["case_count"].ToString());
                cstextCard.Append("</h4> </div> </div>");

                ltDailyCase.Text += cstextCard;
                DailyTotal += Convert.ToInt32(row["case_count"]);
            }



            MonthCaseList = dashboardCardController.GeCompanyListDailyMonth(false, true);
            MonthlyTotal = 0;

            foreach (DataRow row in MonthCaseList.Rows)
            {
                StringBuilder cstextCard = new StringBuilder();

                cstextCard.Append(" <div class=\"row\"> <div class=\"col-8\"><h4 class=\"small\">");
                cstextCard.Append(row["company_name"].ToString());
                cstextCard.Append("</h4> </div> <div class=\"col-4\"> <h4 class=\"small d-flex flex-row-reverse\">");
                cstextCard.Append(row["case_count"].ToString());
                cstextCard.Append("</h4> </div> </div>");

                ltMonthlyCase.Text += cstextCard;
                MonthlyTotal += Convert.ToInt32(row["case_count"]);
            }


        }
    }
}