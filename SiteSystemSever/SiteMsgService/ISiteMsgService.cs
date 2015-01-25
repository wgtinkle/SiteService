using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SiteMsgService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ISiteMsgService”。
    [ServiceContract]
    public interface ISiteMsgService
    {
        [OperationContract]
        bool SendMsgToSever(MsgData msgdata);
    }

    //消息数据
    [DataContract]
    public class MsgData
    {
        string sMsgLable = null;      //消息标签
        string sMsgContent = null; //消息内容
        string sMsgTime = null; //消息时间
        string sMsgPrivilege = null; // 消息优先级

        [DataMember]
        public string MsgLable
        {
            get { return sMsgLable; }
            set { sMsgLable = value; }
        }

        [DataMember]
        public string MsgContent
        {
            get { return sMsgContent; }
            set { sMsgContent = value; }
        }

        [DataMember]
        public string MsgTime
        {
            get { return sMsgTime; }
            set { sMsgTime = value; }
        }

        [DataMember]
        public string MsgPrivilege
        {
            get { return sMsgPrivilege; }
            set { sMsgPrivilege = value; }
        }

    }
}
