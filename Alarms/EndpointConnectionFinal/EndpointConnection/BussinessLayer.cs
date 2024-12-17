
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ArsServer2
{

    public class BusinessLayer
    {
        private dbLayer dbl = new dbLayer();
        private DataTable dt = new DataTable();

        public BusinessLayer()
        {
            this.dt.Columns.Add("TagName");
            this.dt.Columns.Add("TimeStamp");
        }

        public int InsertTagValues(object TagName, object TagValue, object TimeStamp)
        {
            return this.dbl.ExecSqlNonQuery("sp_InsertTagValues", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@TagName", TagName),
        new SqlParameter("@TagValue", TagValue),
        new SqlParameter("@TimeStamp", TimeStamp)
      });
        }

        public int InsertNormalAlarams(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_NORMAL", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@TagName", TagName),
        new SqlParameter("@TagValue", TagValue),
        new SqlParameter("@AlarmType", AlarmType),
        new SqlParameter("@LineName", LineName),
        new SqlParameter("@StationName", StationName),
        new SqlParameter("@Model", Model),
        new SqlParameter("@AlarmDesc", AlarmDesc),
        new SqlParameter("@Reason", Reason),
        new SqlParameter("@TimeStamp", TimeStamp)
      });
        }

        public int InsertNormalAlarams_BSLH(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_BSLH", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. BSLH. : " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_BSRH(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_BSRH", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. BSRH. : " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_FDRDRH(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_FDRDRH", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. FDRDRH. : " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_FDRDLH(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_FDRDLH", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. FDRDLH. : " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_FE(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_FE", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. FE...: " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_FF(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_FF", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. FF...: " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_RF(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_RF", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. RF...: " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_UB(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_UB", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. UB...: " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_OH(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_OH", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. OH...: " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_RP(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_RP", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. RP...: " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_MF(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_MF", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. MF...: " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_MFL(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_MFL", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. MFL...: " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_OT(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_OT", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. OT...: " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_TG(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_TG", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. TG...: " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_FN(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_FN", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. FN...: " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_HO(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_HO", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. HO...: " + ex.ToString()));
                return 0;
            }
        }

        public int InsertNormalAlarams_RO(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object Model,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_RO", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@TagValue", TagValue),
          new SqlParameter("@AlarmType", AlarmType),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@Model", Model),
          new SqlParameter("@AlarmDesc", AlarmDesc),
          new SqlParameter("@Reason", Reason),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("InsertAlarms in BL. RO...: " + ex.ToString()));
                return 0;
            }
        }

        public int InsertIdealAlarams(
          object TagName,
          object TagValue,
          object AlarmType,
          object LineName,
          object StationName,
          object AlarmDesc,
          object Reason,
          object TimeStamp)
        {
            return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ALARMS_IDEAL", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@TagName", TagName),
        new SqlParameter("@TagValue", TagValue),
        new SqlParameter("@AlarmType", AlarmType),
        new SqlParameter("@LineName", LineName),
        new SqlParameter("@StationName", StationName),
        new SqlParameter("@AlarmDesc", AlarmDesc),
        new SqlParameter("@Reason", Reason),
        new SqlParameter("@TimeStamp", TimeStamp)
      });
        }

        public int UpdateAlarms(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_BSLH(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_BSLH", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL. BSLH. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_BSRH(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_BSRH", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL BSRH.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_FDRDRH(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_FDRDRH", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL FDRDRH.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_FDRDLH(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_FDRDLH", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL FDRDLH.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_FE(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_FE", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL FE.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_FF(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_FF", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL FF.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_RF(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_RF", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL RF.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_UB(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_UB", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL UB.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_OH(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_OH", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL OH.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_RP(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_RP", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL RP.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_MF(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_MF", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL MF.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_MFL(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_MFL", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL MFL.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_OT(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_OT", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL OT.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_TG(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_TG", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL TG.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_FN(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_FN", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL FN.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_HO(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_HO", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL HO.. : " + ex.ToString()));
                return 0;
            }
        }

        public int UpdateAlarms_RO(object TagName, string LineName, object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_UPDATE_ALARMS_RO", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@Line", (object) LineName),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("UpdateAlarms in BL RO.. : " + ex.ToString()));
                return 0;
            }
        }

        public int tbIlnsertLogData(object LogData)
        {
            return this.dbl.ExecSqlNonQuery("sp_InsertLogAlarmData", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@LogData", LogData)
      });
        }

        public DataSet GetPlcInputData()
        {
            return this.dbl.ExecSqlDataSet("sp_LastLog_Alarms", CommandType.StoredProcedure);
        }

        public int tblInsertBuyOffData(object ProductionLine, object EngineNo)
        {
            return this.dbl.ExecSqlNonQuery("SPTestEngineDataprint", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@ProductionLine", ProductionLine),
        new SqlParameter("@EngineNo", EngineNo)
      });
        }

        public int tbIlnsertGangOKDetails(object EngineNo, object StageNo)
        {
            return this.dbl.ExecSqlNonQuery("sp_InsertGangOKDetails", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@EngineNo", EngineNo),
        new SqlParameter("@StageNo", StageNo)
      });
        }

        public int tblInserttblTempData(object dt)
        {
            return this.dbl.ExecSqlNonQuery("sp_tblPLCDataInsert", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@dt", dt)
      });
        }

        public string GetNotOkStages(object EngineNo)
        {
            return this.dbl.ExecSqlScalar("select dbo.GetNotOkStages(@EngineNo)", CommandType.Text, new List<SqlParameter>()
      {
        new SqlParameter("@EngineNo", EngineNo)
      }).ToString();
        }

        public object IsGangOk(object Desc)
        {
            return this.dbl.ExecSqlScalar("select dbo.IsGangOk(@Desc)", CommandType.Text, new List<SqlParameter>()
      {
        new SqlParameter("@Desc", Desc)
      });
        }

        public object IsGangOkAtLastStage(object EngineNo)
        {
            return this.dbl.ExecSqlScalar("select dbo.IsGangOkAtLastStage(@EngineNo)", CommandType.Text, new List<SqlParameter>()
      {
        new SqlParameter("@EngineNo", EngineNo)
      });
        }

        public DataSet GetdtblPLCAddresses()
        {
            return this.dbl.ExecSqlDataSet("sp_GetdtblPLCAddresses", CommandType.StoredProcedure);
        }

        public int tblEventLogInsert(object EventDesc)
        {
            return this.dbl.ExecSqlNonQuery("sp_InsertEventLog", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@Desc", EventDesc)
      });
        }

        public DataSet GetEventLogData()
        {
            return this.dbl.ExecSqlDataSet("sp_GetEventData", CommandType.StoredProcedure);
        }

        public DataSet GetPLC_Tags()
        {
            return this.dbl.ExecSqlDataSet("sp_GetPLC_AlarmsTags", CommandType.StoredProcedure);
        }

        public int tblPLCDataInertNew(
          object OriginalData,
          object EngineNo,
          object StageNo,
          int SpindleNumber,
          int CycleNo,
          float Torque,
          float Angle,
          object Result)
        {
            return this.dbl.ExecSqlNonQuery("sp_InsertDataPLC_New", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@OriginalData", OriginalData),
        new SqlParameter("@EngineNo", EngineNo),
        new SqlParameter("@StageNo", StageNo),
        new SqlParameter("@SpindleNumber", (object) SpindleNumber),
        new SqlParameter("@CycleNo", (object) CycleNo),
        new SqlParameter("@Torque", (object) Torque),
        new SqlParameter("@Angle", (object) Angle),
        new SqlParameter("@Result", Result)
      });
        }

        public bool BulkInsertDataTable(string tableName, DataTable dataTable)
        {
            bool flag;
            try
            {
                SqlConnection connection = new SqlConnection("Server='SANDIP-PC\\SQLENTRP';Database='DATALOGMM';user Id='sa';Password='sadb@123';");
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, (SqlTransaction)null);
                sqlBulkCopy.DestinationTableName = tableName;
                connection.Open();
                sqlBulkCopy.ColumnMappings.Add("OriginalData", "OriginalData");
                sqlBulkCopy.ColumnMappings.Add("EngineNo", "EngineNo");
                sqlBulkCopy.ColumnMappings.Add("StageNo", "StageNo");
                sqlBulkCopy.ColumnMappings.Add("CycleNo", "CycleNo");
                sqlBulkCopy.ColumnMappings.Add("Torque", "Torque");
                sqlBulkCopy.ColumnMappings.Add("Angle", "Angle");
                sqlBulkCopy.ColumnMappings.Add("Result", "Result");
                sqlBulkCopy.WriteToServer(dataTable, DataRowState.Added);
                connection.Close();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        public DataSet GetListOfAlarms_ForUpdate(string LineName)
        {
            DataSet ofAlarmsForUpdate = new DataSet();
            try
            {
                ofAlarmsForUpdate = this.dbl.ExecSqlDataSet("USP_GetAlarmsTagForUpdate", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@Line", (object) LineName)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("GetListOfAlarms in BL.. : " + ex.Message.ToString()));
            }
            return ofAlarmsForUpdate;
        }

        public DataSet GetListOfAlarms_ForUpdate_BSLH(string LineName)
        {
            DataSet alarmsForUpdateBslh = new DataSet();
            try
            {
                alarmsForUpdateBslh = this.dbl.ExecSqlDataSet("USP_GetAlarmsTagForUpdate", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@Line", (object) LineName)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("GetListOfAlarms in BL.. : " + ex.Message.ToString()));
            }
            return alarmsForUpdateBslh;
        }
    }
}

