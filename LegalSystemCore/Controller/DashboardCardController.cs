using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{
    public interface IDashboardCardController
    {
        DataTable GetCardDetails();
        DataTable GeCompanyListDailyMonth(bool daily, bool monthly);
        DataTable GetCardDetailsCompanyUnit(int companyId);
        DataTable GetCardDetailsUnit(int unitId);
        DataTable GetMonthProgress();
        DataTable GetMonthProgressUnit(bool unit, int id);
        DataTable GetClaimAmountPercentage();
        DataTable GetClaimAmountPercentageUnit(bool unit, int id);
    }

    public class DashBoardCardControllerSqlImpl : IDashboardCardController
    {
        IDashboardCardDAO dashboardCardDAO = DAOFactory.CreateDashboardCardDAO();
        public DataTable GetCardDetails()
        {
            DbConnection dbConnection = null;
            DataTable listCard = new DataTable();
            try
            {
                dbConnection = new DbConnection();
                listCard = dashboardCardDAO.GeCompanyList(dbConnection);
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
            return listCard;
        }

        public DataTable GeCompanyListDailyMonth(bool daily, bool monthly)
        {
            DbConnection dbConnection = null;
            DataTable listCard = new DataTable();
            try
            {
                dbConnection = new DbConnection();
                listCard = dashboardCardDAO.GeCompanyListDailyMonth(daily, monthly, dbConnection);
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
            return listCard;
        }


        public DataTable GetCardDetailsCompanyUnit(int companyId)
        {
            DbConnection dbConnection = null;
            DataTable listCard = new DataTable();
            try
            {
                dbConnection = new DbConnection();
                listCard = dashboardCardDAO.GeCompanyUnitList(companyId, dbConnection);
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
            return listCard;
        }
        public DataTable GetCardDetailsUnit(int unitId)
        {
            DbConnection dbConnection = null;
            DataTable listCard = new DataTable();
            try
            {
                dbConnection = new DbConnection();
                listCard = dashboardCardDAO.GeCompanyUnit(unitId, dbConnection);
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
            return listCard;
        }

        public DataTable GetMonthProgress()
        {
            DbConnection dbConnection = null;
            DataTable listCard = new DataTable();
            try
            {
                dbConnection = new DbConnection();
                listCard = dashboardCardDAO.GeMonthProgress(dbConnection);
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
            return listCard;
        }
        public DataTable GetMonthProgressUnit(bool unit, int id)
        {
            DbConnection dbConnection = null;
            DataTable listCard = new DataTable();
            try
            {
                dbConnection = new DbConnection();
                listCard = dashboardCardDAO.GeMonthProgressUnit(unit, id, dbConnection);
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
            return listCard;
        }

        public DataTable GetClaimAmountPercentage()
        {
            DbConnection dbConnection = null;
            DataTable listCard = new DataTable();
            try
            {
                dbConnection = new DbConnection();
                listCard = dashboardCardDAO.GeClaimAmountPercentage(dbConnection);
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
            return listCard;
        }
        public DataTable GetClaimAmountPercentageUnit(bool unit, int id)
        {
            DbConnection dbConnection = null;
            DataTable listCard = new DataTable();
            try
            {
                dbConnection = new DbConnection();
                listCard = dashboardCardDAO.GeClaimAmountPercentageUnit(unit, id, dbConnection);
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
            return listCard;
        }
    }
}
