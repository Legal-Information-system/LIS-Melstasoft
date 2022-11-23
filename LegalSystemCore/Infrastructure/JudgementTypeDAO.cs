using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalSystemCore.Infrastructure
{
    public interface IJudgementTypeDAO
    {
        int Save(JudgementType judgementType, DbConnection dbConnection);
        int Update(JudgementType judgementType, DbConnection dbConnection);
        List<JudgementType> GetJudgementTypeList(DbConnection dbConnection);
    }

    public class JudgementTypeDAOSqlImpl : IJudgementTypeDAO
    {
        public List<JudgementType> GetJudgementTypeList(DbConnection dbConnection)
        {
            List<JudgementType> listJudgementType = new List<JudgementType>();

            dbConnection = new DbConnection();

            dbConnection.cmd.CommandText = "select * from judgement_type WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listJudgementType = dataAccessObject.ReadCollection<JudgementType>(dbConnection.dr);
            dbConnection.dr.Close();


            return listJudgementType;
        }

        public int Save(JudgementType judgementType, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "INSERT INTO judgement_type (judgement_type_name) " +
            "VALUES (@JTypeName)";

            dbConnection.cmd.Parameters.AddWithValue("@JTypeName", judgementType.JTypeName);

            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }

        public int Update(JudgementType judgementType, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE judgement_type SET judgement_type_name = @JTypeName WHERE judgement_type_id = @JTypeId ";

            dbConnection.cmd.Parameters.AddWithValue("@JTypeName", judgementType.JTypeName);
            dbConnection.cmd.Parameters.AddWithValue("@JTypeId", judgementType.JTypeId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }
    }
}
