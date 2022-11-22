using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LegalSystemCore.Common
{
    public class DAOFactory
    {
        public static ICompanyDAO CreateCompanyDAO()
        {
            ICompanyDAO companyDAO = new CompanySqlDAOImpl();
            return (ICompanyDAO)companyDAO;
        }

        public static ICompanyUnitDAO CreateCompanyUnitDAO()
        {
            ICompanyUnitDAO companyUnitDAO = new CompanyUnitSqlDAOImpl();
            return (ICompanyUnitDAO)companyUnitDAO;
        }

        public static IUserRoleDAO CreateUserRoleDAO()
        {
            IUserRoleDAO userRoleDAO = new UserRoleDAOSqlImpl();
            return (IUserRoleDAO)userRoleDAO;
        }

        public static ICaseActionDAO CreateCaseActionDAO()
        {
            ICaseActionDAO caseActionDAO = new CaseActionDAOSqlImpl();
            return (ICaseActionDAO)caseActionDAO;
        }

        public static ICaseStatusDAO CreateCaseStatusDAO()
        {
            ICaseStatusDAO caseStatusDAO = new CaseStatusDAOSqlImpl();
            return (ICaseStatusDAO)caseStatusDAO;
        }
    }
}
