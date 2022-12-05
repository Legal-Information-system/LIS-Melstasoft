using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface ILawyerDAO
    {
        int Save(Lawyer lawyer, DbConnection dbConnection);
        int Delete(Lawyer lawyer, DbConnection dbConnection);
        int Update(Lawyer lawyer, DbConnection dbConnection);
        List<Lawyer> GetLawyerList(bool with0, DbConnection dbConnection);

        Lawyer GetLawyer(int lawyerId, DbConnection dbConnection);

    }

    public class LawyerSqlDAOImpl : ILawyerDAO
    {
        public int Save(Lawyer lawyer, DbConnection dbConnection)
        {
            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into lawyer (lawyer_name,l_email,l_contact) " +
                                           "values (@LawerName,@LawerEmail,@LawerContact) SELECT SCOPE_IDENTITY() ";



            dbConnection.cmd.Parameters.AddWithValue("@LawerName", lawyer.LawyerName);
            dbConnection.cmd.Parameters.AddWithValue("@LawerEmail", lawyer.LawyerEmail);
            dbConnection.cmd.Parameters.AddWithValue("@LawerContact", lawyer.LawyerContact);



            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());


            return output;
        }

        public List<Lawyer> GetLawyerList(bool with0, DbConnection dbConnection)
        {

            List<Lawyer> lawyerList = new List<Lawyer>();

            if (with0)
                dbConnection.cmd.CommandText = "select * from lawyer";
            else
                dbConnection.cmd.CommandText = "select * from lawyer WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            lawyerList = dataAccessObject.ReadCollection<Lawyer>(dbConnection.dr);
            dbConnection.dr.Close();

            return lawyerList;

        }
        public int Update(Lawyer lawyer, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update lawyer set lawyer_name = @LawerName ,l_email = @LawerEmail,l_contact=@LawerContact WHERE lawyer_id = @LawerId ";

            dbConnection.cmd.Parameters.AddWithValue("@LawerId", lawyer.LawyerId);
            dbConnection.cmd.Parameters.AddWithValue("@LawerName", lawyer.LawyerName);
            dbConnection.cmd.Parameters.AddWithValue("@LawerEmail", lawyer.LawyerEmail);
            dbConnection.cmd.Parameters.AddWithValue("@LawerContact", lawyer.LawyerContact);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int Delete(Lawyer lawyer, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE lawyer SET is_active = 0 WHERE lawyer_id = @lawyerId";

            dbConnection.cmd.Parameters.AddWithValue("@lawyerId", lawyer.LawyerId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public Lawyer GetLawyer(int lawyerId, DbConnection dbConnection)
        {
            Lawyer lawyer = new Lawyer();

            dbConnection = new DbConnection();
            dbConnection.cmd.CommandText = "select * from lawyer WHERE lawyer_id = @LawyerId";
            dbConnection.cmd.Parameters.AddWithValue("@LawyerId", lawyerId);

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            lawyer = dataAccessObject.GetSingleOject<Lawyer>(dbConnection.dr);
            dbConnection.dr.Close();

            return lawyer;
        }
    }
}
