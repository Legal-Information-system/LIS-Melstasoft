using LegalSystemCore.Domain;
using System;
using LegalSystemCore.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface ICourtLocationDAO
    {
        int Save(CourtLocation courtlocation, DbConnection dbConnection);
        int Update(CourtLocation courtlocation, DbConnection dbConnection);
        List<CourtLocation> GetCourtLocationList(DbConnection dbConnection);
    }
    public class CourtLocationSqlImpl : ICourtLocationDAO
    {
        public int Save(CourtLocation courtlocation, DbConnection dbConnection)
        {
            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into court_locationc (court_id,locationc_id) " +
                                           "values (@CourtId,@LocationId) ";


            //dbConnection.cmd.Parameters.AddWithValue("@CompanyId", company.CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@CourtId", courtlocation.CourtId);
            dbConnection.cmd.Parameters.AddWithValue("@LocationId", courtlocation.LocationId);


            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());


            return output;
        }

        public int Update(CourtLocation courtlocation, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update court_locationc set court_id = @CourtId ,locationc_id = @LocationId WHERE court_id = @CourtId AND locationc_id=@LocationId";

            dbConnection.cmd.Parameters.AddWithValue("@CourtId", courtlocation.CourtId);
            dbConnection.cmd.Parameters.AddWithValue("@location_id", courtlocation.LocationId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public List<CourtLocation> GetCourtLocationList(DbConnection dbConnection)
        {
            List<CourtLocation> listcourtlocation = new List<CourtLocation>();

            dbConnection.cmd.CommandText = "select * from court_locationc";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listcourtlocation = dataAccessObject.ReadCollection<CourtLocation>(dbConnection.dr);
            dbConnection.dr.Close();


            return listcourtlocation;
        }
    }
}
