using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface IUserRolePrivilegeDAO
    {
        int Save(UserRolePrivilege userRolePrivilege, DbConnection dbConnection);
        int Delete(UserRolePrivilege userRolePrivilege, DbConnection dbConnection);
        List<UserRolePrivilege> GetUserRolePrivilegeList(DbConnection dbConnection);
    }

    public class UserRolePrivilegeDAOSqlImpl : IUserRolePrivilegeDAO
    {

        public List<UserRolePrivilege> GetUserRolePrivilegeList(DbConnection dbConnection)
        {
            List<UserRolePrivilege> listUserRolePrivilege = new List<UserRolePrivilege>();

            //dbConnection = new DbConnection();

            dbConnection.cmd.CommandText = "select * from user_role_privilege WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listUserRolePrivilege = dataAccessObject.ReadCollection<UserRolePrivilege>(dbConnection.dr);
            dbConnection.dr.Close();


            return listUserRolePrivilege;
        }

        public int Save(UserRolePrivilege userRolePrivilege, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "INSERT INTO user_role_privilege (user_role_id,function_id) " +
                                           "VALUES (@UserRoleId , @FunctionId) SELECT SCOPE_IDENTITY()";

            dbConnection.cmd.Parameters.AddWithValue("@UserRoleId", userRolePrivilege.UserRoleId);
            dbConnection.cmd.Parameters.AddWithValue("@FunctionId", userRolePrivilege.FunctionId);

            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }

        public int Init(UserRolePrivilege userRolePrivilege, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "INSERT INTO user_role_privilege (user_role_id,function_id) " +
                                           "VALUES (@UserRoleId , @FunctionId) SELECT SCOPE_IDENTITY()";

            dbConnection.cmd.Parameters.AddWithValue("@UserRoleId", userRolePrivilege.UserRoleId);
            dbConnection.cmd.Parameters.AddWithValue("@FunctionId", userRolePrivilege.FunctionId);

            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }


        public int Delete(UserRolePrivilege userRolePrivilege, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE user_role_privillege SET is_active = 0 WHERE user_role_id = @UserRoleId  AND function_id = @FunctionId";

            dbConnection.cmd.Parameters.AddWithValue("@UserRoleId", userRolePrivilege.UserRoleId);
            dbConnection.cmd.Parameters.AddWithValue("@FunctionId", userRolePrivilege.FunctionId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }
    }
}
