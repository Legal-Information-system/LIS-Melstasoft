using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface ICaseNatureController
    {
        int Save(CaseNature caseNature);
        int Update(CaseNature caseNature);
        List<CaseNature> GetCaseNatureList();
    }

    public class CaseNatureControllerImpl : ICaseNatureController
    {

        ICaseNatureDAO caseNatureDAO = DAOFactory.CreateCaseNatureDAO();
        public int Save(CaseNature caseNature)
        {
            Common.DbConnection dbConnection = null;
            try
            {
                dbConnection = new Common.DbConnection();
                return caseNatureDAO.Save(caseNature, dbConnection);

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
        public List<CaseNature> GetCaseNatureList()
        {
            Common.DbConnection dbConnection = null;
            List<CaseNature> listCaseNature = new List<CaseNature>();

            try
            {
                dbConnection = new Common.DbConnection();
                listCaseNature = caseNatureDAO.GetCaseNatureList(dbConnection);

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
            return listCaseNature;
        }
        public int Update(CaseNature caseNature)
        {
            Common.DbConnection dbConnection = null;
            try
            {
                dbConnection = new Common.DbConnection();
                return caseNatureDAO.Update(caseNature, dbConnection);
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
