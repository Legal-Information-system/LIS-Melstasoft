using LegalSystemCore;
using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{
    public partial class DailyCases : System.Web.UI.Page
    {
        ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
        List<CaseMaster> caseMasterList = new List<CaseMaster>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User_Id"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    if (Session["User_Role_Id"].ToString() != "1")
                    {
                        Response.Redirect("404.aspx");
                    }
                    else
                    {

                        //BindCaseList();
                        BindCompanyList();
                        BindCompanyUnitList();
                    }
                }
            }
        }

        //private void BindCaseList()
        //{


        //    int companyUnitId = Convert.ToInt32(Session["company_unit_id"].ToString());
        //    int companyId = Convert.ToInt32(Session["company_id"].ToString());
        //    int UserRoleId = Convert.ToInt32(Session["User_Role_Id"].ToString());

        //    if (UserRoleId == 4)
        //        caseMasterList = caseMasterList.Where(c => c.CompanyId == companyId).ToList();
        //    if (UserRoleId == 5)
        //        caseMasterList = caseMasterList.Where(c => c.CompanyUnitId == companyUnitId).ToList();
        //    if (ddlCompany.SelectedValue != "" && ddlCompanyUnit.SelectedValue != "")
        //    {
        //        ddlCaseNo.DataSource = caseMasterList.Where(x => x.CompanyId == Int32.Parse(ddlCompany.SelectedValue)).Where(y => y.CompanyUnitId == Int32.Parse(ddlCompanyUnit.SelectedValue));
        //    }

        //    ddlCaseNo.DataValueField = "CaseNumber";
        //    ddlCaseNo.DataTextField = "CaseNumber";
        //    ddlCaseNo.DataBind();

        //    ddlCaseNo.Items.Insert(0, new ListItem("-- select case --", "0"));

        //}
        private void BindCompanyList()
        {
            ICompanyController companyController = ControllerFactory.CreateCompanyController();
            List<Company> companyList = companyController.GetCompanyList(false);



            ddlCompany.DataSource = companyList;
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("-- select company --", "0"));

        }
        private void BindCompanyUnitList()
        {
            ICompanyUnitController companyUnitController = ControllerFactory.CreateCompanyUnitController();
            if (ddlCompany.SelectedValue != "")
            {
                List<CompanyUnit> companyUnitList = companyUnitController.GetCompanyUnitListFilter(false, ddlCompany.SelectedValue);



                ddlCompanyUnit.DataSource = companyUnitList;
                ddlCompanyUnit.DataValueField = "CompanyUnitId";
                ddlCompanyUnit.DataTextField = "CompanyUnitName";

                ddlCompanyUnit.DataBind();

            }
            else
            {
                ddlCompanyUnit.Items.Clear();
            }
            ddlCompanyUnit.Items.Insert(0, new ListItem("-- select company unit--", "0"));
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCompanyUnitList();
            //BindCaseList();
        }

        protected void ddlCompanyUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindCaseList();
            DataLoad();
        }

        protected void DataLoad()
        {
            StringBuilder cstextCard = new StringBuilder();

            List<CaseMaster> caseMasterListDataLoad = new List<CaseMaster>();
            caseMasterList = caseMasterController.GetCaseMasterList(true);
            caseMasterListDataLoad = caseMasterList;
            caseMasterListDataLoad = caseMasterListDataLoad.Where(x => x.CompanyId == Int32.Parse(ddlCompany.SelectedValue)).ToList();
            caseMasterListDataLoad = caseMasterListDataLoad.Where(y => y.CompanyUnitId == Int32.Parse(ddlCompanyUnit.SelectedValue)).ToList();
            foreach (CaseMaster caseMaster in caseMasterListDataLoad.GroupBy(x => x.CompanyId).Select(y => y.First()))
            {
                cstextCard.Append("<div class=\"card\" style=\"width: 100%\">");
                cstextCard.Append("<div class=\"card-body\" style=\"padding-left: 30px;\">\r\n " +
                    "                                   <div class=\"row mb-1\">\r\n  " +
                    "                                      <div class=\"col-sm-6\">\r\n   " +
                    "                                         <p>Company</p>\r\n          " +
                    "                              </div>\r\n                         " +
                    "               <div class=\"col-md-6\">");
                cstextCard.Append(caseMaster.company.CompanyName);
                cstextCard.Append("</div>\r\n                                    </div>");
                cstextCard.Append("<br />\r\n            <br />");

                foreach (CaseMaster caseMasterCompany in caseMasterListDataLoad.GroupBy(x => x.CompanyUnitId).Select(y => y.First()))
                {
                    cstextCard.Append("<div class=\"row mb-1\">\r\n  " +
                            "                                      <div class=\"col-sm-6\">\r\n  " +
                            "                                          <p>Company Unit</p>\r\n    " +
                            "                                    </div>\r\n    " +
                            "                                    <div class=\"col-md-6\">");
                    cstextCard.Append(caseMasterCompany.companyUnit.CompanyUnitName);
                    cstextCard.Append("</div>\r\n                                    </div>");
                    cstextCard.Append("<br />");

                    foreach (CaseMaster caseMasterCompanyUnit in caseMasterListDataLoad.Where(x => x.CompanyUnitId == caseMasterCompany.CompanyUnitId))
                    {
                        cstextCard.Append("<div class=\"row mb-1\">\r\n  " +
                            "                                      <div class=\"col-sm-6\">\r\n  " +
                            "                                          <p>Case Number</p>\r\n    " +
                            "                                    </div>\r\n    " +
                            "                                    <div class=\"col-md-6\">");
                        cstextCard.Append(caseMasterCompanyUnit.CaseNumber);
                        cstextCard.Append("</div>\r\n                                    </div>");
                        cstextCard.Append("<div class=\"row mb-1\">\r\n  " +
                            "                                      <div class=\"col-sm-6\">\r\n  " +
                            "                                          <p>Case Created Date</p>\r\n    " +
                            "                                    </div>\r\n    " +
                            "                                    <div class=\"col-md-6\">");
                        cstextCard.Append(caseMasterCompanyUnit.CreatedDate);
                        cstextCard.Append("</div>\r\n                                    </div>");
                        cstextCard.Append("<div class=\"row mb-1\">\r\n  " +
                            "                                      <div class=\"col-sm-6\">\r\n  " +
                            "                                          <p>Case Opened Date</p>\r\n    " +
                            "                                    </div>\r\n    " +
                            "                                    <div class=\"col-md-6\">");
                        cstextCard.Append(caseMasterCompanyUnit.CaseOpenDate);
                        cstextCard.Append("</div>\r\n                                    </div>");
                        cstextCard.Append("<div class=\"row mb-1\">\r\n  " +
                            "                                      <div class=\"col-sm-6\">\r\n  " +
                            "                                          <p>Claim amount</p>\r\n    " +
                            "                                    </div>\r\n    " +
                            "                                    <div class=\"col-md-6\">");
                        cstextCard.Append(caseMasterCompanyUnit.ClaimAmount);
                        cstextCard.Append("</div>\r\n                                    </div>\r\n <br />");
                    }

                }

                cstextCard.Append(" </div>\r\n                            </div>\r\n                            </div>");
            }

            ltDetails.Text = cstextCard.ToString();


        }
    }
}