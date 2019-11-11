using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RegDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegTool.Init("RegDemo", "scimence");    // 修改RegDemo为您的软件名称、修改scimence为您的支付宝收款账号
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegTool.CheckRegDoing(ToolLogic);       // 调用付费注册逻辑，当付费完成后执行ToolLogic()
        }

        /// <summary>
        /// 注册完成后，执行的功能逻辑
        /// </summary>
        public void ToolLogic()
        {
            MessageBox.Show("success，当前软件已完成注册！！！");
            // 在ToolLogic中，调用执行软件的相关功能逻辑。
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "http://scimence.gitee.io/url/QRPay.html";
            System.Diagnostics.Process.Start(url);
        }
    }
}
