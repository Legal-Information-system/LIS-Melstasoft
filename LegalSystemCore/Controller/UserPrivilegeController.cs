using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface IUserPrivilegeController
    {
        int Save(UserPrivilege userPrivilege);
        int Update(UserPrivilege userPrivilege);
        List<UserPrivilege> GetUserPrivilegeList(int userLoginId);

        int Delete(UserPrivilege userPrivilege);

        UserPrivilege GetUserPrivilege(int userLoginId, int functionId);
    }

    public class UserPrivilegeControllerImpl : IUserPrivilegeController

    {
        IUserPrivilegeDAO userPrivilegeDAO = DAOFactory.CreateUserPrivilegeDAO();
        public int Save(UserPrivilege userPrivilege)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return userPrivilegeDAO.Save(userPrivilege, dbconnection);
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

        public List<UserPrivilege> GetUserPrivilegeList(int userLoginId)
        {
            Common.DbConnection dbConnection = null;
            List<UserPrivilege> listActivity = new List<UserPrivilege>();

            try
            {
                dbConnection = new Common.DbConnection();
                listActivity = userPrivilegeDAO.GetUserPrivilegeList(userLoginId, dbConnection);

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
            return listActivity;
        }

        public int Delete(UserPrivilege userPrivilege)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return userPrivilegeDAO.Delete(userPrivilege, dbConnection);
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

        public int Update(UserPrivilege userPrivilege)
        {
            Common.DbConnection dbConnection = null;
            try
            {
                dbConnection = new Common.DbConnection();
                return userPrivilegeDAO.Update(userPrivilege, dbConnection);
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

        public UserPrivilege GetUserPrivilege(int userLoginId, int functionId)
        {
            Common.DbConnection dbConnection = null;
            UserPrivilege activity = new UserPrivilege();
            try
            {
                dbConnection = new Common.DbConnection();
                activity = userPrivilegeDAO.GetUserPrivilege(dbConnection, userLoginId, functionId);

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
            return activity;
        }

    }
}
