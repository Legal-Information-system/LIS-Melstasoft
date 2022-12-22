using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface IDocumentDAO
    {
        int Save(Document document, DbConnection dbConnection);
        int Update(Document document, DbConnection dbConnection);
        int Delete(Document document, DbConnection dbConnection);
        List<Document> GetDocumentList(DbConnection dbConnection);
        Document GetDocument(int documentId, DbConnection dbConnection);

    }
    public class DocumentDAOSqlImpl : IDocumentDAO
    {

        public Document GetDocument(int documentId, DbConnection dbConnection)
        {
            Document document = new Document();

            //dbConnection = new DbConnection();
            dbConnection.cmd.CommandText = "select * from document WHERE document_id =" + documentId;

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            document = dataAccessObject.GetSingleOject<Document>(dbConnection.dr);
            dbConnection.dr.Close();

            return document;
        }

        public List<Document> GetDocumentList(DbConnection dbConnection)
        {
            List<Document> listDocument = new List<Document>();

            //dbConnection = new DbConnection();
            dbConnection.cmd.CommandText = "select * from document WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listDocument = dataAccessObject.ReadCollection<Document>(dbConnection.dr);
            dbConnection.dr.Close();

            return listDocument;
        }

        public int Save(Document document, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "INSERT INTO document (document_type) " +
            "VALUES (@DocumentType) SELECT SCOPE_IDENTITY()";

            dbConnection.cmd.Parameters.AddWithValue("@DocumentType", document.DocumentType);

            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }

        public int Update(Document document, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE document SET document_type = @DocumentType WHERE document_id = @DocumentId ";

            dbConnection.cmd.Parameters.AddWithValue("@DocumentType", document.DocumentType);
            dbConnection.cmd.Parameters.AddWithValue("@DocumentId", document.DocumentId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int Delete(Document document, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE document SET is_active = 0 WHERE document_id = @DocumentId ";

            dbConnection.cmd.Parameters.AddWithValue("@DocumentId", document.DocumentId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }
    }
}
