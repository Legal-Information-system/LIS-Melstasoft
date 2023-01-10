using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface ICounselorController
    {
        void Save(Counselor counselor);
        List<Counselor> GetCounselorList(string CustomQuery = null);
        List<Counselor> GetCounselorListDeleted(string CustomQuery = null);
        int ReInit(Counselor counselor);
        int Delete(Counselor counselor);

        int DeletePermenent(String CaseNumber);

    }

    public class CounselorControllerImpl : ICounselorController
    {
        ICounselorDAO counselorDAO = DAOFactory.CreateCounselorDAO();
        public void Save(Counselor counselor)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                counselorDAO.Save(counselor, dbconnection);
            }
            catch (Exception)
            {
                dbconnection.RollBack();
                throw;
            }
            finally
            {
                if (dbconnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbconnection.Commit();
                }
            }
        }

        public List<Counselor> GetCounselorListDeleted(string CustomQuery = null)
        {
            Common.DbConnection dbConnection = null;
            List<Counselor> listCounselor = new List<Counselor>();

            try
            {
                dbConnection = new Common.DbConnection();
                if (CustomQuery == null)
                {
                    listCounselor = counselorDAO.GetCounselorListDeleted(dbConnection);
                }
                else
                {
                    listCounselor = counselorDAO.GetCounselorListDeleted(dbConnection, CustomQuery);
                }

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
            return listCounselor;
        }

        public List<Counselor> GetCounselorList(string CustomQuery = null)
        {
            Common.DbConnection dbConnection = null;
            List<Counselor> listCounselor = new List<Counselor>();

            try
            {
                dbConnection = new Common.DbConnection();
                if (CustomQuery == null)
                {
                    listCounselor = counselorDAO.GetCounselorList(dbConnection);
                }
                else
                {
                    listCounselor = counselorDAO.GetCounselorList(dbConnection, CustomQuery);
                }

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
            return listCounselor;
        }

        public int Delete(Counselor counselor)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return counselorDAO.Delete(counselor, dbConnection);
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

        public int DeletePermenent(String CaseNumber)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return counselorDAO.DeletePermenent(CaseNumber, dbConnection);
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

        public int ReInit(Counselor counselor)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return counselorDAO.ReInit(counselor, dbConnection);
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
