using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Threading;
[assembly: OwinStartup(typeof(Thunder.Startup))]

namespace Thunder
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
           
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
