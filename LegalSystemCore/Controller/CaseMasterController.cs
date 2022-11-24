using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalSystemCore.Controller
{
    public interface ICaseMasterController
    {
        int Save(CaseMaster caseMaster);
        int Update(CaseMaster caseMaster);
        int Delete(CaseMaster caseMaster);
        List<CaseMaster> GetCaseMasterList();
        CaseMaster GetCaseMaster(string caseNumber);
    }

    public class CaseMasterControllerImpl : ICaseMasterController
    {

        ICaseMasterDAO caseMasterDAO = DAOFactory.CreateCaseMasterDAO();

        public CaseMaster GetCaseMaster(string caseNumber)
        {
            DbConnection dbConnection = null;
            CaseMaster caseMaster = new CaseMaster();
            try
            {
                dbConnection = new DbConnection();
                caseMaster = caseMasterDAO.GetCaseMaster(caseNumber, dbConnection);
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
            return caseMaster;
        }

        public List<CaseMaster> GetCaseMasterList()
        {
            DbConnection dbConnection = null;
            List<CaseMaster> listCaseMaster = new List<CaseMaster>();
            try
            {
                dbConnection = new DbConnection();
                listCaseMaster = caseMasterDAO.GetCaseMasterList(dbConnection);
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
            return listCaseMaster;
        }

        public int Save(CaseMaster caseMaster)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return caseMasterDAO.Save(caseMaster, dbConnection);
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

        public int Update(CaseMaster caseMaster)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return caseMasterDAO.Update(caseMaster, dbConnection);
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

        public int Delete(CaseMaster caseMaster)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return caseMasterDAO.Delete(caseMaster, dbConnection);
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
