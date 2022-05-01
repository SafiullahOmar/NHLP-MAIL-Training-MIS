using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMIS.GeneralClasses;
using System.Web.Security;

namespace TMIS.Forms
{
    public partial class TrainingInformation : System.Web.UI.Page
    {
        [WebMethod]
        public static TrainingMaster GetFormByID(string TrainingID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@TrainingID" };
            string[] v = { TrainingID };
            DataTable dt = db.selectProcedureExecuter(p, v, "spPageTrainingInformationByID");
            TrainingMaster t = new TrainingMaster();
            foreach (DataRow row in dt.Rows)
            {
                t.TrainingID = row["TrainingID"].ToString();
                t.TrainingTitle = row["TrainingTitle"].ToString();
                t.Year = row["Year"].ToString();
                t.Component = row["Component"].ToString();
                t.ComponentName = row["ComponentName"].ToString();
                t.SubComponent = row["SubComponent"].ToString();
                t.ProvinceID = row["Province"].ToString();
                t.ProvinceName = row["ProvinceEngName"].ToString();
                t.DistrictID = row["District"].ToString();
                t.DistrictName = row["DistrictEngName"].ToString();
                t.TrainingLocation = row["TrainingLocation"].ToString();
                t.StartDate = DateTime.Parse(row["StartDate"].ToString());
                t.EndDate = DateTime.Parse(row["EndDate"].ToString());
                t.Trainer = row["Trainer"].ToString();
                t.Remarks = row["Remarks"].ToString();
            }
            return t;

        }
        [WebMethod]
        public static List<TrainingMaster> GetFormDetail(int YearID,int ComponentID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@YearID", "@ComponentID" };
            string[] v = { YearID.ToString(),ComponentID.ToString()};
            DataTable dt = db.selectProcedureExecuter(p, v, "spPageTrainingInformationLoad");
            List<TrainingMaster> lst = new List<TrainingMaster>();
            foreach (DataRow row in dt.Rows)
            {
                TrainingMaster t = new TrainingMaster();
                t.TrainingID = row["TrainingID"].ToString();
                t.TrainingTitle = row["TrainingTitle"].ToString();
                t.Year = row["Year"].ToString();
                t.Component = row["Component"].ToString();
                t.ComponentName = row["ComponentName"].ToString();
                t.SubComponent = row["SubComponent"].ToString();
                t.ProvinceID = row["Province"].ToString();
                t.ProvinceName = row["ProvinceEngName"].ToString();
                t.DistrictID = row["District"].ToString();
                t.DistrictName = row["DistrictEngName"].ToString();
                t.TrainingLocation = row["TrainingLocation"].ToString();
                t.StartDate = DateTime.Parse(row["StartDate"].ToString());
                t.EndDate = DateTime.Parse(row["EndDate"].ToString());
                t.Trainer = row["Trainer"].ToString();
                t.Remarks = row["Remarks"].ToString();
                t.Uploaded = row["Uploaded"].ToString();
                
                var roles = new List<string> { "Admin", "Super User" };
                var userRoles = Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name);

                if (userRoles.Any(u => roles.Contains(u)))
                {
                    t.Edit = true;
                }
                else
                    t.Edit = false;
                lst.Add(t);
            }
            return lst;

        }
        [WebMethod]
        public static string Save(TrainingMaster obj)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string[] p = { "@TrainingTitle", "@Year", "@Component", "@SubComponent", "@Province", "@District", "@TrainingLocation", "@StartDate", "@EndDate", "@Trainer", "@Remarks", "@User", "@Date", "@Error" }; 
            string[] v = { obj.TrainingTitle, obj.Year, obj.Component, obj.SubComponent, obj.ProvinceID, obj.DistrictID, obj.TrainingLocation, obj.StartDate.ToShortDateString(),obj.EndDate.ToShortDateString(),obj.Trainer,obj.Remarks, currentUser.ProviderUserKey.ToString(), DateTime.Now.ToString() };
            string r = db.DML_SP_Executer_string(p, v, "spPageTrainingInformationSave");
            return r;
        }
        [WebMethod]
        public static string Update(TrainingMaster obj)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string[] p = { "@TrainingID", "@TrainingTitle", "@Year", "@Component", "@SubComponent", "@Province", "@District", "@TrainingLocation", "@StartDate", "@EndDate", "@Trainer", "@Remarks", "@User", "@Date", "@Error" };
            string[] v = {obj.TrainingID, obj.TrainingTitle, obj.Year, obj.Component, obj.SubComponent, obj.ProvinceID, obj.DistrictID, obj.TrainingLocation, obj.StartDate.ToShortDateString(), obj.EndDate.ToShortDateString(), obj.Trainer, obj.Remarks, currentUser.ProviderUserKey.ToString(), DateTime.Now.ToString() };
            string r = db.DML_SP_Executer_string(p, v, "spPageTrainingInformationUpdate");
            return r;
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
        [WebMethod]
        public static List<ListItem> GetComponent()
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            DataTable dt = UserInfo.GetUserComponents();
            List<ListItem> LstData = new List<ListItem>();
            foreach (DataRow row in dt.Rows)
            {
                LstData.Add(new ListItem
                {
                    Value = row[0].ToString(),
                    Text = row[1].ToString()
                });
            }
            return LstData;

        }
        [WebMethod]
        public static List<ListItem> GetSubComponent(int ComponentID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@ComponentID" };
            string[] v = { ComponentID.ToString() };
            DataTable dt = db.selectProcedureExecuter(p, v, "SharedGetSubComponent");
            List<ListItem> LstData = new List<ListItem>();
            foreach (DataRow row in dt.Rows)
            {
                LstData.Add(new ListItem
                {
                    Value = row[0].ToString(),
                    Text = row[1].ToString()
                });
            }
            return LstData;

        }
        [WebMethod]
        public static List<ListItem> GetProvince()
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            DataTable dt = UserInfo.GetUserProvinces();
            List<ListItem> LstData = new List<ListItem>();
            foreach (DataRow row in dt.Rows)
            {
                LstData.Add(new ListItem
                {
                    Value = row[0].ToString(),
                    Text = row[1].ToString()
                });
            }
            return LstData;

        }
        [WebMethod]
        public static List<ListItem> GetDistrict(int ProvinceID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@ProvinceId" };
            string[] v = { ProvinceID.ToString() };
            DataTable dt = db.selectProcedureExecuter(p, v, "SharedGetDistrict");
            List<ListItem> LstData = new List<ListItem>();
            foreach (DataRow row in dt.Rows)
            {
                LstData.Add(new ListItem
                {
                    Value = row["DistrictID"].ToString(),
                    Text = row["DistrictEngName"].ToString()
                });
            }
            return LstData;

        }

        public class TrainingMaster
        {
            public string TrainingID { get; set; }
            public string TrainingTitle { get; set; }
            public string Year { get; set; }
            public string Component { get; set; }
            public string ComponentName { get; set; }
            public string SubComponent { get; set; }
            public string ProvinceID { get; set; }
            public string ProvinceName { get; set; }
            public string DistrictID { get; set; }
            public string DistrictName { get; set; }
            public string TrainingLocation { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Trainer { get; set; }
            public string Remarks { get; set; }
            public string Uploaded { get; set; }
            public bool Edit { get; set; }
        }
    }
}