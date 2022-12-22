using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface IDashboardCardDAO
    {
        DataTable GeCompanyList(DbConnection dbConnection);
        DataTable GeCompanyUnitList(int companyId, DbConnection dbConnection);
        DataTable GeCompanyUnit(int companyUnitId, DbConnection dbConnection);
        DataTable GeMonthProgress(DbConnection dbConnection);
        DataTable GeMonthProgressUnit(bool unit, int id, DbConnection dbConnection);
        DataTable GeClaimAmountPercentage(DbConnection dbConnection);
        DataTable GeClaimAmountPercentageUnit(bool unit, int id, DbConnection dbConnection);

    }

    public class DashboardCardDAOImpl : IDashboardCardDAO
    {
        public DataTable GeCompanyList(DbConnection dbConnection)
        {
            DataTable companyList = new DataTable();

            //dbConnection = new DbConnection();
            dbConnection.cmd.CommandText = "SELECT COUNT(case_master.company_id ) AS case_count, company.company_name FROM case_master " +
                "INNER JOIN company ON company.company_id = case_master.company_id " +
                "WHERE case_master.case_status_id = 1 AND company.is_active = 1" +
                "GROUP BY company.company_name;";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbConnection.cmd);
            dataAdapter.Fill(companyList);


            return companyList;
        }

        public DataTable GeCompanyUnitList(int companyId, DbConnection dbConnection)
        {
            DataTable companyList = new DataTable();

            //dbConnection = new DbConnection();

            dbConnection.cmd.CommandText = "SELECT COUNT(case_master.company_unit_id ) AS case_count, company_unit.company_unit_name AS company_name FROM case_master " +
                "INNER JOIN company_unit ON company_unit.company_unit_id = case_master.company_unit_id " +
                "WHERE case_master.case_status_id = 1 AND case_master.company_id =" + companyId +
                "GROUP BY company_unit.company_unit_name;";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbConnection.cmd);
            dataAdapter.Fill(companyList);


            return companyList;
        }

        public DataTable GeCompanyUnit(int companyUnitId, DbConnection dbConnection)
        {
            DataTable companyList = new DataTable();

            //dbConnection = new DbConnection();

            dbConnection.cmd.CommandText = "SELECT COUNT(case_master.company_unit_id ) AS case_count, company_unit.company_unit_name  AS company_name  FROM case_master " +
                "INNER JOIN company_unit ON company_unit.company_unit_id = case_master.company_unit_id " +
                "WHERE case_master.case_status_id = 1 AND case_master.company_unit_id =" + companyUnitId +
                "GROUP BY company_unit.company_unit_name;";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbConnection.cmd);
            dataAdapter.Fill(companyList);


            return companyList;
        }
        public DataTable GeMonthProgress(DbConnection dbConnection)
        {
            DataTable companyList = new DataTable();

            //dbConnection = new DbConnection();

            DateTime prev = new DateTime();
            prev = DateTime.Now;
            prev = prev.AddMonths(-1);

            dbConnection.cmd.CommandText = "SELECT COUNT(created_date) AS case_count, DAY(created_date) AS month_day FROM case_master " +
                "WHERE MONTH(created_date) = " + prev.Month +
                "GROUP BY DAY(created_date);";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbConnection.cmd);
            dataAdapter.Fill(companyList);

            dbConnection.cmd.CommandText = "SELECT COUNT(created_date) AS case_count, DAY(created_date) AS month_day FROM case_master " +
              "WHERE MONTH(created_date) = " + DateTime.Now.Month +
              "GROUP BY DAY(created_date);";

            SqlDataAdapter dataAdapter2 = new SqlDataAdapter(dbConnection.cmd);
            dataAdapter2.Fill(companyList);


            return companyList;
        }

        public DataTable GeMonthProgressUnit(bool unit, int id, DbConnection dbConnection)
        {
            DataTable companyList = new DataTable();

            DateTime prev = new DateTime();
            prev = DateTime.Now;
            prev = prev.AddMonths(-1);

            //dbConnection = new DbConnection();

            if (unit)
            {
                dbConnection.cmd.CommandText = "SELECT COUNT(created_date) AS case_count, DAY(created_date) AS month_day FROM case_master " +
               "WHERE MONTH(created_date) = " + prev.Month + "AND  company_unit_id =" + id +
               "GROUP BY DAY(created_date);";

                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(dbConnection.cmd);
                dataAdapter1.Fill(companyList);

                dbConnection.cmd.CommandText = "SELECT COUNT(created_date) AS case_count, DAY(created_date) AS month_day FROM case_master " +
               "WHERE MONTH(created_date) = " + DateTime.Now.Month + "AND  company_unit_id =" + id +
               "GROUP BY DAY(created_date);";

                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(dbConnection.cmd);
                dataAdapter2.Fill(companyList);
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT COUNT(created_date) AS case_count, DAY(created_date) AS month_day FROM case_master " +
               "WHERE MONTH(created_date) = " + prev.Month + "AND  company_unit_id =" + id +
               "GROUP BY DAY(created_date);";

                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(dbConnection.cmd);
                dataAdapter1.Fill(companyList);

                dbConnection.cmd.CommandText = "SELECT COUNT(created_date) AS case_count, DAY(created_date) AS month_day FROM case_master " +
               "WHERE MONTH(created_date) = " + DateTime.Now.Month + "AND  company_unit_id =" + id +
               "GROUP BY DAY(created_date);";

                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(dbConnection.cmd);
                dataAdapter2.Fill(companyList);
            }

            return companyList;
        }

        public DataTable GeClaimAmountPercentage(DbConnection dbConnection)
        {
            DataTable companyList = new DataTable();

            //dbConnection = new DbConnection();

            dbConnection.cmd.CommandText = "SELECT case_number, ROUND((total_paid_amount/claim_amount)*100, 2) AS per FROM case_master " +
                "WHERE case_status_id = 1 ORDER BY per DESC;";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbConnection.cmd);
            dataAdapter.Fill(companyList);


            return companyList;
        }

        public DataTable GeClaimAmountPercentageUnit(bool unit, int id, DbConnection dbConnection)
        {
            DataTable companyList = new DataTable();

            //dbConnection = new DbConnection();

            if (unit)
            {
                dbConnection.cmd.CommandText = "SELECT case_number, ROUND((total_paid_amount/claim_amount)*100, 2) AS per FROM case_master " +
               "WHERE case_status_id = 1 AND company_unit_id =" + id +
               "ORDER BY per DESC ;";
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT case_number, ROUND((total_paid_amount/claim_amount)*100, 2) AS per FROM case_master " +
               "WHERE case_status_id = 1 AND company_id =" + id +
               "ORDER BY per DESC ;";
            }


            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbConnection.cmd);
            dataAdapter.Fill(companyList);


            return companyList;
        }
    }
}
