using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public class CounselorDAO
    {
    }

    public interface ICounselorDAO
    {
        void Save(Counselor counselor, DbConnection dbConnection);
        List<Counselor> GetCounselorList(DbConnection dbConnection, string CustomQuery = null);

        int Delete(Counselor counselor, DbConnection dbConnection);

    }
    public class CounselorDAOSqlImpl : ICounselorDAO
    {
        public void Save(Counselor counselor, DbConnection dbConnection)
        {
            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into counselor (case_number,lawyer_id) " +
                                           "values (@CaseNumber,@LawyerId) SELECT SCOPE_IDENTITY() ";



            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", counselor.CaseNumber);
            dbConnection.cmd.Parameters.AddWithValue("@LawyerId", counselor.LawyerId);




            dbConnection.cmd.ExecuteScalar();



        }

        public List<Counselor> GetCounselorList(DbConnection dbConnection, string CustomQuery = null)
        {
            List<Counselor> CounselorList = new List<Counselor>();
            if (CustomQuery != null)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                dbConnection.cmd.CommandText = "SELECT * FROM counselor WHERE case_number = @CaseNumber AND is_active = 1;";

                dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", CustomQuery);
            }

            else
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                dbConnection.cmd.CommandText = "SELECT * FROM counselor WHERE is_active = 1;";
            }

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            CounselorList = dataAccessObject.ReadCollection<Counselor>(dbConnection.dr);
            dbConnection.dr.Close();

            return CounselorList;
        }

        public int Delete(Counselor counselor, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE counselor SET is_active = 0 WHERE case_number = @CaseNumber AND lawyer_id = @LawyerId ";

            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", counselor.CaseNumber);
            dbConnection.cmd.Parameters.AddWithValue("@LawyerId", counselor.LawyerId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }


    }
}
