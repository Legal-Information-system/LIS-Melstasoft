using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalSystemCore.Infrastructure
{
    public interface IPaymentStatusDAO
    {
        int Save(PaymentStatus paymentStatus, DbConnection dbConnection);
        int Update(PaymentStatus paymentStatus, DbConnection dbConnection);
        List<PaymentStatus> GetPaymentStatusList(DbConnection dbConnection);
    }

    public class PaymentDAOSqlImpl : IPaymentStatusDAO
    {
        public List<PaymentStatus> GetPaymentStatusList(DbConnection dbConnection)
        {
            List<PaymentStatus> listPaymentStatus = new List<PaymentStatus>();

            dbConnection = new DbConnection();

            dbConnection.cmd.CommandText = "select * from payment_status WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listPaymentStatus = dataAccessObject.ReadCollection<PaymentStatus>(dbConnection.dr);
            dbConnection.dr.Close();


            return listPaymentStatus;
        }

        public int Save(PaymentStatus paymentStatus, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "INSERT INTO payment_status (payment_status_name) " +
            "VALUES (@StatusName)";

            dbConnection.cmd.Parameters.AddWithValue("@StatusName", paymentStatus.StatusName);

            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }

        public int Update(PaymentStatus paymentStatus, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE payment_status SET payment_status_name = @StatusName WHERE payment_status_id = @StatusId ";

            dbConnection.cmd.Parameters.AddWithValue("@StatusName", paymentStatus.StatusName);
            dbConnection.cmd.Parameters.AddWithValue("@StatusId", paymentStatus.StatusId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }
    }
}
