using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.Security;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMIS.GeneralClasses;

namespace TMIS.Admin
{
    public partial class CreateUser : System.Web.UI.Page, IRequiresSessionState
    {
        private DbGeneral db = new DbGeneral();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Title = "MAR-MIS : Create User";
            this.Page.ClientScript.RegisterOnSubmitStatement(base.GetType(), "val", "fnOnUpdateValidators();");
            if (!base.IsPostBack)
            {
                this.fillDll();
            }
        }
        private void fillDll()
        {
            DropDownList dropDownList = this.CreateUserWizardStep1.ContentTemplateContainer.FindControl("ddlProvince") as DropDownList;
            if (dropDownList != null)
            {
                DataTable dataSource = this.db.SelectRecords("Select '-1' as ProvinceID,N'--Select--' as ProvinceEngName union SELECT [ProvinceID] ,[ProvinceEngName] FROM  [Province]");
                dropDownList.DataSource = dataSource;
                dropDownList.DataValueField = "ProvinceID";
                dropDownList.DataTextField = "ProvinceEngName";
                dropDownList.DataBind();
            }
        }
        [WebMethod]
        public static string CheckAvailability(string userName)
        {
            SqlConnection selectConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            string result = string.Empty;
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT     aspnet_Users.UserId, aspnet_Users.UserName, aspnet_Applications.ApplicationName\r\nFROM         aspnet_Applications INNER JOIN\r\n                      aspnet_Users ON aspnet_Applications.ApplicationId = aspnet_Users.ApplicationId\r\nWHERE     (aspnet_Applications.ApplicationName = N'Mar' and aspnet_Users.UserName=N'" + userName + "')", selectConnection);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    result = "true";
                }
                else
                {
                    result = "false";
                }
            }
            catch
            {
                result = "erorr";
            }
            return result;
        }
        [WebMethod]
        public static string CheckEmailAvailability(string email)
        {
            SqlConnection selectConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            string result = string.Empty;
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM [aspnet_Membership] where [Email]='" + email + "'", selectConnection);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    result = "true";
                }
                else
                {
                    result = "false";
                }
            }
            catch
            {
                result = "erorr";
            }
            return result;
        }
        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropDownList = this.CreateUserWizardStep1.ContentTemplateContainer.FindControl("ddlDistrict") as DropDownList;
            DropDownList dropDownList2 = this.CreateUserWizardStep1.ContentTemplateContainer.FindControl("ddlProvince") as DropDownList;
            if (dropDownList != null && dropDownList2 != null)
            {
                DataTable dataSource = this.db.SelectRecords("Select '-1' as DistrictID,N'--Select--' as DistrictEngName union SELECT [DistrictID] ,[DistrictEngName] FROM [dbo].[District] where ProvinceID=" + dropDownList2.SelectedValue);
                dropDownList.DataSource = dataSource;
                dropDownList.DataValueField = "DistrictID";
                dropDownList.DataTextField = "DistrictEngName";
                dropDownList.DataBind();
            }
        }
        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            //ProfileCommon profileCommon = (ProfileCommon) ProfileBase.Create(this.CreateUserWizard1.UserName);
            //profileCommon.FullName = ((TextBox)this.CreateUserWizardStep1.ContentTemplateContainer.FindControl("txtFullName")).Text;
            //profileCommon.Age = ((TextBox)this.CreateUserWizardStep1.ContentTemplateContainer.FindControl("txtAge")).Text;
            //profileCommon.Designation = ((TextBox)this.CreateUserWizardStep1.ContentTemplateContainer.FindControl("txtDesignation")).Text;
            //profileCommon.Province = ((DropDownList)this.CreateUserWizardStep1.ContentTemplateContainer.FindControl("ddlProvince")).SelectedValue;
            //profileCommon.District = ((DropDownList)this.CreateUserWizardStep1.ContentTemplateContainer.FindControl("ddlDistrict")).SelectedValue;
            //profileCommon.Save();
            MembershipUser user = Membership.GetUser(this.CreateUserWizard1.UserName);
            string text = user.ProviderUserKey.ToString();
            this.db.ExecuteQuery(string.Concat(new string[]
		{
			"Update aspnet_Users set ProvinceID=",
			((DropDownList)this.CreateUserWizardStep1.ContentTemplateContainer.FindControl("ddlProvince")).SelectedValue,
			", DistrictID=",
			((DropDownList)this.CreateUserWizardStep1.ContentTemplateContainer.FindControl("ddlDistrict")).SelectedValue,
			" where UserId=N'",
			text,
			"'"
		}));
        }
    }
    public class ProfileCommon : ProfileBase
    {
        public virtual string District
        {
            get
            {
                return (string)base.GetPropertyValue("District");
            }
            set
            {
                base.SetPropertyValue("District", value);
            }
        }

        public virtual string Province
        {
            get
            {
                return (string)base.GetPropertyValue("Province");
            }
            set
            {
                base.SetPropertyValue("Province", value);
            }
        }

        public virtual string FullName
        {
            get
            {
                return (string)base.GetPropertyValue("FullName");
            }
            set
            {
                base.SetPropertyValue("FullName", value);
            }
        }

        public virtual string Age
        {
            get
            {
                return (string)base.GetPropertyValue("Age");
            }
            set
            {
                base.SetPropertyValue("Age", value);
            }
        }

        public virtual string Designation
        {
            get
            {
                return (string)base.GetPropertyValue("Designation");
            }
            set
            {
                base.SetPropertyValue("Designation", value);
            }
        }

        public virtual ProfileCommon GetProfile(string username)
        {
            return (ProfileCommon)ProfileBase.Create(username);
        }
    }
}