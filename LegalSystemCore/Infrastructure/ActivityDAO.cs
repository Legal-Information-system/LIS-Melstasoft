using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface IActivityDAO
    {
        int Save(Activity activity, DbConnection dbConnection);
        int Update(Activity activity, DbConnection dbConnection);
        List<Activity> GetActivityList(DbConnection dbConnection);

        int Delete(Activity activity, DbConnection dbConnection);

        Activity GetActivity(DbConnection dbConnection, int activityId);
    }
    public class ActivityDAOSqlImpl : IActivityDAO
    {
        public int Save(Activity activity, DbConnection dbConnection)
        {
            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into activity (activity_name) " +
                                           "values (@ActivityName) SELECT SCOPE_IDENTITY() ";



            dbConnection.cmd.Parameters.AddWithValue("@ActivityName", activity.ActivityName);




            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());


            return output;
        }

        public List<Activity> GetActivityList(DbConnection dbConnection)
        {
            List<Activity> GetActivityList = new List<Activity>();

            dbConnection.cmd.CommandText = "select * from activity WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            GetActivityList = dataAccessObject.ReadCollection<Activity>(dbConnection.dr);
            dbConnection.dr.Close();

            return GetActivityList;
        }
        public int Update(Activity activity, DbConnection dbConnection)
        {

            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update activity set activity_name = @ActivityName WHERE activity_id = @ActivityId ";


            dbConnection.cmd.Parameters.AddWithValue("@ActivityId", activity.ActivityId);
            dbConnection.cmd.Parameters.AddWithValue("@ActivityName", activity.ActivityName);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int Delete(Activity activity, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE activity SET is_active = 0 WHERE activity_id = @ActivityId ";

            dbConnection.cmd.Parameters.AddWithValue("@ActivityId", activity.ActivityId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public Activity GetActivity(DbConnection dbConnection, int activityId)
        {
            Activity activity = new Activity();
            dbConnection.cmd.CommandText = "select * from activity WHERE activity_id = @ActivityId";
            dbConnection.cmd.Parameters.AddWithValue("@ActivityId", activityId);

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            activity = dataAccessObject.GetSingleOject<Activity>(dbConnection.dr);
            dbConnection.dr.Close();

            return activity;
        }

    }
}
