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
    public interface IDocumentCaseController
    {
        int Save(DocumentCase documentCase);
        int Update(DocumentCase documentCase);
        int Delete(DocumentCase documentCase);
        List<DocumentCase> GetDocumentList();
        DocumentCase GetDocument(int documentCaseId);
    }

    public class DocumentCaseControllerImpl : IDocumentCaseController
    {
        IDocumentCaseDAO documentCaseDAO = DAOFactory.CreateDocumentCaseDAO();

        public DocumentCase GetDocument(int documentCaseId)
        {
            DbConnection dbConnection = null;
            DocumentCase documentCase = new DocumentCase();
            try
            {
                dbConnection = new DbConnection();
                documentCase = documentCaseDAO.GetDocumentCase(documentCaseId, dbConnection);
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
            return documentCase;
        }

        public List<DocumentCase> GetDocumentList()
        {
            DbConnection dbConnection = null;
            List<DocumentCase> listDocumentCase = new List<DocumentCase>();
            try
            {
                dbConnection = new DbConnection();
                listDocumentCase = documentCaseDAO.GetDocumentCaseList(dbConnection);
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
            return listDocumentCase;
        }

        public int Save(DocumentCase documentCase)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return documentCaseDAO.Save(documentCase, dbConnection);
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

        public int Update(DocumentCase documentCase)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return documentCaseDAO.Update(documentCase, dbConnection);
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

        public int Delete(DocumentCase documentCase)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return documentCaseDAO.Delete(documentCase, dbConnection);
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
