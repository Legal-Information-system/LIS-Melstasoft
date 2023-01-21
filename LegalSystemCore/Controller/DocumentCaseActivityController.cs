using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface IDocumentCaseActivityController
    {
        int Save(DocumentCaseActivity documentCaseActivity);
        int Update(DocumentCaseActivity documentCaseActivity);
        int Delete(DocumentCaseActivity documentCaseActivity);
        List<DocumentCaseActivity> GetDocumentList(DocumentCaseActivity documentCaseActivity, bool check);
        DocumentCaseActivity GetDocument(int documentCaseActivityId);
    }

    public class DocumentCaseActivityControllerImpl : IDocumentCaseActivityController
    {
        IDocumentCaseActivityDAO documentCaseActivityDAO = DAOFactory.CreateDocumentCaseActivityDAO();

        public DocumentCaseActivity GetDocument(int documentCaseActivityId)
        {
            DbConnection dbConnection = null;
            DocumentCaseActivity documentCase = new DocumentCaseActivity();
            try
            {
                dbConnection = new DbConnection();
                documentCase = documentCaseActivityDAO.GetDocumentCaseActivity(documentCaseActivityId, dbConnection);
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

        public List<DocumentCaseActivity> GetDocumentList(DocumentCaseActivity documentCaseActivity, bool check)
        {
            DbConnection dbConnection = null;
            List<DocumentCaseActivity> listDocumentCase = new List<DocumentCaseActivity>();
            try
            {
                dbConnection = new DbConnection();

                listDocumentCase = documentCaseActivityDAO.GetDocumentCaseActivityList(documentCaseActivity, check, dbConnection);

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

        public int Save(DocumentCaseActivity documentCaseActivity)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return documentCaseActivityDAO.Save(documentCaseActivity, dbConnection);
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

        public int Update(DocumentCaseActivity documentCaseActivity)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return documentCaseActivityDAO.Update(documentCaseActivity, dbConnection);
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

        public int Delete(DocumentCaseActivity documentCaseActivity)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return documentCaseActivityDAO.Delete(documentCaseActivity, dbConnection);
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
