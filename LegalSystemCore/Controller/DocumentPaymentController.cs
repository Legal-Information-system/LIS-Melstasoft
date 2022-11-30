using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface IDocumentPaymentController
    {
        int Save(DocumentPayment documentCase);
        int Update(DocumentPayment documentCase);
        int Delete(DocumentPayment documentCase);
        List<DocumentPayment> GetDocumentList();
        DocumentPayment GetDocument(int documentPaymentId);
    }

    public class DocumentPaymentControllerImpl : IDocumentPaymentController
    {
        IDocumentPaymentDAO documentPaymentDAO = DAOFactory.CreateDocumentPaymentDAO();

        public DocumentPayment GetDocument(int documentPaymentId)
        {
            DbConnection dbConnection = null;
            DocumentPayment documentPayment = new DocumentPayment();
            try
            {
                dbConnection = new DbConnection();
                documentPayment = documentPaymentDAO.GetDocumentPayment(documentPaymentId, dbConnection);
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
            return documentPayment;
        }

        public List<DocumentPayment> GetDocumentList()
        {
            DbConnection dbConnection = null;
            List<DocumentPayment> listDocumentPayment = new List<DocumentPayment>();
            try
            {
                dbConnection = new DbConnection();
                listDocumentPayment = documentPaymentDAO.GetDocumentPaymentList(dbConnection);
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
            return listDocumentPayment;
        }

        public int Save(DocumentPayment documentPayment)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return documentPaymentDAO.Save(documentPayment, dbConnection);
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

        public int Update(DocumentPayment documentPayment)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return documentPaymentDAO.Update(documentPayment, dbConnection);
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

        public int Delete(DocumentPayment documentPayment)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return documentPaymentDAO.Delete(documentPayment, dbConnection);
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
