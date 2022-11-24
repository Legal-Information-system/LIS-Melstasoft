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
        public static ILawyerDAO CreateLawyerDAO()
        {
            ILawyerDAO lawyerDAO = new LawyerSqlDAOImpl();
            return (ILawyerDAO)lawyerDAO;
        }

        public static IActivityDAO CreateActivityDAO()
        {
            IActivityDAO activityDAO = new ActivityDAOSqlImpl();
            return (IActivityDAO)activityDAO;
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

        public static ICourtLocationDAO CreatCourtLocationDAO()
        {
            ICourtLocationDAO courtlocationDAO = new CourtLocationSqlImpl();
            return (ICourtLocationDAO)courtlocationDAO;
        }

        public static IPaymentStatusDAO CreatePaymentStatusDAO()
        {
            IPaymentStatusDAO paymentStatusDAO = new PaymentStatusDAOSqlImpl();
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
            return (IUserLoginDAO)userLoginDAO;
        }

        public static ICaseNatureDAO CreateCaseNatureDAO()
        {
            ICaseNatureDAO caseNatureDAO = new CaseNatureSqlDAOImpl();
            return ((ICaseNatureDAO)caseNatureDAO);
        }

        public static IPaymentDAO CreatePaymentDAO()
        {
            IPaymentDAO paymentDAO = new PaymentDAOSqlImpl();
            return ((IPaymentDAO)paymentDAO);
        }

        public static IPaymentActivityDAO CreatePaymentActivityDAO()
        {
            IPaymentActivityDAO paymentActivityDAO = new PaymentActivityDAOSqlImpl();
            return ((IPaymentActivityDAO)paymentActivityDAO);
        }

        public static ICaseMasterDAO CreateCaseMasterDAO()
        {
            ICaseMasterDAO caseMasterDAO = new CaseMasterDAOSqlImpl();
            return ((ICaseMasterDAO)caseMasterDAO);
        }

        public static IDocumentDAO CreateDocumentDAO()
        {
            IDocumentDAO documentDAO = new DocumentDAOSqlImpl();
            return (IDocumentDAO)documentDAO;
        }

        public static IDocumentCaseDAO CreateDocumentCaseDAO()
        {
            IDocumentCaseDAO documentCaseDAO = new DocumentCaseDAOSqlImpl();
            return (IDocumentCaseDAO)documentCaseDAO;
        }
    }
}
