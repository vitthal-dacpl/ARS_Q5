using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Opc;
using Opc.Da;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Configuration;

namespace ArsServer1
{
    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        #region Iniatialization

        private List<string> lstStnCT_Tags;
        private List<string> lstLnCT_Tags;
        private List<string> lstShiftEndTrigger_Tags;
        private List<string> lstHourlyProduction_Tags;
        private List<string> lstKPI_Tags;
        private List<string> lstNutRunners;
        private List<string> lstStnList;
        private List<string> lstScrapRbld_Tags;
        private List<string> lstTakeInTakeOut_Tags;
        private List<Item> OPCItemsList1 = new List<Item>();
        private List<Item> OPCItemsList2 = new List<Item>();
        private List<Item> OPCItemsList3 = new List<Item>();
        private List<Item> OPCItemsList4 = new List<Item>();
        private List<Item> OPCItemsList5 = new List<Item>();
        private List<Item> OPCItemsList6 = new List<Item>();
        private List<Item> OPCItems_ScrapRbldList = new List<Item>();
        private List<Item> OPCItems_TakeInTakeOutList = new List<Item>();
        private Opc.Da.Server WinCCOPCServer;
        private OpcCom.Factory COMFactory = new OpcCom.Factory();
        private Subscription ReadGroupSubscription1;
        private Subscription ReadGroupSubscription2;
        private Subscription ReadGroupSubscription3;
        private Subscription ReadGroupSubscription4;
        private Subscription ReadGroupSubscription5;
        private Subscription ReadGroupSubscription6;
        private Subscription ReadGroupSubscription_ScrapRbld;
        private Subscription ReadGroupSubscription_TakeInTakeOut;
        private SubscriptionState ReadGroupSubscriptionState1;
        private SubscriptionState ReadGroupSubscriptionState2;
        private SubscriptionState ReadGroupSubscriptionState3;
        private SubscriptionState ReadGroupSubscriptionState4;
        private SubscriptionState ReadGroupSubscriptionState5;
        private SubscriptionState ReadGroupSubscriptionState6;
        private SubscriptionState ReadGroupSubscriptionState__ScrapRbld;
        private SubscriptionState ReadGroupSubscriptionState_TakeInTakeOut;
        private Subscription WriteGroupSubscription;
        private SubscriptionState WriteGroupSubscriptionState;
        private BusinessLayer bl = (BusinessLayer)null;
        private System.Timers.Timer tmrDelay;
        private System.Timers.Timer tmrHeartBit;
        //  private IContainer components = (IContainer)null;
        #endregion
        // private IContainer components = (IContainer)null;


        // private OPC_UA_CLIENT opcUaClient;
        public static string connectionString;
        private string endpointurl;

        private void InitTagList()
        {
            try
            {
                DataSet plcTags = this.bl.GetPLC_Tags();
                this.lstStnCT_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("GroupName") == "StationWise_CycleTime")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lstLnCT_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("GroupName") == "LineWise_CycleTime")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lstShiftEndTrigger_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("GroupName") == "ShiftEndTriggers")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lstHourlyProduction_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("GroupName") == "HourlyProduction")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lstKPI_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("GroupName") == "KpiShiftEnd_Trigger")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                // this.lstNutRunners = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("GroupName") == "NutRunner_Tags")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lstScrapRbld_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("GroupName") == "ScrapRebuild")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lstTakeInTakeOut_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("GroupName") == "TakeIn-TakeOut")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();

            }
            catch (Exception ex)
            {
                bl.tbIlnsertLogData("Exception while InitTagList Tags: " + ex.ToString());
            }
        }

        private void ConnectToWinCCOpcServer(string endpoint)
        {
            try
            {
                Opc.Da.Server server = new Opc.Da.Server((Opc.Factory)this.COMFactory, (URL)null);
                server.Url = new URL(endpoint);
                this.WinCCOPCServer = server;
                this.WinCCOPCServer.Connect();

                this.ReadGroupSubscriptionState1 = new SubscriptionState()
                {
                    Name = "TagGroup1",
                    Active = true
                };
                this.ReadGroupSubscription1 = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState1);
                this.ReadGroupSubscriptionState1.UpdateRate = 200;
                this.ReadGroupSubscription1.DataChanged += new DataChangedEventHandler(this.ReadGroupSubscription_StationCycleTimeDataChanged);
                foreach (string lstStnCtTag in this.lstStnCT_Tags)
                {
                    Item obj = new Item();
                    obj.ItemName = lstStnCtTag;
                    this.OPCItemsList1.Add(obj);
                }
                this.ReadGroupSubscription1.AddItems(this.OPCItemsList1.ToArray());

                #region Old 

                this.ReadGroupSubscriptionState2 = new SubscriptionState()
                {
                    Name = "TagGroup2",
                    Active = true
                };
                this.ReadGroupSubscription2 = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState2);
                this.ReadGroupSubscriptionState2.UpdateRate = 200;
                this.ReadGroupSubscription2.DataChanged += new DataChangedEventHandler(this.ReadGroupSubscription_LineCycleTimeDataChanged);
                foreach (string lstLnCtTag in this.lstLnCT_Tags)
                {
                    Item obj = new Item();
                    obj.ItemName = lstLnCtTag;
                    this.OPCItemsList2.Add(obj);
                }
                this.ReadGroupSubscription2.AddItems(this.OPCItemsList2.ToArray());
                this.ReadGroupSubscriptionState3 = new SubscriptionState()
                {
                    Name = "TagGroup3",
                    Active = true
                };
                this.ReadGroupSubscription3 = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState3);
                this.ReadGroupSubscriptionState3.UpdateRate = 200;
                this.ReadGroupSubscription3.DataChanged += new DataChangedEventHandler(this.ReadGroupSubscription_ShiftTriggerDataChanged);
                foreach (string shiftEndTriggerTag in this.lstShiftEndTrigger_Tags)
                {
                    Item obj = new Item();
                    obj.ItemName = shiftEndTriggerTag;
                    this.OPCItemsList3.Add(obj);
                }
                this.ReadGroupSubscription3.AddItems(this.OPCItemsList3.ToArray());
                this.ReadGroupSubscriptionState4 = new SubscriptionState()
                {
                    Name = "TagGroup4",
                    Active = true
                };
                this.ReadGroupSubscription4 = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState4);
                this.ReadGroupSubscriptionState4.UpdateRate = 200;
                this.ReadGroupSubscription4.DataChanged += new DataChangedEventHandler(this.ReadGroupSubscription_HourlyProductionDataChanged);
                foreach (string hourlyProductionTag in this.lstHourlyProduction_Tags)
                {
                    Item obj = new Item();
                    obj.ItemName = hourlyProductionTag;
                    this.OPCItemsList4.Add(obj);
                }
                this.ReadGroupSubscription4.AddItems(this.OPCItemsList4.ToArray());
                this.ReadGroupSubscriptionState5 = new SubscriptionState()
                {
                    Name = "TagGroup5",
                    Active = true
                };
                this.ReadGroupSubscription5 = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState5);
                this.ReadGroupSubscriptionState5.UpdateRate = 200;
                this.ReadGroupSubscription5.DataChanged += new DataChangedEventHandler(this.ReadGroupSubscription_KPI_DataChanged);
                foreach (string lstKpiTag in this.lstKPI_Tags)
                {
                    Item obj = new Item();
                    obj.ItemName = lstKpiTag;
                    this.OPCItemsList5.Add(obj);
                }
                this.ReadGroupSubscription5.AddItems(this.OPCItemsList5.ToArray());
                //this.ReadGroupSubscriptionState6 = new SubscriptionState()
                //{
                //    Name = "TagGroup6",
                //    Active = true
                //};
                //this.ReadGroupSubscription6 = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState6);
                //this.ReadGroupSubscriptionState6.UpdateRate = 200;
                //this.ReadGroupSubscription6.DataChanged += new DataChangedEventHandler(this.ReadGroupSubscription_NutRunner_DataChanged);
                //foreach (string lstNutRunner in this.lstNutRunners)
                //{
                //    Item obj = new Item();
                //    obj.ItemName = lstNutRunner;
                //    this.OPCItemsList6.Add(obj);
                //}
                //this.ReadGroupSubscription6.AddItems(this.OPCItemsList6.ToArray());
                this.ReadGroupSubscriptionState__ScrapRbld = new SubscriptionState()
                {
                    Name = "TagGroup7",
                    Active = true
                };
                this.ReadGroupSubscription_ScrapRbld = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState__ScrapRbld);
                this.ReadGroupSubscriptionState__ScrapRbld.UpdateRate = 200;
                this.ReadGroupSubscription_ScrapRbld.DataChanged += new DataChangedEventHandler(this.ReadGroupSubscription_ScrapRebuild_DataChanged);
                foreach (string lstScrapRbldTag in this.lstScrapRbld_Tags)
                {
                    Item obj = new Item();
                    obj.ItemName = lstScrapRbldTag;
                    this.OPCItems_ScrapRbldList.Add(obj);
                }
                this.ReadGroupSubscription_ScrapRbld.AddItems(this.OPCItems_ScrapRbldList.ToArray());
                this.ReadGroupSubscriptionState_TakeInTakeOut = new SubscriptionState()
                {
                    Name = "TagGroup8",
                    Active = true
                };
                this.ReadGroupSubscription_TakeInTakeOut = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_TakeInTakeOut);
                this.ReadGroupSubscriptionState_TakeInTakeOut.UpdateRate = 200;
                this.ReadGroupSubscription_TakeInTakeOut.DataChanged += new DataChangedEventHandler(this.ReadGroupSubscription_TAKEIN_TAKEOUT_DataChanged);
                foreach (string takeInTakeOutTag in this.lstTakeInTakeOut_Tags)
                {
                    Item obj = new Item();
                    obj.ItemName = takeInTakeOutTag;
                    this.OPCItems_TakeInTakeOutList.Add(obj);
                }
                this.ReadGroupSubscription_TakeInTakeOut.AddItems(this.OPCItems_TakeInTakeOutList.ToArray());

                #endregion
                this.WriteGroupSubscriptionState = new SubscriptionState();
                this.WriteGroupSubscriptionState.Name = "Write Group";
                this.WriteGroupSubscriptionState.Active = false;
                this.WriteGroupSubscription = (Subscription)this.WinCCOPCServer.CreateSubscription(this.WriteGroupSubscriptionState);
                this.bl.tbIlnsertLogData((object)"Wincc is connected properly  ....");
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)ex.ToString());
            }
        }
        private void ReadGroupSubscription_StationCycleTimeDataChanged(
    object subscriptionHandle,
    object requestHandle,
    ItemValueResult[] values)
        {
            try
            {
                Parallel.ForEach<ItemValueResult>((IEnumerable<ItemValueResult>)values, (Action<ItemValueResult>)(itemValue =>
                {
                    if (!(itemValue.Value.ToString() != "False") || !(itemValue.Value.ToString() != "0"))
                        return;
                    try
                    {
                        char[] chArray = new char[1] { '_' };
                        string[] strArray = itemValue.ItemName.ToString().Split(chArray);
                        strArray[0].ToString();
                        string TagName1 = strArray[0] + "_" + strArray[1] + "_" + strArray[2] + "_Item_ID";
                        string TagName2 = strArray[0] + "_" + strArray[1] + "_" + strArray[2] + "_Cycle_Time";
                        string TagName3 = strArray[0] + "_" + strArray[1] + "_" + strArray[2] + "_Model_Code";
                        string ModelName = strArray[2];
                        string CycleTime = this.ReadTag(TagName2);
                        string ModelId = this.ReadTag(TagName3);
                        string ItemId = this.ReadTag(TagName1);
                        if (ItemId.Trim() != "0")
                            this.bl.InsertStationCycleTime((object)itemValue.ItemName.ToString(), (object)CycleTime, (object)strArray[0], (object)strArray[1], (object)ModelName, (object)ItemId, (object)ModelId, (object)itemValue.Timestamp);
                    }
                    catch (Exception ex)
                    {
                        this.bl.tbIlnsertLogData((object)("Station CycleTime Method : " + ex.ToString()));
                    }
                }));
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)ex.ToString());
            }
        }

        private void ReadGroupSubscription_HourlyProductionDataChanged(
      object subscriptionHandle,
      object requestHandle,
      ItemValueResult[] values)
        {
            try
            {
                Parallel.ForEach<ItemValueResult>((IEnumerable<ItemValueResult>)values, (Action<ItemValueResult>)(itemValue =>
                {
                    if (!(itemValue.Value.ToString() == "True"))
                        return;
                    char[] chArray = new char[1] { '_' };
                    string[] strArray = itemValue.ItemName.ToString().Split(chArray);
                    string LineName = strArray[0];
                    string str1 = itemValue.ItemName.Replace(strArray[0] + "_Shift_Trigger_H_", "");
                    int ProductionCount = int.Parse(this.ReadTag(LineName + "_Production_Count_Hour_wise_Count_H_" + str1));
                    string str2 = str1.Replace('_', ':');
                    if (this.IsRightTrigger_Hourly(str2))
                        this.bl.InsertHourlyProduction((object)itemValue.ItemName.ToString(), (object)LineName, (object)str2, (object)ProductionCount, (object)itemValue.Timestamp);
                    else
                        this.bl.tbIlnsertLogData((object)("Odd Time Tag Trigger for :" + itemValue.ItemName.ToString()));
                }));
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)ex.ToString());
            }
        }

        private void ReadGroupSubscription_KPI_DataChanged(
     object subscriptionHandle,
     object requestHandle,
     ItemValueResult[] values)
        {
            try
            {
                Parallel.ForEach<ItemValueResult>((IEnumerable<ItemValueResult>)values, (Action<ItemValueResult>)(itemValue =>
                {
                    if (!(itemValue.Value.ToString() == "True"))
                        return;
                    string itemName = itemValue.ItemName;
                    char[] chArray = new char[1] { '_' };
                    string[] strArray = itemValue.ItemName.ToString().Split(chArray);
                    string LineName = strArray[0];
                    string Shift = strArray[3];
                    string TagName1 = LineName + "_MTBF_Shift_" + Shift;
                    string TagName2 = LineName + "_MTTR_Shift_" + Shift;
                    string TagName3 = LineName + "_OEE_Shift_" + Shift;
                    string TagName4 = LineName + "_NumberOf_Failure_Shift" + Shift;
                    string TagName5 = LineName + "_Availability_Shift_" + Shift;
                    string TagName6 = LineName + "_PerformanceRate_Shift_" + Shift;
                    string TagName7 = LineName + "_QualityRate_Shift_" + Shift;
                    string TagName8 = LineName + "_OperatingTime_Shift_" + Shift;
                    string TagName9 = LineName + "_DownTime_Shift_" + Shift;
                    Decimal MTBF = Decimal.Parse(this.ReadTag(TagName1));
                    Decimal MTTR = Decimal.Parse(this.ReadTag(TagName2));
                    Decimal OEE = Decimal.Parse(this.ReadTag(TagName3));
                    Decimal Availability = Decimal.Parse(this.ReadTag(TagName5));
                    Decimal PerformanceRate = Decimal.Parse(this.ReadTag(TagName6));
                    Decimal QualityRate = Decimal.Parse(this.ReadTag(TagName7));
                    Decimal OperatingTime = Decimal.Parse(this.ReadTag(TagName8));
                    Decimal DownTime = Decimal.Parse(this.ReadTag(TagName9));
                    int NoOfFailure = int.Parse(this.ReadTag(TagName4));
                    this.bl.InsertKPIValues((object)itemName, (object)LineName, (object)MTBF, (object)MTTR, (object)OEE, (object)NoOfFailure, (object)Availability, (object)PerformanceRate, (object)QualityRate, (object)OperatingTime, (object)DownTime, (object)Shift, (object)itemValue.Timestamp);
                }));
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)ex.ToString());
            }
        }

        private void ReadGroupSubscription_LineCycleTimeDataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
                Parallel.ForEach<ItemValueResult>((IEnumerable<ItemValueResult>)values, (Action<ItemValueResult>)(itemValue =>
                {
                    if (!(itemValue.Value.ToString() != "0"))
                        return;

                    char[] chArray = new char[1] { '_' };
                    string LineName = itemValue.ItemName.ToString().Split(chArray)[0];
                    string TagName1 = itemValue.ItemName.Replace("Item_ID", "Cycle_Time");
                    string TagName2 = itemValue.ItemName.Replace("Item_ID", "Model_Code");
                    string CycleTime = this.ReadTag(TagName1);
                    string ModelId = this.ReadTag(TagName2);
                    this.bl.InsertLineCycleTime((object)itemValue.ItemName.ToString(), (object)CycleTime, (object)LineName, (object)itemValue.Value.ToString(), (object)ModelId, (object)itemValue.Timestamp);
                }));
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)ex.ToString());
            }
        }

        private void ReadGroupSubscription_NutRunner_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
                foreach (ItemValueResult itemValueResult in values)
                {
                    char[] chArray = new char[1] { '_' };
                    string[] strArray1 = itemValueResult.ItemName.Split(chArray);
                    char[] separator = new char[1] { ' ' };
                    string[] strArray2 = itemValueResult.Value.ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    if (strArray2.Length != 0 && strArray1.Length > 1)
                    {
                        string LineName = strArray1[0];
                        string StationName = strArray1[1];
                        itemValueResult.Value.ToString();
                        string Result = strArray2[0].Trim().Substring(0);
                        string ControllerNo = strArray2[0].Trim().Substring(0);
                        string PrgNo = strArray2[1].Trim();
                        string JobCount = strArray2[2].Trim();
                        string TighteningStep = strArray2[3].Trim();
                        string Quality = strArray2[4].Trim();
                        string TorqueLowerLimit = strArray2[5].Trim();
                        string TorqueActual = strArray2[6].Trim();
                        string TorqueHigherLimit = strArray2[7].Trim();
                        string AngleLowerLimit = strArray2[8].Trim();
                        string AngleActual = strArray2[9].Trim();
                        string AngleHigherLimit = strArray2[10].Trim();
                        string ItemId = strArray2[11].Trim();
                        string ModelCode = strArray2[12].Trim();
                        this.bl.tbIlnsertNutRunnerData((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)LineName, (object)StationName, (object)Result, (object)ControllerNo, (object)PrgNo, (object)JobCount, (object)TighteningStep, (object)Quality, (object)TorqueLowerLimit, (object)TorqueActual, (object)TorqueHigherLimit, (object)AngleLowerLimit, (object)AngleActual, (object)AngleHigherLimit, (object)ItemId, (object)ModelCode);
                    }
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)ex.ToString());
            }
        }

        private void ReadGroupSubscription_ScrapRebuild_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
                foreach (ItemValueResult itemValueResult in values)
                {
                    if (itemValueResult.Value.ToString() == "True" || itemValueResult.Value.ToString() == "1")
                    {
                        char[] chArray = new char[1] { '_' };
                        string[] strArray = itemValueResult.ItemName.Split(chArray);
                        string str = this.ReadTag(strArray[0].ToString() + "_SCRAP_VALUE").ToString();
                        string itemid = this.ReadTag(strArray[0].ToString() + "_SCRAP_Item_ID").ToString();
                        string Variant = this.ReadTag(strArray[0].ToString() + "_SCRAP_VARIANT").ToString();
                        DateTime timestamp = itemValueResult.Timestamp;
                        this.bl.tbIlnsertScrapRebuildData((object)itemValueResult.ItemName.ToString(), (object)itemid, (object)strArray[0].ToString(), (object)str, (object)Variant, (object)timestamp);
                    }
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error : ScrapNRebuiild Main : " + ex.ToString()));
            }
        }

        private void ReadGroupSubscription_ShiftTriggerDataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
                Parallel.ForEach<ItemValueResult>((IEnumerable<ItemValueResult>)values, (Action<ItemValueResult>)(itemValue =>
                {
                    if (!(itemValue.Value.ToString() == "True"))
                        return;
                    string itemName = itemValue.ItemName;
                    char[] chArray = new char[1] { '_' };
                    string[] strArray = itemValue.ItemName.ToString().Split(chArray);
                    string LineName = strArray[0];
                    string Shift = strArray[6];
                    if (this.IsRightTrigger(Shift))
                    {

                        try
                        {
                            string TagName14 = LineName + "_Downtime_Losses[0]_Shift_" + Shift + "_Downtime_Breakdown";
                            string TagName15 = LineName + "_Downtime_Losses[0]_Shift_" + Shift + "_Downtime_Emergency";
                            string TagName16 = LineName + "_Downtime_Losses[0]_Shift_" + Shift + "_Downtime_Manual_Mode";
                            string TagName17 = LineName + "_Downtime_Losses[0]_Shift_" + Shift + "_Downtime_Marriage_Mismatch";
                            string TagName18 = LineName + "_Downtime_Losses[0]_Shift_" + Shift + "_Downtime_Starving_Time";
                            string TagName19 = LineName + "_Downtime_Losses[0]_Shift_" + Shift + "_Downtime_Tip_Change";
                            string TagName20 = LineName + "_Downtime_Losses[0]_Shift_" + Shift + "_Downtime_Block_Time";
                            string TagName21 = LineName + "_Downtime_Losses[0]_Shift_" + Shift + "_Downtime_Cycle_Not_Started";
                            string TagName22 = LineName + "_Downtime_Losses[0]_Shift_" + Shift + "_Downtime_Extra_Cycle_Time";
                            string TagName23 = LineName + "_Downtime_Losses[0]_Shift_" + Shift + "_Downtime_Material_Call";
                            string TagName24 = LineName + "_Downtime_Losses[0]_Shift_" + Shift + "_Downtime_Quality_Call";
                            string TagName25 = LineName + "_Downtime_Losses[0]_Shift_" + Shift + "_Downtime_Safety_Gate_Open";
                            string TagName26 = LineName + "_Downtime_Losses[0]_Shift_" + Shift + "_Downtime_Total_Losses";
                            int Breakdown = int.Parse(this.ReadTag(TagName14));
                            int Emergency = int.Parse(this.ReadTag(TagName15));
                            int Manual_Mode = int.Parse(this.ReadTag(TagName16));
                            int Marriage_Mismatch = int.Parse(this.ReadTag(TagName17));
                            int Starving_Time = int.Parse(this.ReadTag(TagName18));
                            int Tip_Change = int.Parse(this.ReadTag(TagName19));
                            int Block_Time = int.Parse(this.ReadTag(TagName20));
                            int Cycle_Not_Started = int.Parse(this.ReadTag(TagName21));
                            int Extra_Cycle_Time = int.Parse(this.ReadTag(TagName22));
                            int Material_Call = int.Parse(this.ReadTag(TagName23));
                            int Quality_Call = int.Parse(this.ReadTag(TagName24));
                            int Safety_Gate_Open = int.Parse(this.ReadTag(TagName25));
                            int Total_Losses = int.Parse(this.ReadTag(TagName26));
                            this.bl.InsertDownTimeDetails((object)itemName, (object)LineName, (object)Breakdown, (object)Emergency, (object)Manual_Mode, (object)Marriage_Mismatch, (object)Starving_Time, (object)Tip_Change, (object)Block_Time, (object)Cycle_Not_Started, (object)Extra_Cycle_Time, (object)Material_Call, (object)Quality_Call, (object)Safety_Gate_Open, (object)Total_Losses, (object)Shift, (object)itemValue.Timestamp);
                        }
                        catch (Exception ex)
                        {
                            this.bl.tbIlnsertLogData((object)ex.ToString());
                        }
                        try
                        {
                            string TagName27 = LineName + "_Production_Count_Target_Production_Total_Shift_" + Shift;
                            string TagName28 = LineName + "_Production_Count_Total_Shift_" + Shift;
                            string TagName29 = LineName + "_Production_Count_Target_Production_Shift_" + Shift + "_H5";
                            string TagName30 = LineName + "_Production_Count_Shift_" + Shift + "_H5";
                            string TagName31 = LineName + "_Production_Count_Target_Production_Shift_" + Shift + "_H7";
                            string TagName32 = LineName + "_Production_Count_Shift_" + Shift + "_H7";
                            string TagName33 = LineName + "_Production_Count_Target_Production_Shift_" + Shift + "_H5GNCAP";
                            string TagName34 = LineName + "_Production_Count_Shift_" + Shift + "_H5GNCAP";
                            string TagName35 = LineName + "_Production_Count_Target_Production_Shift_" + Shift + "_H7GNCAP";
                            string TagName36 = LineName + "_Production_Count_Shift_" + Shift + "_H7GNCAP";


                            //string TagName37 = LineName + "_Production_Count_Target_Production_Shift_" + Shift + "_H5EV";
                            //string TagName38 = LineName + "_Production_Count_Shift_" + Shift + "_H5EV";


                            int Target_Production_Total_Shift = int.Parse(this.ReadTag(TagName27));
                            int Actual_Production_Total_Shift = int.Parse(this.ReadTag(TagName28));
                            int Target_Production_Shift_A_H5 = int.Parse(this.ReadTag(TagName29));
                            int Actual_Production_Shift_A_H5 = int.Parse(this.ReadTag(TagName30));
                            int Target_Production_Shift_A_H7 = int.Parse(this.ReadTag(TagName31));
                            int Actual_Production_Shift_A_H7 = int.Parse(this.ReadTag(TagName32));
                            int Target_Production_Shift_H5_GNCAP = int.Parse(this.ReadTag(TagName33));
                            int Actual_Production_Shift_H5_GNCAP = int.Parse(this.ReadTag(TagName34));
                            int Target_Production_Shift_H7_GNCAP = int.Parse(this.ReadTag(TagName35));
                            int Actual_Production_Shift_H7_GNCAP = int.Parse(this.ReadTag(TagName36));

                            //int Target_Production_Shift_H5_EV = int.Parse(this.ReadTag(TagName37));
                            //int Actual_Production_Shift_H5_EV = int.Parse(this.ReadTag(TagName38));


                            this.bl.InsertShiftProduction((object)itemName, (object)LineName, (object)Target_Production_Total_Shift, (object)Actual_Production_Total_Shift, (object)Target_Production_Shift_A_H5, (object)Actual_Production_Shift_A_H5, (object)Target_Production_Shift_A_H7, (object)Actual_Production_Shift_A_H7, (object)Shift, (object)itemValue.Timestamp, (object)Target_Production_Shift_H5_GNCAP, (object)Actual_Production_Shift_H5_GNCAP, (object)Target_Production_Shift_H7_GNCAP, (object)Actual_Production_Shift_H7_GNCAP);//,(object) Target_Production_Shift_H5_EV,(object) Actual_Production_Shift_H5_EV
                        }
                        catch (Exception ex)
                        {
                            this.bl.tbIlnsertLogData((object)ex.ToString());
                        }
                        try
                        {
                            this.lstStnList = this.bl.Get_StationList((object)LineName).Tables[0].AsEnumerable().Select<DataRow, string>((System.Func<DataRow, string>)(r => r.Field<string>(0))).ToList<string>();
                            Parallel.ForEach<string>((IEnumerable<string>)this.lstStnList, (Action<string>)(stn =>
                            {
                                if (!(LineName != "OT"))
                                    return;
                                string station = stn.ToString();
                                string TagName37 = LineName + "_" + station + "_Downtime_Losses[1]_Shift_" + Shift + "_Downtime_Block_Time";
                                string TagName38 = LineName + "_" + station + "_Downtime_Losses[1]_Shift_" + Shift + "_Downtime_Breakdown";
                                string TagName39 = LineName + "_" + station + "_Downtime_Losses[1]_Shift_" + Shift + "_Downtime_Emergency";
                                string TagName40 = LineName + "_" + station + "_Downtime_Losses[1]_Shift_" + Shift + "_Downtime_Cycle_Not_Started";
                                string TagName41 = LineName + "_" + station + "_Downtime_Losses[1]_Shift_" + Shift + "_Downtime_Extra_Cycle_Time";
                                string TagName42 = LineName + "_" + station + "_Downtime_Losses[1]_Shift_" + Shift + "_Downtime_Manual_Mode";
                                string TagName43 = LineName + "_" + station + "_Downtime_Losses[1]_Shift_" + Shift + "_Downtime_Marriage_Mismatch";
                                string TagName44 = LineName + "_" + station + "_Downtime_Losses[1]_Shift_" + Shift + "_Downtime_Material_Call";
                                string TagName45 = LineName + "_" + station + "_Downtime_Losses[1]_Shift_" + Shift + "_Downtime_Quality_Call";
                                string TagName46 = LineName + "_" + station + "_Downtime_Losses[1]_Shift_" + Shift + "_Downtime_Safety_Gate_Open";
                                string TagName47 = LineName + "_" + station + "_Downtime_Losses[1]_Shift_" + Shift + "_Downtime_Starving_Time";
                                string TagName48 = LineName + "_" + station + "_Downtime_Losses[1]_Shift_" + Shift + "_Downtime_Tip_Change";
                                string TagName49 = LineName + "_" + station + "_Downtime_Losses[1]_Shift_" + Shift + "_Downtime_Total_Losses";
                                try
                                {
                                    int Block_Time = int.Parse(this.ReadTag(TagName37));
                                    int Breakdown = int.Parse(this.ReadTag(TagName38));
                                    int Emergency = int.Parse(this.ReadTag(TagName39));
                                    int Cycle_Not_Started = int.Parse(this.ReadTag(TagName40));
                                    int Extra_Cycle_Time = int.Parse(this.ReadTag(TagName41));
                                    int Manual_Mode = int.Parse(this.ReadTag(TagName42));
                                    int Marriage_Mismatch = int.Parse(this.ReadTag(TagName43));
                                    int Material_Call = int.Parse(this.ReadTag(TagName44));
                                    int Quality_Call = int.Parse(this.ReadTag(TagName45));
                                    int Safety_Gate_Open = int.Parse(this.ReadTag(TagName46));
                                    int Starving_Time = int.Parse(this.ReadTag(TagName47));
                                    int Tip_Change = int.Parse(this.ReadTag(TagName48));
                                    int Total_Losses = int.Parse(this.ReadTag(TagName49));
                                    this.bl.InsertStationDownTimeDetails((object)station, (object)LineName, (object)Breakdown, (object)Emergency, (object)Manual_Mode, (object)Marriage_Mismatch, (object)Starving_Time, (object)Tip_Change, (object)Block_Time, (object)Cycle_Not_Started, (object)Extra_Cycle_Time, (object)Material_Call, (object)Quality_Call, (object)Safety_Gate_Open, (object)Total_Losses, (object)Shift, (object)itemValue.Timestamp);
                                }
                                catch (Exception ex)
                                {
                                    this.bl.tbIlnsertLogData((object)("Station Losses Parellel ForEach : " + ex.ToString()));
                                }
                            }));
                        }
                        catch (Exception ex)
                        {
                            this.bl.tbIlnsertLogData((object)ex);
                        }
                    }
                    else
                        this.bl.tbIlnsertLogData((object)("Odd time triggered" + itemName + " is triggered "));
                }));
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)ex.ToString());
            }
        }

        private void ReadGroupSubscription_TAKEIN_TAKEOUT_DataChanged(
     object subscriptionHandle,
     object requestHandle,
     ItemValueResult[] values)
        {
            try
            {
                foreach (ItemValueResult itemValueResult in values)
                {

                    if (itemValueResult.Value.ToString() == "True" || itemValueResult.Value.ToString() == "1")
                    {
                        char[] chArray = new char[1] { '_' };
                        string[] strArray = itemValueResult.ItemName.Split(chArray);
                        string StationName;
                        string Type;
                        if (strArray.Length == 5)
                        {
                            StationName = strArray[1] + "_" + strArray[2];
                            Type = strArray[3].ToString();
                        }
                        else
                        {
                            StationName = strArray[1];
                            Type = strArray[2].ToString();
                        }
                        strArray[1].ToString();
                        if (Type == "TAKEOUT")
                        {
                            string LineName = strArray[0].ToString();
                            string ItemId = this.ReadTag(LineName + "_" + StationName + "_" + Type + "_ItemID").ToString();
                            string Variant = this.ReadTag(LineName + "_" + StationName + "_" + Type + "_ModelCode").ToString();
                            DateTime timestamp = itemValueResult.Timestamp;
                            this.bl.tbIlnsertTakeInTakeOut((object)itemValueResult.ItemName, (object)LineName, (object)StationName, (object)ItemId, (object)Type, (object)Variant, (object)timestamp);
                        }
                        else
                        {
                            string LineName = strArray[0].ToString();
                            string ItemId = this.ReadTag(LineName + "_" + StationName + "_" + Type + "_ItemID").ToString();
                            string Variant = this.ReadTag(LineName + "_" + StationName + "_" + Type + "_ModelCode").ToString();
                            DateTime timestamp = itemValueResult.Timestamp;
                            this.bl.tbIlnsertTakeInTakeOut((object)itemValueResult.ItemName, (object)LineName, (object)StationName, (object)ItemId, (object)Type, (object)Variant, (object)timestamp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error : TAKEIN_TAKEOUT Main : " + ex.ToString()));
            }
        }

        public bool IsRightTrigger(string shift)
        {
            if (shift == null || !(shift != ""))
                return false;
            TimeSpan timeSpan1 = TimeSpan.Parse(DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss"));
            TimeSpan timeSpan2 = TimeSpan.Parse("14:55:00");
            TimeSpan timeSpan3 = TimeSpan.Parse("15:30:00");
            TimeSpan timeSpan4 = TimeSpan.Parse("23:25:00");
            TimeSpan timeSpan5 = TimeSpan.Parse("23:59:00");
            TimeSpan timeSpan6 = TimeSpan.Parse("06:20:00");
            TimeSpan timeSpan7 = TimeSpan.Parse("06:35:00");
            switch (shift.ToUpper())
            {
                case "A":
                    return timeSpan1 >= timeSpan2 && timeSpan1 <= timeSpan3;
                case "B":
                    return timeSpan1 >= timeSpan4 && timeSpan1 <= timeSpan5;
                case "C":
                    return timeSpan1 >= timeSpan6 && timeSpan1 <= timeSpan7;
                default:
                    return false;
            }
        }

        public bool IsRightTrigger_Hourly(string hourRange)
        {
            if (hourRange == null || !(hourRange != ""))
                return false;
            char[] chArray = new char[1] { '-' };
            TimeSpan timeSpan1 = TimeSpan.Parse(hourRange.Trim().Split(chArray)[1]);
            TimeSpan timeSpan2 = TimeSpan.Parse(DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss"));
            TimeSpan timeSpan3 = timeSpan1.Subtract(TimeSpan.Parse("00:05:00"));
            TimeSpan timeSpan4 = timeSpan1.Add(TimeSpan.Parse("00:15:00"));
            return timeSpan2 >= timeSpan3 && timeSpan2 <= timeSpan4;
        }

        private void WriteTag(string TagName, string value)
        {
            Item[] items1 = new Item[1] { new Item() };
            items1[0].ItemName = TagName;
            ItemValue[] items2 = new ItemValue[1]
            {
        new ItemValue((ItemIdentifier) items1[0])
            };
            bool flag = false;
            foreach (Item obj in this.WriteGroupSubscription.Items)
            {
                if (obj.ItemName == items1[0].ItemName)
                {
                    items2[0].ServerHandle = obj.ServerHandle;
                    flag = true;
                }
            }
            if (!flag)
            {
                this.WriteGroupSubscription.AddItems(items1);
                items2[0].ServerHandle = this.WriteGroupSubscription.Items[this.WriteGroupSubscription.Items.Length - 1].ServerHandle;
            }
            items2[0].Value = (object)value;
            this.WriteGroupSubscription.Write(items2);
        }
        public string ReadTag(string TagName)
        {
            try
            {
                Item[] items = new Item[1] { new Item() };
                items[0].ItemName = TagName;
                return this.WinCCOPCServer.Read(items)[0].Value.ToString();
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Missed TagName : " + TagName));
                this.bl.tbIlnsertLogData((object)("TagName : " + TagName + "  " + ex.ToString()));
                return "-0";
            }
        }

        public Form1()
        {

            InitializeComponent();
            this.ShowInTaskbar = false;

            this.MaximizeBox = false;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));



        }

        private void Form1_Load(object sender, EventArgs e)
        {

            button3.Visible = true;

            checkBox1.Checked = false;
            checkBox1.Visible = false;

        }

        //-------------Connecting to OPCDA Server-------------------------------------
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string servername = ServerName.Text;
            string username = UserName.Text;
            string password = Password.Text;
            if (checkBox1.Checked)
            {
                UserName.ReadOnly = true;
                Password.ReadOnly = true;
                Test_Connection.Hide();
                if (servername != "")
                {
                    connectionString = $"Data Source={servername};Integrated Security=True";
                    MessageBox.Show("Connected Windows Authentication");
                }
                else
                {
                    MessageBox.Show("NotConnected Windows Authentication");
                }
            }
            else
            {
                UserName.ReadOnly = false;
                Password.ReadOnly = false;

                if (servername != "" && username != "" || password != "")
                {
                    connectionString = $"Data Source={servername};User ID={username};Password={password}";
                    MessageBox.Show("Connected SQLServer Authentication");
                }
                else
                {
                    MessageBox.Show("NotConnected SQLServer Authentication");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string servername = ServerName.Text;
            string username = UserName.Text;
            string password = Password.Text;

            UserName.ReadOnly = false;
            Password.ReadOnly = false;
            string Catalog = ConfigurationManager.AppSettings["CatalogName"].ToString();
            if (servername != "" && username != "" && password != "")
            {
                Cursor.Current = Cursors.WaitCursor;
                connectionString = $"Data Source={servername};User ID={username};Password={password};Initial Catalog={Catalog}";


                SqlConnection myConn = new SqlConnection(connectionString);
                try
                {
                    myConn.Open();
                    Cursor.Current = Cursors.Default;
                    button3.Enabled = true;
                    MessageBox.Show("Connection Successfully Established");
                }
                catch (Exception)
                {
                    myConn.Close();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Connection Failed! Please enter valid credentials");
                }

            }
            else
            {

                MessageBox.Show("Please enter valid credentials");
            }
        }


        //private int currentIndex = 0;

        //-------------Connecting to OPCDA Server and Insert Data-------------------------------------  
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //var test = connectionString;
                bl = new BusinessLayer();
                InitTagList();
                var endpoint = Endpoint.Text;
                Task.Factory.StartNew(() => ConnectToWinCCOpcServer(endpoint));

                bl.tbIlnsertLogData("Report Service is started ....");

                groupBox2.Enabled = true;
                timer1.Start();

                groupBox1.Enabled = false;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                bl.tbIlnsertLogData("Exception while starting the report service : " + ex.ToString());
            }
        }

        private void UpdateScadaConnection()
        {
            try
            {
                string check = this.ReadTag("RF_Line_CM_Item_ID");
                if (check == "-0")

                {
                    label8.Text = "Not Connected";
                    label8.ForeColor = Color.Red; // corrected ForeColor to Color
                    this.WinCCOPCServer = (Opc.Da.Server)null;

                    var endpoint = Endpoint.Text;
                    Task.Factory.StartNew(() => ConnectToWinCCOpcServer(endpoint));

                }
                else
                {
                    label8.Text = "Connected";
                    label8.ForeColor = Color.Green; // corrected ForeColor to Color

                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)"Check Scada Connection With timer Tick" + ex.ToString());

            }
        }

        private void UpdateDataBaseConnection()
        {
            SqlConnection myConn = new SqlConnection(connectionString);
            try
            {
                myConn.Open();
                label9.Text = "Connected";
                label9.ForeColor = Color.Green;
            }
            catch (Exception)
            {
                label9.Text = "Not Connected";
                label9.ForeColor = Color.Red;
            }
            try
            {
                DataSet ds = this.bl.LastLog();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Access the first table and first row's first column ;
                    label7.Text = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                WriteToFile("Getting last Record error " + ex.Message.ToString());
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            button3.Enabled = false;
            groupBox2.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateScadaConnection();

            UpdateDataBaseConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to stop data logging?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {


                if (result == DialogResult.Yes)
                {
                    timer1.Enabled = false;
                    timer1.Stop();
                    if (this.WinCCOPCServer != null)
                    {
                        if (this.WinCCOPCServer.IsConnected)
                            this.WinCCOPCServer.Disconnect();
                        this.WinCCOPCServer.Dispose();
                        this.COMFactory.Dispose();
                        this.bl.tbIlnsertLogData((object)"disposed all objects..");
                    }
                    this.bl.tbIlnsertLogData((object)"Ars Server 1 Applicationis stopped ....");


                    //Environment.Exit(Environment.ExitCode);
                    System.Windows.Forms.Application.Exit();
                }
            }
            catch (Exception)
            {
                this.bl.tbIlnsertLogData((object)"Ars Server 1 Application is exit");
                System.Windows.Forms.Application.Exit();
            }
        }

        public static void WriteToFile(string text)
        {
            string path = "E:\\Log\\ARS_Error.txt";
            // Check if the directory exists
            if (!Directory.Exists(path))
            {
                try
                {
                    // Create the directory
                    Directory.CreateDirectory(path);

                }
                catch (Exception)
                {
                    // Handle any errors (e.g., permission issues)

                }
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine($"{text} {"Print time=>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                    writer.Close();
                }
            }
        }
    }
}
