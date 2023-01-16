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
        int Init(DbConnection dbConnection);
        List<UserRolePrivilege> GetUserRolePrivilegeList(DbConnection dbConnection);

        List<UserRolePrivilege> GetUserRolePrivilegeListByRole(string userRoleId, DbConnection dbConnection);
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

        public List<UserRolePrivilege> GetUserRolePrivilegeListByRole(string userRoleId, DbConnection dbConnection)
        {
            List<UserRolePrivilege> listUserRolePrivilege = new List<UserRolePrivilege>();

            //dbConnection = new DbConnection();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "select * from user_role_privilege WHERE is_active = 1 AND user_role_id = @UserRoleId";
            dbConnection.cmd.Parameters.AddWithValue("@UserRoleId", userRoleId);

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

        public int Init(DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "INSERT INTO user_role_privilege (user_role_id,function_id) " +
                                           "VALUES (1,1), (1,2), (1,3) , (1,4) , (1,5) , (1,6) , (1,7), (1,8), (1,9)" +
                                           ",(1,10), (1,11), (1,12),(1,13),(1,14),(1,15),(1,16), (1,17), (1,18), (1,19)" +
                                           ",(1,20),(1,21),(1,22),(1,23),(1,24),(1,25), (1,26) , (1,27), (1,28) , (1,29), (1,30)" +
                                           " ,(2,17), (2,22), (2,13), (2,20), (2,26), (2,25), (2,30), (2,28), (2,29)" +
                                           ",(2,27), (2,15) , (4,16), (4,20), (4,21), (4,13), (4,26), (4,24), (4,25), (4,14), (4,29)" +
                                           ",(3,27), (3,24), (3,25), (3,26) ,(5,20), (5,16), (5,21), (5,12), (5,26), (5,24), (5,25), (5,14), (5,30)";



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
