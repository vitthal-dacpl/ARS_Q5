using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace ArsServer1
{
    class dbLayer
    {

        private string SqlCon;

        public dbLayer()
        {
            //this.SqlCon = ConfigurationManager.AppSettings["Sqlconnstring"].ToString();
            SqlCon = Form1.connectionString;
        }

        public int ExecSqlNonQuery(
          string strSQL,
          CommandType cmdType,
          List<SqlParameter> ListSqlParams)
        {
            SqlConnection sqlConnection = new SqlConnection(this.SqlCon);
            SqlCommand cmd = new SqlCommand();
            try
            {
                this.getSqlPara(ListSqlParams, cmd);
                cmd.CommandType = cmdType;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                cmd.CommandText = strSQL;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State != 0)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
                cmd.Dispose();
            }
        }
                     

        public DataSet ExecSqlDataSet(string strSQL, CommandType cmdtype) => this.ExecSqlDataSet(strSQL, cmdtype, (List<SqlParameter>)null);

        public DataSet ExecSqlDataSet(
          string strSQL,
          CommandType cmdtype,
          List<SqlParameter> ListSqlParams)
        {
            DataSet ds = new DataSet("DataSet");
            this.ExecSqlDataSet(ds, strSQL, cmdtype, ListSqlParams);
            return ds;
        }
        public void ExecSqlDataSet(
          DataSet ds,
          string strSQL,
          CommandType cmdtype,
          List<SqlParameter> ListSqlParams)
        {
            SqlConnection sqlConnection = new SqlConnection(this.SqlCon);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            try
            {
                this.getSqlPara(ListSqlParams, cmd);
                cmd.CommandType = cmdtype;
                cmd.Connection = sqlConnection;
                cmd.CommandText = strSQL;
                cmd.CommandTimeout = 120;
                sqlDataAdapter.SelectCommand = cmd;
                sqlDataAdapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State != 0)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
                cmd.Dispose();
            }
        }

        private void getSqlPara(List<SqlParameter> parameters, SqlCommand cmd)
        {
            if (parameters == null)
                return;
            int int32 = Convert.ToInt32(parameters.Count);
            if (int32 > 0)
            {
                for (int index = 0; index <= int32 - 1; ++index)
                {
                    SqlParameter sqlParameter = new SqlParameter();
                    SqlParameter parameter = parameters[index];
                    cmd.Parameters.Add(parameter);
                }
            }
        }

    }
}

