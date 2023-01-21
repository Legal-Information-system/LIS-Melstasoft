using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface IDocumentCaseActivityDAO
    {
        int Save(DocumentCaseActivity documentCaseActivity, DbConnection dbConnection);
        int Update(DocumentCaseActivity documentCaseActivity, DbConnection dbConnection);
        int Delete(DocumentCaseActivity documentCaseActivity, DbConnection dbConnection);
        List<DocumentCaseActivity> GetDocumentCaseActivityList(DocumentCaseActivity documentCaseActivity, bool check, DbConnection dbConnection);
        DocumentCaseActivity GetDocumentCaseActivity(int documentCaseActivityId, DbConnection dbConnection);
    }
    public class DocumentCaseActivityDAOsqlImpl : IDocumentCaseActivityDAO
    {
        public DocumentCaseActivity GetDocumentCaseActivity(int documentCaseActivityId, DbConnection dbConnection)
        {
            DocumentCaseActivity documentCase = new DocumentCaseActivity();

            //dbConnection = new DbConnection();
            dbConnection.cmd.CommandText = "select * from case_activity_document WHERE case_activity_document_id =" + documentCaseActivityId;

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            documentCase = dataAccessObject.GetSingleOject<DocumentCaseActivity>(dbConnection.dr);
            dbConnection.dr.Close();

            return documentCase;
        }

        public List<DocumentCaseActivity> GetDocumentCaseActivityList(DocumentCaseActivity documentCaseActivity, bool check, DbConnection dbConnection)
        {
            List<DocumentCaseActivity> listDocumentCase = new List<DocumentCaseActivity>();

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            if (check)
            {
                dbConnection.cmd.CommandText = "select * from case_activity_document WHERE is_active = 1 ";
            }
            else
            {
                dbConnection.cmd.CommandText = "select * from case_activity_document WHERE is_active = 1 AND case_activity_id = @CaseActivityNumber";
                dbConnection.cmd.Parameters.AddWithValue("@CaseActivityNumber", documentCaseActivity.CaseActivityId);
            }

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listDocumentCase = dataAccessObject.ReadCollection<DocumentCaseActivity>(dbConnection.dr);
            dbConnection.dr.Close();

            return listDocumentCase;
        }

        public int Save(DocumentCaseActivity documentCaseActivity, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "INSERT INTO case_activity_document (document_type_id, document_name, document_description, case_activity_id) " +
            "VALUES (@DocumentId, @DocumentName, @DocumentDescription, @CaseActivityId) SELECT SCOPE_IDENTITY()";

            dbConnection.cmd.Parameters.AddWithValue("@DocumentId", documentCaseActivity.DocumentId);
            dbConnection.cmd.Parameters.AddWithValue("@DocumentName", documentCaseActivity.DocumentName);
            dbConnection.cmd.Parameters.AddWithValue("@DocumentDescription", documentCaseActivity.DocumentDescription);
            dbConnection.cmd.Parameters.AddWithValue("@CaseActivityId", documentCaseActivity.CaseActivityId);


            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }

        public int Update(DocumentCaseActivity documentCaseActivity, DbConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int Delete(DocumentCaseActivity documentCaseActivity, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE case_activity_document SET is_active = 0 WHERE case_activity_document_id = @DocumentCaseId ";

            dbConnection.cmd.Parameters.AddWithValue("@DocumentCaseId", documentCaseActivity.DocumentCaseActivityId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }
    }
}

