using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{

    public interface IPaymentDAO
    {
        int Save(Payment payment, DbConnection dbConnection);
        int Update(Payment payment, DbConnection dbConnection);
        int Delete(Payment payment, DbConnection dbConnection);
        List<Payment> GetPaymentList(DbConnection dbConnection);
    }
    public class PaymentDAOSqlImpl : IPaymentDAO
    {
        public int Save(Payment payment, DbConnection dbConnection)
        {
            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into payment_master (case_number,lawyer_id,amount,payment_status_id,created_date,create_user_id,remarks) " +
                                           "values (@CaseNumber,@LawyerId,@Amount,@PaymentStatusId,@CreatedDate,@CreateUserId,@Remark) SELECT SCOPE_IDENTITY() ";



            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", payment.CaseNumber);
            dbConnection.cmd.Parameters.AddWithValue("@LawyerId", payment.LawyerId);
            dbConnection.cmd.Parameters.AddWithValue("@Amount", payment.Amount);
            dbConnection.cmd.Parameters.AddWithValue("@PaymentStatusId", payment.PaymentStatusId);
            dbConnection.cmd.Parameters.AddWithValue("@CreatedDate", payment.CreatedDate);
            dbConnection.cmd.Parameters.AddWithValue("@CreateUserId", payment.CreateUserId);
            dbConnection.cmd.Parameters.AddWithValue("@Remark", payment.Remarks);





            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());


            return output;
        }


        public int Update(Payment payment, DbConnection dbConnection)
        {

            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update payment_master set case_number = @CaseNumber,lawyer_id = @LawyerId ,amount = @Amount,remarks = @Remarks,payment_status_id = @PaymentStatusId, action_remarks = @ActionRemarks WHERE payment_id = @PaymentId ";


            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", payment.CaseNumber);
            dbConnection.cmd.Parameters.AddWithValue("@LawyerId", payment.LawyerId);
            dbConnection.cmd.Parameters.AddWithValue("@Amount", payment.Amount);
            dbConnection.cmd.Parameters.AddWithValue("@Remarks", payment.Remarks);
            dbConnection.cmd.Parameters.AddWithValue("@PaymentStatusId", payment.PaymentStatusId);
            dbConnection.cmd.Parameters.AddWithValue("@CreatedDate", payment.CreatedDate);
            dbConnection.cmd.Parameters.AddWithValue("@CreateUserId", payment.CreateUserId);
            dbConnection.cmd.Parameters.AddWithValue("@ActionTakenDate", payment.ActionTakenDate);
            dbConnection.cmd.Parameters.AddWithValue("@ActionRemarks", payment.ActionRemarks);
            dbConnection.cmd.Parameters.AddWithValue("@ActionTakenUserId", payment.ActionTakenUserId);
            dbConnection.cmd.Parameters.AddWithValue("@PaymentId", payment.PaymentId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int Delete(Payment payment, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE payment_master SET is_active = 0 WHERE payment_id = @PaymentId ";

            dbConnection.cmd.Parameters.AddWithValue("@PaymentId", payment.PaymentId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public List<Payment> GetPaymentList(DbConnection dbConnection)
        {
            List<Payment> paymentList = new List<Payment>();

            dbConnection.cmd.CommandText = "select * from payment_master WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            paymentList = dataAccessObject.ReadCollection<Payment>(dbConnection.dr);
            dbConnection.dr.Close();

            return paymentList;
        }

    }
}
