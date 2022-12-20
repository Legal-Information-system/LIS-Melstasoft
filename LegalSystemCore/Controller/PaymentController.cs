using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface IPaymentController
    {
        int Save(Payment payment);
        int Update(Payment payment);
        int Delete(Payment payment);
        List<Payment> GetPaymentList(bool withActiveity, bool withLawyer, bool withStatus);

        Payment GetPayment(int paymentId);

        int UpdatePaymentStatus(Payment payment);
    }

    public class PaymentControllerImpl : IPaymentController
    {
        IPaymentDAO PaymentDAO = DAOFactory.CreatePaymentDAO();

        public int Save(Payment payment)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return PaymentDAO.Save(payment, dbConnection);
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

        public int Update(Payment payment)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return PaymentDAO.Update(payment, dbConnection);
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

        public int Delete(Payment payment)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return PaymentDAO.Delete(payment, dbConnection);
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
        public List<Payment> GetPaymentList(bool withActiveity, bool withLawyer, bool withStatus)
        {
            DbConnection dbConnection = null;
            List<Payment> listPayment = new List<Payment>();
            try
            {
                dbConnection = new DbConnection();
                listPayment = PaymentDAO.GetPaymentList(dbConnection);
                if (withActiveity)
                {
                    IPaymentActivityDAO paymentActuvityDao = DAOFactory.CreatePaymentActivityDAO();
                    IActivityDAO activityDAO = DAOFactory.CreateActivityDAO();

                    foreach (var payment in listPayment)
                    {
                        payment.listPaymentActivity = paymentActuvityDao.GetPaymentActivityList(dbConnection, payment.PaymentId);

                        if (payment.listPaymentActivity.Count > 0)
                        {
                            foreach (var activity in payment.listPaymentActivity)
                            {
                                activity.activity = activityDAO.GetActivity(dbConnection, activity.ActivityId);

                                if (payment.Actions == null)
                                {
                                    payment.Actions = activity.activity.ActivityName;
                                }
                                else
                                {
                                    payment.Actions = payment.Actions + " , " + activity.activity.ActivityName;
                                }
                            }
                        }
                    }
                }

                if (withLawyer)
                {
                    ILawyerDAO lawyerDAO = DAOFactory.CreateLawyerDAO();
                    List<Lawyer> lawyerList = lawyerDAO.GetLawyerList(true, dbConnection);

                    foreach (var payment in listPayment)
                    {
                        payment.lawyer = lawyerList.Where(x => x.LawyerId == payment.LawyerId).Single();
                    }
                }

                if (withStatus)
                {
                    IPaymentStatusDAO paymentStatusDAO = DAOFactory.CreatePaymentStatusDAO();
                    List<PaymentStatus> paymentStatusList = paymentStatusDAO.GetPaymentStatusList(dbConnection);

                    foreach (var payment in listPayment)
                    {
                        payment.paymentStatus = paymentStatusList.Where(x => x.StatusId == payment.PaymentStatusId).Single();
                    }
                }

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
            return listPayment;
        }



        public Payment GetPayment(int paymentId)
        {
            DbConnection dbConnection = null;
            Payment payment = new Payment();
            try
            {
                dbConnection = new DbConnection();
                payment = PaymentDAO.GetPayment(paymentId, dbConnection);
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
            return payment;
        }

        public int UpdatePaymentStatus(Payment payment)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return PaymentDAO.UpdateStatus(payment, dbConnection);
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
    }

}
