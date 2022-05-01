using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web;
using System.Web.Profile;
using System.Web.Security;

namespace TMIS.GeneralClasses
{
    public class UserInfo
    {
        private DbGeneral db = new DbGeneral();

        private string username;

        private string provinceid;

        private string districtid;

        private string email;

        private string designation;

        private string fullname;

        private string age;

        public string UserName
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }

        public string ProvinceID
        {
            get
            {
                return this.provinceid;
            }
            set
            {
                this.provinceid = value;
            }
        }

        public string DistrictID
        {
            get
            {
                return this.districtid;
            }
            set
            {
                this.districtid = value;
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        public string Designation
        {
            get
            {
                return this.designation;
            }
            set
            {
                this.designation = value;
            }
        }

        public string FullName
        {
            get
            {
                return this.fullname;
            }
            set
            {
                this.fullname = value;
            }
        }

        public string Age
        {
            get
            {
                return this.age;
            }
            set
            {
                this.age = value;
            }
        }

        public UserInfo(string user_name, string province_id, string district_id, string E_mail, string Desig, string full_name)
        {
            user_name = this.username;
            this.provinceid = province_id;
            this.districtid = district_id;
            this.email = E_mail;
            Desig = this.designation;
            this.fullname = full_name;
        }

        public UserInfo()
        {
        }

        public static string GetUserID()
        {
            MembershipUser user = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            return user.ProviderUserKey.ToString();
        }

        public static string GetUserName(string userID)
        {
            string result = "";
            if (userID != "")
            {
                DataTable dataTable = new DbGeneral().SelectRecords("SELECT     aspnet_Users.UserId, aspnet_Users.UserName, aspnet_Users.LoweredUserName, aspnet_Applications.ApplicationName\r\nFROM         aspnet_Users INNER JOIN\r\n                      aspnet_Applications ON aspnet_Users.ApplicationId = aspnet_Applications.ApplicationId\r\nWHERE     (aspnet_Users.UserId = CAST('" + userID + "' AS uniqueIdentifier)) and aspnet_Applications.ApplicationName=N'HC'");
                if (dataTable.Rows.Count > 0)
                {
                    result = dataTable.Rows[0]["UserName"].ToString();
                }
            }
            return result;
        }

        public static string GetApplicationName(string userID)
        {
            string result = "";
            if (userID != "")
            {
                DataTable dataTable = new DbGeneral().SelectRecords("SELECT     aspnet_Users.UserId, aspnet_Users.UserName, aspnet_Users.LoweredUserName, aspnet_Applications.ApplicationName\r\nFROM         aspnet_Users INNER JOIN\r\n                      aspnet_Applications ON aspnet_Users.ApplicationId = aspnet_Applications.ApplicationId\r\nWHERE     (aspnet_Users.UserId = CAST('" + userID + "' AS uniqueIdentifier)) and aspnet_Applications.ApplicationName=N'HC'");
                if (dataTable.Rows.Count > 0)
                {
                    result = dataTable.Rows[0]["ApplicationName"].ToString();
                }
            }
            return result;
        }

        public static DataTable GetUserProvinces()
        {
            SqlParameter[] commandParameter = new SqlParameter[]
			{
				new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)
				{
					Value = new SqlGuid(UserInfo.GetUserID())
				}
			};
            return new DbGeneral().ExecuteSelectStoreProcedure("SharedGetUserProvinces", commandParameter, true);
        }
        public static DataTable GetUserComponents()
        {
            SqlParameter[] commandParameter = new SqlParameter[]
			{
				new SqlParameter("@UserId", SqlDbType.UniqueIdentifier)
				{
					Value = new SqlGuid(UserInfo.GetUserID())
				}
			};
            return new DbGeneral().ExecuteSelectStoreProcedure("SharedGetUserComponents", commandParameter, true);
        }
        public static DataTable GetUserLeadFormers()
        {
            return new DbGeneral().SelectRecords("SELECT DISTINCT \r\n                      aspnet_UserProvince.ProvinceID, OCM_Province.ProvinceName, OCM_Province.ProvinceEngName, Tbl_LeadFormerWorker.Name, \r\n                      Tbl_LeadFormerWorker.ExtWID,Tbl_LeadFormerWorker.ActivityId\r\nFROM         aspnet_UserProvince INNER JOIN\r\n                      OCM_Province ON aspnet_UserProvince.ProvinceID = OCM_Province.ProvinceID INNER JOIN\r\n                      Tbl_LeadFormerWorker ON OCM_Province.ProvinceID = Tbl_LeadFormerWorker.ProvinceID\r\nwhere  aspnet_UserProvince.UserID=N'" + UserInfo.GetUserID() + "' order by aspnet_UserProvince.ProvinceID");
        }

        public static string GetUserProvincesIDs()
        {
            DataTable userProvinces = UserInfo.GetUserProvinces();
            string text = "(";
            if (userProvinces.Rows.Count == 1)
            {
                text = "(" + userProvinces.Rows[0]["ProvinceID"].ToString() + ")";
            }
            else if (userProvinces.Rows.Count > 1)
            {
                int count = userProvinces.Rows.Count;
                int num = 0;
                foreach (DataRow dataRow in userProvinces.Rows)
                {
                    if (num < count - 1)
                    {
                        text = text + dataRow["ProvinceID"].ToString() + ",";
                    }
                    else
                    {
                        text = text + dataRow["ProvinceID"].ToString() + ")";
                    }
                    num++;
                }
            }
            return text;
        }

        public UserInfo GetUserInfo(string username)
        {
            MembershipUser user = Membership.GetUser(username);
            string str = user.ProviderUserKey.ToString();
            DataTable dataTable = this.db.SelectRecords("SELECT     aspnet_Users.UserId, aspnet_Users.UserName, aspnet_Users.ProvinceID, aspnet_Users.DistrictID, aspnet_Membership.Email, aspnet_Membership.IsApproved, \r\n                      aspnet_Membership.IsLockedOut, aspnet_Membership.ApplicationId, aspnet_Applications.ApplicationName\r\nFROM         aspnet_Users INNER JOIN\r\n                      aspnet_Membership ON aspnet_Users.UserId = aspnet_Membership.UserId INNER JOIN\r\n                      aspnet_Applications ON aspnet_Users.ApplicationId = aspnet_Applications.ApplicationId\r\nWHERE     (aspnet_Applications.ApplicationName = N'ocm') AND (aspnet_Users.UserId = '" + str + "')");
            if (dataTable.Rows.Count > 0)
            {
                username = dataTable.Rows[0]["UserName"].ToString();
                this.provinceid = dataTable.Rows[0]["ProvinceID"].ToString();
                this.districtid = dataTable.Rows[0]["DistrictID"].ToString();
                this.email = dataTable.Rows[0]["Email"].ToString();
                ProfileBase profileBase = ProfileBase.Create(username, true);
                this.fullname = profileBase.GetPropertyValue("FullName").ToString();
                this.designation = profileBase.GetPropertyValue("Designation").ToString();
                this.age = profileBase.GetPropertyValue("Age").ToString();
            }
            return new UserInfo(this.UserName, this.ProvinceID, this.DistrictID, this.Email, this.Designation, this.FullName);
        }
    }
}