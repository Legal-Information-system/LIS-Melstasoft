using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface ICourtController
    {
        int Save(Court court);
        int Update(Court court);
        List<Court> GetCourtList();

    }
    public class CourtControllerImpl : ICourtController
    {
        ICourtDAO courtDAO = DAOFactory.CreateCourtDAO();
        public int Save(Court court)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return courtDAO.Save(court, dbconnection);
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

        public int Update(Court court)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return courtDAO.Update(court, dbconnection);
            }
            catch (Exception)
            {
                dbconnection.RollBack();
                throw;
            }
        }

        public List<Court> GetCourtList()
        {
            Common.DbConnection dbConnection = null;
            List<Court> listcourt = new List<Court>();
            try
            {
                dbConnection = new Common.DbConnection();
                listcourt = courtDAO.GetCourtList(dbConnection);

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
            return listcourt;
        }
    }
}
