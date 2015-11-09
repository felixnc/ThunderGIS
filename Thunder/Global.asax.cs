using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading;
using System.Text;
using System.IO;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Oracle.DataAccess.Client;
namespace Thunder
{
     
    public class Global : System.Web.HttpApplication
    {
        
        
        protected void Application_Start(object sender, EventArgs e)
        {
          

    }
        protected void Application_End(object sender, EventArgs e)
        {
           
        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

       
    }
}