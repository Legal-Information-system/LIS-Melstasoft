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
        List<Lawyer> GetLawyerList(DbConnection dbConnection);

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

        public List<Lawyer> GetLawyerList(DbConnection dbConnection)
        {

            List<Lawyer> lawyerList = new List<Lawyer>();

            dbConnection.cmd.CommandText = "select * from lawyer WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            lawyerList = dataAccessObject.ReadCollection<Lawyer>(dbConnection.dr);
            dbConnection.dr.Close();

            return lawyerList;

        }
    }
}
