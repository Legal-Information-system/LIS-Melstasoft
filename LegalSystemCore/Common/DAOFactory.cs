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
    }
}
