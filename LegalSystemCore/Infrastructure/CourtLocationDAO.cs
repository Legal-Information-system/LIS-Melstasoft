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
        int Delete(CourtLocation courtlocation, DbConnection dbConnection);
        int Enable(CourtLocation courtlocation, DbConnection dbConnection);

        int Update(CourtLocation courtlocation, int courtId, int locationid, DbConnection dbConnectIon);
        List<CourtLocation> GetCourtLocationList(bool with0, DbConnection dbConnection);
        List<CourtLocation> GetCourtLocationListFilter(int courtId, DbConnection dbConnection);


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

        public int Delete(CourtLocation courtlocation, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update court_locationc set is_active = 0 WHERE court_id = @CourtId AND locationc_id=@locationId";

            dbConnection.cmd.Parameters.AddWithValue("@CourtId", courtlocation.CourtId);
            dbConnection.cmd.Parameters.AddWithValue("@locationId", courtlocation.LocationId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int Enable(CourtLocation courtlocation, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update court_locationc set is_active = 1 WHERE court_id = @CourtId AND locationc_id=@locationId";

            dbConnection.cmd.Parameters.AddWithValue("@CourtId", courtlocation.CourtId);
            dbConnection.cmd.Parameters.AddWithValue("@locationId", courtlocation.LocationId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int Update(CourtLocation courtlocation, int courtId, int locationId, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update court_locationc set court_id = @CourtNewId ,locationc_id = @locationNewId WHERE court_id = @CourtId AND locationc_id=@locationId";

            dbConnection.cmd.Parameters.AddWithValue("@CourtNewId", courtlocation.CourtId);
            dbConnection.cmd.Parameters.AddWithValue("@locationNewId", courtlocation.LocationId);
            dbConnection.cmd.Parameters.AddWithValue("@CourtId", courtId);
            dbConnection.cmd.Parameters.AddWithValue("@locationId", locationId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public List<CourtLocation> GetCourtLocationList(bool with0, DbConnection dbConnection)
        {
            List<CourtLocation> listcourtlocation = new List<CourtLocation>();

            String query;

            if (with0)
                query = "select * from court_locationc";
            else
                query = "select * from court_locationc WHERE is_active = 1";

            dbConnection.cmd.CommandText = query;

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listcourtlocation = dataAccessObject.ReadCollection<CourtLocation>(dbConnection.dr);
            dbConnection.dr.Close();


            return listcourtlocation;
        }

        public List<CourtLocation> GetCourtLocationListFilter(int courtId, DbConnection dbConnection)
        {
            List<CourtLocation> listcourtlocation = new List<CourtLocation>();

            dbConnection.cmd.CommandText = "select * from court_locationc WHERE is_active = 1 AND court_id =" + courtId;

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listcourtlocation = dataAccessObject.ReadCollection<CourtLocation>(dbConnection.dr);
            dbConnection.dr.Close();


            return listcourtlocation;
        }
    }
}
