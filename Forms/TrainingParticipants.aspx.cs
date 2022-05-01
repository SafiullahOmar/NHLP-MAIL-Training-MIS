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
    public partial class TrainingParticipants : System.Web.UI.Page
    {
        [WebMethod]
        public static ParticipantInformation GetFormByID(string ParticipantID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@ParticipantID" };
            string[] v = { ParticipantID };
            DataTable dt = db.selectProcedureExecuter(p, v, "spPageTrainingParticipantByID");
            ParticipantInformation t = new ParticipantInformation();
            foreach (DataRow row in dt.Rows)
            {
                t.ParticipantID = row["ParticipantID"].ToString();
                t.Year = row["Year"].ToString();
                t.Component = row["Component"].ToString();
                t.TrainingID = row["TrainingID"].ToString();
                t.Name = row["Name"].ToString();
                t.FatherName = row["FatherName"].ToString();
                t.Gender = row["Gender"].ToString();
                t.ProvinceID = row["Province"].ToString();
                t.ProvinceName = row["ProvinceEngName"].ToString();
                t.DistrictID = row["District"].ToString();
                t.DistrictName = row["DistrictEngName"].ToString();
                t.OrganizationID = row["Organization"].ToString();
                t.OrganizationName = row["OrgName"].ToString();
                t.Position = row["Position"].ToString();
                t.MobileNo = row["MobileNo"].ToString();
                t.Email = row["Email"].ToString();
                t.Remarks = row["Remarks"].ToString();
            }
            return t;

        }
        [WebMethod]
        public static List<ParticipantInformation> GetFormDetail(int TrainingID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@TrainingID" };
            string[] v = { TrainingID.ToString() };
            DataTable dt = db.selectProcedureExecuter(p, v, "spPageTrainingParticipantLoad");
            List<ParticipantInformation> lst = new List<ParticipantInformation>();
            foreach (DataRow row in dt.Rows)
            {
                ParticipantInformation t = new ParticipantInformation();
                t.ParticipantID = row["ParticipantID"].ToString();
                t.TrainingID = row["TrainingID"].ToString();
                t.Name = row["Name"].ToString();
                t.FatherName = row["FatherName"].ToString();
                t.Gender = row["Gender"].ToString();
                t.ProvinceID = row["Province"].ToString();
                t.ProvinceName = row["ProvinceEngName"].ToString();
                t.DistrictID = row["District"].ToString();
                t.DistrictName = row["DistrictEngName"].ToString();
                t.OrganizationID = row["Organization"].ToString();
                t.OrganizationName = row["OrgName"].ToString();
                t.Position = row["Position"].ToString();
                t.MobileNo = row["MobileNo"].ToString();
                t.Email = row["Email"].ToString();
                t.Remarks = row["Remarks"].ToString();
                
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
        public static string Save(ParticipantInformation obj)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string[] p = { "@TrainingID", "@Name", "@FatherName", "@Gender", "@Province", "@District", "@Organization", "@Position", "@MobileNo", "@Email", "@Remarks", "@User", "@Date", "@Error" };
            string[] v = { obj.TrainingID, obj.Name, obj.FatherName, obj.Gender, obj.ProvinceID, obj.DistrictID, obj.OrganizationID, obj.Position, obj.MobileNo, obj.Email, obj.Remarks, currentUser.ProviderUserKey.ToString(), DateTime.Now.ToString() };
            string r = db.DML_SP_Executer_string(p, v, "spPageTrainingParticipantSave");
            return r;
        }
        [WebMethod]
        public static string Update(ParticipantInformation obj)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string[] p = { "@ParticipantID", "@TrainingID", "@Name", "@FatherName", "@Gender", "@Province", "@District", "@Organization", "@Position", "@MobileNo", "@Email", "@Remarks", "@User", "@Date", "@Error" };
            string[] v = { obj.ParticipantID, obj.TrainingID, obj.Name, obj.FatherName, obj.Gender, obj.ProvinceID, obj.DistrictID, obj.OrganizationID, obj.Position, obj.MobileNo, obj.Email, obj.Remarks, currentUser.ProviderUserKey.ToString(), DateTime.Now.ToString() };
            string r = db.DML_SP_Executer_string(p, v, "spPageTrainingParticipantUpdate");
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
        public static List<ListItem> GetTraining(int YearID, int ComponentID)
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
        [WebMethod]
        public static List<ListItem> GetOrganization()
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            DataTable dt = db.selectProcedureExecuter("SharedGetOrganization");
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

        public class ParticipantInformation
        {
            public string ParticipantID { get; set; }
            public string Year { get; set; }
            public string Component { get; set; }
            public string TrainingID { get; set; }
            public string Name { get; set; }
            public string FatherName { get; set; }
            public string Gender { get; set; }
            public string ProvinceID { get; set; }
            public string ProvinceName { get; set; }
            public string DistrictID { get; set; }
            public string DistrictName { get; set; }
            public string OrganizationID { get; set; }
            public string OrganizationName { get; set; }
            public string Position { get; set; }
            public string MobileNo { get; set; }
            public string Email { get; set; }
            public string Remarks { get; set; }
            public bool Edit { get; set; }
        }
    }
}