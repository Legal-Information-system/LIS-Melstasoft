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
        int Save(CaseActivity caseActivity);
        int Update(CaseActivity caseActivity);
        List<CaseActivity> GetUpdateCaseList();
    }
    public class CaseActivityControllerImpl : ICaseActivityController
    {
        ICaseActivityDAO caseActivityDAO = DAOFactory.CreateCaseActivityDAO();

        public List<CaseActivity> GetUpdateCaseList()
        {
            DbConnection dbConnection = null;
            List<CaseActivity> listCaseActivity = new List<CaseActivity>();
            try
            {
                dbConnection = new DbConnection();
                listCaseActivity = caseActivityDAO.GetCaseActivityList(dbConnection);
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

        public int Save(CaseActivity caseActivity)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return caseActivityDAO.Save(caseActivity, dbConnection);
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
