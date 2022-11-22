using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegalSystemCore.Controller;

namespace LegalSystemCore.Common
{
    public class ControllerFactory
    {
        public static ICompanyController CreateCompanyController()
        {
            ICompanyController companyController = new CompanyControllerImpl();
            return (ICompanyController)companyController;
        }

        public static ICompanyUnitController CreateCompanyUnitController()
        {
            ICompanyUnitController companyUnitController = new CompanyUnitControllerImpl();
            return (ICompanyUnitController)companyUnitController;
        }

        public static ILocationController CreateLocationController()
        {
            ILocationController locationController = new LocationControllerImpl();
            return (ILocationController)locationController;
        }

        public static ICourtController CreateCourtController()
        {
            ICourtController courtController = new CourtControllerImpl();
            return (ICourtController)courtController;
        }

        public static IUserRoleController CreateUserRoleController()
        {
            IUserRoleController userRoleController = new UserControllerImpl();
            return (IUserRoleController)userRoleController;
        }
    }
}
