using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LegalSystemCore.Infrastructure
{

    public interface IUserLoginDAO
    {
        int Save(UserLogin userLogin, DbConnection dbConnection);
        int Update(UserLogin userLogin, DbConnection dbConnection);
        List<UserLogin> GetUserLoginList(bool with0, DbConnection dbConnection);
        int UpdatePassword(UserLogin userLogin, DbConnection dbConnection);
        UserLogin GetUserLogin(DbConnection dbConnection, string userLoginId);
        UserLogin GetUserLogin(DbConnection dbConnection, UserLogin userLogin);
    }

    public class UserLoginSqlDAOImpl : IUserLoginDAO
    {
        public int Save(UserLogin userLogin, DbConnection dbConnection)
        {

            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into user_login (user_login_name,user_password,company_id,company_unit_id,user_role_id) " +
                                           "values (@UserName,@Password,@CompanyId,@CompanyUnitId,@UserRoleId) SELECT SCOPE_IDENTITY() ";


            //dbConnection.cmd.Parameters.AddWithValue("@CompanyId", company.CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@UserName", userLogin.UserName);
            dbConnection.cmd.Parameters.AddWithValue("@Password", userLogin.Password);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", userLogin.CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyUnitId", userLogin.CompanyUnitId);
            dbConnection.cmd.Parameters.AddWithValue("@UserRoleId", userLogin.UserRoleId);


            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());


            return output;
        }

        public int Update(UserLogin userLogin, DbConnection dbConnection)
        {

            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update user_login set user_login_name = @UserName ,user_password = @Password, company_id= @CompanyId, company_unit_id = @CompanyUnitId, user_role_id = @UserRoleId WHERE user_login_id = @UserId ";


            dbConnection.cmd.Parameters.AddWithValue("@UserId", userLogin.UserId);
            dbConnection.cmd.Parameters.AddWithValue("@UserName", userLogin.UserName);
            dbConnection.cmd.Parameters.AddWithValue("@Password", userLogin.Password);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", userLogin.CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyUnitId", userLogin.CompanyUnitId);
            dbConnection.cmd.Parameters.AddWithValue("@UserRoleId", userLogin.UserRoleId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int UpdatePassword(UserLogin userLogin, DbConnection dbConnection)
        {

            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update user_login set user_password = @Password WHERE user_login_id = @UserId ";


            dbConnection.cmd.Parameters.AddWithValue("@UserId", userLogin.UserId);
            dbConnection.cmd.Parameters.AddWithValue("@Password", userLogin.Password);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public List<UserLogin> GetUserLoginList(bool with0, DbConnection dbConnection)
        {
            List<UserLogin> listUserLogin = new List<UserLogin>();

            if (with0)
                dbConnection.cmd.CommandText = "select * from user_login";
            else
                dbConnection.cmd.CommandText = "select * from user_login WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listUserLogin = dataAccessObject.ReadCollection<UserLogin>(dbConnection.dr);
            dbConnection.dr.Close();


            return listUserLogin;
        }

        public UserLogin GetUserLogin(DbConnection dbConnection, UserLogin userLogin)
        {

            dbConnection.cmd.CommandText = "select * from user_login  where user_login_name = @UserName AND user_password = @Password AND is_active = 1";

            dbConnection.cmd.Parameters.AddWithValue("@UserName", userLogin.UserName);
            dbConnection.cmd.Parameters.AddWithValue("@Password", userLogin.Password);

            dbConnection.dr = dbConnection.cmd.ExecuteReader();

            DataAccessObject dataAccessObject = new DataAccessObject();
            userLogin = dataAccessObject.GetSingleOject<UserLogin>(dbConnection.dr);
            dbConnection.dr.Close();
            return userLogin;
        }

        public UserLogin GetUserLogin(DbConnection dbConnection, string userLoginId)
        {

            dbConnection.cmd.CommandText = "select * from user_login  where user_login_id = @UserLoginId AND is_active = 1";

            dbConnection.cmd.Parameters.AddWithValue("@UserLoginId", userLoginId);


            dbConnection.dr = dbConnection.cmd.ExecuteReader();

            UserLogin userLogin = new UserLogin();
            DataAccessObject dataAccessObject = new DataAccessObject();
            userLogin = dataAccessObject.GetSingleOject<UserLogin>(dbConnection.dr);
            dbConnection.dr.Close();
            return userLogin;
        }
    }

}
