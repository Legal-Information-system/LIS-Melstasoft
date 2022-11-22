using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface ILocationController
    {
        int Save(Location location);
        int Update(Location location);
        List<Location> GetLocationList();

    }
    public class LocationControllerImpl : ILocationController
    {
        ILocationDAO locationDAO = DAOFactory.CreateLocationDAO();
        public int Save(Location location)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return locationDAO.Save(location, dbconnection);
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

        public int Update(Location location)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return locationDAO.Update(location, dbconnection);
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

        public List<Location> GetLocationList()
        {
            Common.DbConnection dbConnection = null;
            List<Location> listlocation = new List<Location>();
            try
            {
                dbConnection = new Common.DbConnection();
                listlocation = locationDAO.GetLocationList(dbConnection);

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
            return listlocation;
        }
    }
}
