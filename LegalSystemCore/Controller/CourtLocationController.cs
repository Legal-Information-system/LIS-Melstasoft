using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{

    public interface ICourtLocationController
    {
        int Save(CourtLocation courtlocation);
        int Update(CourtLocation courtlocation);
        List<CourtLocation> GetCourtLocationList();
    }

    public class CourtLocationControllerImpl : ICourtLocationController
    {
        ICourtLocationDAO courtlocationDAO = DAOFactory.CreatCourtLocationDAO();
        public List<CourtLocation> GetCourtLocationList()
        {
            Common.DbConnection dbConnection = null;
            List<CourtLocation> listcourtlocation = new List<CourtLocation>();
            try
            {
                dbConnection = new Common.DbConnection();
                listcourtlocation = courtlocationDAO.GetCourtLocationList(dbConnection);

                ICourtDAO courtDAO = DAOFactory.CreateCourtDAO();
                List<Court> listCourt = courtDAO.GetCourtList(dbConnection);

                foreach (var courtlocation in listcourtlocation)
                {
                    courtlocation.court = listCourt.Where(x => x.CourtId == courtlocation.CourtId).Single();
                }

                ILocationDAO locationDAO = DAOFactory.CreateLocationDAO();
                List<Location> listLocation = locationDAO.GetLocationList(dbConnection);

                foreach (var courtlocation in listcourtlocation)
                {
                    courtlocation.location = listLocation.Where(x => x.LocationId == courtlocation.LocationId).Single();
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
            return listcourtlocation;
        }

        public int Save(CourtLocation courtlocation)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return courtlocationDAO.Save(courtlocation, dbconnection);
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

        public int Update(CourtLocation courtlocation)
        {
            Common.DbConnection dbConnection = null;
            try
            {
                dbConnection = new Common.DbConnection();
                return courtlocationDAO.Update(courtlocation, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }
    }
}
