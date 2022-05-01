using System;
using System.Web;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web.Security;
using System.Data;
using TMIS.GeneralClasses;

namespace TMIS.HTTPHandler
{
    /// <summary>
    /// Summary description for TrainingDocUpload
    /// </summary>
    public class TrainingDocUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["action"])
            {
                case "SAVE":
                    Save(context);
                    break;
                case "UPDATE":
                    //Update(context);
                    break;
                case "SAVEFILE":
                    //SAVEFILE(context);
                    break;
            }
        }
        public void Save(HttpContext context)
        {
            DatabaseCommunicator db = new DatabaseCommunicator();
            var currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            string query = "";
            //Uploading images....!

            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                for (int i = 1; i <= files.Count; i++)
                {
                    HttpPostedFile file = files[i - 1];
                    string fname;
                    fname = "~/TraDocuments/" + context.Request["TrainingID"] + "-" + i + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + System.IO.Path.GetExtension(file.FileName).ToLower();
                    string strSaveLocation = context.Server.MapPath(fname);
                    //fname = System.IO.Path.Combine(context.Server.MapPath("~/TraDocuments/"), fname);
                    file.SaveAs(strSaveLocation);
                    query += @"INSERT INTO tblTRA_TrainingDocuments(TrainingID,Path,Remark,InsertedBy,InsertedDate) values (" + context.Request["TrainingID"] + ",N'" + fname + "',N'" + context.Request["Remarks"] + "','" + currentUser.ProviderUserKey.ToString() + "', '" + DateTime.Now.ToString() + "')";
                }
            }

            string[] p = { "@sql", "@Error" };
            string[] v = { query };
            string r = db.DML_SP_Executer_string(p, v, "spPageTrainingDocSave");
            context.Response.ContentType = "text/plain";
            context.Response.Write(r);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}