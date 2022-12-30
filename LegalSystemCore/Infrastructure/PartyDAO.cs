using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface IPartyDAO
    {
        int Save(Party party, DbConnection dbConnection);
        int Update(Party party, DbConnection dbConnection);
        int Delete(Party party, DbConnection dbConnection);

        Party GetParty(int partyId, DbConnection dbConnection);

        List<Party> GetPartyList(DbConnection dbConnection);

    }

    public class PartyDAOSqlImpl : IPartyDAO
    {
        public List<Party> GetPartyList(DbConnection dbConnection)
        {
            List<Party> partyList = new List<Party>();
            dbConnection.cmd.CommandText = "select * from party WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            partyList = dataAccessObject.ReadCollection<Party>(dbConnection.dr);
            dbConnection.dr.Close();

            return partyList;
        }

        public Party GetParty(int partyId, DbConnection dbConnection)
        {
            Party party = new Party();

            //dbConnection = new DbConnection();
            dbConnection.cmd.CommandText = "select * from party WHERE party_id = @PartyId";
            dbConnection.cmd.Parameters.AddWithValue("@PartyId", partyId);

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            party = dataAccessObject.GetSingleOject<Party>(dbConnection.dr);
            dbConnection.dr.Close();

            return party;
        }


        public int Save(Party party, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            dbConnection.cmd.CommandText = "Insert into party ( party_name ) output INSERTED.party_id " +
                "values (@PartyName) SELECT SCOPE_IDENTITY()";


            dbConnection.cmd.Parameters.AddWithValue("@PartyName", party.PartyName);


            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }



        public int Update(Party party, DbConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int Delete(Party party, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE party SET is_active = 0 WHERE party_id = @PartyId ";

            dbConnection.cmd.Parameters.AddWithValue("@PartyId", party.PartyId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

    }
}
