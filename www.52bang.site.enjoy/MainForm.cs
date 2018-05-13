using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using www_52bang_site_enjoy.enjoy;

namespace www_52bang_site_enjoy
{
    public partial class MainForm : Form
    {
        private int subType;
        private int sendTime;
        private long fromQQ;
        private string msg;
        private int font;
        private MyPlugin myPlugin;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(int subType, int sendTime, long fromQQ, string msg, int font, enjoy.MyPlugin myPlugin)
        {
            this.subType = subType;
            this.sendTime = sendTime;
            this.fromQQ = fromQQ;
            this.msg = msg;
            this.font = font;
            this.myPlugin = myPlugin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        internal void displayMsg(string msg)
        {
            this.textBox1.Text = msg;
        }
    }
}
