
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace ArsServer1
{

    class BusinessLayer
    {

        private dbLayer dbl = new dbLayer();

        public bool BulkInsertDataTable(string tableName, DataTable dataTable)
        {
            try
            {
                SqlConnection connection = new SqlConnection("Server='SANDIP-PC\\SQLENTRP';Database='DATALOGMM';user Id='sa';Password='sadb@123';");
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, (SqlTransaction)null)
                {
                    DestinationTableName = tableName
                };
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void FileWriter(string s)
        {
            try
            {
                string str1 = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).ToString();
                DateTime today = DateTime.Today;
                string str2 = today.ToString("dd - MMM - yyyy").Substring(today.ToString("dd - MMM - yyyy").Length - 4);
                string str3 = today.ToString("MMM");
                string str4 = today.ToString("dd-MMM-yyyy");
                using (StreamWriter streamWriter = new StreamWriter(Directory.CreateDirectory(str1 + "\\" + str2 + "\\" + str3 + "\\" + str4).FullName + "\\ AutoBackupLogFile_" + str4 + ".txt", true))
                {
                    streamWriter.WriteLine("                                                                             ");
                    streamWriter.WriteLine("--------------------------------------------------------------------------------------------------------------");
                    streamWriter.WriteLine("--------------------------------------------------------------------------------------------------------------");
                    streamWriter.WriteLine("                                                                        ");
                    streamWriter.WriteLine(DateTime.Now.ToString() + "  " + s);
                    streamWriter.Flush();
                }
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("Exception at " + (object)DateTime.Now + " " + (object)ex));
                this.FileWriter("                                                                        ");
                this.FileWriter("--------------------------------------------------------------------------------------------------------------");
                this.FileWriter("                                                                        ");
            }
        }

        public DataSet Get_StationList(object lineGroup) => this.dbl.ExecSqlDataSet("sp_GetStationList", CommandType.StoredProcedure, new List<SqlParameter>()
    {
      new SqlParameter("@linegroup", lineGroup)
    });

        public DataSet GetPLC_Tags() => this.dbl.ExecSqlDataSet("sp_GetPLC_Tags", CommandType.StoredProcedure);

        public DataSet LastLog() => this.dbl.ExecSqlDataSet("sp_LastLog", CommandType.StoredProcedure);

        public int InsertDownTimeDetails(
          object TagName,
          object LineName,
          object Breakdown,
          object Emergency,
          object Manual_Mode,
          object Marriage_Mismatch,
          object Starving_Time,
          object Tip_Change,
          object Block_Time,
          object Cycle_Not_Started,
          object Extra_Cycle_Time,
          object Material_Call,
          object Quality_Call,
          object Safety_Gate_Open,
          object Total_Losses,
          object Shift,
          object TimeStamp)
        {
            return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_DOWNTIME", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@TagName", TagName),
        new SqlParameter("@LineName", LineName),
        new SqlParameter("@Breakdown", Breakdown),
        new SqlParameter("@Emergency", Emergency),
        new SqlParameter("@Manual_Mode", Manual_Mode),
        new SqlParameter("@Marriage_Mismatch", Marriage_Mismatch),
        new SqlParameter("@Starving_Time", Starving_Time),
        new SqlParameter("@Tip_Change", Tip_Change),
        new SqlParameter("@Block_Time", Block_Time),
        new SqlParameter("@Cycle_Not_Started", Cycle_Not_Started),
        new SqlParameter("@Extra_Cycle_Time", Extra_Cycle_Time),
        new SqlParameter("@Material_Call", Material_Call),
        new SqlParameter("@Quality_Call", Quality_Call),
        new SqlParameter("@Safety_Gate_Open", Safety_Gate_Open),
        new SqlParameter("@Total_Losses", Total_Losses),
        new SqlParameter("@Shift", Shift),
        new SqlParameter("@TimeStamp", TimeStamp)
      });
        }

        public int InsertHourlyDownTimeDetails(
          object TagName,
          object LineName,
          object Shift,
          object HourRange,
          object Breakdown,
          object Emergency,
          object Idle_Time,
          object Manual_Mode,
          object Marriage_Mismatch,
          object Starving_Time,
          object Tip_Change,
          object Total_Losses,
          object TimeStamp)
        {
            return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_HOURLY_DOWNTIME", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@TagName", TagName),
        new SqlParameter("@LineName", LineName),
        new SqlParameter("@Shift", Shift),
        new SqlParameter("@HourRange", HourRange),
        new SqlParameter("@Breakdown", Breakdown),
        new SqlParameter("@Emergency", Emergency),
        new SqlParameter("@Idle_Time", Idle_Time),
        new SqlParameter("@Manual_Mode", Manual_Mode),
        new SqlParameter("@Marriage_Mismatch", Marriage_Mismatch),
        new SqlParameter("@Starving_Time", Starving_Time),
        new SqlParameter("@Tip_Change", Tip_Change),
        new SqlParameter("@Total_Losses", Total_Losses),
        new SqlParameter("@TimeStamp", TimeStamp)
      });
        }

        public int InsertHourlyProduction(
          object TagName,
          object LineName,
          object HourRange,
          object ProductionCount,
          object TimeStamp)
        {
            return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_HOURLY_PRODUCTION", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@TagName", TagName),
        new SqlParameter("@LineName", LineName),
        new SqlParameter("@HourRange", HourRange),
        new SqlParameter("@ProductionCount", ProductionCount),
        new SqlParameter("@TimeStamp", TimeStamp)
      });
        }

        public int InsertKPIValues(
          object TagName,
          object LineName,
          object MTBF,
          object MTTR,
          object OEE,
          object NoOfFailure,
          object Availability,
          object PerformanceRate,
          object QualityRate,
          object OperatingTime,
          object DownTime,
          object Shift,
          object TimeStamp)
        {
            return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_KPI", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@TagName", TagName),
        new SqlParameter("@LineName", LineName),
        new SqlParameter("@MTBF", MTBF),
        new SqlParameter("@MTTR", MTTR),
        new SqlParameter("@OEE", OEE),
        new SqlParameter("@NoOfFailure", NoOfFailure),
        new SqlParameter("@Availability", Availability),
        new SqlParameter("@PerformanceRate", PerformanceRate),
        new SqlParameter("@QualityRate", QualityRate),
        new SqlParameter("@OperatingTime", OperatingTime),
        new SqlParameter("@DownTime", DownTime),
        new SqlParameter("@Shift", Shift),
        new SqlParameter("@TimeStamp", TimeStamp)
      });
        }

        public int InsertLineCycleTime(
          object TagName,
          object CycleTime,
          object LineName,
          object ItemId,
          object ModelId,
          object TimeStamp)
        {
            return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_LINE_CT", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@TagName", TagName),
        new SqlParameter("@CycleTime", CycleTime),
        new SqlParameter("@LineName", LineName),
        new SqlParameter("@ItemId", ItemId),
        new SqlParameter("@ModelId", ModelId),
        new SqlParameter("@TimeStamp", TimeStamp)
      });
        }

        public int InsertLineEnergyConsumptionDetails(
          object TagName,
          object LineName,
          object StationName,
          object PlcName,
          object PanelName,
          object MeterName,
          object HourRange,
          object Energy_Consumption,
          object TimeStamp)
        {
            return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_ENRGY_DETAILS", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@TagName", TagName),
        new SqlParameter("@LineName", LineName),
        new SqlParameter("@StationName", StationName),
        new SqlParameter("@PlcName", PlcName),
        new SqlParameter("@PanelName", PanelName),
        new SqlParameter("@MeterName", MeterName),
        new SqlParameter("@HourRange", HourRange),
        new SqlParameter("@Energy_Consumption", Energy_Consumption),
        new SqlParameter("@TimeStamp", TimeStamp)
      });
        }

        public int InsertShiftProduction(
          object TagName,
          object LineName,
          object Target_Production_Total_Shift,
          object Actual_Production_Total_Shift,
          object Target_Production_Shift_A_H5,
          object Actual_Production_Shift_A_H5,
          object Target_Production_Shift_A_H7,
          object Actual_Production_Shift_A_H7,
          object Shift,
          object TimeStamp,
          object Target_Production_Shift_H5_GNCAP,
          object Actual_Production_Shift_H5_GNCAP,
          object Target_Production_Shift_H7_GNCAP,
          object Actual_Production_Shift_H7_GNCAP) //,object Target_Production_Shift_H5_EV,object Actual_Production_Shift_H5_EV
        {
            return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_SHIFT_PRODUCTION", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@TagName", TagName),
        new SqlParameter("@LineName", LineName),
        new SqlParameter("@Target_Production_Total_Shift", Target_Production_Total_Shift),
        new SqlParameter("@Actual_Production_Total_Shift", Actual_Production_Total_Shift),
        new SqlParameter("@Target_Production_Shift_A_H5", Target_Production_Shift_A_H5),
        new SqlParameter("@Actual_Production_Shift_A_H5", Actual_Production_Shift_A_H5),
        new SqlParameter("@Target_Production_Shift_A_H7", Target_Production_Shift_A_H7),
        new SqlParameter("@Actual_Production_Shift_A_H7", Actual_Production_Shift_A_H7),
        new SqlParameter("@Target_Production_Shift_H5_GNCAP", Target_Production_Shift_H5_GNCAP),
        new SqlParameter("@Actual_Production_Shift_H5_GNCAP", Actual_Production_Shift_H5_GNCAP),
        new SqlParameter("@Target_Production_Shift_H7_GNCAP", Target_Production_Shift_H7_GNCAP),
        new SqlParameter("@Actual_Production_Shift_H7_GNCAP", Actual_Production_Shift_H7_GNCAP),

       //, new SqlParameter("@Target_Production_Shift_H5_EV", Target_Production_Shift_H5_EV),
       // new SqlParameter("@Actual_Production_Shift_H5_EV", Actual_Production_Shift_H5_EV),


        new SqlParameter("@Shift", Shift),
        new SqlParameter("@TimeStamp", TimeStamp)
      });
        }

        public int InsertStationCycleTime(
          object TagName,
          object CycleTime,
          object LineName,
          object StationName,
          object ModelName,
          object ItemId,
          object ModelId,
          object TimeStamp)
        {
            return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_STN_CT", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@TagName", TagName),
        new SqlParameter("@CycleTime", CycleTime),
        new SqlParameter("@LineName", LineName),
        new SqlParameter("@StationName", StationName),
        new SqlParameter("@ModelName", ModelName),
        new SqlParameter("@ItemId", ItemId),
        new SqlParameter("@ModelId", ModelId),
        new SqlParameter("@TimeStamp", TimeStamp)
      });
        }

        public int InsertStationDownTimeDetails(
          object station,
          object LineName,
          object Breakdown,
          object Emergency,
          object Manual_Mode,
          object Marriage_Mismatch,
          object Starving_Time,
          object Tip_Change,
          object Block_Time,
          object Cycle_Not_Started,
          object Extra_Cycle_Time,
          object Material_Call,
          object Quality_Call,
          object Safety_Gate_Open,
          object Total_Losses,
          object Shift,
          object TimeStamp)
        {
            return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_STATION_DOWNTIME", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@station", station),
        new SqlParameter("@LineName", LineName),
        new SqlParameter("@Breakdown", Breakdown),
        new SqlParameter("@Emergency", Emergency),
        new SqlParameter("@Manual_Mode", Manual_Mode),
        new SqlParameter("@Marriage_Mismatch", Marriage_Mismatch),
        new SqlParameter("@Starving_Time", Starving_Time),
        new SqlParameter("@Tip_Change", Tip_Change),
        new SqlParameter("@Block_Time", Block_Time),
        new SqlParameter("@Cycle_Not_Started", Cycle_Not_Started),
        new SqlParameter("@Extra_Cycle_Time", Extra_Cycle_Time),
        new SqlParameter("@Material_Call", Material_Call),
        new SqlParameter("@Quality_Call", Quality_Call),
        new SqlParameter("@Safety_Gate_Open", Safety_Gate_Open),
        new SqlParameter("@Total_Losses", Total_Losses),
        new SqlParameter("@Shift", Shift),
        new SqlParameter("@TimeStamp", TimeStamp)
      });
        }

     

        public int tbIlnsertLogData(object LogData) => this.dbl.ExecSqlNonQuery("sp_InsertLogData", CommandType.StoredProcedure, new List<SqlParameter>()
    {
      new SqlParameter("@LogData", LogData)
    });

        public int tbIlnsertNutRunnerData(
          object TagName,
          object Data,
          object LineName,
          object StationName,
          object Result,
          object ControllerNo,
          object PrgNo,
          object JobCount,
          object TighteningStep,
          object Quality,
          object TorqueLowerLimit,
          object TorqueActual,
          object TorqueHigherLimit,
          object AngleLowerLimit,
          object AngleActual,
          object AngleHigherLimit,
          object ItemId,
          object ModelCode)
        {
            return this.dbl.ExecSqlNonQuery("usp_InsertNutRunnerData", CommandType.StoredProcedure, new List<SqlParameter>()
      {
        new SqlParameter("@TagName", TagName),
        new SqlParameter("@Data", Data),
        new SqlParameter("@LineName", LineName),
        new SqlParameter("@StationName", StationName),
        new SqlParameter("@Result", Result),
        new SqlParameter("@ControllerNo", ControllerNo),
        new SqlParameter("@PrgNo", PrgNo),
        new SqlParameter("@JobCount", JobCount),
        new SqlParameter("@TighteningStep", TighteningStep),
        new SqlParameter("@Quality", Quality),
        new SqlParameter("@TorqueLowerLimit", TorqueLowerLimit),
        new SqlParameter("@TorqueActual", TorqueActual),
        new SqlParameter("@TorqueHigherLimit", TorqueHigherLimit),
        new SqlParameter("@AngleLowerLimit", AngleLowerLimit),
        new SqlParameter("@AngleActual", AngleActual),
        new SqlParameter("@AngleHigherLimit", AngleHigherLimit),
        new SqlParameter("@ItemId", ItemId),
        new SqlParameter("@ModelCode", ModelCode)
      });
        }

        public int tbIlnsertScrapRebuildData(
          object TagName,
          object itemid,
          object LineName,
          object Value,
          object Variant,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_SCRAP_REBUILDS", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@ItemID", itemid),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@Value", Value),
          new SqlParameter("@Variant", Variant),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("Error : ScrapNRebuild BL : " + ex.ToString()));
                return 0;
            }
        }

        public int tbIlnsertTakeInTakeOut(
          object TagName,
          object LineName,
          object StationName,
          object ItemId,
          object Type,
          object Variant,
          object TimeStamp)
        {
            try
            {
                return this.dbl.ExecSqlNonQuery("USP_SCADA_INSERT_TAKEIN_TAKEOUT", CommandType.StoredProcedure, new List<SqlParameter>()
        {
          new SqlParameter("@TagName", TagName),
          new SqlParameter("@StationName", StationName),
          new SqlParameter("@ItemID", ItemId),
          new SqlParameter("@LineName", LineName),
          new SqlParameter("@Type", Type),
          new SqlParameter("@Variant", Variant),
          new SqlParameter("@TimeStamp", TimeStamp)
        });
            }
            catch (Exception ex)
            {
                this.tbIlnsertLogData((object)("Error : TAKEIN_TAKEOUT BL : " + ex.ToString()));
                return 0;
            }
        }

        
    }
}

