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

namespace TMIS.Admin
{
    public partial class SubComponentSetup : System.Web.UI.Page
    {
        [WebMethod]
        public static List<SubComponentInformation> GetFormDetail(int ComponentID)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            string[] p = { "@ComponentID" };
            string[] v = { ComponentID.ToString() };
            DataTable dt = db.selectProcedureExecuter(p, v, "spPageSubComponentLoad");
            List<SubComponentInformation> lst = new List<SubComponentInformation>();
            foreach (DataRow row in dt.Rows)
            {
                SubComponentInformation t = new SubComponentInformation();
                t.SubComponentID = row["SubComponentID"].ToString();
                t.SubComponentName = row["SubComponentName"].ToString();
                t.SubComponentNameLocal = row["SubComponentNameLocal"].ToString();
                t.ComponentID = row["Component"].ToString();
                t.ComponentName = row["ComponentName"].ToString();
                t.ComponentNameLocal = row["ComponentNameLocal"].ToString();

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
        public static string Save(SubComponentInformation obj)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string[] p = { "@ComponentID", "@Name", "@NameLocal", "@User", "@Date", "@Error" };
            string[] v = { obj.ComponentID, obj.SubComponentName, obj.SubComponentNameLocal, currentUser.ProviderUserKey.ToString(), DateTime.Now.ToString() };
            string r = db.DML_SP_Executer_string(p, v, "spPageSubComponentSave");
            return r;
        }
        [WebMethod]
        public static string Update(SubComponentInformation obj)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string[] p = { "@SubComponentID", "@ComponentID", "@Name", "@NameLocal", "@User", "@Date", "@Error" };
            string[] v = { obj.SubComponentID, obj.ComponentID, obj.SubComponentName, obj.SubComponentNameLocal, currentUser.ProviderUserKey.ToString(), DateTime.Now.ToString() };
            string r = db.DML_SP_Executer_string(p, v, "spPageSubComponentUpdate");
            return r;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

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

        public class SubComponentInformation
        {
            public string SubComponentID { get; set; }
            public string SubComponentName { get; set; }
            public string SubComponentNameLocal { get; set; }
            public string ComponentID { get; set; }
            public string ComponentName { get; set; }
            public string ComponentNameLocal { get; set; }
            public bool Edit { get; set; }

        }
    }
}