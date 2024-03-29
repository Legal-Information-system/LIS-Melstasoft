﻿using LegalSystemCore.Domain;
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
        int Delete(Location location, DbConnection dbConnection);
        int Update(Location location, DbConnection dbConnection);
        List<Location> GetLocationList(bool with0, DbConnection dbConnection);
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
            dbConnection.cmd.Parameters.AddWithValue("@locationc_name", location.locationName);


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
            dbConnection.cmd.Parameters.AddWithValue("@location", location.locationName);


            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int Delete(Location location, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE locationc SET is_active = 0 WHERE locationc_id = @LocationId";
            dbConnection.cmd.Parameters.AddWithValue("@LocationId", location.LocationId);
            output = dbConnection.cmd.ExecuteNonQuery();

            dbConnection.cmd.CommandText = "UPDATE court_locationc SET is_active = 0 WHERE locationc_id = @LocationId";

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public List<Location> GetLocationList(bool with0, DbConnection dbConnection)
        {
            List<Location> listLocation = new List<Location>();

            if (with0)
                dbConnection.cmd.CommandText = "select * from locationc";
            else
                dbConnection.cmd.CommandText = "select * from locationc WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listLocation = dataAccessObject.ReadCollection<Location>(dbConnection.dr);
            dbConnection.dr.Close();


            return listLocation;
        }
    }
}
