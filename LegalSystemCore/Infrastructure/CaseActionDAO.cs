using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface ICaseActionDAO
    {
        int Save(CaseAction caseAction, DbConnection dbConnection);
        int Update(CaseAction caseAction, DbConnection dbConnection);
        List<CaseAction> GetCaseActionList(DbConnection dbConnection);
    }

    public class CaseActionDAOSqlImpl : ICaseActionDAO
    {
        public List<CaseAction> GetCaseActionList(DbConnection dbConnection)
        {
            List<CaseAction> listCaseAction = new List<CaseAction>();

            dbConnection = new DbConnection();

            dbConnection.cmd.CommandText = "select * from case_action";
            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listCaseAction = dataAccessObject.ReadCollection<CaseAction>(dbConnection.dr);
            dbConnection.dr.Close();

            return listCaseAction;
        }

        public int Save(CaseAction caseAction, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "INSERT INTO case_action (case_action_name) " +
                                           "VALUES (@ActionName)";

            dbConnection.cmd.Parameters.AddWithValue("@ActionName", caseAction.ActionName);

            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }

        public int Update(CaseAction caseAction, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE case_action SET case_action_name = @ActionName WHERE case_action_id = @ActionId ";

            dbConnection.cmd.Parameters.AddWithValue("@ActionName", caseAction.ActionName);
            dbConnection.cmd.Parameters.AddWithValue("@ActionId", caseAction.ActionId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }
    }
}
