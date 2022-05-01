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
    public partial class TrainingDocuments : System.Web.UI.Page
    {
        [WebMethod]
        public static List<TrainingDocumentInformation> GetFormDetail(int TrainingID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@TrainingID" };
            string[] v = { TrainingID.ToString() };
            DataTable dt = db.selectProcedureExecuter(p, v, "spPageTrainingDocLoad");
            List<TrainingDocumentInformation> lst = new List<TrainingDocumentInformation>();
            foreach (DataRow row in dt.Rows)
            {
                TrainingDocumentInformation t = new TrainingDocumentInformation();
                t.ID = row["id"].ToString();
                t.TrainingID = row["TrainingID"].ToString();
                t.TrainingTitle = row["TrainingTitle"].ToString();
                t.Year = row["YearName"].ToString();
                t.ComponentName = row["ComponentName"].ToString();
                t.ProvinceName = row["ProvinceEngName"].ToString();
                t.DistrictName = row["DistrictEngName"].ToString();
                t.DocPath = row["Path"].ToString();
                t.Remarks = row["Remark"].ToString();
                t.InsertedBy = row["UserName"].ToString();
                t.InsertedDate = row["InsertedDate"].ToString();
                var roles = new List<string> { "Admin", "Super User" };
                var userRoles = Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name);

                if (userRoles.Any(u => roles.Contains(u)))
                {
                    t.Delete = true;
                }
                else
                    t.Delete = false;
                lst.Add(t);
            }
            return lst;

        }
        [WebMethod]
        public static string DeleteDoc(int DocID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@ID", "@Error" };
            string[] v = { DocID.ToString() };
            string r = db.DML_SP_Executer_string(p, v, "spPageTrainingDocDelete");
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
        public static List<ListItem> GetTraining(int YearID,int ComponentID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@YearID", "@ComponentID" };
            string[] v = { YearID.ToString(), ComponentID.ToString() };
            DataTable dt = db.selectProcedureExecuter(p, v, "SharedGetTraining");
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
        public class TrainingDocumentInformation
        {
            public string ID { get; set; }
            public string TrainingID { get; set; }
            public string TrainingTitle { get; set; }
            public string Year { get; set; }
            public string ComponentName { get; set; }
            public string ProvinceName { get; set; }
            public string DistrictName { get; set; }
            public string TrainingLocation { get; set; }
            public string DocPath { get; set; }
            public string Remarks { get; set; }
            public string InsertedBy { get; set; }
            public string InsertedDate { get; set; }
            public bool Delete { get; set; }
            public bool Download { get; set; }
        }
    }
}