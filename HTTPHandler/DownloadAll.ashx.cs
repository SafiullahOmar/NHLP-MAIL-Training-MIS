using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace TMIS.HTTPHandler
{
    /// <summary>
    /// Summary description for DownloadAll
    /// </summary>
    public class DownloadAll : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            using (ZipFile zip = new ZipFile())
            {
                string TrainingID = context.Request.QueryString["ID"];
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                //zip.AddDirectoryByName("Files");
                DatabaseCommunicator db = new DatabaseCommunicator();
                string[] p = { "@TrainingID" };
                string[] v={TrainingID.ToString()};
                DataTable dt = db.selectProcedureExecuter(p, v, "spPageTrainingDocLoad");
                foreach (DataRow row in dt.Rows)
                {
                    string filePath = row["Path"].ToString();
                    zip.AddFile(context.Server.MapPath(filePath), row["Remark"].ToString());
                    
                }
                context.Response.Clear();
                context.Response.BufferOutput = false;
                string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                context.Response.ContentType = "application/zip";
                context.Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(context.Response.OutputStream);
                context.Response.End();
            }
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