using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;


namespace LegalSystemCore.Controller
{

    public interface ICompanyController
    {
        int Save(Company company);
        int Delete(Company company);
        int Update(Company company);
        Company GetCompany(int CompanyId);
        List<Company> GetCompanyList();

    }

    public class CompanyControllerImpl : ICompanyController
    {
        ICompanyDAO companyDAO = DAOFactory.CreateCompanyDAO();

        public int Save(Company company)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return companyDAO.Save(company, dbconnection);
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
            Common.DbConnection dbConnection = null;
            try
            {
                dbConnection = new Common.DbConnection();
                return companyDAO.Update(company, dbConnection);
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
        }

        public Company GetCompany(int CompanyId)
        {
            Common.DbConnection dbConnection = null;
            Company company = new Company();
            try
            {
                dbConnection = new Common.DbConnection();
                company = companyDAO.GetCompany(CompanyId, dbConnection);

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
            return company;
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
        public int Delete(Company company)
        {
            Common.DbConnection dbConnection = null;
            try
            {
                dbConnection = new Common.DbConnection();
                return companyDAO.Delete(company, dbConnection);
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
        }
    }

}
