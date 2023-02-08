﻿using LegalSystemCore.Common;
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
        int Update(CaseActivity caseActivity, bool nextDate);

        int Delete(CaseActivity caseActivity);
        List<CaseActivity> GetUpdateCaseList(bool withMatchData);

        CaseActivity GetCaseActivity(string caseActivityId, bool withMatchData);
    }
    public class CaseActivityControllerImpl : ICaseActivityController
    {
        ICaseActivityDAO caseActivityDAO = DAOFactory.CreateCaseActivityDAO();

        public CaseActivity GetCaseActivity(string caseActivityId, bool withMatchData)
        {
            DbConnection dbConnection = null;
            CaseActivity caseActivity = new CaseActivity();
            try
            {
                dbConnection = new DbConnection();
                caseActivity = caseActivityDAO.GetCaseActivity(caseActivityId, dbConnection);
                if (withMatchData)
                {
                    ILawyerController lawyerController = ControllerFactory.CreateLawyerController();
                    List<Lawyer> lawyerlist = lawyerController.GetLawyerList(true);

                    ICaseActionController caseActionController = ControllerFactory.CreateCaseActionController();
                    List<CaseAction> caseActionsList = caseActionController.GetCaseActionList(true);


                    caseActivity.assignAttorney = lawyerlist.Where(l => l.LawyerId == caseActivity.AssignAttorneyId).Single();
                    caseActivity.counsilor = lawyerlist.Where(l => l.LawyerId == caseActivity.CounsilorId).Single();
                    caseActivity.actionTaken = caseActionsList.Where(l => l.ActionId == caseActivity.ActionTakenId).Single();
                    caseActivity.nextAction = caseActionsList.Where(l => l.ActionId == caseActivity.NextActionId).Single();

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
            return caseActivity;
        }
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
                    List<Lawyer> lawyerlist = lawyerController.GetLawyerList(true);

                    ICaseActionController caseActionController = ControllerFactory.CreateCaseActionController();
                    List<CaseAction> caseActionsList = caseActionController.GetCaseActionList(true);

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

        public int Update(CaseActivity caseActivity, bool nextDate)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return caseActivityDAO.Update(caseActivity, nextDate, dbConnection);
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

        public int Delete(CaseActivity caseActivity)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return caseActivityDAO.Delete(caseActivity, dbConnection);
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
