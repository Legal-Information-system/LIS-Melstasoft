using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface ICaseStatusController
    {
        int Save(CaseStatus caseStatus);
        int Update(CaseStatus caseStatus);
        List<CaseStatus> GetCaseStatusList();
    }

    public class CaseStatusController : ICaseStatusController
    {
        ICaseStatusDAO caseStatusDAO = DAOFactory.CreateCaseStatusDAO();

        public List<CaseStatus> GetCaseStatusList()
        {
            DbConnection dbConnection = null;
            List<CaseStatus> listCaseStatus = new List<CaseStatus>();
            try
            {
                dbConnection = new DbConnection();
                listCaseStatus = caseStatusDAO.GetCaseStatusList(dbConnection);
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
            return listCaseStatus;
        }

        public int Save(CaseStatus caseStatus)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return caseStatusDAO.Save(caseStatus, dbConnection);
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

        public int Update(CaseStatus caseStatus)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return caseStatusDAO.Update(caseStatus, dbConnection);
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
