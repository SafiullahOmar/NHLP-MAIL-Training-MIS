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
    public partial class OrganizationSetup : System.Web.UI.Page
    {
        [WebMethod]
        public static List<OrganizationInformation> GetFormDetail()
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            DataTable dt = db.selectProcedureExecuter("spPageOrganizationLoad");
            List<OrganizationInformation> lst = new List<OrganizationInformation>();
            foreach (DataRow row in dt.Rows)
            {
                OrganizationInformation t = new OrganizationInformation();
                t.OrganizationID = row["OrganizationID"].ToString();
                t.Name = row["OrgName"].ToString();
                t.NameLocal = row["OrgNameLocal"].ToString();

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
        public static string Save(OrganizationInformation obj)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string[] p = { "@Name", "@NameLocal", "@User", "@Date", "@Error" };
            string[] v = { obj.Name, obj.NameLocal, currentUser.ProviderUserKey.ToString(), DateTime.Now.ToString() };
            string r = db.DML_SP_Executer_string(p, v, "spPageOrganizationSave");
            return r;
        }
        [WebMethod]
        public static string Update(OrganizationInformation obj)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string[] p = { "@OrganizationID", "@Name", "@NameLocal", "@User", "@Date", "@Error" };
            string[] v = { obj.OrganizationID, obj.Name, obj.NameLocal, currentUser.ProviderUserKey.ToString(), DateTime.Now.ToString() };
            string r = db.DML_SP_Executer_string(p, v, "spPageOrganizationUpdate");
            return r;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public class OrganizationInformation
        {
            public string OrganizationID { get; set; }
            public string Name { get; set; }
            public string NameLocal { get; set; }
            public bool Edit { get; set; }

        }
    }
}