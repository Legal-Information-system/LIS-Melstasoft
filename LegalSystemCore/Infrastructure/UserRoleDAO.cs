using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface IUserRoleDAO
    {
        int Save(UserRole userRole, DbConnection dbConnection);
        int Update(UserRole userRole, DbConnection dbConnection);
        int Delete(UserRole userRole, DbConnection dbConnection);
        List<UserRole> GetUserRoleList(DbConnection dbConnection);
        UserRole GetUserRole(int roleId, DbConnection dbConnection);
    }

    public class UserRoleDAOSqlImpl : IUserRoleDAO
    {

        public List<UserRole> GetUserRoleList(DbConnection dbConnection)
        {
            List<UserRole> listUserRole = new List<UserRole>();

            //dbConnection = new DbConnection();

            dbConnection.cmd.CommandText = "select * from user_role WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listUserRole = dataAccessObject.ReadCollection<UserRole>(dbConnection.dr);
            dbConnection.dr.Close();


            return listUserRole;
        }

        public UserRole GetUserRole(int roleId, DbConnection dbConnection)
        {
            UserRole userRole = new UserRole();

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "select * from user_role WHERE user_role_id = @RoleId";
            dbConnection.cmd.Parameters.AddWithValue("@RoleId", roleId);

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            userRole = dataAccessObject.GetSingleOject<UserRole>(dbConnection.dr);
            dbConnection.dr.Close();

            return userRole;
        }

        public int Save(UserRole userRole, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "INSERT INTO user_role (user_role_name) " +
                                           "VALUES (@RoleName) SELECT SCOPE_IDENTITY()";

            dbConnection.cmd.Parameters.AddWithValue("@RoleName", userRole.RoleName);

            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }

        public int Update(UserRole userRole, DbConnection dbConnection)
        {

            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE user_role SET user_role_name = @RoleName WHERE user_role_id = @RoleId ";

            dbConnection.cmd.Parameters.AddWithValue("@RoleName", userRole.RoleName);
            dbConnection.cmd.Parameters.AddWithValue("@RoleId", userRole.RoleId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int Delete(UserRole userRole, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE user_role SET is_active = 0 WHERE user_role_id = @RoleId ";

            dbConnection.cmd.Parameters.AddWithValue("@RoleId", userRole.RoleId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }
    }
}
