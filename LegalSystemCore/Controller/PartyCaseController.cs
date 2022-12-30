using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface IPartyCaseController
    {
        int Save(PartyCase partyCase);
        int Update(PartyCase partyCase);
        List<PartyCase> GetPartyCaseList(string caseNumber);
        int Delete(PartyCase partyCase);
    }

    public class PartyCaseControllerImpl : IPartyCaseController
    {
        IPartyCaseDAO partyCaseDAO = DAOFactory.CreatePartyCaseDAO();
        public int Save(PartyCase partyCase)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return partyCaseDAO.Save(partyCase, dbconnection);
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

        public List<PartyCase> GetPartyCaseList(string caseNumber)
        {
            Common.DbConnection dbConnection = null;
            List<PartyCase> listPartyCase = new List<PartyCase>();

            try
            {
                dbConnection = new Common.DbConnection();
                listPartyCase = partyCaseDAO.GetPartyCaseList(caseNumber, dbConnection);

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
            return listPartyCase;
        }

        public int Delete(PartyCase partyCase)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return partyCaseDAO.Delete(partyCase, dbConnection);
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

        public int Update(PartyCase partyCase)
        {
            Common.DbConnection dbConnection = null;
            try
            {
                dbConnection = new Common.DbConnection();
                return partyCaseDAO.Update(partyCase, dbConnection);
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
