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
    }
}
