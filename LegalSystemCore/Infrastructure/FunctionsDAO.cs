using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface IFunctionsDAO
    {
        int Save(Functions functions, DbConnection dbConnection);
        int Update(Functions functions, DbConnection dbConnection);
        List<Functions> GetFunctionList(bool with0, DbConnection dbConnection);

        Functions GetFunctions(DbConnection dbConnection, string functionName);
    }
    public class FunctionsDAOSqlImpl : IFunctionsDAO
    {
        public int Save(Functions functions, DbConnection dbConnection)
        {
            int output = 0;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Insert into functions (function_name) " +
                                           "values (@FunctionName) SELECT SCOPE_IDENTITY() ";



            dbConnection.cmd.Parameters.AddWithValue("@FunctionName", functions.FunctionName);




            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());


            return output;
        }

        public List<Functions> GetFunctionList(bool with0, DbConnection dbConnection)
        {
            List<Functions> functionList = new List<Functions>();
            if (with0)
                dbConnection.cmd.CommandText = "select * from functions";
            else
                dbConnection.cmd.CommandText = "select * from functions WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            functionList = dataAccessObject.ReadCollection<Functions>(dbConnection.dr);
            dbConnection.dr.Close();

            return functionList;
        }
        public int Update(Functions functions, DbConnection dbConnection)
        {

            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update functions set function_name = @FunctionName WHERE function_id = @FunctionId ";


            dbConnection.cmd.Parameters.AddWithValue("@FunctionId", functions.FunctionId);
            dbConnection.cmd.Parameters.AddWithValue("@FunctionName", functions.FunctionName);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }



        public Functions GetFunctions(DbConnection dbConnection, string functionName)
        {
            Functions functions = new Functions();

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "select * from functions WHERE function_id = @FunctionId";

            dbConnection.cmd.Parameters.AddWithValue("@FunctionId", functionName);

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            functions = dataAccessObject.GetSingleOject<Functions>(dbConnection.dr);
            dbConnection.dr.Close();

            return functions;
        }

    }
}
