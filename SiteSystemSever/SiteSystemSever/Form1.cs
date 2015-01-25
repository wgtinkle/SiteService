using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SiteSystemSever.PageGenerate;

namespace SiteSystemSever
{

    public partial class Form1 : Form
    {
        SiteMsgModule MsgModule = new SiteMsgModule();
        SiteMsgListener MsgListener = new SiteMsgListener();
        
        

        public Form1()
        {
            InitializeComponent();
            MsgListener.SetTextBox(MsgBox);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            MsgBox.Text = "启动服务......\r\n";
            MsgModule.SiteMsgModuleInit();
            MsgListener.SiteMsgListenerCreatQueue();

            MsgBox.Text += "服务启动完成\r\n";
        }

        private void BtnEnd_Click(object sender, EventArgs e)
        {
            MsgBox.Text += "关闭服务......\r\n";
            MsgModule.SiteMsgModuleClose();
            MsgListener.SiteMsgListenerCloseQueue();
            MsgBox.Text += "服务已停止\r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IndexGenerate indexg = new IndexGenerate();
            indexg.GenerateIndexFile("E:\\SiteService\\SiteSystemSever\\SiteSystemSever\\PageTemplate\\Index.htm", "E:\\SiteService\\SiteSystemSever\\SiteSystemSever\\PageTemplate\\Index.htm");
        }

        
    }
}
