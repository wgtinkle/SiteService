using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;


using SiteMsgService;



namespace SiteClient
{
    class Program
    {
        static void Main(string[] args)
        {
            EndpointAddress ea = new EndpointAddress("http://127.0.0.1:8000/SiteMsgService/");
            ISiteMsgService proxy = ChannelFactory<ISiteMsgService>.CreateChannel(new BasicHttpBinding(), ea);

            for (int i = 0; i < 10; i++)
            {
                MsgData msg = new MsgData();
                msg.MsgLable = "WebMsg";
                msg.MsgContent = "快速启动";
                msg.MsgPrivilege = "High";
                msg.MsgTime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");

                string str = msg.ToString();

                proxy.SendMsgToSever(msg);

                Thread.Sleep(2000);
            }
            
        }
    }
}
