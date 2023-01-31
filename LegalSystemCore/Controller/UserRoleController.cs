using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface IUserRoleController
    {
        int Save(UserRole userRole);
        int Update(UserRole userRole);
        int Delete(UserRole userRole);
        List<UserRole> GetUserRoleList();
        UserRole GetUserRole(int userRoleId);
    }

    public class UserControllerImpl : IUserRoleController
    {
        IUserRoleDAO userRoleDAO = DAOFactory.CreateUserRoleDAO();

        public UserRole GetUserRole(int userRoleId)
        {
            DbConnection dbConnection = null;
            UserRole userRole = new UserRole();
            try
            {
                dbConnection = new DbConnection();
                userRole = userRoleDAO.GetUserRole(userRoleId, dbConnection);
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
            return userRole;
        }
        public List<UserRole> GetUserRoleList()
        {
            DbConnection dbConnection = null;
            List<UserRole> listUserRole = new List<UserRole>();
            try
            {
                dbConnection = new DbConnection();
                listUserRole = userRoleDAO.GetUserRoleList(dbConnection);
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
            return listUserRole;
        }

        public int Save(UserRole userRole)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return userRoleDAO.Save(userRole, dbConnection);
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

        public int Update(UserRole userRole)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return userRoleDAO.Update(userRole, dbConnection);
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


        public int Delete(UserRole userRole)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return userRoleDAO.Delete(userRole, dbConnection);
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
