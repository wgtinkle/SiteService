using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Messaging;



namespace SiteMsgService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“SiteMsgService”。
    public class SiteMsgService : ISiteMsgService
    {
        public bool SendMsgToSever(MsgData msgdata)
        {
            bool bResult = false;

            MessageQueue MqClient;
            try
            {

                if (MessageQueue.Exists(@".\Private$\SiteMsgQueue"))
                {
                    MqClient = new MessageQueue(@".\Private$\SiteMsgQueue");
                    MqClient.Label = "LSMsg";

                    string smsg = msgdata.MsgLable + "," + msgdata.MsgContent + "," + msgdata.MsgPrivilege + "," + msgdata.MsgTime;

                    MqClient.Send(smsg); //发送消息到队列
                }

                bResult = true;
                
            }
            catch (Exception e)
            {
            }



            return bResult;
        }
    }
}
