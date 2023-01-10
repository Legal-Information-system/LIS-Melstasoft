using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface IPartyController
    {
        int Save(Party party);
        int Update(Party party);

        int Delete(Party party);

        List<Party> GetPartyList();
        Party GetParty(int partyId);
    }

    public class PartyControllerImpl : IPartyController
    {
        IPartyDAO partyDAO = DAOFactory.CreatePartyDAO();

        public List<Party> GetPartyList()
        {
            Common.DbConnection dbConnection = null;
            List<Party> listParty = new List<Party>();

            try
            {
                dbConnection = new Common.DbConnection();
                listParty = partyDAO.GetPartyList(dbConnection);

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
            return listParty;
        }
        public int Save(Party party)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return partyDAO.Save(party, dbconnection);
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


        public int Delete(Party party)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return partyDAO.Delete(party, dbConnection);
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

        public int Update(Party party)
        {
            Common.DbConnection dbConnection = null;
            try
            {
                dbConnection = new Common.DbConnection();
                return partyDAO.Update(party, dbConnection);
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

        public Party GetParty(int partyId)
        {
            Common.DbConnection dbConnection = null;
            Party party = new Party();
            try
            {
                dbConnection = new Common.DbConnection();
                party = partyDAO.GetParty(partyId, dbConnection);

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
            return party;
        }

    }
}
