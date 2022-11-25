using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface IDocumentPaymentDAO
    {
        int Save(DocumentPayment documentPayment, DbConnection dbConnection);
        int Update(DocumentPayment documentPayment, DbConnection dbConnection);
        int Delete(DocumentPayment documentPayment, DbConnection dbConnection);
        List<DocumentPayment> GetDocumentPaymentList(DbConnection dbConnection);
        DocumentPayment GetDocumentPayment(int documentPaymentId, DbConnection dbConnection);
    }

    public class DocumentPaymentDAOSqlImpl : IDocumentPaymentDAO
    {

        public DocumentPayment GetDocumentPayment(int documentPaymentId, DbConnection dbConnection)
        {
            DocumentPayment documentPayment = new DocumentPayment();

            dbConnection = new DbConnection();
            dbConnection.cmd.CommandText = "select * from payment_document WHERE paymnent_document_id =" + documentPaymentId + "AND is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            documentPayment = dataAccessObject.GetSingleOject<DocumentPayment>(dbConnection.dr);
            dbConnection.dr.Close();

            return documentPayment;
        }

        public List<DocumentPayment> GetDocumentPaymentList(DbConnection dbConnection)
        {
            List<DocumentPayment> listDocumentPayment = new List<DocumentPayment>();

            dbConnection = new DbConnection();
            dbConnection.cmd.CommandText = "select * from payment_document WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listDocumentPayment = dataAccessObject.ReadCollection<DocumentPayment>(dbConnection.dr);
            dbConnection.dr.Close();

            return listDocumentPayment;
        }

        public int Save(DocumentPayment documentPayment, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "INSERT INTO payment_document (document_type_id, document_name, document_description, payment_id) " +
            "VALUES (@DocumentId, @DocumentName, @DocumentDescription, @PaymentId) SELECT SCOPE_IDENTITY()";

            dbConnection.cmd.Parameters.AddWithValue("@DocumentId", documentPayment.DocumentId);
            dbConnection.cmd.Parameters.AddWithValue("@DocumentName", documentPayment.DocumentName);
            dbConnection.cmd.Parameters.AddWithValue("@DocumentDescription", documentPayment.DocumentDescription);
            dbConnection.cmd.Parameters.AddWithValue("@PaymentId", documentPayment.PaymentId);


            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }

        public int Update(DocumentPayment documentPayment, DbConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int Delete(DocumentPayment documentPayment, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE payment_document SET is_active = 0 WHERE payment_document_id = @DocumentPaymentId ";

            dbConnection.cmd.Parameters.AddWithValue("@DocumentPaymentId", documentPayment.DocumentPaymentId);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }
    }
}
