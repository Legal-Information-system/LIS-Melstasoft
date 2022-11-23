using LegalSystemCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LegalSystemCore.Infrastructure
{

    public interface ICompanyDAO
    {
        int Save(Company company, DbConnection dbConnection);
        int Update(Company company, DbConnection dbConnection);
        List<Company> GetCompanyList(DbConnection dbConnection);
    }

    public class CompanySqlDAOImpl : ICompanyDAO
    {
        public int Save(Company company, DbConnection dbConnection)
        {

            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into company (company_name,company_address) " +
                                           "values (@CompanyName,@CompanyAddress) SELECT SCOPE_IDENTITY() ";


            //dbConnection.cmd.Parameters.AddWithValue("@CompanyId", company.CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyAddress", company.CompanyAddress);


            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());


            return output;
        }

        public int Update(Company company, DbConnection dbConnection)
        {

            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update company set company_name = @CompanyName ,company_address = @CompanyAddress WHERE company_id = @CompanyId ";


            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", company.CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyAddress", company.CompanyAddress);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public List<Company> GetCompanyList(DbConnection dbConnection)
        {
            List<Company> listCompany = new List<Company>();

            dbConnection.cmd.CommandText = "select * from company WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listCompany = dataAccessObject.ReadCollection<Company>(dbConnection.dr);
            dbConnection.dr.Close();


            return listCompany;
        }
    }

}
