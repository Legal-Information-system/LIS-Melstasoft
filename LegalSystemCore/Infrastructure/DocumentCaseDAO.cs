using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LegalSystemCore.Infrastructure
{
    public interface IDocumentCaseDAO
    {
        int Save(DocumentCase documentCase, DbConnection dbConnection);
        int Update(DocumentCase documentCase, DbConnection dbConnection);
        int Delete(DocumentCase documentCase, DbConnection dbConnection);
        List<DocumentCase> GetDocumentCaseList(DbConnection dbConnection);
        DocumentCase GetDocumentCase(int documentCaseId, DbConnection dbConnection);
    }

    public class DocumentCaseDAOSqlImpl : IDocumentCaseDAO
    {

        public DocumentCase GetDocumentCase(int documentCaseId, DbConnection dbConnection)
        {
            DocumentCase documentCase = new DocumentCase();

            dbConnection = new DbConnection();
            dbConnection.cmd.CommandText = "select * from case_document WHERE case_document_id =" + documentCaseId;

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            documentCase = dataAccessObject.GetSingleOject<DocumentCase>(dbConnection.dr);
            dbConnection.dr.Close();

            return documentCase;
        }

        public List<DocumentCase> GetDocumentCaseList(DbConnection dbConnection)
        {
            List<DocumentCase> listDocumentCase = new List<DocumentCase>();

            dbConnection = new DbConnection();
            dbConnection.cmd.CommandText = "select * from case_document WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listDocumentCase = dataAccessObject.ReadCollection<DocumentCase>(dbConnection.dr);
            dbConnection.dr.Close();

            return listDocumentCase;
        }

        public int Save(DocumentCase documentCase, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "INSERT INTO case_document (document_type_id, document_name, document_description, case_number) " +
            "VALUES (@DocumentId, @DocumentName, @DocumentDescription, @CaseNumber) SELECT SCOPE_IDENTITY()";

            dbConnection.cmd.Parameters.AddWithValue("@DocumentId", documentCase.DocumentId);
            dbConnection.cmd.Parameters.AddWithValue("@DocumentName", documentCase.DocumentName);
            dbConnection.cmd.Parameters.AddWithValue("@DocumentDescription", documentCase.DocumentDescription);
            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", documentCase.CaseNumber);


            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }

        public int Update(DocumentCase documentCase, DbConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int Delete(DocumentCase documentCase, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE case_document SET is_active = 0 WHERE case_document_id = @DocumentCaseId ";

            dbConnection.cmd.Parameters.AddWithValue("@DocumentCaseId", documentCase.DocumentCaseId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }
    }
}
