using DeepNaiWorkshop_6001.MyTool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using www_52bang_site_enjoy.MyModel;
using www_52bang_site_enjoy.MyTool;

namespace Plugin_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg = "哈哈";
            //查看信息是否是4位数字
            try
            {
                if (msg.Length == 4)
                {
                    int kl = Convert.ToInt32(msg);
                    Console.WriteLine(kl);
                }
                //CoolQApi.SendPrivateMsg(fromQQ, "此口令资源不存在");
                //return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                
                Console.WriteLine(13);
            }
            catch (Exception e2fs)//如果不是4位数字
            {
                Console.WriteLine(e2fs);
                Console.WriteLine(2);
            }
            //return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
            Console.WriteLine(3);
        }

        private long AddTime(double money)
        {
            if (money == 3)
            {
                return 7L * 24 * 60 * 60 * 1000;
            }
            else if (money == 10)
            {
                return 30L * 24 * 60 * 60 * 1000;
            }
            else
            {
                return 0L;
            }

        }
    }
}
