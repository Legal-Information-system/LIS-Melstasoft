﻿using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface ICaseMasterDAO
    {
        int Save(CaseMaster caseMaster, DbConnection dbConnection);
        int Update(CaseMaster caseMaster, DbConnection dbConnection);
        int CaseClose(CaseMaster caseMaster, DbConnection dbConnection);
        int Delete(CaseMaster caseMaster, DbConnection dbConnection);
        List<CaseMaster> GetCaseMasterList(bool withoutclosed, DbConnection dbConnection);

        List<CaseMaster> GetCaseMasterListAll(DbConnection dbConnection);
        CaseMaster GetCaseMaster(string caseNumber, DbConnection dbConnection);

        double GetCaseMasterWithTotalPaidAmount(String caseNumber, DbConnection dbConnection);
        int UpdateCasePaidAmount(CaseMaster caseMaster, DbConnection dbConnection);
    }

    public class CaseMasterDAOSqlImpl : ICaseMasterDAO
    {

        public CaseMaster GetCaseMaster(string caseNumber, DbConnection dbConnection)
        {
            CaseMaster caseMaster = new CaseMaster();

            //dbConnection = new DbConnection();
            dbConnection.cmd.CommandText = "select * from case_master WHERE case_number = @CaseNumber";
            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", caseNumber);

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            caseMaster = dataAccessObject.GetSingleOject<CaseMaster>(dbConnection.dr);
            dbConnection.dr.Close();

            return caseMaster;
        }

        public List<CaseMaster> GetCaseMasterList(bool withoutclosed, DbConnection dbConnection)
        {
            List<CaseMaster> listCaseMaster = new List<CaseMaster>();

            //dbConnection = new DbConnection();
            if (withoutclosed)
                dbConnection.cmd.CommandText = "select * from case_master WHERE case_status_id = 1 AND is_active = 1";
            else
                dbConnection.cmd.CommandText = "select * from case_master WHERE case_status_id = 2 AND is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listCaseMaster = dataAccessObject.ReadCollection<CaseMaster>(dbConnection.dr);
            dbConnection.dr.Close();

            return listCaseMaster;
        }

        public List<CaseMaster> GetCaseMasterListAll(DbConnection dbConnection)
        {
            List<CaseMaster> listCaseMaster = new List<CaseMaster>();

            //dbConnection = new DbConnection();

            dbConnection.cmd.CommandText = "select * from case_master";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            listCaseMaster = dataAccessObject.ReadCollection<CaseMaster>(dbConnection.dr);
            dbConnection.dr.Close();

            return listCaseMaster;
        }


        public int Save(CaseMaster caseMaster, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            if (caseMaster.PrevCaseNumber != " ")
            {
                dbConnection.cmd.CommandText = "Insert into case_master (case_number, company_id, company_unit_id, case_nature_id, case_description, claim_amount, is_plaintif,  court_id, locationc_id, " +
                "assign_attorney_id, created_by_user, created_date, case_status_id, case_open_date, prev_case_number ) " +
                "values (@CaseNumber, @CompanyId, @CompanyUnitId, @CaseNatureId, @CaseDescription, @ClaimAmount, @IsPlentif, @CourtId, @LocationId," +
                "@AssignAttornerId, @CreatedUserId, @CreatedDate, @CaseStatusId, @CaseOpenDate, @PrevCaseNumber)";
            }
            else
            {
                dbConnection.cmd.CommandText = "Insert into case_master (case_number, company_id, company_unit_id, case_nature_id, case_description, claim_amount, is_plaintif,  court_id, locationc_id, " +
                "assign_attorney_id, created_by_user, created_date, case_status_id, case_open_date) " +
                "values (@CaseNumber, @CompanyId, @CompanyUnitId, @CaseNatureId, @CaseDescription, @ClaimAmount, @IsPlentif, @CourtId, @LocationId," +
                "@AssignAttornerId, @CreatedUserId, @CreatedDate, @CaseStatusId, @CaseOpenDate)";
            }




            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", caseMaster.CaseNumber);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", caseMaster.CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyUnitId", caseMaster.CompanyUnitId);
            dbConnection.cmd.Parameters.AddWithValue("@CaseNatureId", caseMaster.CaseNatureId);
            dbConnection.cmd.Parameters.AddWithValue("@CaseDescription", caseMaster.CaseDescription);
            dbConnection.cmd.Parameters.AddWithValue("@ClaimAmount", caseMaster.ClaimAmount);
            dbConnection.cmd.Parameters.AddWithValue("@IsPlentif", caseMaster.IsPlentif);
            dbConnection.cmd.Parameters.AddWithValue("@CourtId", caseMaster.CourtId);
            dbConnection.cmd.Parameters.AddWithValue("@LocationId", caseMaster.LocationId);
            dbConnection.cmd.Parameters.AddWithValue("@PrevCaseNumber", caseMaster.PrevCaseNumber);
            dbConnection.cmd.Parameters.AddWithValue("@AssignAttornerId", caseMaster.AssignAttornerId);
            dbConnection.cmd.Parameters.AddWithValue("@CreatedUserId", caseMaster.CreatedUserId);
            dbConnection.cmd.Parameters.AddWithValue("@CreatedDate", caseMaster.CreatedDate);
            dbConnection.cmd.Parameters.AddWithValue("@CaseStatusId", caseMaster.CaseStatusId);
            dbConnection.cmd.Parameters.AddWithValue("@CaseOpenDate", caseMaster.CaseOpenDate);
            //dbConnection.cmd.Parameters.AddWithValue("@JudgementTypeId", caseMaster.JudgementTypeId);
            //dbConnection.cmd.Parameters.AddWithValue("@CaseOutcome", caseMaster.CaseOutcome);
            //dbConnection.cmd.Parameters.AddWithValue("@ClosedRemarks", caseMaster.ClosedRemarks);
            //dbConnection.cmd.Parameters.AddWithValue("@ClosedDate", caseMaster.ClosedDate);
            //dbConnection.cmd.Parameters.AddWithValue("@ClosedUserId", caseMaster.ClosedUserId);

            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }

        public int CaseClose(CaseMaster caseMaster, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "Update case_master SET case_status_id = @CaseStatusId, judgement_type = @JudgementTypeId, case_outcome = @CaseOutcome, " +
                "closed_remarks = @ClosedRemarks, closed_date = @ClosedDate, close_by_user = @ClosedUserId WHERE case_number = @CaseNumber ";


            dbConnection.cmd.Parameters.AddWithValue("@CaseStatusId", caseMaster.CaseStatusId);
            dbConnection.cmd.Parameters.AddWithValue("@JudgementTypeId", caseMaster.JudgementTypeId);
            dbConnection.cmd.Parameters.AddWithValue("@CaseOutcome", caseMaster.CaseOutcome);
            dbConnection.cmd.Parameters.AddWithValue("@ClosedRemarks", caseMaster.ClosedRemarks);
            dbConnection.cmd.Parameters.AddWithValue("@ClosedDate", caseMaster.ClosedDate);
            dbConnection.cmd.Parameters.AddWithValue("@ClosedUserId", caseMaster.ClosedUserId);
            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", caseMaster.CaseNumber);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int Update(CaseMaster caseMaster, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            if (caseMaster.PrevCaseNumber == "")
            {
                dbConnection.cmd.CommandText = "Update case_master SET case_number=@CaseNumber, company_id = @CompanyId,company_unit_id = @CompanyUnitId, case_nature_id = @CaseNatureId, " +
                    "case_description = @CaseDescription ,claim_amount = @ClaimAmount , is_plaintif = @IsPlentif, court_id = @CourtId, locationc_id = @LocationId," +
                    "assign_attorney_id = @AssignAttornerId, created_by_user = @CreatedUserId, created_date = @CreatedDate, case_status_id = @CaseStatusId, case_open_date = @CaseOpenDate" +
                    " WHERE case_number = @PrevCaseNumberUpdate ";
            }
            else
            {
                dbConnection.cmd.CommandText = "Update case_master SET case_number=@CaseNumber, company_id = @CompanyId,company_unit_id = @CompanyUnitId, case_nature_id = @CaseNatureId, " +
                    "case_description = @CaseDescription ,claim_amount = @ClaimAmount , is_plaintif = @IsPlentif, court_id = @CourtId, locationc_id = @LocationId," +
                    "assign_attorney_id = @AssignAttornerId, created_by_user = @CreatedUserId, created_date = @CreatedDate, case_status_id = @CaseStatusId, case_open_date = @CaseOpenDate," +
                    "prev_case_number = @PrevCaseNumber WHERE case_number = @PrevCaseNumberUpdate ";
            }


            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", caseMaster.CaseNumber);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", caseMaster.CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyUnitId", caseMaster.CompanyUnitId);
            dbConnection.cmd.Parameters.AddWithValue("@CaseNatureId", caseMaster.CaseNatureId);
            dbConnection.cmd.Parameters.AddWithValue("@CaseDescription", caseMaster.CaseDescription);
            dbConnection.cmd.Parameters.AddWithValue("@ClaimAmount", caseMaster.ClaimAmount);
            dbConnection.cmd.Parameters.AddWithValue("@IsPlentif", caseMaster.IsPlentif);
            dbConnection.cmd.Parameters.AddWithValue("@CourtId", caseMaster.CourtId);
            dbConnection.cmd.Parameters.AddWithValue("@LocationId", caseMaster.LocationId);
            dbConnection.cmd.Parameters.AddWithValue("@PrevCaseNumber", caseMaster.PrevCaseNumber);
            dbConnection.cmd.Parameters.AddWithValue("@AssignAttornerId", caseMaster.AssignAttornerId);
            dbConnection.cmd.Parameters.AddWithValue("@CreatedUserId", caseMaster.CreatedUserId);
            dbConnection.cmd.Parameters.AddWithValue("@CreatedDate", caseMaster.CreatedDate);
            dbConnection.cmd.Parameters.AddWithValue("@CaseStatusId", caseMaster.CaseStatusId);
            dbConnection.cmd.Parameters.AddWithValue("@CaseOpenDate", caseMaster.CaseOpenDate);
            dbConnection.cmd.Parameters.AddWithValue("@PrevCaseNumberUpdate", caseMaster.PrevCaseNumberUpdate);


            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public int Delete(CaseMaster caseMaster, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE case_master SET is_active = 0 WHERE case_number = @CaseNumber ";

            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", caseMaster.CaseNumber);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }

        public double GetCaseMasterWithTotalPaidAmount(String caseNumber, DbConnection dbConnection)
        {
            int output = 0;
            double totalPaidAmount;

            CaseMaster caseMaster = new CaseMaster();

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "SELECT * FROM case_master WHERE case_number = @CaseNumber";
            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", caseNumber);

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            caseMaster = dataAccessObject.GetSingleOject<CaseMaster>(dbConnection.dr);
            dbConnection.dr.Close();

            totalPaidAmount = caseMaster.totalPaidAmoutToPresent;

            return totalPaidAmount;
        }

        public int UpdateCasePaidAmount(CaseMaster caseMaster, DbConnection dbConnection)
        {
            int output = 0;
            CaseMaster caseMaster1 = new CaseMaster();

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "select * from case_master WHERE case_number = @CaseNumber AND is_active = 1";
            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", caseMaster.CaseNumber);

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            caseMaster1 = dataAccessObject.GetSingleOject<CaseMaster>(dbConnection.dr);
            dbConnection.dr.Close();

            caseMaster.totalPaidAmoutToPresent = caseMaster1.totalPaidAmoutToPresent + caseMaster.payableAmount;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "Update case_master SET total_paid_amount = @totalPaidAmoutToPresent WHERE case_number = @CaseNumber ";

            dbConnection.cmd.Parameters.AddWithValue("@totalPaidAmoutToPresent", caseMaster.totalPaidAmoutToPresent);
            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", caseMaster.CaseNumber);

            output = dbConnection.cmd.ExecuteNonQuery();

            return output;
        }
    }
}
