using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMIS.GeneralClasses;

namespace TMIS.Admin
{
    public partial class RoleManager : System.Web.UI.Page
    {
        DbGeneral db = new DbGeneral();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Training MIS : Role Manager";
            if (!IsPostBack)
            {

                fillRolesGrid();
                fillUserGrid("");
                fillgridManageUsers("");

            }
            if (!User.IsInRole("Super User"))
            {
                BtnAddRole.Enabled = false;
            }

        }
        private void fillgridManageUsers(string userName)
        {
            MembershipUser usr = Membership.GetUser(User.Identity.Name);
            string UserID = usr.ProviderUserKey.ToString();
            UserInfo userInfo = new UserInfo();
            userInfo = userInfo.GetUserInfo(User.Identity.Name);
            string proinfo = "";
            if (userInfo.ProvinceID != null)
                proinfo = " and aspnet_Users.ProvinceID=" + userInfo.ProvinceID;
            else
                proinfo = "";
            if (User.IsInRole("Super User"))
            {
                if (userName == "")
                {
                    grdManageUsers.DataSource = db.SelectRecords(@"SELECT     aspnet_Users.UserId, aspnet_Users.UserName, aspnet_Membership.Email, aspnet_Membership.IsLockedOut, aspnet_Applications.ApplicationName
FROM         aspnet_Users INNER JOIN
                      aspnet_Membership ON aspnet_Users.UserId = aspnet_Membership.UserId INNER JOIN
                      aspnet_Applications ON aspnet_Membership.ApplicationId = aspnet_Applications.ApplicationId
where aspnet_Applications.ApplicationName='Tra' and aspnet_Users.UserId<>'" + UserID + "'");
                    grdManageUsers.DataBind();
                }
                else
                {
                    grdManageUsers.DataSource = db.SelectRecords(@"SELECT     aspnet_Users.UserId, aspnet_Users.UserName, aspnet_Membership.Email, aspnet_Membership.IsLockedOut, aspnet_Applications.ApplicationName
FROM         aspnet_Users INNER JOIN
                      aspnet_Membership ON aspnet_Users.UserId = aspnet_Membership.UserId INNER JOIN
                      aspnet_Applications ON aspnet_Membership.ApplicationId = aspnet_Applications.ApplicationId
where aspnet_Applications.ApplicationName='Tra' and aspnet_Users.UserName=N'" + userName + "' and aspnet_Users.UserName<>N'" + usr.UserName + "'");
                    grdManageUsers.DataBind();
                }
            }
            else
            {
                if (userName == "")
                {
                    grdManageUsers.DataSource = db.SelectRecords(@"SELECT     aspnet_Users.UserId, aspnet_Users.UserName, aspnet_Membership.Email, aspnet_Membership.IsLockedOut, aspnet_Applications.ApplicationName
FROM         aspnet_Users INNER JOIN
                      aspnet_Membership ON aspnet_Users.UserId = aspnet_Membership.UserId INNER JOIN
                      aspnet_Applications ON aspnet_Membership.ApplicationId = aspnet_Applications.ApplicationId
where aspnet_Applications.ApplicationName='Tra' and aspnet_Users.UserId<>'" + UserID + "' " + proinfo + @" and aspnet_Users.UserId not in(SELECT     aspnet_UsersInRoles.UserId
FROM         aspnet_Roles INNER JOIN
                      aspnet_UsersInRoles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId
WHERE     (aspnet_Roles.RoleName = N'Super User'))");
                    grdManageUsers.DataBind();
                }
                else
                {
                    grdManageUsers.DataSource = db.SelectRecords(@"SELECT     aspnet_Users.UserId, aspnet_Users.UserName, aspnet_Membership.Email, aspnet_Membership.IsLockedOut, aspnet_Applications.ApplicationName
FROM         aspnet_Users INNER JOIN
                      aspnet_Membership ON aspnet_Users.UserId = aspnet_Membership.UserId INNER JOIN
                      aspnet_Applications ON aspnet_Membership.ApplicationId = aspnet_Applications.ApplicationId
where aspnet_Applications.ApplicationName='Tra' and aspnet_Users.UserName=N'" + userName + "' and aspnet_Users.UserName<>N'" + usr.UserName + "' " + proinfo + @" and aspnet_Users.UserId not in(SELECT     aspnet_UsersInRoles.UserId
FROM         aspnet_Roles INNER JOIN
                      aspnet_UsersInRoles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId
WHERE     (aspnet_Roles.RoleName = N'Super User'))");
                    grdManageUsers.DataBind();
                }
            }
        }
        private void fillRolesGrid()
        {
            if (User.IsInRole("Super User"))
            {

                GrdRoles.DataSource = db.SelectRecords(@"SELECT     aspnet_Roles.ApplicationId, aspnet_Roles.RoleId, aspnet_Roles.RoleName, aspnet_Roles.LoweredRoleName, aspnet_Roles.Description
FROM         aspnet_Roles INNER JOIN
                      aspnet_Applications ON aspnet_Roles.ApplicationId = aspnet_Applications.ApplicationId
WHERE     (aspnet_Applications.ApplicationName = N'Tra')");
                GrdRoles.DataBind();

                GrdUserRoles.DataSource = db.SelectRecords(@"SELECT     aspnet_Roles.ApplicationId, aspnet_Roles.RoleId, aspnet_Roles.RoleName, aspnet_Roles.LoweredRoleName, aspnet_Roles.Description
FROM         aspnet_Roles INNER JOIN
                      aspnet_Applications ON aspnet_Roles.ApplicationId = aspnet_Applications.ApplicationId
WHERE     (aspnet_Applications.ApplicationName = N'Tra')");
                GrdUserRoles.DataBind();
            }
            else
            {
                GrdRoles.DataSource = db.SelectRecords(@"SELECT     aspnet_Roles.ApplicationId, aspnet_Roles.RoleId, aspnet_Roles.RoleName, aspnet_Roles.LoweredRoleName, aspnet_Roles.Description
FROM         aspnet_Roles INNER JOIN
                      aspnet_Applications ON aspnet_Roles.ApplicationId = aspnet_Applications.ApplicationId
WHERE     (aspnet_Applications.ApplicationName = N'Tra') and  aspnet_Roles.RoleName<>N'Super User'");
                GrdRoles.DataBind();

                GrdUserRoles.DataSource = db.SelectRecords(@"SELECT     aspnet_Roles.ApplicationId, aspnet_Roles.RoleId, aspnet_Roles.RoleName, aspnet_Roles.LoweredRoleName, aspnet_Roles.Description
FROM         aspnet_Roles INNER JOIN
                      aspnet_Applications ON aspnet_Roles.ApplicationId = aspnet_Applications.ApplicationId
WHERE     (aspnet_Applications.ApplicationName = N'Tra') and  aspnet_Roles.RoleName<>N'Super User'");
                GrdUserRoles.DataBind();

            }
        }
        private void fillUserGrid(string userName)
        {

            MembershipUser usr = Membership.GetUser(User.Identity.Name);
            string UserID = usr.ProviderUserKey.ToString();
            UserInfo userInfo = new UserInfo();
            userInfo = userInfo.GetUserInfo(User.Identity.Name);
            string proinfo = "";
            if (userInfo.ProvinceID != null)
                proinfo = " and aspnet_Users.ProvinceID=" + userInfo.ProvinceID;
            else
                proinfo = "";
            if (User.IsInRole("Super User"))
            {
                if (userName == "")
                {


                    grdUser.DataSource = db.SelectRecords(@"SELECT     aspnet_Users.UserId, aspnet_Users.UserName, aspnet_Membership.Email, aspnet_Users.ApplicationId, aspnet_Applications.ApplicationName
FROM         aspnet_Users INNER JOIN
                      aspnet_Membership ON aspnet_Users.UserId = aspnet_Membership.UserId INNER JOIN
                      aspnet_Applications ON aspnet_Users.ApplicationId = aspnet_Applications.ApplicationId
WHERE     (aspnet_Applications.ApplicationName = 'Tra') and  aspnet_Users.UserId<>'" + UserID + "' " + proinfo);
                    grdUser.DataBind();
                }
                else
                {
                    grdUser.DataSource = db.SelectRecords(@"SELECT     aspnet_Users.UserId, aspnet_Users.UserName, aspnet_Membership.Email, aspnet_Users.ApplicationId, aspnet_Applications.ApplicationName
FROM         aspnet_Users INNER JOIN
                      aspnet_Membership ON aspnet_Users.UserId = aspnet_Membership.UserId INNER JOIN
                      aspnet_Applications ON aspnet_Users.ApplicationId = aspnet_Applications.ApplicationId
WHERE     (aspnet_Applications.ApplicationName = 'Tra') and aspnet_Users.UserName=N'" + userName + "' and aspnet_Users.UserName<>N'" + usr.UserName + "' " + proinfo);
                    grdUser.DataBind();
                }
            }
            else
            {
                if (userName == "")
                {
                    grdUser.DataSource = db.SelectRecords(@"SELECT     aspnet_Users.UserId, aspnet_Users.UserName, aspnet_Membership.Email, aspnet_Users.ApplicationId, aspnet_Applications.ApplicationName
FROM         aspnet_Users INNER JOIN
                      aspnet_Membership ON aspnet_Users.UserId = aspnet_Membership.UserId INNER JOIN
                      aspnet_Applications ON aspnet_Users.ApplicationId = aspnet_Applications.ApplicationId
WHERE     (aspnet_Applications.ApplicationName = 'Tra') and  aspnet_Users.UserId<>'" + UserID + "'" + @" and aspnet_Users.UserId not in(SELECT     aspnet_UsersInRoles.UserId
FROM         aspnet_Roles INNER JOIN
                      aspnet_UsersInRoles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId
WHERE     (aspnet_Roles.RoleName = N'Super User'))");
                    grdUser.DataBind();
                }
                else
                {
                    grdUser.DataSource = db.SelectRecords(@"SELECT     aspnet_Users.UserId, aspnet_Users.UserName, aspnet_Membership.Email, aspnet_Users.ApplicationId, aspnet_Applications.ApplicationName
FROM         aspnet_Users INNER JOIN
                      aspnet_Membership ON aspnet_Users.UserId = aspnet_Membership.UserId INNER JOIN
                      aspnet_Applications ON aspnet_Users.ApplicationId = aspnet_Applications.ApplicationId
WHERE     (aspnet_Applications.ApplicationName = 'Tra') and aspnet_Users.UserName=N'" + userName + "'" + @" and aspnet_Users.UserId not in(SELECT     aspnet_UsersInRoles.UserId
FROM         aspnet_Roles INNER JOIN
                      aspnet_UsersInRoles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId
WHERE     (aspnet_Roles.RoleName = N'Super User'))");
                    grdUser.DataBind();
                }
            }
        }
        protected void BtnAddRole_Click(object sender, EventArgs e)
        {
            if (!Roles.RoleExists(txtRoleName.Text))
            {
                Roles.CreateRole(txtRoleName.Text);

                fillRolesGrid();
                txtRoleName.Text = "";
            }
        }
        [WebMethod]
        public static void InsertRole(string rolename)
        {
            if (!Roles.RoleExists(rolename))
            {
                Roles.CreateRole(rolename);

            }
        }

        protected void GrdRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgDelete = e.Row.FindControl("imgDelete") as ImageButton;
                if (imgDelete != null)
                {
                    if (!User.IsInRole("Super User"))
                        imgDelete.Enabled = false;
                    else
                        imgDelete.Enabled = false;
                }
            }
        }
        protected void imgDelete_Command(object sender, CommandEventArgs e)
        {
            db.ExecuteQuery("delete from aspnet_Roles where RoleID='" + e.CommandArgument.ToString() + "'");
            fillRolesGrid();
        }
        protected void grdUser_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            grdUser.SelectedIndex = e.NewSelectedIndex;
            fillUserGrid("");
            //Page.ClientScript.
            Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "@@@@@@@tabs", " $(function () { $('#tabs').tabs({active:'1'});});");
            return;
        }
        protected void lnkAssingRoles_Command(object sender, CommandEventArgs e)
        {
            pnlroles.Visible = true;
            pnlUsers.Visible = false;
            System.Guid uid = new Guid(e.CommandArgument.ToString());
            MembershipUser usr = Membership.GetUser(uid);
            UserInfo userInfo = new UserInfo();
            userInfo = userInfo.GetUserInfo(usr.UserName);
            lblUserInfo.Text = "Manage Roles for :  " + usr.UserName;
            hdUserID.Value = e.CommandArgument.ToString();
            if (!User.IsInRole("Super User"))
            {
                grdSubProgram.DataSource = UserInfo.GetUserProvinces();
                grdComponent.DataSource = UserInfo.GetUserComponents();
            }
            else
            {
                grdSubProgram.DataSource = db.SelectRecords("SELECT [ProvinceID],[ProvinceEngName]  FROM [Province]");
                grdComponent.DataSource = db.SelectRecords("SELECT [ComponentID],[ComponentName]  FROM [Component]");
            } 
            grdSubProgram.DataBind();
            grdComponent.DataBind();
            fillRolesGrid();
            this.Page.ClientScript.RegisterOnSubmitStatement(base.GetType(), "val", "checkUncheckALL();");
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            pnlroles.Visible = false;
            pnlUsers.Visible = true;

        }
        protected void GrdUserRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRoleID = e.Row.FindControl("lblRoleID") as Label;
                CheckBox chkAssign = e.Row.FindControl("chkAssign") as CheckBox;
                if (lblRoleID != null && (hdUserID.Value != "0" && hdUserID.Value != ""))
                {
                    if (db.SelectRecords(@"SELECT [UserId] ,[RoleId] FROM [aspnet_UsersInRoles] where UserID=cast('" + hdUserID.Value + "' as uniqueidentifier) and RoleID=CAST('" + lblRoleID.Text + "' as uniqueidentifier)").Rows.Count > 0)
                        chkAssign.Checked = true;
                    else
                        chkAssign.Checked = false;

                }
            }
        }
        protected void AssignRoles_Click(object sender, EventArgs e)
        {
            db.ExecuteQuery("delete  from aspnet_UsersInRoles where UserID=Cast('" + hdUserID.Value + "' as uniqueidentifier)");
            if (hdUserID.Value != "0" && hdUserID.Value != "")
            {
                foreach (GridViewRow r in GrdUserRoles.Rows)
                {
                    Label lblRoleID = r.FindControl("lblRoleID") as Label;
                    CheckBox chkAssign = r.FindControl("chkAssign") as CheckBox;
                    if (lblRoleID != null && chkAssign != null)
                    {
                        if (chkAssign.Checked)
                        {
                            db.ExecuteQuery("insert into aspnet_UsersInRoles values('" + hdUserID.Value + "','" + lblRoleID.Text + "')");

                        }
                    }
                }
                //Assigning Provincess...
                db.ExecuteQuery("delete from aspnet_UserProvince where UserID=N'" + hdUserID.Value + "'");
                foreach (GridViewRow row in grdSubProgram.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label lblSubProgramID = row.FindControl("lblSubProgramID") as Label;
                        CheckBox chksubPro = row.FindControl("chksubPro") as CheckBox;
                        if (lblSubProgramID != null && chksubPro != null)
                        {
                            if (chksubPro.Checked)
                                db.ExecuteQuery("Insert into aspnet_UserProvince values(N'" + hdUserID.Value + "'," + lblSubProgramID.Text + ")");
                        }
                    }
                }
                //Assigning Components...
                db.ExecuteQuery("delete from aspnet_UserComponent where UserID=N'" + hdUserID.Value + "'");
                foreach (GridViewRow row in grdComponent.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label lblComponentID = row.FindControl("lblComponentID") as Label;
                        CheckBox chksubPro = row.FindControl("chksubPro") as CheckBox;
                        if (lblComponentID != null && chksubPro != null)
                        {
                            if (chksubPro.Checked)
                                db.ExecuteQuery("Insert into aspnet_UserComponent values(N'" + hdUserID.Value + "'," + lblComponentID.Text + ")");
                        }
                    }
                }
                btnBack_Click(null, null);
            }
        }
        protected void btnSearchUser_Click(object sender, EventArgs e)
        {
            fillUserGrid(txtSearchuserName.Text);
        }
        protected void btnSearchManageUser_Click(object sender, EventArgs e)
        {
            fillgridManageUsers(txtSearchManageUser.Text);
        }
        protected void lnkLock_Command(object sender, CommandEventArgs e)
        {
            LinkButton lnkLock = (LinkButton)sender;
            if (lnkLock != null)
            {
                if (lnkLock.Text.ToLower() == "lock")
                {
                    db.ExecuteQuery(@"update aspnet_Membership set    IsLockedOut='True'
where UserId='" + e.CommandArgument.ToString() + "'");
                    fillgridManageUsers("");
                }
                else
                {
                    db.ExecuteQuery(@"update aspnet_Membership set    IsLockedOut='False'
where UserId=cast('" + e.CommandArgument.ToString() + "' as uniqueidentifier)");
                    fillgridManageUsers("");
                }
            }
        }
        protected void lnkResetPassword_Command(object sender, CommandEventArgs e)
        {
            db.ExecuteQuery(@"update  aspnet_Membership  set  Password=N't877VWNp1mSPkvXezvmw6oLkAuQ=', PasswordSalt=N'SEqj/lYzMMOosRpHJYgRzQ=='
where UserId=cast('" + e.CommandArgument.ToString() + "' as uniqueidentifier)");
            fillgridManageUsers("");
        }
        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            fillUserGrid("");
            //Page.ClientScript.
            //String hiddenFieldValue = hidLastTab.Value;
            //StringBuilder js = new StringBuilder();
            //js.Append("<script type='text/javascript'>");
            //js.Append("var previouslySelectedTab = ");
            //js.Append(hiddenFieldValue);
            //js.Append(";</script>");
            //this.Header.Controls.Add(new LiteralControl(js.ToString()));
            //return;
        }
        protected void grdManageUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdManageUsers.PageIndex = e.NewPageIndex;
            fillgridManageUsers("");
        }
        protected void grdSubProgram_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSubProgramID = e.Row.FindControl("lblSubProgramID") as Label;
                CheckBox chksubPro = e.Row.FindControl("chksubPro") as CheckBox;
                if (lblSubProgramID != null && chksubPro != null)
                {
                    DataTable dt = db.SelectRecords(@"SELECT [UserID]
              ,[ProvinceID]
          FROM  [aspnet_UserProvince] where UserID=N'" + hdUserID.Value + "' and [ProvinceID]='" + lblSubProgramID.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        chksubPro.Checked = true;
                    }
                    else
                        chksubPro.Checked = false;


                }
            }
        }
        protected void grdComponent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblComponentID = e.Row.FindControl("lblComponentID") as Label;
                CheckBox chksubPro = e.Row.FindControl("chksubPro") as CheckBox;
                if (lblComponentID != null && chksubPro != null)
                {
                    DataTable dt = db.SelectRecords(@"SELECT [UserID]
              ,[ComponentID]
          FROM  [aspnet_UserComponent] where UserID=N'" + hdUserID.Value + "' and [ComponentID]='" + lblComponentID.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        chksubPro.Checked = true;
                    }
                    else
                        chksubPro.Checked = false;


                }
            }
        }
    }
}