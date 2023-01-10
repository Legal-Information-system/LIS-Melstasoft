using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface IPartyCaseDAO
    {
        int Save(PartyCase partyCase, DbConnection dbConnection);
        int Update(PartyCase partyCase, DbConnection dbConnection);
        int Delete(PartyCase partyCase, DbConnection dbConnection);

        int DeletePermenent(String CaseNumber, DbConnection dbConnection);
        int ReInit(PartyCase partyCase, DbConnection dbConnection);

        List<PartyCase> GetPartyCaseList(string caseNumber, DbConnection dbConnection);
        List<PartyCase> GetPartyCaseListDeleted(string caseNumber, DbConnection dbConnection);

    }

    public class PartyCaseDAOSqlImpl : IPartyCaseDAO
    {

        public List<PartyCase> GetPartyCaseList(string caseNumber, DbConnection dbConnection)
        {
            List<PartyCase> listPartyCase = new List<PartyCase>();

            dbConnection.cmd.CommandText = "select * from party_case WHERE is_active = 1 AND case_number = @CaseNumber";
            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", caseNumber);


            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listPartyCase = dataAccessObject.ReadCollection<PartyCase>(dbConnection.dr);
            dbConnection.dr.Close();

            return listPartyCase;
        }

        public List<PartyCase> GetPartyCaseListDeleted(string caseNumber, DbConnection dbConnection)
        {
            List<PartyCase> listPartyCase = new List<PartyCase>();

            dbConnection.cmd.CommandText = "select * from party_case WHERE is_active = 0 AND case_number = @CaseNumber";
            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", caseNumber);


            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listPartyCase = dataAccessObject.ReadCollection<PartyCase>(dbConnection.dr);
            dbConnection.dr.Close();

            return listPartyCase;
        }


        public int Save(PartyCase partyCase, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            dbConnection.cmd.CommandText = "Insert into party_case ( party_id, case_number, is_plaintif )  " +
                "values (@PartyId,@CaseNumber,@IsPlaintif) ";


            dbConnection.cmd.Parameters.AddWithValue("@PartyId", partyCase.PartyId);
            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", partyCase.CaseNumber);
            dbConnection.cmd.Parameters.AddWithValue("@IsPlaintif", partyCase.IsPlaintif);


            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }



        public int Update(PartyCase partyCase, DbConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int Delete(PartyCase partyCase, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE party_case SET is_active = 0 WHERE party_id = @PartyId AND case_number = @CaseNumber ";

            dbConnection.cmd.Parameters.AddWithValue("@PartyId", partyCase.PartyId);
            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", partyCase.CaseNumber);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int DeletePermenent(String CaseNumber, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "DELETE FROM party_case  WHERE case_number = @CaseNumber ";


            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", CaseNumber);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }
        public int ReInit(PartyCase partyCase, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE party_case SET is_active = 0 , is_plaintif = @IsPlaintif WHERE party_id = @PartyId AND case_number = @CaseNumber ";

            dbConnection.cmd.Parameters.AddWithValue("@PartyId", partyCase.PartyId);
            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", partyCase.CaseNumber);
            dbConnection.cmd.Parameters.AddWithValue("@IsPlaintif", partyCase.IsPlaintif);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

    }
}
