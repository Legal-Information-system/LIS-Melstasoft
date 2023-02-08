﻿using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;

namespace LegalSystemCore.Controller
{

    public interface IUserLoginController
    {
        int Save(UserLogin userLogin);
        int Update(UserLogin userLogin);
        List<UserLogin> GetUserLoginList(bool with0);
        UserLogin GetUserLogin(UserLogin userLogin);

        UserLogin GetUserLogin(string userId);
        int UpdatePassword(UserLogin userLogin);
        UserLogin GetUserLoginById(string userLoginId);
    }

    public class UserLoginControllerImpl : IUserLoginController
    {
        IUserLoginDAO userLoginDAO = DAOFactory.CreateUserLoginDAO();

        public int Save(UserLogin userLogin)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return userLoginDAO.Save(userLogin, dbconnection);
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("500.aspx");
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

        public int Update(UserLogin userLogin)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return userLoginDAO.Update(userLogin, dbconnection);
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("500.aspx");
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

        public int UpdatePassword(UserLogin userLogin)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return userLoginDAO.UpdatePassword(userLogin, dbconnection);
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("500.aspx");
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

        public List<UserLogin> GetUserLoginList(bool with0)
        {
            Common.DbConnection dbConnection = null;
            List<UserLogin> listUserLogin = new List<UserLogin>();
            try
            {
                dbConnection = new Common.DbConnection();
                listUserLogin = userLoginDAO.GetUserLoginList(with0, dbConnection);

            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("500.aspx");
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
            return listUserLogin;
        }

        public UserLogin GetUserLogin(UserLogin userLogin)
        {
            Common.DbConnection dbConnection = null;

            try
            {
                dbConnection = new Common.DbConnection();
                userLogin = userLoginDAO.GetUserLogin(dbConnection, userLogin);



            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("500.aspx");
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
            return userLogin;
        }

        public UserLogin GetUserLogin(string userId)
        {
            Common.DbConnection dbConnection = null;
            UserLogin userLogin = new UserLogin();
            try
            {
                dbConnection = new Common.DbConnection();
                userLogin = userLoginDAO.GetUserLogin(dbConnection, userId);



            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("500.aspx");
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
            return userLogin;
        }

        public UserLogin GetUserLoginById(string userLoginId)
        {
            Common.DbConnection dbConnection = null;
            UserLogin userLogin = new UserLogin();
            try
            {
                dbConnection = new Common.DbConnection();

                userLogin = userLoginDAO.GetUserLogin(dbConnection, userLoginId);
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("500.aspx");
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
            return userLogin;
        }

    }

}
