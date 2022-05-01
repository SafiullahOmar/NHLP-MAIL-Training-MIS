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
    public partial class TrainingAttendance : System.Web.UI.Page
    {
        [WebMethod]
        public static List<AttendanceMaster> GetFormDetail(int TrainingID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@TrainingID" };
            string[] v = { TrainingID.ToString() };
            DataTable dt = db.selectProcedureExecuter(p, v, "spPageAttendanceLoad");
            List<AttendanceMaster> lst = new List<AttendanceMaster>();
            foreach (DataRow row in dt.Rows)
            {
                AttendanceMaster t = new AttendanceMaster();
                t.AttMstID = row["AttMstID"].ToString();
                t.TrainingID = row["TrainingID"].ToString();
                t.TrainingTitle = row["TrainingTitle"].ToString();
                t.YearName = row["YearName"].ToString();
                t.ComponentName = row["ComponentName"].ToString();
                t.Province = row["ProvinceEngName"].ToString();
                t.TrainingDay = row["TrainingDay"].ToString();
                
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
        public static List<ParticipantInformation> GetParticipantList(int TrainingID)
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

                lst.Add(t);
            }
            return lst;

        }
        [WebMethod]
        public static List<ParticipantInformation> GetParticipantListByMstID(int MstID,int TrainingID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@AttMstID", "@TrainingID" };
            string[] v = { MstID.ToString(), TrainingID.ToString() };
            DataTable dt = db.selectProcedureExecuter(p, v, "spPageAttendanceLoadByMstID");
            List<ParticipantInformation> lst = new List<ParticipantInformation>();
            foreach (DataRow row in dt.Rows)
            {
                ParticipantInformation t = new ParticipantInformation();
                t.ParticipantID = row["Participant"].ToString();
                t.Name = row["Name"].ToString();
                t.FatherName = row["FatherName"].ToString();
                t.Gender = row["Gender"].ToString();
                t.ProvinceName = row["ProvinceEngName"].ToString();
                t.OrganizationName = row["OrgName"].ToString();
                t.TrainingDay = row["TrainingDay"].ToString();
                t.PresentStatus =bool.Parse(row["PresentStatus"].ToString());
                var roles = new List<string> { "Admin", "Super User" };
                var userRoles = Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name);

                lst.Add(t);
            }
            return lst;

        }
        [WebMethod]
        public static string Save(AttendanceMaster obj)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string query = "";
            foreach (AttendanceDetail item in obj.lstDetail)
            {
                query += @"INSERT INTO tblTRA_AttendanceDetail(AttMstID,Participant,PresentStatus,InsertedBy,InsertedDate) values (*,N'" + item.Participant + "',N'"+item.PresentStatus+"',N'" + currentUser.ProviderUserKey.ToString() + "',N'" + DateTime.Now.ToString() + "')";
            }

            string[] p = { "@TrainingID", "@TrainingDay", "@User", "@Date", "@sql", "@Error" };
            string[] v = { obj.TrainingID, obj.TrainingDay, currentUser.ProviderUserKey.ToString(), DateTime.Now.ToString(),query };
            string r = db.DML_SP_Executer_string(p, v, "spPageAttendanceSave");
            return r;
        }
        [WebMethod]
        public static string Update(AttendanceMaster obj)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string query = "";
            foreach (AttendanceDetail item in obj.lstDetail)
            {
                query += @"INSERT INTO tblTRA_AttendanceDetail(AttMstID,Participant,PresentStatus,InsertedBy,InsertedDate) values (*,N'" + item.Participant + "',N'" + item.PresentStatus + "',N'" + currentUser.ProviderUserKey.ToString() + "',N'" + DateTime.Now.ToString() + "')";
            }

            string[] p = { "@AttMstID", "@TrainingID", "@TrainingDay", "@User", "@Date", "@sql", "@Error" };
            string[] v = { obj.AttMstID, obj.TrainingID, obj.TrainingDay, currentUser.ProviderUserKey.ToString(), DateTime.Now.ToString(), query };
            string r = db.DML_SP_Executer_string(p, v, "spPageAttendanceUpdate");
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
        public static List<ListItem> GetTrainingDays(int TrainingID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@TrainingID" };
            string[] v = { TrainingID.ToString()};
            DataTable dt = db.selectProcedureExecuter(p,v,"SharedGetTrainingDays");
            List<ListItem> LstData = new List<ListItem>();
            int Days = 0;
            if(dt.Rows.Count>0)
                Days = int.Parse(dt.Rows[0][0].ToString());
            for(int i=1;i<=Days;i++)
            {
                LstData.Add(new ListItem
                {
                    Value = i.ToString(),
                    Text = ("Day - " + i).ToString()
                });
            }
            return LstData;

        }

        public class ParticipantInformation:AttendanceDetail
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
        public class AttendanceMaster
        {
            public string AttMstID { get; set; }
            public string TrainingID { get; set; }
            public string TrainingTitle { get; set; }
            public string YearName { get; set; }
            public string ComponentName { get; set; }
            public string Province { get; set; }
            public string TrainingDay { get; set; }
            public List<AttendanceDetail> lstDetail { get; set; }
            public bool Edit { get; set; }
        }
        public class AttendanceDetail:AttendanceMaster
        {

            public string AttDetailID { get; set; }
            //public string AttMstID { get; set; }
            public string Participant { get; set; }
            public bool PresentStatus { get; set; }
            
        }
    }
}