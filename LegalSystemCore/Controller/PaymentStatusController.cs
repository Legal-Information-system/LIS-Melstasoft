using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalSystemCore.Controller
{
    public interface IPaymentStatusController
    {
        int Save(PaymentStatus paymentStatus);
        int Update(PaymentStatus paymentStatus);
        int Delete(PaymentStatus paymentStatus);
        List<PaymentStatus> GetPaymentStatusList();

        PaymentStatus GetPaymentStatus(int paymentStatusId);
    }

    public class PaymentStatusControllerImpl : IPaymentStatusController
    {
        IPaymentStatusDAO PaymentStatusDAO = DAOFactory.CreatePaymentStatusDAO();

        public List<PaymentStatus> GetPaymentStatusList()
        {
            DbConnection dbConnection = null;
            List<PaymentStatus> listPaymentStatus = new List<PaymentStatus>();
            try
            {
                dbConnection = new DbConnection();
                listPaymentStatus = PaymentStatusDAO.GetPaymentStatusList(dbConnection);
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
            return listPaymentStatus;
        }

        public int Save(PaymentStatus paymentStatus)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return PaymentStatusDAO.Save(paymentStatus, dbConnection);
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

        public int Update(PaymentStatus paymentStatus)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return PaymentStatusDAO.Update(paymentStatus, dbConnection);
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

        public int Delete(PaymentStatus paymentStatus)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return PaymentStatusDAO.Delete(paymentStatus, dbConnection);
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

        public PaymentStatus GetPaymentStatus(int paymentStatusId)
        {
            DbConnection dbConnection = null;
            PaymentStatus paymentStatus = new PaymentStatus();
            try
            {
                dbConnection = new DbConnection();
                paymentStatus = PaymentStatusDAO.GetPaymentStatus(paymentStatusId, dbConnection);
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
            return paymentStatus;
        }
    }
}
