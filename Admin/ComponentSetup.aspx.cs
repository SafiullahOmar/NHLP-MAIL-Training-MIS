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
    public partial class ComponentSetup : System.Web.UI.Page
    {
        [WebMethod]
        public static List<ComponentInformation> GetFormDetail()
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            DataTable dt = db.selectProcedureExecuter("spPageComponentLoad");
            List<ComponentInformation> lst = new List<ComponentInformation>();
            foreach (DataRow row in dt.Rows)
            {
                ComponentInformation t = new ComponentInformation();
                t.ComponentID = row["ComponentID"].ToString();
                t.Name = row["ComponentName"].ToString();
                t.NameLocal = row["ComponentNameLocal"].ToString();

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
        public static string Save(ComponentInformation obj)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string[] p = { "@Name", "@NameLocal", "@User", "@Date", "@Error" };
            string[] v = { obj.Name, obj.NameLocal, currentUser.ProviderUserKey.ToString(), DateTime.Now.ToString() };
            string r = db.DML_SP_Executer_string(p, v, "spPageComponentSave");
            return r;
        }
        [WebMethod]
        public static string Update(ComponentInformation obj)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string[] p = { "@ComponentID", "@Name", "@NameLocal", "@User", "@Date", "@Error" };
            string[] v = { obj.ComponentID, obj.Name, obj.NameLocal, currentUser.ProviderUserKey.ToString(), DateTime.Now.ToString() };
            string r = db.DML_SP_Executer_string(p, v, "spPageComponentUpdate");
            return r;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public class ComponentInformation
        {
            public string ComponentID { get; set; }
            public string Name { get; set; }
            public string NameLocal { get; set; }
            public bool Edit { get; set; }

        }

    }
}