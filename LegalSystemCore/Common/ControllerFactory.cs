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

        public static ICaseActionController CreateCaseActionController()
        {
            ICaseActionController caseActionController = new CaseActionControllerImpl();
            return (ICaseActionController)caseActionController;
        }

        public static ICourtLocationController CreateCourtLocationController()
        {
            ICourtLocationController casecourtlocationController = new CourtLocationControllerImpl();
            return (ICourtLocationController)casecourtlocationController;
        }

        public static IUserLoginController CreateUserLoginController()
        {
            IUserLoginController userLoginController = new UserLoginControllerImpl();
            return (IUserLoginController)userLoginController;
        }

        public static ICaseStatusController CreateCaseStatusController()
        {
            ICaseStatusController caseStatusController = new CaseStatusController();
            return (ICaseStatusController)caseStatusController;
        }

        public static IPaymentStatusController CreatePaymentStatusController()
        {
            IPaymentStatusController paymentStatusController = new PaymentStatusControllerImpl();
            return (IPaymentStatusController)paymentStatusController;
        }

        public static IJudgementTypeController CreateJudgementTypeController()
        {
            IJudgementTypeController judgementTypeController = new JudgementTypeControllerImpl();
            return (IJudgementTypeController)judgementTypeController;
        }

        public static ILawyerController CreateLawyerController()
        {
            ILawyerController lawyerController = new LawyerControllerImpl();
            return (ILawyerController)lawyerController;
        }

        public static IActivityController CreateAddActivity()
        {
            IActivityController activityController = new ActivityControllerImpl();
            return (IActivityController)activityController;
        }

        public static ICaseNatureController CreateCaseNatureController()
        {
            ICaseNatureController caseNatureController = new CaseNatureControllerImpl();
            return (ICaseNatureController)caseNatureController;
        }

        public static ICaseMasterController CreateCaseMasterController()
        {
            ICaseMasterController caseMasterController = new CaseMasterControllerImpl();
            return (ICaseMasterController)caseMasterController;
        }

        public static IDocumentController CreateDocumentController()
        {
            IDocumentController documentController = new DocumentControllerImpl();
            return (IDocumentController)documentController;
        }

        public static IDocumentCaseController CreateDocumentCaseController()
        {
            IDocumentCaseController documentCaseController = new DocumentCaseControllerImpl();
            return (IDocumentCaseController)documentCaseController;
        }
    }
}
