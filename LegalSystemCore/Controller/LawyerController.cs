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
        List<Lawyer> GetLawyerList();

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

        public List<Lawyer> GetLawyerList()
        {
            Common.DbConnection dbConnection = null;
            List<Lawyer> listLawyer = new List<Lawyer>();

            try
            {
                dbConnection = new Common.DbConnection();
                listLawyer = lawyerDAO.GetLawyerList(dbConnection);

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
    }
}
