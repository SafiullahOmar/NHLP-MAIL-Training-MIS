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
    public partial class FieldVisit : System.Web.UI.Page
    {
        [WebMethod]
        public static VisitInformation GetFormByID(string ID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@ID" };
            string[] v = { ID };
            DataTable dt = db.selectProcedureExecuter(p, v, "spPageFieldVisitByID");
            VisitInformation t = new VisitInformation();
            foreach (DataRow row in dt.Rows)
            {
                t.FieldVisitID = row["FieldVisitID"].ToString();
                t.Year = row["Year"].ToString();
                t.VisitDate = DateTime.Parse(row["VisitDate"].ToString()).ToString("dd-MMM-yyyy");
                t.Component = row["Component"].ToString();
                t.ComponentName = row["ComponentName"].ToString();
                t.SubComponent = row["SubComponent"].ToString();
                t.VisitPurpose = row["VisitPurpose"].ToString();
                t.ProvinceID = row["Province"].ToString();
                t.ProvinceName = row["ProvinceEngName"].ToString();
                t.DistrictID = row["District"].ToString();
                t.DistrictName = row["DistrictEngName"].ToString();
                t.VisitConductedBy = row["VisitConductedBy"].ToString();
                t.Remarks = row["Remarks"].ToString();
            }
            return t;

        }
        [WebMethod]
        public static List<VisitInformation> GetFormDetail(int YearID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@YearID" };
            string[] v = { YearID.ToString()};
            DataTable dt = db.selectProcedureExecuter(p, v, "spPageFieldVisitLoad");
            List<VisitInformation> lst = new List<VisitInformation>();
            foreach (DataRow row in dt.Rows)
            {
                VisitInformation t = new VisitInformation();
                t.FieldVisitID = row["FieldVisitID"].ToString();
                t.Year = row["Year"].ToString();
                t.VisitDate = DateTime.Parse(row["VisitDate"].ToString()).ToString("dd-MMM-yyyy");
                t.Component = row["Component"].ToString();
                t.ComponentName = row["ComponentName"].ToString();
                t.SubComponent = row["SubComponent"].ToString();
                t.VisitPurpose = row["VisitPurpose"].ToString();
                t.ProvinceID = row["Province"].ToString();
                t.ProvinceName = row["ProvinceEngName"].ToString();
                t.DistrictID = row["District"].ToString();
                t.DistrictName = row["DistrictEngName"].ToString();
                t.VisitConductedBy = row["VisitConductedBy"].ToString();
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
        public static string Save(VisitInformation obj)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string[] p = { "@Year","@VisitDate","@Component","@SubComponent","@VisitPurpose","@Province","@District","@VisitConductedBy","@Remarks", "@User", "@Date", "@Error" };
            string[] v = { obj.Year, obj.VisitDate, obj.Component, obj.SubComponent,obj.VisitPurpose, obj.ProvinceID, obj.DistrictID, obj.VisitConductedBy, obj.Remarks, currentUser.ProviderUserKey.ToString(), DateTime.Now.ToString() };
            string r = db.DML_SP_Executer_string(p, v, "spPageFieldVisitSave");
            return r;
        }
        [WebMethod]
        public static string Update(VisitInformation obj)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string[] p = { "@FieldVisitID", "@Year", "@VisitDate", "@Component", "@SubComponent", "@VisitPurpose", "@Province", "@District", "@VisitConductedBy", "@Remarks", "@User", "@Date", "@Error" };
            string[] v = { obj.FieldVisitID, obj.Year, obj.VisitDate, obj.Component, obj.SubComponent, obj.VisitPurpose, obj.ProvinceID, obj.DistrictID, obj.VisitConductedBy, obj.Remarks, currentUser.ProviderUserKey.ToString(), DateTime.Now.ToString() };
            string r = db.DML_SP_Executer_string(p, v, "spPageFieldVisitUpdate");
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

        public class VisitInformation
        {
            public string FieldVisitID { get; set; }
            public string Year { get; set; }
            public string VisitDate { get; set; }
            public string Component { get; set; }
            public string ComponentName { get; set; }
            public string SubComponent { get; set; }
            public string VisitPurpose { get; set; }
            public string ProvinceID { get; set; }
            public string ProvinceName { get; set; }
            public string DistrictID { get; set; }
            public string DistrictName { get; set; }
            public string VisitConductedBy { get; set; }
            public string Remarks { get; set; }
            public bool Edit { get; set; }
        }
    }
}