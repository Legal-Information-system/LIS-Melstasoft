using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface IFunctionsController
    {
        int Save(Functions functions);
        int Update(Functions functions);
        List<Functions> GetFunctionList(bool with0);
        Functions GetFunctions(string functionName);
    }

    public class FunctionsControllerImpl : IFunctionsController
    {
        IFunctionsDAO functionsDAO = DAOFactory.CreateFunctionsDAO();
        public int Save(Functions functions)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return functionsDAO.Save(functions, dbconnection);
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

        public List<Functions> GetFunctionList(bool with0)
        {
            Common.DbConnection dbConnection = null;
            List<Functions> listFunction = new List<Functions>();

            try
            {
                dbConnection = new Common.DbConnection();
                listFunction = functionsDAO.GetFunctionList(with0, dbConnection);

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
            return listFunction;
        }


        public int Update(Functions functions)
        {
            Common.DbConnection dbConnection = null;
            try
            {
                dbConnection = new Common.DbConnection();
                return functionsDAO.Update(functions, dbConnection);
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

        public Functions GetFunctions(string functionName)
        {
            Common.DbConnection dbConnection = null;
            Functions functions = new Functions();
            try
            {
                dbConnection = new Common.DbConnection();
                functions = functionsDAO.GetFunctions(dbConnection, functionName);

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
            return functions;
        }

    }
}
