using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface ICompanyUnitDAO
    {
        int Save(CompanyUnit companyUnit, DbConnection dbConnection);
        int Delete(CompanyUnit companyUnit, DbConnection dbConnection);
        int Update(CompanyUnit companyUnit, DbConnection dbConnection);
        List<CompanyUnit> GetCompanyUnitList(DbConnection dbConnection, string CompanyId = null);
    }

    public class CompanyUnitSqlDAOImpl : ICompanyUnitDAO
    {
        public int Save(CompanyUnit companyUnit, DbConnection dbConnection)
        {

            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into company_unit (company_unit_name,company_id) " +
                                           "values (@CompanyUnitName,@CompanyId) SELECT SCOPE_IDENTITY() ";


            dbConnection.cmd.Parameters.AddWithValue("@CompanyUnitName", companyUnit.CompanyUnitName);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", companyUnit.CompanyId);


            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());


            return output;
        }

        public int Update(CompanyUnit companyUnit, DbConnection dbConnection)
        {

            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update company_unit set company_unit_name = @CompanyUnitName ,company_id = @CompanyId WHERE company_unit_id = @CompanyUnitId ";


            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", companyUnit.CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyUnitName", companyUnit.CompanyUnitName);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyUnitId", companyUnit.CompanyUnitId);

            output = dbConnection.cmd.ExecuteNonQuery();


            return output;
        }

        public List<CompanyUnit> GetCompanyUnitList(DbConnection dbConnection, string companyId = null)
        {
            List<CompanyUnit> listCompanyUnit = new List<CompanyUnit>();

            dbConnection = new DbConnection();

            String query = "select * from company_unit WHERE is_active = 1";

            if (companyId != null)
            {
                query = query + " " + " AND company_id=" + companyId;
            }

            dbConnection.cmd.CommandText = query;

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listCompanyUnit = dataAccessObject.ReadCollection<CompanyUnit>(dbConnection.dr);
            dbConnection.dr.Close();


            return listCompanyUnit;
        }
        public int Delete(CompanyUnit companyUnit, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE company_unit SET is_active = 0 WHERE activity_id = @CompanyUnitId ";

            dbConnection.cmd.Parameters.AddWithValue("@CompanyUnitId", companyUnit.CompanyUnitId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }
    }
}
