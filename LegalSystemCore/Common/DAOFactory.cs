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

        public static ILocationDAO CreateLocationDAO()
        {
            ILocationDAO locationDAO = new LocationSqlDAOImpl();
            return (ILocationDAO)locationDAO;
        }

        public static ICourtDAO CreateCourtDAO()
        {
            ICourtDAO courtDAO = new CourtSqlDAOImpl();
            return (ICourtDAO)courtDAO;
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

        public static IPaymentStatusDAO CreatePaymentStatusDAO()
        {
            IPaymentStatusDAO paymentStatusDAO = new PaymentDAOSqlImpl();
            return (IPaymentStatusDAO)paymentStatusDAO;
        }

        public static IJudgementTypeDAO CreateJudgementTypeDAO()
        {
            IJudgementTypeDAO judgementTypeDAO = new JudgementTypeDAOSqlImpl();
            return (IJudgementTypeDAO)judgementTypeDAO;
        }

        public static IUserLoginDAO CreateUserLoginDAO()
        {
            IUserLoginDAO userLoginDAO = new UserLoginSqlDAOImpl();
            return(IUserLoginDAO)userLoginDAO;
        }
    }
}
