using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Script.Services;
using System.Data;
using System.Web.Services;

namespace TMIS
{
    public partial class DashBoard : System.Web.UI.Page
    {
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static InfoByActivity[] GetParticipantByRegion(int YearID)
        {
            StringBuilder sb = new StringBuilder();
            DatabaseCommunicator db = new DatabaseCommunicator();
            DataTable dt = new DataTable();
            string[] p = { "@YearID" };
            string[] v = { YearID.ToString() };
            dt = db.selectProcedureExecuter(p, v, "spPageDashboardParticipantRegion");
            List<InfoByActivity> lst = new List<InfoByActivity>();
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new InfoByActivity()
                {
                    Title = row[0].ToString(),
                    sample1 = double.Parse(row[1].ToString()),
                    sample2 = double.Parse(row[2].ToString())

                });
            }
            return lst.ToArray();
        }
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static InfoByActivity[] GetParticipantByComponent(int YearID)
        {
            StringBuilder sb = new StringBuilder();
            DatabaseCommunicator db = new DatabaseCommunicator();
            DataTable dt = new DataTable();
            string[] p = { "@YearID" };
            string[] v = { YearID.ToString() };
            dt = db.selectProcedureExecuter(p, v, "spPageDashboardParticipantComponent");
            List<InfoByActivity> lst = new List<InfoByActivity>();
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new InfoByActivity()
                {
                    Title = row[0].ToString(),
                    sample1 = double.Parse(row[1].ToString()),
                    sample2 = double.Parse(row[2].ToString())

                });
            }
            return lst.ToArray();
        }
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static InfoByActivity[] GetParticipantByProvince(int YearID)
        {
            StringBuilder sb = new StringBuilder();
            DatabaseCommunicator db = new DatabaseCommunicator();
            DataTable dt = new DataTable();
            string[] p = { "@YearID" };
            string[] v = { YearID.ToString() };
            dt = db.selectProcedureExecuter(p, v, "spPageDashboardParticipantProvince");
            List<InfoByActivity> lst = new List<InfoByActivity>();
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new InfoByActivity()
                {
                    Title = row[0].ToString(),
                    sample1 = double.Parse(row[1].ToString()),
                    sample2 = double.Parse(row[2].ToString())
                    
                });
            }
            return lst.ToArray();
        }
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static InfoByActivity[] GetTrainingByProvince(int YearID)
        {
            StringBuilder sb = new StringBuilder();
            DatabaseCommunicator db = new DatabaseCommunicator();
            DataTable dt = new DataTable();
            string[] p = { "@YearID" };
            string[] v = { YearID.ToString()};
            dt = db.selectProcedureExecuter(p,v,"spPageDashboardTrainingProvince");
            List<InfoByActivity> lst = new List<InfoByActivity>();
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new InfoByActivity()
                {
                    Title = row[0].ToString(),
                    sample1 = double.Parse(row[1].ToString()),
                    sample2 = double.Parse(row[2].ToString()),
                    sample3 = double.Parse(row[3].ToString()),
                    sample4 = double.Parse(row[4].ToString()),
                    sample5 = double.Parse(row[5].ToString())

                });
            }
            return lst.ToArray();
        }
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static InfoByActivity[] GetTrainingByRegion(int YearID)
        {
            StringBuilder sb = new StringBuilder();
            DatabaseCommunicator db = new DatabaseCommunicator();
            DataTable dt = new DataTable();
            string[] p = { "@YearID" };
            string[] v = { YearID.ToString() };
            dt = db.selectProcedureExecuter(p, v, "spPageDashboardTrainingRegion");
            List<InfoByActivity> lst = new List<InfoByActivity>();
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new InfoByActivity()
                {
                    Title = row[0].ToString(),
                    sample1 = double.Parse(row[1].ToString()),
                    sample2 = double.Parse(row[2].ToString()),
                    sample3 = double.Parse(row[3].ToString()),
                    sample4 = double.Parse(row[4].ToString()),
                    sample5 = double.Parse(row[5].ToString())

                });
            }
            return lst.ToArray();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static List<ListItem> GetYear()
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            DataTable dt = db.selectProcedureExecuter("SharedGetYear");
            List<ListItem> LstData = new List<ListItem>();
            foreach (DataRow row in dt.Rows)
            {
                LstData.Add(new ListItem
                {
                    Value = row["YearID"].ToString(),
                    Text = row["Year"].ToString()
                });
            }
            return LstData;

        }
        public class InfoByActivity
        {
            public double count { get; set; }
            public string Title { get; set; }
            public double sample1 { get; set; }
            public double sample2 { get; set; }
            public double sample3 { get; set; }
            public double sample4 { get; set; }
            public double sample5 { get; set; }
            
        }
    }
}