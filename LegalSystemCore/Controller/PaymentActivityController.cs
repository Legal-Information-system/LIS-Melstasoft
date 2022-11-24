using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface IPaymentActivityController
    {
        void Save(PaymentActivity paymentActivity);
        int Update(PaymentActivity paymentActivity);
        int Delete(PaymentActivity paymentActivity);
        List<PaymentActivity> GetPaymentActivityList();
    }

    public class PaymentActivityControllerImpl : IPaymentActivityController
    {
        IPaymentActivityDAO paymentActivityDAO = DAOFactory.CreatePaymentActivityDAO();

        public void Save(PaymentActivity paymentActivity)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                paymentActivityDAO.Save(paymentActivity, dbConnection);
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

        public int Update(PaymentActivity paymentActivity)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return paymentActivityDAO.Update(paymentActivity, dbConnection);
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

        public int Delete(PaymentActivity paymentActivity)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return paymentActivityDAO.Delete(paymentActivity, dbConnection);
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
        public List<PaymentActivity> GetPaymentActivityList()
        {
            DbConnection dbConnection = null;
            List<PaymentActivity> listPaymentActivity = new List<PaymentActivity>();
            try
            {
                dbConnection = new DbConnection();
                listPaymentActivity = paymentActivityDAO.GetPaymentActivityList(dbConnection);
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
            return listPaymentActivity;
        }
    }
}
