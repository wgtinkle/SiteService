using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

using SiteMsgService;

namespace SiteSystemSever
{
    class SiteMsgModule
    {
        public ServiceHost MsgHost = null;
        public void SiteMsgModuleInit()
        {
            MsgHost = new ServiceHost(typeof(SiteMsgService.SiteMsgService));

            MsgHost.AddServiceEndpoint(typeof(ISiteMsgService), new BasicHttpBinding(),
                new Uri("http://127.0.0.1:8000/SiteMsgService/"));

            if (MsgHost.State != CommunicationState.Opening)
            {
                MsgHost.Open();
            }

            TextBox mtb = (TextBox)Form1.ActiveForm.Controls.Find("MsgBox", false)[0];
            mtb.Text += "消息服务启动完成\r\n";

        }

        public void SiteMsgModuleClose()
        {
            if (MsgHost != null)
            {
                MsgHost.Close();               
            }
        }
    }
}
