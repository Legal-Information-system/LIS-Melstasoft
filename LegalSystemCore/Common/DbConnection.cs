using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Common
{
    public class DbConnection : IDisposable
    {

        public SqlConnection con;
        public SqlCommand cmd;
        public SqlTransaction tr;
        public SqlDataReader dr;


        public DbConnection()
        {
            con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["dbConnection"].ToString());
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            try
            {
                con.Open();
                tr = con.BeginTransaction();
                cmd.Transaction = tr;

            }
            catch (Exception)
            {
                this.RollBack();
                throw;
            }

            //
            // TODO: Add constructor logic here
            //
        }

        public void Commit()
        {
            this.cmd.Dispose();
            if (this.dr != null)
                this.dr.Close();
            tr.Commit();
            this.con.Close();
        }

        public void Dispose()
        {
            this.Commit();
        }

        public void RollBack()
        {
            this.tr.Rollback();
            if (this.dr != null)
                this.dr.Close();
            this.cmd.Dispose();
            this.con.Close();
        }


    }
}
