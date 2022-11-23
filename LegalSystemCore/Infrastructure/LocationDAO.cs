using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegalSystemCore.Common;

namespace LegalSystemCore.Infrastructure
{
    public interface ILocationDAO
    {
        int Save(Location location, DbConnection dbConnection);
        int Update(Location location, DbConnection dbConnection);
        List<Location> GetLocationList(DbConnection dbConnection);
    }
    public class LocationSqlDAOImpl : ILocationDAO
    {
        public int Save(Location location, DbConnection dbConnection)
        {

            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into locationc (locationc_name) " +
                                           "values (@locationc_name) SELECT SCOPE_IDENTITY() ";


            //dbConnection.cmd.Parameters.AddWithValue("@CompanyId", company.CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@locationc_id", location.LocationId);
            dbConnection.cmd.Parameters.AddWithValue("@locationc_name", location.location);


            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());


            return output;
        }

        public int Update(Location location, DbConnection dbConnection)
        {

            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update locationc set locationc_name = @location WHERE locationc_id = @locationId ";


            dbConnection.cmd.Parameters.AddWithValue("@locationId", location.LocationId);
            dbConnection.cmd.Parameters.AddWithValue("@location", location.location);


            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public List<Location> GetLocationList(DbConnection dbConnection)
        {
            List<Location> listLocation = new List<Location>();

            dbConnection.cmd.CommandText = "select * from locationc WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listLocation = dataAccessObject.ReadCollection<Location>(dbConnection.dr);
            dbConnection.dr.Close();


            return listLocation;
        }
    }
}
