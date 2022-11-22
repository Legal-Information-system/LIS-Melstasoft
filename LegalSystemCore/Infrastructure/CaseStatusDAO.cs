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

            dbConnection.cmd.CommandText = "select * from case_status";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listCaseStatus = dataAccessObject.ReadCollection<CaseStatus>(dbConnection.dr);
            dbConnection.dr.Close();


            return listCaseStatus;
        }

        public int Save(CaseStatus caseStatus, DbConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int Update(CaseStatus caseStatus, DbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}
