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
using System.Runtime.InteropServices;
using System.Collections;

namespace ArsServer2
{
    public partial class Form1 : Form
    {
        public static string Connectionstring = "";
        private const int CP_NOCLOSE_BUTTON = 0x200;
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
        private List<string> lst_BSLH_Tags;
        private List<string> lst_BSRH_Tags;
        private List<string> lst_FE_Tags;
        private List<string> lst_FF_Tags;
        private List<string> lst_RF_Tags;
        private List<string> lst_UB_Tags;
        private List<string> lst_OH_Tags;
        private List<string> lst_RP_Tags;
        private List<string> lst_MFL_Tags;
        private List<string> lst_MF_Tags;
        private List<string> lst_OT_Tags;
        private List<string> lst_FDRDRH_Tags;
        private List<string> lst_FDRDLH_Tags;
        private List<string> lst_TG_Tags;
        private List<string> lst_FN_Tags;
        private List<string> lst_HO_Tags;
        private List<string> lst_RO_Tags;
        private ArrayList TagList1;
        private ArrayList TagList2;
        private ArrayList TagList3;
        private ArrayList TagList4;
        private ArrayList TagList5;
        private List<Item> OPCItemsList_BSLH = new List<Item>();
        private List<Item> OPCItemsList_BSRH = new List<Item>();
        private List<Item> OPCItemsList_FE = new List<Item>();
        private List<Item> OPCItemsList_FF = new List<Item>();
        private List<Item> OPCItemsList_RF = new List<Item>();
        private List<Item> OPCItemsList_UB = new List<Item>();
        private List<Item> OPCItemsList_OH = new List<Item>();
        private List<Item> OPCItemsList_RP = new List<Item>();
        private List<Item> OPCItemsList_MFL = new List<Item>();
        private List<Item> OPCItemsList_MF = new List<Item>();
        private List<Item> OPCItemsList_OT = new List<Item>();
        private List<Item> OPCItemsList_FDRDRH = new List<Item>();
        private List<Item> OPCItemsList_FDRDLH = new List<Item>();
        private List<Item> OPCItemsList_TG = new List<Item>();
        private List<Item> OPCItemsList_FN = new List<Item>();
        private List<Item> OPCItemsList_HO = new List<Item>();
        private List<Item> OPCItemsList_RO = new List<Item>();
        private List<Item> OPCItemsList6 = new List<Item>();
        private Opc.Da.Server WinCCOPCServer;
        private OpcCom.Factory COMFactory = new OpcCom.Factory();
        private Subscription ReadGroupSubscription1;
        private Subscription ReadGroupSubscription6;
        private Subscription ReadGroupSubscription_BSLH;
        private Subscription ReadGroupSubscription_BSRH;
        private Subscription ReadGroupSubscription_FE;
        private Subscription ReadGroupSubscription_FF;
        private Subscription ReadGroupSubscription_RF;
        private Subscription ReadGroupSubscription_UB;
        private Subscription ReadGroupSubscription_OH;
        private Subscription ReadGroupSubscription_RP;
        private Subscription ReadGroupSubscription_MFL;
        private Subscription ReadGroupSubscription_MF;
        private Subscription ReadGroupSubscription_OT;
        private Subscription ReadGroupSubscription_FDRDRH;
        private Subscription ReadGroupSubscription_FDRDLH;
        private Subscription ReadGroupSubscription_TG;
        private Subscription ReadGroupSubscription_FN;
        private Subscription ReadGroupSubscription_HO;
        private Subscription ReadGroupSubscription_RO;
        private SubscriptionState ReadGroupSubscriptionState1;
        private SubscriptionState ReadGroupSubscriptionState6;
        private SubscriptionState ReadGroupSubscriptionState_BSLH;
        private SubscriptionState ReadGroupSubscriptionState_BSRH;
        private SubscriptionState ReadGroupSubscriptionState_FE;
        private SubscriptionState ReadGroupSubscriptionState_FF;
        private SubscriptionState ReadGroupSubscriptionState_RF;
        private SubscriptionState ReadGroupSubscriptionState_UB;
        private SubscriptionState ReadGroupSubscriptionState_OH;
        private SubscriptionState ReadGroupSubscriptionState_RP;
        private SubscriptionState ReadGroupSubscriptionState_MFL;
        private SubscriptionState ReadGroupSubscriptionState_MF;
        private SubscriptionState ReadGroupSubscriptionState_OT;
        private SubscriptionState ReadGroupSubscriptionState_FDRDRH;
        private SubscriptionState ReadGroupSubscriptionState_FDRDLH;
        private SubscriptionState ReadGroupSubscriptionState_TG;
        private SubscriptionState ReadGroupSubscriptionState_FN;
        private SubscriptionState ReadGroupSubscriptionState_HO;
        private SubscriptionState ReadGroupSubscriptionState_RO;
        private Subscription WriteGroupSubscription;
        private SubscriptionState WriteGroupSubscriptionState;
        private BusinessLayer bl = (BusinessLayer)null;
        private System.Timers.Timer tmrDelay;
        // private IContainer components = (IContainer)null;
        #endregion

        // private OPC_UA_CLIENT opcUaClient;
        public static string connectionString;
        // private string endpointurl;


        private void InitTagList()
        {
            try
            {
                DataSet plcTags = this.bl.GetPLC_Tags();
                this.lst_BSLH_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "BSLH")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_BSRH_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "BSRH")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_FE_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "FE")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_FF_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "FF")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_RF_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "RF")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_UB_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "UB")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_OH_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "OH")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_RP_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "RP")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_MFL_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "MFL")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_MF_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "MF")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_OT_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "OT")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_FDRDRH_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "FDRDRH")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_FDRDLH_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "FDRDLH")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_TG_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "TG")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_FN_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "FN")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_HO_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "HO")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();
                this.lst_RO_Tags = plcTags.Tables[0].AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>)(p => p.Field<string>("LineName") == "RO")).Select<DataRow, string>((System.Func<DataRow, string>)(p => p.Field<string>("TagName"))).Distinct<string>().ToList<string>();

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

                ////BSLH
                //this.ReadGroupSubscriptionState_BSLH = new SubscriptionState()
                //{
                //    Name = "BSLH",
                //    Active = true
                //};
                //this.ReadGroupSubscription_BSLH = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_BSLH);
                //this.ReadGroupSubscriptionState_BSLH.UpdateRate = 200;
                //this.ReadGroupSubscription_BSLH.DataChanged += new DataChangedEventHandler(this.BSLH_DataChanged);
                //foreach (string lstBslhTag in this.lst_BSLH_Tags)
                //{
                //    Item obj = new Item();
                //    obj.ItemName = lstBslhTag;
                //    this.OPCItemsList_BSLH.Add(obj);
                //}
                //this.ReadGroupSubscription_BSLH.AddItems(this.OPCItemsList_BSLH.ToArray());

                //BSRH
                this.ReadGroupSubscriptionState_BSRH = new SubscriptionState()
                {
                    Name = "BSRH",
                    Active = true
                };
                this.ReadGroupSubscription_BSRH = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_BSRH);
                this.ReadGroupSubscriptionState_BSRH.UpdateRate = 200;
                this.ReadGroupSubscription_BSRH.DataChanged += new DataChangedEventHandler(this.BSRH_DataChanged);
                foreach (string lstBsrhTag in this.lst_BSRH_Tags)
                {
                    Item obj = new Item();
                    obj.ItemName = lstBsrhTag;
                    this.OPCItemsList_BSRH.Add(obj);
                }
                this.ReadGroupSubscription_BSRH.AddItems(this.OPCItemsList_BSRH.ToArray());

            //    //FE
            //    this.ReadGroupSubscriptionState_FE = new SubscriptionState()
            //    {
            //        Name = "FE",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_FE = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_FE);
            //    this.ReadGroupSubscriptionState_FE.UpdateRate = 200;
            //    this.ReadGroupSubscription_FE.DataChanged += new DataChangedEventHandler(this.FE_DataChanged);
            //    foreach (string lstFeTag in this.lst_FE_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstFeTag;
            //        this.OPCItemsList_FE.Add(obj);
            //    }
            //    this.ReadGroupSubscription_FE.AddItems(this.OPCItemsList_FE.ToArray());

            //    //FE
            //    this.ReadGroupSubscriptionState_FF = new SubscriptionState()
            //    {
            //        Name = "FF",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_FF = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_FF);
            //    this.ReadGroupSubscriptionState_FF.UpdateRate = 200;
            //    this.ReadGroupSubscription_FF.DataChanged += new DataChangedEventHandler(this.FF_DataChanged);
            //    foreach (string lstFfTag in this.lst_FF_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstFfTag;
            //        this.OPCItemsList_FF.Add(obj);
            //    }
            //    this.ReadGroupSubscription_FF.AddItems(this.OPCItemsList_FF.ToArray());

            //    //RF
            //    this.ReadGroupSubscriptionState_RF = new SubscriptionState()
            //    {
            //        Name = "RF",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_RF = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_RF);
            //    this.ReadGroupSubscriptionState_RF.UpdateRate = 200;
            //    this.ReadGroupSubscription_RF.DataChanged += new DataChangedEventHandler(this.RF_DataChanged);
            //    foreach (string lstRfTag in this.lst_RF_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstRfTag;
            //        this.OPCItemsList_RF.Add(obj);
            //    }
            //    this.ReadGroupSubscription_RF.AddItems(this.OPCItemsList_RF.ToArray());

            //    //UB
            //    this.ReadGroupSubscriptionState_UB = new SubscriptionState()
            //    {
            //        Name = "UB",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_UB = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_UB);
            //    this.ReadGroupSubscriptionState_UB.UpdateRate = 200;
            //    this.ReadGroupSubscription_UB.DataChanged += new DataChangedEventHandler(this.UB_DataChanged);
            //    foreach (string lstUbTag in this.lst_UB_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstUbTag;
            //        this.OPCItemsList_UB.Add(obj);
            //    }
            //    this.ReadGroupSubscription_UB.AddItems(this.OPCItemsList_UB.ToArray());

            //    //OH
            //    this.ReadGroupSubscriptionState_OH = new SubscriptionState()
            //    {
            //        Name = "OH",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_OH = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_OH);
            //    this.ReadGroupSubscriptionState_OH.UpdateRate = 200;
            //    this.ReadGroupSubscription_OH.DataChanged += new DataChangedEventHandler(this.OH_DataChanged);
            //    foreach (string lstOhTag in this.lst_OH_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstOhTag;
            //        this.OPCItemsList_OH.Add(obj);
            //    }
            //    this.ReadGroupSubscription_OH.AddItems(this.OPCItemsList_OH.ToArray());

            //    //RP
            //    this.ReadGroupSubscriptionState_RP = new SubscriptionState()
            //    {
            //        Name = "RP",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_RP = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_RP);
            //    this.ReadGroupSubscriptionState_RP.UpdateRate = 200;
            //    this.ReadGroupSubscription_RP.DataChanged += new DataChangedEventHandler(this.RP_DataChanged);
            //    foreach (string lstRpTag in this.lst_RP_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstRpTag;
            //        this.OPCItemsList_RP.Add(obj);
            //    }
            //    this.ReadGroupSubscription_RP.AddItems(this.OPCItemsList_RP.ToArray());

            //    //MF
            //    this.ReadGroupSubscriptionState_MF = new SubscriptionState()
            //    {
            //        Name = "MF",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_MF = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_MF);
            //    this.ReadGroupSubscriptionState_MF.UpdateRate = 200;
            //    this.ReadGroupSubscription_MF.DataChanged += new DataChangedEventHandler(this.MF_DataChanged);
            //    foreach (string lstMfTag in this.lst_MF_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstMfTag;
            //        this.OPCItemsList_MF.Add(obj);
            //    }
            //    this.ReadGroupSubscription_MF.AddItems(this.OPCItemsList_MF.ToArray());

            //    //OT
            //    this.ReadGroupSubscriptionState_OT = new SubscriptionState()
            //    {
            //        Name = "OT",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_OT = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_OT);
            //    this.ReadGroupSubscriptionState_OT.UpdateRate = 200;
            //    this.ReadGroupSubscription_OT.DataChanged += new DataChangedEventHandler(this.OT_DataChanged);
            //    foreach (string lstOtTag in this.lst_OT_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstOtTag;
            //        this.OPCItemsList_OT.Add(obj);
            //    }
            //    this.ReadGroupSubscription_OT.AddItems(this.OPCItemsList_OT.ToArray());

            //    //MFL
            //    this.ReadGroupSubscriptionState_MFL = new SubscriptionState()
            //    {
            //        Name = "MFL",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_MFL = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_MFL);
            //    this.ReadGroupSubscriptionState_MFL.UpdateRate = 200;
            //    this.ReadGroupSubscription_MFL.DataChanged += new DataChangedEventHandler(this.MFL_DataChanged);
            //    foreach (string lstMflTag in this.lst_MFL_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstMflTag;
            //        this.OPCItemsList_MFL.Add(obj);
            //    }
            //    this.ReadGroupSubscription_MFL.AddItems(this.OPCItemsList_MFL.ToArray());


            //    //FDRDRH
            //    this.ReadGroupSubscriptionState_FDRDRH = new SubscriptionState()
            //    {
            //        Name = "FDRDRH",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_FDRDRH = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_FDRDRH);
            //    this.ReadGroupSubscriptionState_FDRDRH.UpdateRate = 200;
            //    this.ReadGroupSubscription_FDRDRH.DataChanged += new DataChangedEventHandler(this.FDRDRH_DataChanged);
            //    foreach (string lstFdrdrhTag in this.lst_FDRDRH_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstFdrdrhTag;
            //        this.OPCItemsList_FDRDRH.Add(obj);
            //    }
            //    this.ReadGroupSubscription_FDRDRH.AddItems(this.OPCItemsList_FDRDRH.ToArray());


            //    //FDRDLH
            //    this.ReadGroupSubscriptionState_FDRDLH = new SubscriptionState()
            //    {
            //        Name = "FDRDLH",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_FDRDLH = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_FDRDLH);
            //    this.ReadGroupSubscriptionState_FDRDLH.UpdateRate = 200;
            //    this.ReadGroupSubscription_FDRDLH.DataChanged += new DataChangedEventHandler(this.FDRDLH_DataChanged);
            //    foreach (string lstFdrdlhTag in this.lst_FDRDLH_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstFdrdlhTag;
            //        this.OPCItemsList_FDRDLH.Add(obj);
            //    }
            //    this.ReadGroupSubscription_FDRDLH.AddItems(this.OPCItemsList_FDRDLH.ToArray());

            //    //TG
            //    this.ReadGroupSubscriptionState_TG = new SubscriptionState()
            //    {
            //        Name = "TG",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_TG = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_TG);
            //    this.ReadGroupSubscriptionState_TG.UpdateRate = 200;
            //    this.ReadGroupSubscription_TG.DataChanged += new DataChangedEventHandler(this.TG_DataChanged);
            //    foreach (string lstTgTag in this.lst_TG_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstTgTag;
            //        this.OPCItemsList_TG.Add(obj);
            //    }
            //    this.ReadGroupSubscription_TG.AddItems(this.OPCItemsList_TG.ToArray());

            //    //FN
            //    this.ReadGroupSubscriptionState_FN = new SubscriptionState()
            //    {
            //        Name = "FN",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_FN = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_FN);
            //    this.ReadGroupSubscriptionState_FN.UpdateRate = 200;
            //    this.ReadGroupSubscription_FN.DataChanged += new DataChangedEventHandler(this.FN_DataChanged);
            //    foreach (string lstFnTag in this.lst_FN_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstFnTag;
            //        this.OPCItemsList_FN.Add(obj);
            //    }
            //    this.ReadGroupSubscription_FN.AddItems(this.OPCItemsList_FN.ToArray());

            //    //HO
            //    this.ReadGroupSubscriptionState_HO = new SubscriptionState()
            //    {
            //        Name = "HO",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_HO = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_HO);
            //    this.ReadGroupSubscriptionState_HO.UpdateRate = 200;
            //    this.ReadGroupSubscription_HO.DataChanged += new DataChangedEventHandler(this.HO_DataChanged);
            //    foreach (string lstHoTag in this.lst_HO_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstHoTag;
            //        this.OPCItemsList_HO.Add(obj);
            //    }
            //    this.ReadGroupSubscription_HO.AddItems(this.OPCItemsList_HO.ToArray());

            //    //RO
            //    this.ReadGroupSubscriptionState_RO = new SubscriptionState()
            //    {
            //        Name = "RO",
            //        Active = true
            //    };
            //    this.ReadGroupSubscription_RO = (Subscription)this.WinCCOPCServer.CreateSubscription(this.ReadGroupSubscriptionState_RO);
            //    this.ReadGroupSubscriptionState_RO.UpdateRate = 200;
            //    this.ReadGroupSubscription_RO.DataChanged += new DataChangedEventHandler(this.RO_DataChanged);
            //    foreach (string lstRoTag in this.lst_RO_Tags)
            //    {
            //        Item obj = new Item();
            //        obj.ItemName = lstRoTag;
            //        this.OPCItemsList_RO.Add(obj);
            //    }
            //    this.ReadGroupSubscription_RO.AddItems(this.OPCItemsList_RO.ToArray());


            //    this.WriteGroupSubscriptionState = new SubscriptionState();
            //    this.WriteGroupSubscriptionState.Name = "Write Group";
            //    this.WriteGroupSubscriptionState.Active = false;
            //    this.WriteGroupSubscription = (Subscription)this.WinCCOPCServer.CreateSubscription(this.WriteGroupSubscriptionState);
            //    this.bl.tbIlnsertLogData((object)"Wincc is connected properly  ....");
            //
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)ex.ToString());
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
            // button3.Visible = true;
            button3.Enabled = false;
            groupBox2.Enabled = false;
            checkBox1.Visible = false;
            button4_Click(null, null);
            //label6.Text = "";
            //label8.Text = "";

        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string servername = ServerName.Text;
            string username = UserName.Text;
            string password = Password.Text;
            if (checkBox1.Checked)
            {
                UserName.ReadOnly = true;
                Password.ReadOnly = true;
                button4.Hide();
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

            if (servername != "" && username != "" && password != "")
            {
                Cursor.Current = Cursors.WaitCursor;

                connectionString = $"Data Source={servername};User ID={username};Password={password};Initial Catalog=TATA_MOTERS_Q5";

                SqlConnection myConn = new SqlConnection(connectionString);
                try
                {
                    myConn.Open();
                    Cursor.Current = Cursors.Default;
                   // Connectionstring = connectionString;
                    button3.Enabled = true;
                    // MessageBox.Show("Connection Successfully Established");
                    button3_Click(null,null);
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


        //-------------Connecting to OPCDA Server and Insert Data-------------------------------------  
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;
               // var test = connectionString;
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

        private void RO_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
                
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_RO((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_RO((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in RO : " + ex.ToString()));
            }
        }

        private void HO_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
               
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_HO((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_HO((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in HO : " + ex.ToString()));
            }
        }

        private void FN_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_FN((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_FN((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in FN : " + ex.ToString()));
            }
        }

        private void TG_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
               
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_TG((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_TG((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in TG : " + ex.ToString()));
            }
        }

        private void MFL_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_MFL((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_MFL((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in MFL : " + ex.ToString()));
            }
        }

        private void OT_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
               
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_OT((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_OT((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in OT : " + ex.ToString()));
            }
        }

        private void MF_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                    {
                        this.bl.UpdateAlarms_MF((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                        this.bl.tbIlnsertLogData((object)("MF_Update_Triggered==TagName : " + itemValueResult.ItemName + " ==SCADA Time:" + (object)itemValueResult.Timestamp));
                    }
                    else
                    {
                        this.bl.InsertNormalAlarams_MF((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                        this.bl.tbIlnsertLogData((object)("MF_Insert_Triggered==TagName : " + itemValueResult.ItemName + " ==SCADA Time:" + (object)itemValueResult.Timestamp));
                    }
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in MF : " + ex.ToString()));
            }
        }

        private void RP_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
                
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_RP((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_RP((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in RP : " + ex.ToString()));
            }
        }

        private void OH_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
               
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_OH((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_OH((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in OH : " + ex.ToString()));
            }
        }

        private void UB_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
               
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_UB((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_UB((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in UB : " + ex.ToString()));
            }
        }

        private void RF_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_RF((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_RF((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in RF : " + ex.ToString()));
            }
        }

        private void FF_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_FF((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_FF((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in FF : " + ex.ToString()));
            }
        }

        private void FE_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_FE((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_FE((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in FE : " + ex.ToString()));
            }
        }

        private void FDRDLH_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
               
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_FDRDLH((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_FDRDLH((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in FDRDLH_DataChanged : " + ex.ToString()));
            }
        }

        private void FDRDRH_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
                
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_FDRDRH((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_FDRDRH((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in FDRDRH_DataChanged : " + ex.ToString()));
            }
        }

        private void BSRH_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
               
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_BSRH((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_BSRH((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in BSRH_DataChanged : " + ex.ToString()));
            }
        }

        private void BSLH_DataChanged(
          object subscriptionHandle,
          object requestHandle,
          ItemValueResult[] values)
        {
            try
            {
               
                foreach (ItemValueResult itemValueResult in values)
                {
                    string[] strArray = itemValueResult.ItemName.ToString().Split('_');
                    string LineName = strArray[0];
                    string StationName = strArray[1];
                    string AlarmType = strArray[3];
                    string Model = strArray[4];
                    string Reason = strArray[5];
                    string itemName = itemValueResult.ItemName;
                    if (itemValueResult.Value.ToString() == "False" || itemValueResult.Value.ToString() == "0")
                        this.bl.UpdateAlarms_BSLH((object)itemValueResult.ItemName, LineName, (object)itemValueResult.Timestamp);
                    else
                        this.bl.InsertNormalAlarams_BSLH((object)itemValueResult.ItemName, (object)itemValueResult.Value.ToString(), (object)AlarmType, (object)LineName, (object)StationName, (object)Model, (object)itemName, (object)Reason, (object)itemValueResult.Timestamp);
                }
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("Error in BSLH_DataChanged : " + ex.ToString()));
            }
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
            catch (Exception)
            {
                this.bl.tbIlnsertLogData((object)("Missed TagName : " + TagName)); 

                return "-0";
            }
        }

        public object[] ReadTag_Mannual(string TagName)
        {
            object[] objArray = new object[2];
            try
            {
                Item[] items = new Item[1] { new Item() };
                items[0].ItemName = TagName;
                ItemValueResult[] itemValueResultArray = this.WinCCOPCServer.Read(items);
                string str = itemValueResultArray[0].Value.ToString();
                DateTime timestamp = itemValueResultArray[0].Timestamp;
                objArray[0] = (object)str;
                objArray[1] = (object)timestamp;
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)("At ReadTag_Mannual : " + ex.Message));
            }
            return objArray;
        }

        //protected override void OnStop()
        //{
        //    try
        //    {
        //        this.tmrDelay.Stop();
        //        this.tmrDelay.Enabled = false;
        //        this.tmrDelay.Dispose();
        //        if (this.WinCCOPCServer != null)
        //        {
        //            if (this.WinCCOPCServer.IsConnected)
        //                this.WinCCOPCServer.Disconnect();
        //            this.WinCCOPCServer.Dispose();
        //            this.COMFactory.Dispose();
        //            this.bl.tbIlnsertLogData((object)"disposed all objects..");
        //        }
        //        this.bl.tbIlnsertLogData((object)"Alarms Service is stopped ....");
        //        Thread.Sleep(4000);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.bl.tbIlnsertLogData((object)("Service stop " + ex.ToString()));
        //    }
        //}

       

        private void Updatelabel9()
        {
            try
            {
                string check = this.ReadTag("BSRH_BS040RHPS01_PLC_ALARM_CM_DIAG-MF70[1]-BSRH-040-RBT01-IN-MANUAL-OR-SIM-MODE");
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

        private void UpdateLabel()
        {
            try
            {
                DataSet ds = this.bl.GetPlcInputData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Access the first table and first row's first column
                    label9.Text = ds.Tables[0].Rows[0][0].ToString();

                    // Update label to show connection status
                  
                    label6.Text = "Connected";
                    label6.ForeColor = Color.Green;
                }
                //using (SqlConnection con = new SqlConnection(connectionString))
                //{
                //    using (SqlCommand cmd = new SqlCommand("sp_LastLog_Alarms", con))
                //    {
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.CommandTimeout *= 4;
                //        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                //        {
                //            using (DataTable dt = new DataTable())
                //            {
                //                da.Fill(dt);
                //                if (dt.Rows.Count > 0)
                //                {
                //                    label9.Text = dt.Rows[0][0].ToString();

                //                }
                //                label6.Text = "Connected";
                //                label6.ForeColor = Color.Green;
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                label6.Text = "Not Connected";
                label6.ForeColor = Color.Red;

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to stop data logging?", "Question", MessageBoxButtons.YesNo);
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
                    this.bl.tbIlnsertLogData((object)"Ars Server 2 Application is stopped ....");


                    //Environment.Exit(Environment.ExitCode);
                    System.Windows.Forms.Application.Exit();
                }
            }
            catch (Exception)
            {
                this.bl.tbIlnsertLogData((object)"Ars Server 2 Application is exit");
                System.Windows.Forms.Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Updatelabel9();
                UpdateLabel();
            }
            catch (Exception ex)
            {
                this.bl.tbIlnsertLogData((object)"Timer Tick Error ...." + ex.ToString());
            }

        }
    }
}
