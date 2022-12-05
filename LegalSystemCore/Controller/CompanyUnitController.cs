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
        int Delete(CompanyUnit companyUnit);
        int Update(CompanyUnit companyUnit);
        CompanyUnit GetCompanyUnit(int unitId);
        List<CompanyUnit> GetCompanyUnitList(bool with0, bool withCompanyName);
        List<CompanyUnit> GetCompanyUnitListFilter(bool with0, string withCompanyId);

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

        public int Delete(CompanyUnit companyUnit)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return companyUnitDAO.Delete(companyUnit, dbconnection);
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

        public CompanyUnit GetCompanyUnit(int unitId)
        {
            Common.DbConnection dbConnection = null;
            CompanyUnit companyUnit = new CompanyUnit();
            try
            {
                dbConnection = new Common.DbConnection();
                companyUnit = companyUnitDAO.GetCompanyUnit(unitId, dbConnection);

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
            return companyUnit;
        }

        public List<CompanyUnit> GetCompanyUnitList(bool with0, bool withCompanyName)
        {
            Common.DbConnection dbConnection = null;
            List<CompanyUnit> listCompanyUnit = new List<CompanyUnit>();
            try
            {
                dbConnection = new Common.DbConnection();
                listCompanyUnit = companyUnitDAO.GetCompanyUnitList(with0, dbConnection);
                if (withCompanyName)
                {
                    ICompanyDAO companyDAO = DAOFactory.CreateCompanyDAO();
                    List<Company> listCompany = companyDAO.GetCompanyList(false, dbConnection);

                    foreach (var companyUnit in listCompanyUnit)
                    {
                        companyUnit.company = listCompany.Where(x => x.CompanyId == companyUnit.CompanyId).Single();
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
        public List<CompanyUnit> GetCompanyUnitListFilter(bool with0, string withCompanyId)
        {
            Common.DbConnection dbConnection = null;
            List<CompanyUnit> listCompanyUnit = new List<CompanyUnit>();
            try
            {
                dbConnection = new Common.DbConnection();
                listCompanyUnit = companyUnitDAO.GetCompanyUnitList(with0, dbConnection, withCompanyId);

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
