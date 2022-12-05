using LegalSystemCore.Domain;
using LegalSystemCore.Common;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LegalSystemCore.Controller
{
    public interface ILawyerController
    {
        int Save(Lawyer lawyer);
        int Delete(Lawyer lawyer);
        int Update(Lawyer lawyer);
        List<Lawyer> GetLawyerList(bool with0);

        Lawyer GetLawyer(int lawyerId);

    }
    public class LawyerControllerImpl : ILawyerController
    {
        ILawyerDAO lawyerDAO = DAOFactory.CreateLawyerDAO();
        public int Save(Lawyer lawyer)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return lawyerDAO.Save(lawyer, dbconnection);
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

        public List<Lawyer> GetLawyerList(bool with0)
        {
            Common.DbConnection dbConnection = null;
            List<Lawyer> listLawyer = new List<Lawyer>();

            try
            {
                dbConnection = new Common.DbConnection();
                listLawyer = lawyerDAO.GetLawyerList(with0, dbConnection);

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
            return listLawyer;
        }

        public int Update(Lawyer lawyer)
        {
            Common.DbConnection dbConnection = null;
            try
            {
                dbConnection = new Common.DbConnection();
                return lawyerDAO.Update(lawyer, dbConnection);
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

        public int Delete(Lawyer lawyer)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return lawyerDAO.Delete(lawyer, dbConnection);
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

        public Lawyer GetLawyer(int lawyerId)
        {
            DbConnection dbConnection = null;
            Lawyer lawyer = new Lawyer();
            try
            {
                dbConnection = new DbConnection();
                lawyer = lawyerDAO.GetLawyer(lawyerId, dbConnection);
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
            return lawyer;
        }
    }
}
