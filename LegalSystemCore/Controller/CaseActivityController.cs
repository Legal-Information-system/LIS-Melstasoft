using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface ICaseActivityController
    {
        int Save(CaseActivity caseActivity, bool withNextDate);
        int Update(CaseActivity caseActivity);
        List<CaseActivity> GetUpdateCaseList(bool withMatchData);
    }
    public class CaseActivityControllerImpl : ICaseActivityController
    {
        ICaseActivityDAO caseActivityDAO = DAOFactory.CreateCaseActivityDAO();

        public List<CaseActivity> GetUpdateCaseList(bool withMatchData)
        {
            DbConnection dbConnection = null;
            List<CaseActivity> listCaseActivity = new List<CaseActivity>();
            try
            {
                dbConnection = new DbConnection();
                listCaseActivity = caseActivityDAO.GetCaseActivityList(dbConnection);

                if (withMatchData)
                {
                    ILawyerController lawyerController = ControllerFactory.CreateLawyerController();
                    List<Lawyer> lawyerlist = lawyerController.GetLawyerList();

                    ICaseActionController caseActionController = ControllerFactory.CreateCaseActionController();
                    List<CaseAction> caseActionsList = caseActionController.GetCaseActionList();

                    foreach (var caseActivity in listCaseActivity)
                    {
                        caseActivity.assignAttorney = lawyerlist.Where(l => l.LawyerId == caseActivity.AssignAttorneyId).Single();
                        caseActivity.counsilor = lawyerlist.Where(l => l.LawyerId == caseActivity.CounsilorId).Single();
                        caseActivity.actionTaken = caseActionsList.Where(l => l.ActionId == caseActivity.ActionTakenId).Single();
                        caseActivity.nextAction = caseActionsList.Where(l => l.ActionId == caseActivity.NextActionId).Single();
                    }
                }
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
            return listCaseActivity;
        }

        public int Save(CaseActivity caseActivity, bool withNextDate)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return caseActivityDAO.Save(caseActivity, withNextDate, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int Update(CaseActivity caseActivity)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return caseActivityDAO.Update(caseActivity, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
    }
}
