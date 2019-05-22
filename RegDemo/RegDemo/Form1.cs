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
            RegTool.Init("RegDemo", "scimence");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegTool.CheckRegDoing(ToolLogic);
        }

        public void ToolLogic()
        {
            MessageBox.Show("success，执行此处逻辑！！！");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://www.scimence.club/PayFor/SDK.aspx";
            System.Diagnostics.Process.Start(url);
        }
    }
}
