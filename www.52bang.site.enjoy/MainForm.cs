using Newbe.CQP.Framework;
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

        private ICoolQApi coolQApiForTest;

        public MainForm()
        {
            InitializeComponent();
        }

        internal void setCoolQApi(ICoolQApi coolQApi)
        {
            coolQApiForTest = coolQApi;
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


        public void displayMsg2(string msg)
        {
            this.listBox1.Items.Add(msg);
        }
        //发送私人信息
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox2.Text))
            {
                MessageBox.Show("qq号不能为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.textBox3.Text))
            {
                MessageBox.Show("发送消息不能为空");
                return;
            }

            int content = coolQApiForTest.SendPrivateMsg(Convert.ToInt64(this.textBox2.Text), this.textBox3.Text);
            textBox4.Text = ""+content;
            MessageBox.Show("发送完成");
        }
        //获取群列表
        private void button2_Click(object sender, EventArgs e)
        {
            string content = coolQApiForTest.CQ_getGroupList();
            textBox4.Text = content;
            MessageBox.Show("执行完成");
        }
        //获取应用目录
        private void button3_Click(object sender, EventArgs e)
        {
            string content = coolQApiForTest.GetAppDirectory();
            textBox4.Text = content;
            MessageBox.Show("执行完成");    
        }
        //获取群成员信息
        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox6.Text))
            {
                MessageBox.Show("目标群不能为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.textBox5.Text))
            {
                MessageBox.Show("目标qq不能为空");
                return;
            }

            string content = coolQApiForTest.GetGroupMemberInfo(Convert.ToInt64(this.textBox6.Text), Convert.ToInt64(this.textBox5.Text));
            textBox4.Text = content;
            MessageBox.Show("执行完成");
        }
        //取群成员列表
        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox8.Text))
            {
                MessageBox.Show("目标群不能为空");
                return;
            }

            string content = coolQApiForTest.GetGroupMemberListAsString(Convert.ToInt64(this.textBox8.Text));
            textBox4.Text = content;
            MessageBox.Show("执行完成");
        }
        //获取登陆昵称
        private void button6_Click(object sender, EventArgs e)
        {
            string content = coolQApiForTest.GetLoginNick();
            textBox4.Text = content;
            MessageBox.Show("执行完成");
        }
        //获取登陆qq
        private void button7_Click(object sender, EventArgs e)
        {
            string content = ""+coolQApiForTest.GetLoginQQ();
            textBox4.Text = content;
            MessageBox.Show("执行完成");
        }
        //接受语音消息
        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox9.Text))
            {
                MessageBox.Show("收到消息中的语音文件名不能为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.textBox7.Text))
            {
                MessageBox.Show("应用所需的格式不能为空");
                return;
            }

            string content = coolQApiForTest.GetRecord(this.textBox9.Text, this.textBox7.Text);
            textBox4.Text = content;
            MessageBox.Show("执行完成");

        }
        //获取陌生人信息，支持缓存
        private void button9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox11.Text))
            {
                MessageBox.Show("目标qq不能为空");
                return;
            }

            string content = coolQApiForTest.GetStrangerInfo(Convert.ToInt64(this.textBox11.Text), (radioButton1.Checked ? true : false));
            textBox4.Text = content;
            MessageBox.Show("执行完成");
        }
        //发送讨论组信息
        private void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox12.Text))
            {
                MessageBox.Show("目标讨论组号不能为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.textBox10.Text))
            {
                MessageBox.Show("消息内容不能为空");
                return;
            }
            int content = coolQApiForTest.SendDiscussMsg(Convert.ToInt64(this.textBox12.Text), this.textBox10.Text);
            textBox4.Text = ""+content;
            MessageBox.Show("执行完成");

        }
        //发送群消息
        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox14.Text))
            {
                MessageBox.Show("目标群号不能为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.textBox13.Text))
            {
                MessageBox.Show("消息内容不能为空");
                return;
            }
            int content = coolQApiForTest.SendGroupMsg(Convert.ToInt64(this.textBox14.Text), this.textBox13.Text);
            textBox4.Text = "" + content;
            MessageBox.Show("执行完成");
        }
        //退出讨论组
        private void button12_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox15.Text))
            {
                MessageBox.Show("目标讨论组不能为空");
                return;
            }
            int content = coolQApiForTest.SetDiscussLeave(Convert.ToInt64(this.textBox15.Text));
            textBox4.Text = "" + content;
            MessageBox.Show("执行完成");

        }
        //致命错误
        private void button13_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox16.Text))
            {
                MessageBox.Show("错误信息不能为空");
                return;
            }

            int content = coolQApiForTest.SetFatal(this.textBox16.Text);
            textBox4.Text = "" + content;
            MessageBox.Show("执行完成");
        }
        //设置群管理员
        private void button14_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox18.Text))
            {
                MessageBox.Show("目标群号不能为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.textBox17.Text))
            {
                MessageBox.Show("目标QQ不能为空");
                return;
            }
            int content = coolQApiForTest.SetGroupAdmin(Convert.ToInt64(this.textBox18.Text), Convert.ToInt64(this.textBox17.Text), radioButton3.Checked ? true:false);
            textBox4.Text = "" + content;
            MessageBox.Show("执行完成");
        }
        //设置群匿名
        private void button15_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox19.Text))
            {
                MessageBox.Show("目标群号不能为空");
                return;
            }

            int content = coolQApiForTest.SetGroupAnonymous(Convert.ToInt64(this.textBox19.Text), radioButton6.Checked ? true : false);
            textBox4.Text = "" + content;
            MessageBox.Show("执行完成");
        }
        //解禁群员
        private void button16_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox21.Text))
            {
                MessageBox.Show("目标群号不能为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.textBox20.Text))
            {
                MessageBox.Show("目标QQ不能为空");
                return;
            }
            int content = coolQApiForTest.SetGroupBan(Convert.ToInt64(this.textBox21.Text), Convert.ToInt64(this.textBox20.Text), Convert.ToInt64(numericUpDown1.Value));
            textBox4.Text = "" + content;
            MessageBox.Show("执行完成");

        }
        //设置群成员名片
        private void button17_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox23.Text))
            {
                MessageBox.Show("目标群号不能为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.textBox22.Text))
            {
                MessageBox.Show("目标QQ不能为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.textBox24.Text))
            {
                MessageBox.Show("新名片不能为空");
                return;
            }
            int content = coolQApiForTest.SetGroupCard(Convert.ToInt64(this.textBox23.Text), Convert.ToInt64(this.textBox22.Text), this.textBox24.Text);
            textBox4.Text = "" + content;
            MessageBox.Show("执行完成");
        }
        //踢出群员
        private void button18_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox27.Text))
            {
                MessageBox.Show("目标群号不能为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.textBox26.Text))
            {
                MessageBox.Show("目标QQ不能为空");
                return;
            }

            int content = coolQApiForTest.SetGroupKick(Convert.ToInt64(this.textBox27.Text), Convert.ToInt64(this.textBox26.Text), radioButton8.Checked ? true:false);
            textBox4.Text = "" + content;
            MessageBox.Show("执行完成");
        }
        //设置群成员专属头衔
        private void button19_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox28.Text))
            {
                MessageBox.Show("目标群号不能为空");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.textBox25.Text))
            {
                MessageBox.Show("目标QQ不能为空");
                return;
            }
            int content = coolQApiForTest.SetGroupSpecialTitle(Convert.ToInt64(this.textBox28.Text), Convert.ToInt64(this.textBox25.Text), this.textBox29.Text,Convert.ToInt64(this.numericUpDown2.Value));
            textBox4.Text = "" + content;
            MessageBox.Show("执行完成");

        }
        //全群禁言
        private void button20_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.textBox30.Text))
            {
                MessageBox.Show("目标群号不能为空");
                return;
            }

            int content = coolQApiForTest.SetGroupWholeBan(Convert.ToInt64(this.textBox30.Text), radioButton10.Checked ? true:false);
            textBox4.Text = "" + content;
            MessageBox.Show("执行完成");
        }

        
    }
}
