using LegalSystemCore.Common;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;


namespace LegalSystemCore.Controller
{
    public class CompanyController
    {
        public interface ICompanyController
        {
            int Save(Company company);
            int Update(Company company);
            List<Company> GetCompanyList();

        }

        public class CompanyControllerimpl : ICompanyController
        {
            ICompanyDAO companyDAO = DAOFactory.CreateCompanyDAO();

            public int Save(Company company)
            {
                Common.DbConnection dbconnection = null;
                try
                {
                    dbconnection = new Common.DbConnection();
                    return companyDAO.Save(company,dbconnection);
                }
                catch (Exception)
                {
                    dbconnection.RollBack();
                    throw;
                }
                finally
                {
                    if (dbconnection.con.State == System.Data.ConnectionState.Open)
                    {
                        dbconnection.Commit();
                    }
                }
            }

            public int Update(Company company)
            {
                Common.DbConnection dbconnection = null;
                try
                {
                    dbconnection = new Common.DbConnection();
                    return companyDAO.Update(company, dbconnection);
                }
                catch (Exception)
                {
                    dbconnection.RollBack();
                    throw;
                }
            }

            public List<Company> GetCompanyList()
            {
                Common.DbConnection dbConnection = null;
                List<Company> listCompany = new List<Company>();
                try
                {
                    dbConnection = new Common.DbConnection();
                    listCompany = companyDAO.GetCompanyList(dbConnection);
                    
                }
                catch (Exception)
                {
                    dbConnection.RollBack();
                    throw;
                }
                finally
                {
                    if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    {
                        dbConnection.Commit();
                    }
                }
                return listCompany;
            }

        }
    }
}
