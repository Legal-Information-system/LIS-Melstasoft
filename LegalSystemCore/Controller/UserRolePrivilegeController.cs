using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface IUserRolePrivilegeController
    {
        int Save(UserRolePrivilege userRolePrivilege);
        int Delete(UserRolePrivilege userRolePrivilege);
        List<UserRolePrivilege> GetUserRolePrivilegeList();
    }

    public class UserRolePrivilegeControllerImpl : IUserRolePrivilegeController
    {
        IUserRolePrivilegeDAO userRolePrivilegeDAO = DAOFactory.CreateUserRolePrivilegeDAO();

        public List<UserRolePrivilege> GetUserRolePrivilegeList()
        {
            DbConnection dbConnection = null;
            List<UserRolePrivilege> listUserRolePrivilege = new List<UserRolePrivilege>();
            try
            {
                dbConnection = new DbConnection();
                listUserRolePrivilege = userRolePrivilegeDAO.GetUserRolePrivilegeList(dbConnection);
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
            return listUserRolePrivilege;
        }

        public int Save(UserRolePrivilege userRolePrivilege)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return userRolePrivilegeDAO.Save(userRolePrivilege, dbConnection);
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



        public int Delete(UserRolePrivilege userRolePrivilege)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return userRolePrivilegeDAO.Delete(userRolePrivilege, dbConnection);
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
