using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


// 备注： SciReg.dll引用
// 下载：https://gitee.com/scimence/sciTools/raw/master/DLL/SciReg.dll
// 项目->添加引用->浏览->选取SciReg.dll文件

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
            SciReg.RegTool.Init("RegDemo", "scimence");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SciReg.RegTool.CheckRegDoing(ToolLogic);
        }

        public void ToolLogic()
        {
            MessageBox.Show("success，执行此处逻辑！！！");
        }
    }
}
