using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface IPaymentActivityDAO
    {
        void Save(PaymentActivity paymentActivity, DbConnection dbConnection);
        int Update(PaymentActivity paymentActivity, DbConnection dbConnection);
        int Delete(PaymentActivity paymentActivity, DbConnection dbConnection);
        List<PaymentActivity> GetPaymentActivityList(DbConnection dbConnection);
    }
    public class PaymentActivityDAOSqlImpl : IPaymentActivityDAO
    {
        public void Save(PaymentActivity paymentActivity, DbConnection dbConnection)
        {
            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into payment_activity (activity_id,payment_id) " +
                                           "values (@ActivityId,@PaymentId) ";



            dbConnection.cmd.Parameters.AddWithValue("@ActivityId", paymentActivity.ActivityId);
            dbConnection.cmd.Parameters.AddWithValue("@PaymentId", paymentActivity.PaymentId);




            dbConnection.cmd.ExecuteScalar();

        }


        public int Update(PaymentActivity paymentActivity, DbConnection dbConnection)
        {

            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update payment_activity set is_active = @IsActive WHERE payment_id = @PaymentId AND activity_id = @ActivityId";



            dbConnection.cmd.Parameters.AddWithValue("@IsActive", paymentActivity.IsActive);
            dbConnection.cmd.Parameters.AddWithValue("@ActivityId", paymentActivity.ActivityId);
            dbConnection.cmd.Parameters.AddWithValue("@PaymentId", paymentActivity.PaymentId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int Delete(PaymentActivity paymentActivity, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update payment_activity set is_active = @IsActive WHERE payment_id = @PaymentId AND activity_id = @ActivityId";



            dbConnection.cmd.Parameters.AddWithValue("@IsActive", paymentActivity.IsActive);
            dbConnection.cmd.Parameters.AddWithValue("@ActivityId", paymentActivity.ActivityId);
            dbConnection.cmd.Parameters.AddWithValue("@PaymentId", paymentActivity.PaymentId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public List<PaymentActivity> GetPaymentActivityList(DbConnection dbConnection)
        {
            List<PaymentActivity> paymentActivityList = new List<PaymentActivity>();

            dbConnection.cmd.CommandText = "select * from payment_activity WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            paymentActivityList = dataAccessObject.ReadCollection<PaymentActivity>(dbConnection.dr);
            dbConnection.dr.Close();

            return paymentActivityList;
        }

    }
}
