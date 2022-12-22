using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Infrastructure
{
    public interface ICaseActivityDAO
    {
        int Save(CaseActivity caseActivity, bool withNextDate, DbConnection dbConnection);
        int Update(CaseActivity caseActivity, DbConnection dbConnection);
        int Delete(CaseActivity caseActivityte, DbConnection dbConnection);
        List<CaseActivity> GetCaseActivityList(DbConnection dbConnection);

    }

    public class CaseActivityDAOSqlImpl : ICaseActivityDAO
    {
        //public CaseActivity GetActivityCase(string caseActivitys, DbConnection dbConnection)
        //{
        //    CaseActivity caseActivity = new CaseActivity();

        //    dbConnection = new DbConnection();

        //    dbConnection.cmd.CommandText = "select * from case_activity WHERE case_number =" + caseActivitys + "AND is_active = 1";

        //    dbConnection.dr = dbConnection.cmd.ExecuteReader();
        //    DataAccessObject dataAccessObject = new DataAccessObject();
        //    caseActivity = dataAccessObject.GetSingleOject<CaseActivity>(dbConnection.dr);
        //    dbConnection.dr.Close();


        //    return caseActivity;
        //}

        public int Save(CaseActivity caseActivity, bool withNextDate, DbConnection dbConnection)
        {
            int output = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            if (!withNextDate)
            {
                dbConnection.cmd.CommandText = "Insert into case_activity (case_number, activity_Date, assign_attorney_id,counsilor_id , other_side_lawyer, judge_name, " +
                   "company_rep, action_taken_id, remarks, next_action_id, create_user_id ) values (@CaseNumber,@ActivityDate,@AssignAttorneyId,@CounsilorId," +
                   "@OtherSideLawyer,@JudgeName,@CompanyRep,@ActionTakenId,@Remarks,@NextActionId,@CreateUserId)";
            }
            else
            {
                dbConnection.cmd.CommandText = "Insert into case_activity (case_number, activity_Date, assign_attorney_id,counsilor_id , other_side_lawyer, judge_name, " +
                    "company_rep, action_taken_id, next_date, remarks, next_action_id, create_user_id ) values (@CaseNumber,@ActivityDate,@AssignAttorneyId,@CounsilorId," +
                    "@OtherSideLawyer,@JudgeName,@CompanyRep,@ActionTakenId,@NextDate,@Remarks,@NextActionId,@CreateUserId)";
            }



            dbConnection.cmd.Parameters.AddWithValue("@CaseNumber", caseActivity.CaseNumber);
            dbConnection.cmd.Parameters.AddWithValue("@ActivityDate", caseActivity.ActivityDate);
            dbConnection.cmd.Parameters.AddWithValue("@AssignAttorneyId", caseActivity.AssignAttorneyId);
            dbConnection.cmd.Parameters.AddWithValue("@CounsilorId", caseActivity.CounsilorId);
            dbConnection.cmd.Parameters.AddWithValue("@OtherSideLawyer", caseActivity.OtherSideLawyer);
            dbConnection.cmd.Parameters.AddWithValue("@JudgeName", caseActivity.JudgeName);
            dbConnection.cmd.Parameters.AddWithValue("@CompanyRep", caseActivity.CompanyRep);
            dbConnection.cmd.Parameters.AddWithValue("@ActionTakenId", caseActivity.ActionTakenId);
            dbConnection.cmd.Parameters.AddWithValue("@Remarks", caseActivity.Remarks);
            dbConnection.cmd.Parameters.AddWithValue("@NextActionId", caseActivity.NextActionId);
            dbConnection.cmd.Parameters.AddWithValue("@CreateUserId", caseActivity.CreateUserId);

            if (withNextDate)
                dbConnection.cmd.Parameters.AddWithValue("@NextDate", caseActivity.NextDate);
            else
                dbConnection.cmd.Parameters.AddWithValue("@NextDate", DateTime.Now);


            output = Convert.ToInt32(dbConnection.cmd.ExecuteScalar());

            return output;
        }
        public int Delete(CaseActivity caseActivityte, DbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
        public List<CaseActivity> GetCaseActivityList(DbConnection dbConnection)
        {
            List<CaseActivity> caseActivityList = new List<CaseActivity>();

            //dbConnection = new DbConnection();

            dbConnection.cmd.CommandText = "select * from case_activity WHERE is_active = 1";

            dbConnection.dr = dbConnection.cmd.ExecuteReader();
            DataAccessObject dataAccessObject = new DataAccessObject();
            caseActivityList = dataAccessObject.ReadCollection<CaseActivity>(dbConnection.dr);
            dbConnection.dr.Close();


            return caseActivityList;
        }
        public int Update(CaseActivity caseActivity, DbConnection dbConnection)
        {
            throw new NotImplementedException();
        }


    }
}
