using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace ArsServer2
{
    public class dbLayer
    {
        private string SqlCon;

        public dbLayer()
        {
           // this.SqlCon = ConfigurationManager.AppSettings["Sqlconnstring"].ToString();
            this.SqlCon = Form1.connectionString;
        }

        public int ExecSqlNonQuery(string strSQL, CommandType cmdType)
        {
            return this.ExecSqlNonQuery(strSQL, cmdType, (List<SqlParameter>)null);
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

        public int ExecSqlNonQuery_AlarmUpdate(string strSQL, CommandType cmdType)
        {
            SqlConnection sqlConnection = new SqlConnection(this.SqlCon);
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            BusinessLayer businessLayer = new BusinessLayer();
            SqlTransaction sqlTransaction;
            using (sqlTransaction = sqlConnection.BeginTransaction(IsolationLevel.RepeatableRead, "UpdateTrans"))
            {
                try
                {
                    command.Connection = sqlConnection;
                    command.Transaction = sqlTransaction;
                    command.CommandType = cmdType;
                    command.CommandText = strSQL;
                    int num = command.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    return num;
                }
                catch (Exception ex)
                {
                    businessLayer.tbIlnsertLogData((object)ex.ToString());
                    return 0;
                }
                finally
                {
                    if (sqlConnection.State != 0)
                    {
                        sqlConnection.Close();
                        sqlConnection.Dispose();
                        command.Dispose();
                    }
                }
            }
        }

        public void BatchUpdate(DataTable dataTable, int batchSize)
        {
            using (SqlConnection connection = new SqlConnection(this.SqlCon))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter()
                {
                    UpdateCommand = new SqlCommand("update [dbo].[TM_SCADA_ALARM] set [EndTime]=@TimeStamp ,[Duration]=DATEDIFF(SECOND,StartTime, @TimeStamp) WHERE Id = (select top 1 Id from[dbo].[TM_SCADA_ALARM]where LogDateTime >= Convert(datetime , (Dateadd(day,-2, getdate()))) and[TagName]= @TagName AND[EndTime] IS NULL order by id desc )", connection)
                };
                sqlDataAdapter.UpdateCommand.Parameters.Add("@TagName", SqlDbType.NVarChar, 250, "TagName");
                sqlDataAdapter.UpdateCommand.Parameters.Add("@TimeStamp", SqlDbType.DateTime, 4, "TimeStamp");
                sqlDataAdapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                sqlDataAdapter.UpdateBatchSize = batchSize;
                sqlDataAdapter.Update(dataTable);
            }
        }

        public object ExecSqlScalar(
          string strSQL,
          CommandType cmdtype,
          List<SqlParameter> ListSqlParams)
        {
            SqlConnection sqlConnection = new SqlConnection(this.SqlCon);
            SqlCommand cmd = new SqlCommand();
            try
            {
                this.getSqlPara(ListSqlParams, cmd);
                cmd.CommandType = cmdtype;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                cmd.CommandText = strSQL;
                cmd.CommandTimeout = 120;
                return cmd.ExecuteScalar();
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

        public object ExecSqlScalar(string strSQL, CommandType cmdtype)
        {
            SqlConnection sqlConnection = new SqlConnection(this.SqlCon);
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                sqlCommand.CommandType = cmdtype;
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                sqlCommand.CommandText = strSQL;
                sqlCommand.CommandTimeout = 120;
                return sqlCommand.ExecuteScalar();
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
                sqlCommand.Dispose();
            }
        }

        public SqlDataReader ExecSqlDataReader(
          string strSQL,
          CommandType cmdtype,
          List<SqlParameter> ListSqlParams)
        {
            SqlConnection sqlConnection = new SqlConnection(this.SqlCon);
            SqlCommand cmd = new SqlCommand();
            try
            {
                this.getSqlPara(ListSqlParams, cmd);
                cmd.CommandType = cmdtype;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                cmd.CommandText = strSQL;
                cmd.CommandTimeout = 120;
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
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

        public SqlDataReader ExecSqlDataReader(string strSQL, CommandType cmdtype)
        {
            return this.ExecSqlDataReader(strSQL, cmdtype, (List<SqlParameter>)null);
        }

        public DataSet ExecSqlDataSet(string strSQL, CommandType cmdtype)
        {
            return this.ExecSqlDataSet(strSQL, cmdtype, (List<SqlParameter>)null);
        }

        public DataSet ExecSqlDataSet(
          string strSQL,
          CommandType cmdtype,
          List<SqlParameter> ListSqlParams)
        {
            DataSet ds = new DataSet("DataSet");
            this.ExecSqlDataSet(ds, strSQL, cmdtype, ListSqlParams);
            return ds;
        }

        public void ExecSqlDataSet(DataSet ds, string strSQL, CommandType cmdtype)
        {
            this.ExecSqlDataSet(ds, strSQL, cmdtype, (List<SqlParameter>)null);
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

        public DataRow ExecuteSqlRow(
          string strSQL,
          CommandType cmdtype,
          List<SqlParameter> ListSqlParams)
        {
            SqlConnection sqlConnection = new SqlConnection(this.SqlCon);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            try
            {
                this.getSqlPara(ListSqlParams, cmd);
                cmd.CommandType = cmdtype;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                cmd.CommandText = strSQL;
                cmd.CommandTimeout = 120;
                sqlDataAdapter.Fill(dataTable);
                cmd.Parameters.Clear();
                return dataTable.Rows.Count == 0 ? (DataRow)null : dataTable.Rows[0];
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
            }
        }
    }
}

