using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface ICaseStatusDAO
    {
        int Save(CaseStatus caseStatus, DbConnection dbConnection);
        int Update(CaseStatus caseStatus, DbConnection dbConnection);
        List<CaseStatus> GetCaseStatusList(DbConnection dbConnection);
    }

    public class CaseStatusDAOSqlImpl : ICaseStatusDAO
    {
        public List<CaseStatus> GetCaseStatusList(DbConnection dbConnection)
        {
            List<CaseStatus> listCaseStatus = new List<CaseStatus>();

            dbConnection = new DbConnection();

            dbConnection.cmd.CommandText = "select * from case_status WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listCaseStatus = dataAccessObject.ReadCollection<CaseStatus>(dbConnection.dr);
            dbConnection.dr.Close();


            return listCaseStatus;
        }

        public int Save(CaseStatus caseStatus, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "INSERT INTO case_status (case_status_name) " +
                                           "VALUES (@StatusName)";

            dbConnection.cmd.Parameters.AddWithValue("@StatusName", caseStatus.StatusName);

            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }

        public int Update(CaseStatus caseStatus, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE case_status SET case_status_name = @StatusName WHERE case_status_id = @StatusId ";

            dbConnection.cmd.Parameters.AddWithValue("@StatusName", caseStatus.StatusName);
            dbConnection.cmd.Parameters.AddWithValue("@StatusId", caseStatus.StatusId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }
    }
}
