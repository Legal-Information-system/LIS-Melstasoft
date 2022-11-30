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
    public interface IJudgementTypeController
    {
        int Save(JudgementType judgementType);
        int Update(JudgementType judgementType);
        int Delete(JudgementType judgementType);

        List<JudgementType> GetJudgementTypeList();
    }

    public class JudgementTypeControllerImpl : IJudgementTypeController
    {
        IJudgementTypeDAO JudgementTypeDAO = DAOFactory.CreateJudgementTypeDAO();

        public List<JudgementType> GetJudgementTypeList()
        {
            DbConnection dbConnection = null;
            List<JudgementType> listJudgementType = new List<JudgementType>();
            try
            {
                dbConnection = new DbConnection();
                listJudgementType = JudgementTypeDAO.GetJudgementTypeList(dbConnection);
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
            return listJudgementType;
        }

        public int Save(JudgementType judgementType)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return JudgementTypeDAO.Save(judgementType, dbConnection);
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

        public int Update(JudgementType judgementType)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return JudgementTypeDAO.Update(judgementType, dbConnection);
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

        public int Delete(JudgementType judgementType)
        {
            DbConnection dbConnection = null;
            try
            {
                dbConnection = new DbConnection();
                return JudgementTypeDAO.Delete(judgementType, dbConnection);
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
