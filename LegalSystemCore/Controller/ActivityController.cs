using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface IActivityController
    {
        int Save(Activity activity);
        int Update(Activity activity);
        List<Activity> GetActivityList(bool with0);

        int Delete(Activity activity);

        Activity GetActivity(int activityId);
    }

    public class ActivityControllerImpl : IActivityController
    {
        IActivityDAO activityDAO = DAOFactory.CreateActivityDAO();
        public int Save(Activity activity)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return activityDAO.Save(activity, dbconnection);
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

        public List<Activity> GetActivityList(bool with0)
        {
            Common.DbConnection dbConnection = null;
            List<Activity> listActivity = new List<Activity>();

            try
            {
                dbConnection = new Common.DbConnection();
                listActivity = activityDAO.GetActivityList(with0, dbConnection);

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

        public int Delete(Activity activity)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return activityDAO.Delete(activity, dbConnection);
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

        public int Update(Activity activity)
        {
            Common.DbConnection dbConnection = null;
            try
            {
                dbConnection = new Common.DbConnection();
                return activityDAO.Update(activity, dbConnection);
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

        public Activity GetActivity(int activityId)
        {
            Common.DbConnection dbConnection = null;
            Activity activity = new Activity();
            try
            {
                dbConnection = new Common.DbConnection();
                activity = activityDAO.GetActivity(dbConnection, activityId);

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
