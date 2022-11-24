using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegalSystemCore.Controller
{
    public interface IDocumentController
    {
        int Save(Document document);
        int Update(Document document);
        int Delete(Document document);
        List<Document> GetDocumentList();
        Document GetDocument(int documentId);
    }

    public class DocumentControllerImpl : IDocumentController
    {
        IDocumentDAO documentDAO = DAOFactory.CreateDocumentDAO();

        public Document GetDocument(int documentId)
        {
            DbConnection dbConnection = null;
            Document document = new Document();
            try
            {
                dbConnection = new DbConnection();
                document = documentDAO.GetDocument(documentId, dbConnection);
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
            return document;
        }

        public List<Document> GetDocumentList()
        {
            DbConnection dbConnection = null;
            List<Document> listDocument = new List<Document>();
            try
            {
                dbConnection = new DbConnection();
                listDocument = documentDAO.GetDocumentList(dbConnection);
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
            return listDocument;
        }

        public int Save(Document document)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return documentDAO.Save(document, dbConnection);
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

        public int Update(Document document)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return documentDAO.Update(document, dbConnection);
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

        public int Delete(Document document)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return documentDAO.Delete(document, dbConnection);
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
