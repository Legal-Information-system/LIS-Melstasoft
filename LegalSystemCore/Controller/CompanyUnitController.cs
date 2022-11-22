using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    
        public interface ICompanyUnitController
        {
            int Save(CompanyUnit companyUnit);
            int Update(CompanyUnit companyUnit);
            List<CompanyUnit> GetCompanyUnitList();

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
                Common.DbConnection dbconnection = null;
                try
                {
                    dbconnection = new Common.DbConnection();
                    return companyUnitDAO.Update(companyUnit, dbconnection);
                }
                catch (Exception)
                {
                    dbconnection.RollBack();
                    throw;
                }
            }

            public List<CompanyUnit> GetCompanyUnitList()
            {
                Common.DbConnection dbConnection = null;
                List<CompanyUnit> listCompanyUnit = new List<CompanyUnit>();
                try
                {
                    dbConnection = new Common.DbConnection();
                    listCompanyUnit = companyUnitDAO.GetCompanyUnitList(dbConnection);

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
