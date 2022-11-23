using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface ICaseNatureDAO
    {
        int Save(CaseNature caseNature, DbConnection dbConnection);
        List<CaseNature> GetCaseNatureList(DbConnection dbConnection);
    }

    public class CaseNatureSqlDAOImpl : ICaseNatureDAO
    {
        public int Save(CaseNature caseNature, DbConnection dbConnection)
        {
            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into case_nature (case_nature_name) " +
                                           "values (@CaseNatureName) SELECT SCOPE_IDENTITY() ";



            dbConnection.cmd.Parameters.AddWithValue("@CaseNatureName", caseNature.CaseNatureName);




            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());


            return output;
        }

        public List<CaseNature> GetCaseNatureList(DbConnection dbConnection)
        {
            List<CaseNature> GetCaseNatureList = new List<CaseNature>();

            dbConnection.cmd.CommandText = "select * from case_nature WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            GetCaseNatureList = dataAccessObject.ReadCollection<CaseNature>(dbConnection.dr);
            dbConnection.dr.Close();

            return GetCaseNatureList;
        }
    }
}
