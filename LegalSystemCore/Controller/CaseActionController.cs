using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface ICaseActionController
    {
        int Save(CaseAction caseAction);
        int Update(CaseAction caseAction);
        List<CaseAction> GetCaseActionList();
    }

    public class CaseActionControllerImpl : ICaseActionController
    {
        ICaseActionDAO caseActionDAO = DAOFactory.CreateCaseActionDAO();
        public List<CaseAction> GetCaseActionList()
        {
            DbConnection dbConnection = null;
            List<CaseAction> listCaseAction = new List<CaseAction>();
            try
            {
                dbConnection = new DbConnection();
                listCaseAction = caseActionDAO.GetCaseActionList(dbConnection);
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
            return listCaseAction;
        }

        public int Save(CaseAction caseAction)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return caseActionDAO.Save(caseAction, dbConnection);
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

        public int Update(CaseAction caseAction)
        {

            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return caseActionDAO.Update(caseAction, dbConnection);
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
