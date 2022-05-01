using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TMIS.HTTPHandler
{
    /// <summary>
    /// Summary description for Download
    /// </summary>
    public class Download : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string file = context.Request.QueryString["file"];
            if (!string.IsNullOrEmpty(file) && File.Exists(context.Server.MapPath(file)))
            {
                context.Response.Buffer = true;
                context.Response.Clear();
                context.Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(file));
                context.Response.WriteFile(file);
                context.Response.End();
            }
            else
            {
                context.Response.Write("File does not exisit....!");
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