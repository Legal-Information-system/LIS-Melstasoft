﻿using System;
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

        public static IActivityController CreateActivityController()
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

        public static IPaymentController CreatePaymentController()
        {
            IPaymentController paymentController = new PaymentControllerImpl();
            return (IPaymentController)paymentController;
        }

        public static IPaymentActivityController CreatePaymentActivityController()
        {
            IPaymentActivityController paymentActivityController = new PaymentActivityControllerImpl();
            return (IPaymentActivityController)paymentActivityController;
        }

        public static ICaseActivityController CreateCaseActivityController()
        {
            ICaseActivityController caseActivityController = new CaseActivityControllerImpl();
            return (ICaseActivityController)caseActivityController;
        }

        public static IDocumentPaymentController CreateDocumentPaymentController()
        {
            IDocumentPaymentController documentPaymentController = new DocumentPaymentControllerImpl();
            return (IDocumentPaymentController)documentPaymentController;
        }

        public static IDashboardCardController CreateDashboardCardController()
        {
            IDashboardCardController dashboardCardController = new DashBoardCardControllerSqlImpl();
            return (IDashboardCardController)dashboardCardController;
        }

        public static ICounselorController CreateCounselorController()
        {
            ICounselorController counselorController = new CounselorControllerImpl();
            return ((ICounselorController)counselorController);
        }

        public static IPartyController CreatePartyController()
        {
            IPartyController partyController = new PartyControllerImpl();
            return ((IPartyController)partyController);
        }

        public static IPartyCaseController CreatePartyCaseController()
        {
            IPartyCaseController partyCaseController = new PartyCaseControllerImpl();
            return (IPartyCaseController)partyCaseController;
        }

        public static IFunctionsController CreateFunctionsController()
        {
            IFunctionsController functionsController = new FunctionsControllerImpl();
            return (IFunctionsController)functionsController;
        }

        public static IUserPrivilegeController CreateUserPrivilegeController()
        {
            IUserPrivilegeController userPrivilegeController = new UserPrivilegeControllerImpl();
            return (IUserPrivilegeController)userPrivilegeController;
        }

        public static IUserRolePrivilegeController CreateUserRolePrivilegeController()
        {
            IUserRolePrivilegeController userRolePrivilegeController = new UserRolePrivilegeControllerImpl();
            return (IUserRolePrivilegeController)userRolePrivilegeController;
        }

        public static IDocumentCaseActivityController CreateDocumentCaseActivityController()
        {
            IDocumentCaseActivityController documentCaseActivityController = new DocumentCaseActivityControllerImpl();
            return (IDocumentCaseActivityController)documentCaseActivityController;
        }
    }
}
