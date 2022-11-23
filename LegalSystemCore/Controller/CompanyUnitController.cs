using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LegalSystemCore.Controller
{
    
        public interface ICompanyUnitController
        {
            int Save(CompanyUnit companyUnit);
            int Update(CompanyUnit companyUnit);
            List<CompanyUnit> GetCompanyUnitList(bool withCompanyName);
            List<CompanyUnit> GetCompanyUnitListFilter(string withCompanyId);

    }

    public class CompanyUnitControllerImpl : ICompanyUnitController
    {
        ICompanyUnitDAO companyUnitDAO = DAOFactory.CreateCompanyUnitDAO();

        public int Save(CompanyUnit companyUnit)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return companyUnitDAO.Save(companyUnit, dbconnection);
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

        public int Update(CompanyUnit companyUnit)
        {
            Common.DbConnection dbConnection = null;
            try
            {
                dbConnection = new Common.DbConnection();
                return companyUnitDAO.Update(companyUnit, dbConnection);
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

            public List<CompanyUnit> GetCompanyUnitList(bool withCompanyName)
            {
                Common.DbConnection dbConnection = null;
                List<CompanyUnit> listCompanyUnit = new List<CompanyUnit>();
                try
                {
                    dbConnection = new Common.DbConnection();
                    listCompanyUnit = companyUnitDAO.GetCompanyUnitList(dbConnection);
                    if (withCompanyName)
                    {
                        ICompanyDAO companyDAO = DAOFactory.CreateCompanyDAO();
                        List<Company> listCompany = companyDAO.GetCompanyList(dbConnection);

                        foreach (var companyUnit in listCompanyUnit)
                        {
                            companyUnit.company = listCompany.Where(x => x.CompanyId == companyUnit.CompanyId ).Single();
                        }
                    }
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
                return listCompanyUnit;
            }
            public List<CompanyUnit> GetCompanyUnitListFilter(string withCompanyId)
            {
                Common.DbConnection dbConnection = null;
                List<CompanyUnit> listCompanyUnit = new List<CompanyUnit>();
                try
                {
                    dbConnection = new Common.DbConnection();
                    listCompanyUnit = companyUnitDAO.GetCompanyUnitList(dbConnection,withCompanyId);
                    
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
                return listCompanyUnit;
            }

    }

}
