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
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
        IUserPrivilegeController userPrivilegeController = ControllerFactory.CreateUserPrivilegeController();
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
                    if (!((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 18).Any()
                    && !(userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 18 && x.IsGrantRevoke == 0))) ||
                    userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 18 && x.IsGrantRevoke == 1)))
                    {
                        Response.Redirect("404.aspx");
                    }
                    else
                    {
                        dvCompany.Visible = false;
                        dvCompanyUnit.Visible = false;
                        companyUnit.Visible = false;
                        company.Visible = false;
                        txtCaseOpenDate.Text = string.Empty;
                        btnPrint.Visible = false;
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
            ltDetails.Text = string.Empty;
            if (rbCompany.SelectedValue == "1" && rbCompanyUnit.SelectedValue == "0")
            {
                DataLoad();
            }
        }

        protected void ddlCompanyUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindCaseList();
            ltDetails.Text = string.Empty;
            if (rbCompany.SelectedValue == "1" && rbCompanyUnit.SelectedValue == "1")
            {
                DataLoad();
            }

        }



        protected void rbCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            ltDetails.Text = string.Empty;
            if (rbCompany.SelectedValue == "0")
            {
                dvCompany.Visible = false;
                dvCompanyUnit.Visible = false;
                companyUnit.Visible = false;
                DataLoad();

            }
            else if (rbCompany.SelectedValue == "1")
            {
                dvCompany.Visible = true;
                dvCompanyUnit.Visible = false;
                companyUnit.Visible = true;
                rbCompanyUnit.SelectedValue = "0";
            }
            else
            {
                dvCompany.Visible = false;
                dvCompanyUnit.Visible = false;
                companyUnit.Visible = false;
            }

        }

        protected void rbCompanyUnit_SelectedValueChanged(object sender, EventArgs e)
        {
            ltDetails.Text = string.Empty;
            if (rbCompanyUnit.SelectedValue == "0")
            {

                dvCompanyUnit.Visible = false;


            }
            else if (rbCompanyUnit.SelectedValue == "1")
            {

                dvCompanyUnit.Visible = true;

            }
            ltDetails.Text = string.Empty;

        }

        protected void DateChanged(object sender, EventArgs e)
        {
            ltDetails.Text = string.Empty;
            company.Visible = true;
            btnPrint.Visible = false;
            rbCompany.ClearSelection();
            rbCompanyUnit.ClearSelection();
        }




        protected void DataLoad()
        {
            StringBuilder cstextCard = new StringBuilder();

            List<CaseMaster> caseMasterListDataLoad = new List<CaseMaster>();
            caseMasterList = caseMasterController.GetCaseMasterList(true);
            //caseMasterListDataLoad = caseMasterList;
            if (txtCaseOpenDate.Text != string.Empty)
            {
                foreach (CaseMaster global in caseMasterList)
                {
                    string newString = DateTime.Parse(txtCaseOpenDate.Text).ToString("dd/MM/yyyy");
                    string newString2 = global.CaseOpenDate.ToString("dd/MM/yyyy");
                    if (newString2 == newString)
                    {
                        caseMasterListDataLoad.Add(global);
                    }
                }
                //caseMasterListDataLoad = caseMasterListDataLoad.Where(x => x.CreatedDate.ToString("dd/MM/yyyy") == DateTime.Parse(txtCaseOpenDate.Text).ToString("dd/MM/yyyy")).ToList();
                if (rbCompany.SelectedValue == "0" && txtCaseOpenDate.Text != string.Empty)
                {

                }
                else if (rbCompany.SelectedValue == "1" && txtCaseOpenDate.Text != string.Empty)
                {
                    caseMasterListDataLoad = caseMasterListDataLoad.Where(x => x.CompanyId == Int32.Parse(ddlCompany.SelectedValue)).ToList();
                    if (rbCompanyUnit.SelectedValue == "1")
                    {
                        caseMasterListDataLoad = caseMasterListDataLoad.Where(y => y.CompanyUnitId == Int32.Parse(ddlCompanyUnit.SelectedValue)).ToList();
                    }
                }
                if (caseMasterListDataLoad.Any())
                {
                    caseMasterListDataLoad = caseMasterListDataLoad.OrderBy(x => x.CaseOpenDate.ToString("dd/MM/yyyy")).ToList();
                    foreach (CaseMaster caseMaster in caseMasterListDataLoad.GroupBy(x => x.CompanyId).Select(y => y.First()))
                    {
                        cstextCard.Append("<div class=\"card\" style=\"width: 100%; margin-top: 60px;\">");
                        cstextCard.Append("<div class=\"card-body\" style=\"padding-left: 30px;\">\r\n " +
                            "                                   <div class=\"row mb-1\">\r\n  " +
                            "                                      <div class=\"col-sm-6\">\r\n   " +
                            "                                         <p>Company</p>\r\n          " +
                            "                              </div>\r\n                         " +
                            "               <div class=\"col-md-6\" style=\"font-weight: 900;font-size: 1.875rem;\">");
                        cstextCard.Append(caseMaster.company.CompanyName);
                        cstextCard.Append("</div>\r\n                                    </div>");
                        cstextCard.Append("<br />\r\n            <br />");

                        foreach (CaseMaster caseMasterCompany in caseMasterListDataLoad.Where(x => x.CompanyId == caseMaster.CompanyId).GroupBy(x => x.CompanyUnitId).Select(y => y.First()))
                        {
                            cstextCard.Append("<div class=\"row mb-1\" style=\"margin-top: 30px\">\r\n  " +
                                    "                                      <div class=\"col-sm-6\">\r\n  " +
                                    "                                          <p>Company Unit</p>\r\n    " +
                                    "                                    </div>\r\n    " +
                                    "                                    <div class=\"col-md-6\" style=\"font-weight: bold;\">");
                            cstextCard.Append(caseMasterCompany.companyUnit.CompanyUnitName);
                            cstextCard.Append("</div>\r\n                                    </div>");
                            cstextCard.Append("<br />");

                            cstextCard.Append("<table class=\"table\">\r\n                     " +
                                "           <thead>\r\n                    " +
                                "                <tr>\r\n                    " +
                                "                    <th scope=\"col\">Case Number</th>\r\n  " +
                                "                                      <th scope=\"col\">Created Date</th>\r\n  " +
                                "                                      <th scope=\"col\">Case Open Date</th>\r\n    " +
                                "                                    <th scope=\"col\">Claim Amount</th>\r\n     " +
                                "                               </tr>\r\n                                </thead> <tbody>");

                            foreach (CaseMaster caseMasterCompanyUnit in caseMasterListDataLoad.Where(x => x.CompanyUnitId == caseMasterCompany.CompanyUnitId))
                            {
                                cstextCard.Append("<tr>\r\n                                        <th scope=\"row\">");
                                cstextCard.Append(caseMasterCompanyUnit.CaseNumber);
                                cstextCard.Append("</th>\r\n                                        <td>");

                                cstextCard.Append(caseMasterCompanyUnit.CreatedDate.ToString("dd/MM/yyyy"));
                                cstextCard.Append("</td>\r\n                                        <td>");
                                cstextCard.Append(caseMasterCompanyUnit.CaseOpenDate.ToString("dd/MM/yyyy"));
                                cstextCard.Append("</td>\r\n                                        <td>");
                                cstextCard.Append(caseMasterCompanyUnit.ClaimAmount);
                                cstextCard.Append("</td>\r\n                                    </tr>");
                            }
                            cstextCard.Append("</tbody>\r\n                            </table>");

                        }

                        cstextCard.Append(" </div>\r\n                            </div>\r\n                            </div>");
                    }


                    btnPrint.Visible = true;
                }
                else
                {
                    btnPrint.Visible = false;
                    cstextCard.Append("<div class=\"card py-4\" style=\"width: 100%;>\r\n " +
                        "                               <div class=\"card-body\" style=\"padding-left: 30px;\">\r\n  " +
                        "                                  <div class=\"row mb-1\">\r\n            " +
                        "                            <div class=\"col-sm d-flex justify-content-center\">\r\n           " +
                        "                                 <span class=\"align-middle\" style=\"font-weight: bold;\">------ No Cases Available ------</span>\r\n  " +
                        "                                      </div>\r\n                                    </div>\r\n    " +
                        "                            </div>\r\n                            </div>");
                }
                ltDetails.Text = cstextCard.ToString();


            }
        }
    }
}