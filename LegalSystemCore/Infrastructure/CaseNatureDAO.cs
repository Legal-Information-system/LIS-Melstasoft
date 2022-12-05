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

        int Delete(CaseNature caseNature, DbConnection dbConnection);
        int Update(CaseNature caseNature, DbConnection dbConnection);
        CaseNature GetCaseNature(int id, DbConnection dbConnection);

        List<CaseNature> GetCaseNatureList(bool with0, DbConnection dbConnection);
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

        public List<CaseNature> GetCaseNatureList(bool with0, DbConnection dbConnection)
        {
            List<CaseNature> GetCaseNatureList = new List<CaseNature>();

            if (with0)
                dbConnection.cmd.CommandText = "select * from case_nature";
            else
                dbConnection.cmd.CommandText = "select * from case_nature WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            GetCaseNatureList = dataAccessObject.ReadCollection<CaseNature>(dbConnection.dr);
            dbConnection.dr.Close();

            return GetCaseNatureList;
        }

        public CaseNature GetCaseNature(int id, DbConnection dbConnection)
        {
            CaseNature caseNature = new CaseNature();
            dbConnection.cmd.CommandText = "select * from case_nature WHERE case_nature_id = @CaseNatureId";

            dbConnection.cmd.Parameters.AddWithValue("@CaseNatureId", id);

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            caseNature = dataAccessObject.GetSingleOject<CaseNature>(dbConnection.dr);
            dbConnection.dr.Close();

            return caseNature;
        }

        public int Update(CaseNature caseNature, DbConnection dbConnection)
        {

            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update case_nature set case_nature_name = @CaseNatureName WHERE case_nature_id = @CaseNatureId ";


            dbConnection.cmd.Parameters.AddWithValue("@CaseNatureId", caseNature.CaseNatureId);
            dbConnection.cmd.Parameters.AddWithValue("@CaseNatureName", caseNature.CaseNatureName);


            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int Delete(CaseNature caseNature, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE case_nature SET is_active = 0 WHERE case_nature_id = @natureId ";

            dbConnection.cmd.Parameters.AddWithValue("@natureId", caseNature.CaseNatureId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }
    }
}
