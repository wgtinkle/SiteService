using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading;

using System.Windows.Forms;

namespace SiteSystemSever
{    
    delegate void ShowMsgToWnd(string str);

    class SiteMsgListener
    {
        public TextBox mTBox=null;
        public void SetTextBox(TextBox mf)
        {
            mTBox = mf;
        }

        MessageQueue MqClient;   //消息队列
        TextBox mtb = null;            //主界面对话框
        Thread mlistenThread = null; //消息接受线程
        

        public void SiteMsgListenerCreatQueue()
        {
            if (!MessageQueue.Exists(@".\Private$\SiteMsgQueue"))
            {
                MqClient = MessageQueue.Create(@".\Private$\SiteMsgQueue");
                MqClient.Label = "LSMsg";
            }
            else
            {
                MessageQueue.Delete(@".\Private$\SiteMsgQueue");
                MqClient = MessageQueue.Create(@".\Private$\SiteMsgQueue");
                MqClient.Label = "LSMsg";
            }

            mtb = (TextBox)Form1.ActiveForm.Controls.Find("MsgBox", false)[0];
            mtb.Text += "创建消息队列\r\n";

            //创建消息接受线程
            mlistenThread = new Thread(SiteMsgSevericeRead);
            mlistenThread.Start(this);
        }
        public void SiteMsgListenerCloseQueue()
        {
            mlistenThread.Abort();
            MqClient.Dispose();
            
            mtb.Text += "关闭消息队列\r\n";
        }

        public void ShowMsg(string str)
        {
            mTBox.Text += str + "\r\n";
        }

        static void SiteMsgSevericeRead(Object o)
        {
            while (true)
            {
                if (MessageQueue.Exists(@".\Private$\SiteMsgQueue"))
                {
                    MessageQueue mq = new MessageQueue(@".\Private$\SiteMsgQueue");

                    mq.Formatter = new XmlMessageFormatter(new string[] { "System.String" });

                    SiteMsgListener mlistener = (SiteMsgListener)o;
                    if (mq.CanRead)
                    {
                        System.Messaging.Message[]  msgs = mq.GetAllMessages();
                        for (int i = 0; i < msgs.Length; i++)
                        {
                            if (msgs[i].Body.ToString() != null)
                            {
                                ShowMsgToWnd dShow = mlistener.ShowMsg;
                                //dShow(msgs[i].Body.ToString());
                                mlistener.mTBox.Invoke(dShow, msgs[i].Body.ToString());
                            }
                        }
                    }
                    mq.Purge();
                    Thread.Sleep(50);
                }
            }
        }


    }
}
