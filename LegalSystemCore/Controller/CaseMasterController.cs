using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface ICaseMasterController
    {
        int Save(CaseMaster caseMaster);
        int Update(CaseMaster caseMaster);
        List<CaseMaster> GetCaseMasterList();

    }
    public class CaseMasterControllerImpl : ICaseMasterController
    {
        ICaseMasterDAO caseMasterDAO = DAOFactory.CreateCaseMasterDAO();
        public int Save(CaseMaster caseMaster)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return caseMasterDAO.Save(caseMaster, dbconnection);
            }
            catch (Exception)
            {
                dbconnection.RollBack();
                throw;
            }
            finally
            {
                if (dbconnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbconnection.Commit();
                }
            }
        }



        public int Update(CaseMaster caseMaster)
        {
            Common.DbConnection dbConnection = null;
            try
            {
                dbConnection = new Common.DbConnection();
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

        public List<CaseMaster> GetCaseMasterList()
        {
            Common.DbConnection dbConnection = null;
            List<CaseMaster> listCaseMaster = new List<CaseMaster>();

            try
            {
                dbConnection = new Common.DbConnection();
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
    }
}
