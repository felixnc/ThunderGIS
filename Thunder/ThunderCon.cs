using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Text;
using System.IO;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TableDependency.Enums;
using TableDependency.EventArgs;
using TableDependency.Mappers;
using TableDependency.OracleClient;
using Oracle.DataAccess.Client;
using System.Diagnostics;
namespace Thunder
{

    public class ThunderCon : Hub
    {
        public Thread BenginUpdThread;
        public Thread BenginListenThread;

        const int listenPort = 4101;

        


        // Handle new clients connected
        public override System.Threading.Tasks.Task OnConnected()
        {

            // Add existing graphics layers to caller (new client)

            Clients.All.showdata("链接了一个");

            BenginListen();

           
            return base.OnConnected();
        }

        // Handle clients disconnected
        public override System.Threading.Tasks.Task OnDisconnected(bool conn)
        {
            // Update connections counter
          
            return base.OnDisconnected(conn);
        }
        public void showdata(String str)
        {
            // Notify clients to add polygon
            String s = str + "我是服务器加上去的";
            Clients.All.showdata(s);

            
        }
        public void BenginListen()
        {
            Thread t;
            t = new Thread(BenginsendData);
            t.Start();
        }
        public void BenginsendData()
        {

            
            string constr = "Data Source=thunder;User Id=thunder;Password=thunder;";
            //string constr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=119.2.29.18)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=thunder)));User Id=thunder;Password=thunder;";
            string table = "STATIONSTATE";
            //try
            //{
            //    OracleDependency.Port = 49500;

            //    OracleConnection s = new OracleConnection(constr);
            //    s.Open();
            //    OracleDependency dep;
            //    //public static string sql = sqlSelect + "where employee_id < 200";
            //    OracleCommand cmd = new OracleCommand("select * from stationlastdata", s);
            //    //绑定OracleDependency实例与OracleCommand实例
            //    dep = new OracleDependency(cmd);
            //    //指定Notification是object-based还是query-based，前者表示表（本例中为tab_cn）中任意数据变化时都会发出Notification；后者提供更细粒度的Notification，例如可以在前面的sql语句中加上where子句，从而指定Notification只针对查询结果里的数据，而不是全表。
            //    dep.QueryBasedNotification = false;
            //    //是否在Notification中包含变化数据对应的RowId
            //    dep.RowidInfo = OracleRowidInfo.Include;
            //    //指定收到Notification后的事件处理方法
            //    dep.OnChange += new OnChangeEventHandler(OnNotificaton);
            //    //是否在一次Notification后立即移除此次注册
            //    cmd.Notification.IsNotifiedOnce = true;
            //    //此次注册的超时时间（秒），超过此时间，注册将被自动移除。0表示不超时。
            //    cmd.Notification.Timeout = 0;
            //    //False表示Notification将被存于内存中，True表示存于数据库中，选择True可以保证即便数据库重启之后，消息仍然不会丢失
            //    cmd.Notification.IsPersistent = true;
            //    //
            //    OracleDataReader odr = cmd.ExecuteReader();
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}

            try
            {
                OracleTableDependency<Item> tableDependency = null;
                OracleConnection s = new OracleConnection(constr); s.Open();
                var mapper = new ModelToTableMapper<Item>();
                mapper.AddMapping(c => c.NAME, "NAME");
                mapper.AddMapping(c => c.SID, "SID");
                mapper.AddMapping(c => c.DATETIME, "TIMEMARK");

                tableDependency = new OracleTableDependency<Item>(constr, table, mapper);
                tableDependency.OnChanged += Changed;
                tableDependency.Start();
                //naming = tableDependency.DataBaseObjectsNamingConvention;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        static void Changed(object sender, RecordChangedEventArgs<Item> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                var changedEntity = e.Entity;
                Console.WriteLine("DML operation: " + e.ChangeType);
                Console.WriteLine("ID: " + changedEntity.SID);
                Console.WriteLine("Name: " + changedEntity.NAME);
            }
        }
        protected static void OnRowUpdated(object sender,
                                        OracleRowUpdatedEventArgs e)
        {
            DataRow r = e.Row;
        }
        private void OnNotificaton(object sender, OracleNotificationEventArgs e)
        {
            try
            {
                Console.WriteLine("Info: {0} Type: {1} Source: {2}",
                    e.Info, e.Type, e.Source);
                if (e.Type == OracleNotificationType.Change)
                {
                    // Submit the SQL back to the server, 
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
    public class Item
    {
        public string NAME { get; set; }
        public int SID { get; set; }

        public string DATETIME { get; set; }
    }
}