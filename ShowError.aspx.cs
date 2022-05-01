using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMIS
{
    public partial class ShowError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Exception err = ErrorException.Error;
            if (err != null)
            {
                err = err.GetBaseException();
                lblMessage.Text = err.Message;
                lblSource.Text = err.Source;
                lblInnerException.Text = (err.InnerException != null) ? err.InnerException.ToString() : "";
                lblStackTrace.Text = err.StackTrace;

            }
        }
    }
}