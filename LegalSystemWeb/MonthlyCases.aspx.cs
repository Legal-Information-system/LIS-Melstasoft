using LegalSystemCore.Common;
using LegalSystemCore.Controller;
using LegalSystemCore.Domain;
using LegalSystemCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LegalSystemWeb
{

    public partial class MonthlyCases : System.Web.UI.Page
    {
        IUserRolePrivilegeController userRolePrivilegeController = ControllerFactory.CreateUserRolePrivilegeController();
        IUserPrivilegeController userPrivilegeController = ControllerFactory.CreateUserPrivilegeController();
        ICaseMasterController caseMasterController = ControllerFactory.CreateCaseMasterController();
        List<CaseMaster> caseMasterList = new List<CaseMaster>();
        ILawyerController lawyerController = ControllerFactory.CreateLawyerController();
        ICaseActivityController caseActivityController = ControllerFactory.CreateCaseActivityController();
        List<Lawyer> lawyers = new List<Lawyer>();
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
                    if (!((userRolePrivilegeController.GetUserRolePrivilegeListByRole(Session["User_Role_Id"].ToString()).Where(x => x.FunctionId == 19).Any()
                    && !(userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 19 && x.IsGrantRevoke == 0))) ||
                    userPrivilegeController.GetUserPrivilegeList(Convert.ToInt32(Session["User_Id"])).Any(x => x.FunctionId == 19 && x.IsGrantRevoke == 1)))
                    {
                        Response.Redirect("404.aspx");
                    }
                    else
                    {
                        dvCompany.Visible = false;
                        dvCompanyUnit.Visible = false;
                        companyUnit.Visible = false;
                        company.Visible = false;
                        txtYear.Text = string.Empty;
                        btnPrint.Visible = false;
                        dvDate.Visible = false;
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

        protected void rbFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            ltDetails.Text = string.Empty;
            ltDate.Text = string.Empty;
            dvDate.Visible = true;
            dvCompany.Visible = false;
            dvCompanyUnit.Visible = false;
            companyUnit.Visible = false;
            company.Visible = false;
            btnPrint.Visible = false;
            rbCompany.ClearSelection();
            rbCompanyUnit.ClearSelection();
            txtYear.Text = string.Empty;
            if (rbSelectOption.SelectedValue == "0")
            {
                ltDate.Text = "Next Action Month and Year";
            }
            else
            {
                ltDate.Text = "Case Opened Month and Year";
            }
            ltDetails.Text = string.Empty;
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
            rbCompany.ClearSelection();
            rbCompanyUnit.ClearSelection();
        }

        protected void ddlMonthChanged(object sender, EventArgs e)
        {
            if (txtYear.Text.All(x => "0123456789".Contains(x)) && !(Int32.Parse(txtYear.Text) > Int32.Parse(DateTime.Now.ToString(("dd/MM/yyyy").Split('/')[2])) || Int32.Parse(txtYear.Text) < 0))
            {
                if (rbCompany.SelectedValue == "0" || rbCompany.SelectedValue == "1")
                {
                    DataLoad();
                }
            }
        }

        protected void YearChanged(object sender, EventArgs e)
        {
            company.Visible = false;
            ltDetails.Text = string.Empty;
            btnPrint.Visible = false;
            rbCompany.ClearSelection();
            if (txtYear.Text.All(x => "0123456789".Contains(x)))
            {
                if (Int32.Parse(txtYear.Text) > Int32.Parse(DateTime.Now.ToString(("dd/MM/yyyy").Split('/')[2])) || Int32.Parse(txtYear.Text) < 0)
                {
                    lblMsg.Text = "Year is not valid";
                }
                else
                {
                    lblMsg.Text = string.Empty;

                    company.Visible = true;
                }
            }
            else
            {
                lblMsg.Text = "Enter a valid year ( yyyy )";
            }


        }
        protected void DataLoad()
        {
            StringBuilder cstextCard = new StringBuilder();

            List<CaseMaster> caseMasterListDataLoad = new List<CaseMaster>();
            caseMasterList = caseMasterController.GetCaseMasterList(true);
            //caseMasterListDataLoad = caseMasterList;
            if (txtYear.Text != string.Empty)
            {
                List<CaseActivity> caseActivities = caseActivityController.GetUpdateCaseList(true);
                lawyers = lawyerController.GetLawyerList(true);
                if (rbSelectOption.SelectedValue == "0")
                {
                    foreach (CaseMaster global in caseMasterList)
                    {
                        string newString = txtYear.Text;
                        string newStringMonth = ddlMonth.SelectedValue;

                        foreach (CaseActivity activity in caseActivities.Where(x => x.CaseNumber == global.CaseNumber))
                        {

                            string newString2 = activity.NextDate.ToString("dd/MM/yyyy").Split('/')[2];
                            string newStringMonth2 = activity.NextDate.ToString("dd/MM/yyyy").Split('/')[1];
                            if (newString2 == newString && newStringMonth == newStringMonth2)
                            {
                                caseMasterListDataLoad.Add(global);
                            }
                        }
                    }
                }
                else
                {
                    foreach (CaseMaster global in caseMasterList)
                    {
                        string newString = txtYear.Text;
                        string newStringMonth = ddlMonth.SelectedValue;
                        string newString2 = global.CaseOpenDate.ToString("dd/MM/yyyy").Split('/')[2];
                        string newStringMonth2 = global.CaseOpenDate.ToString("dd/MM/yyyy").Split('/')[1];
                        if (newString2 == newString && newStringMonth == newStringMonth2)
                        {
                            caseMasterListDataLoad.Add(global);
                        }
                    }
                }

                //caseMasterListDataLoad = caseMasterListDataLoad.Where(x => x.CreatedDate.ToString("dd/MM/yyyy") == DateTime.Parse(txtCaseOpenDate.Text).ToString("dd/MM/yyyy")).ToList();
                if (rbCompany.SelectedValue == "0" && txtYear.Text != string.Empty)
                {

                }
                else if (rbCompany.SelectedValue == "1" && txtYear.Text != string.Empty)
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
                                "                                      <th scope=\"col\">Court</th>\r\n    " +

                                "                                    <th scope=\"col\">Next Step</th>\r\n     " +
                                "                                    <th scope=\"col\">Next Step Date</th>\r\n     " +
                                "                                    <th scope=\"col\">Counsellor</th>\r\n     " +
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
                                cstextCard.Append(caseMasterCompanyUnit.court.CourtName);

                                cstextCard.Append("</td>\r\n                                        <td>");
                                if (caseActivities.Where(x => x.CaseNumber == caseMasterCompanyUnit.CaseNumber).Any())
                                {
                                    CaseActivity caseActivity = new CaseActivity();
                                    caseActivity = caseActivities.Where(x => x.CaseNumber == caseMasterCompanyUnit.CaseNumber).OrderByDescending(r => r.ActivityDate).FirstOrDefault();
                                    cstextCard.Append(caseActivity.nextAction.ActionName);
                                    cstextCard.Append("</td>\r\n                                        <td>");
                                    cstextCard.Append(caseActivity.NextDate.ToString("dd/MM/yyyy"));
                                    cstextCard.Append("</td>\r\n                                        <td>");
                                }
                                else
                                {
                                    cstextCard.Append("N/A</td>\r\n                                        <td>");
                                    cstextCard.Append("N/A</td>\r\n                                        <td>");
                                }
                                if (lawyers.Any(x => caseMasterCompanyUnit.counselors.Any(y => y.LawyerId == x.LawyerId)))
                                {
                                    var flag = 0;
                                    foreach (Lawyer lawyer in lawyers.Where(x => caseMasterCompanyUnit.counselors.Any(y => y.LawyerId == x.LawyerId)))
                                    {
                                        if (flag == 0)
                                        {
                                            cstextCard.Append(lawyer.LawyerName);
                                            flag = 1;
                                        }
                                        else
                                        {
                                            cstextCard.Append("<br />" + lawyer.LawyerName);
                                        }
                                    }

                                }
                                cstextCard.Append("</td>\r\n                                    </tr>");
                            }
                            cstextCard.Append("</tbody>\r\n                            </table>");

                        }

                        cstextCard.Append(" </div>\r\n                            </div>\r\n<div style=\"break-after:page\"></div>                          ");
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
