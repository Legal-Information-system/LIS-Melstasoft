using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface IUserPrivilegeDAO
    {
        int Save(UserPrivilege userPrivilege, DbConnection dbConnection);
        int Update(UserPrivilege userPrivilege, DbConnection dbConnection);
        List<UserPrivilege> GetUserPrivilegeList(int userLoginId, DbConnection dbConnection);

        int Delete(UserPrivilege userPrivilege, DbConnection dbConnection);

        UserPrivilege GetUserPrivilege(DbConnection dbConnection, int userLoginId, int functionId);
    }
    public class UserPrivilegeDAOSqlImpl : IUserPrivilegeDAO
    {
        public int Save(UserPrivilege userPrivilege, DbConnection dbConnection)
        {
            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into user_privilege (user_login_id,is_grant_revoke,function_id) " +
                                           "values (@UserLoginId,@IsGrantRevoke,@FunctionId) SELECT SCOPE_IDENTITY() ";



            dbConnection.cmd.Parameters.AddWithValue("@UserLoginId", userPrivilege.UserLoginId);
            dbConnection.cmd.Parameters.AddWithValue("@IsGrantRevoke", userPrivilege.IsGrantRevoke);
            dbConnection.cmd.Parameters.AddWithValue("@FunctionId", userPrivilege.FunctionId);




            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());


            return output;
        }

        public List<UserPrivilege> GetUserPrivilegeList(int userLoginId, DbConnection dbConnection)
        {
            List<UserPrivilege> GetUserPrivilegeList = new List<UserPrivilege>();

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "select * from user_privilege where user_login_id = @UserLoginId";

            dbConnection.cmd.Parameters.AddWithValue("@UserLoginId", userLoginId);

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            GetUserPrivilegeList = dataAccessObject.ReadCollection<UserPrivilege>(dbConnection.dr);
            dbConnection.dr.Close();

            return GetUserPrivilegeList;
        }
        public int Update(UserPrivilege userPrivilege, DbConnection dbConnection)
        {

            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update activity set is_grant_revoke = @IsGrantRevoke WHERE user_login_id = @UserLoginId AND function_id = @FunctionId ";


            dbConnection.cmd.Parameters.AddWithValue("@UserLoginId", userPrivilege.UserLoginId);
            dbConnection.cmd.Parameters.AddWithValue("@IsGrantRevoke", userPrivilege.IsGrantRevoke);
            dbConnection.cmd.Parameters.AddWithValue("@FunctionId", userPrivilege.FunctionId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int Delete(UserPrivilege userPrivilege, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "DELETE FROM user_privilege  WHERE user_login_id = @UserLoginId AND function_id = @FunctionId";

            dbConnection.cmd.Parameters.AddWithValue("@UserLoginId", userPrivilege.UserLoginId);
            dbConnection.cmd.Parameters.AddWithValue("@IsGrantRevoke", userPrivilege.IsGrantRevoke);
            dbConnection.cmd.Parameters.AddWithValue("@FunctionId", userPrivilege.FunctionId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public UserPrivilege GetUserPrivilege(DbConnection dbConnection, int userLoginId, int functionId)
        {
            UserPrivilege userPrivilege = new UserPrivilege();

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "select * from user_privilege  WHERE user_login_id = @UserLoginId AND function_id = @FunctionId";

            dbConnection.cmd.Parameters.AddWithValue("@UserLoginId", userLoginId);
            dbConnection.cmd.Parameters.AddWithValue("@FunctionId", functionId);

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            userPrivilege = dataAccessObject.GetSingleOject<UserPrivilege>(dbConnection.dr);
            dbConnection.dr.Close();

            return userPrivilege;
        }

    }
}
