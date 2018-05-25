using DeepNaiWorkshop_6001.MyTool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private void button2_Click(object sender, EventArgs e)
        {
           List<KunyunInfo> list =  KuYunSearch.Search(this.textBox1.Text);
            Console.WriteLine("找到的源------");
            foreach(KunyunInfo k in list)
            {
                Console.WriteLine(k.name + " " + k.url);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string content = "<td height=\"20\" align=\"left\"><a href=\"/detail/?4769.html\" target=\"_blank\">复仇者联盟2：奥创纪元</a></td>";
            string regexStr = "<td height=\"20\" align=\"left\"><a href=\"(.*)\" target =\"_blank\">(.*)</a></td>";
            Regex regex = new Regex(content);
            Match match = regex.Match(content);
            if (match.Success)
            {
                Console.WriteLine(match.Groups[1] + "    " + match.Groups[2]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string str = "搜fds";
            Console.WriteLine(str.Substring(1));
        }


        

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="encodeType">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string Base64Decode(Encoding encodeType, string result)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encodeType.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CoolQApiExtend.ParseGroupList("AAAAAQASAAAAACz8gXYACLXn07DQxbfi");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //CoolQApiExtend.ParseGroupMemberList("AAAAAwBGAAAAACz8gXYAAAAADDOzowAM0MW34tb6ytbQod/3AAAAAAABAAAAEwAAWvvq5lr9PesAAAAAAAMAAAAAAAAAAAAAAAAAAAA+AAAAACz8gXYAAAAADiebCgAEMjAxNgAAAAAAAAAAABsAAFr9aV5a/XdoAAAAAAABAAAAAAAAAAAAAAAAAAAAQgAAAAAs/IF2AAAAAGFd7jAACMnuxM65pLe7AAAAAAAAAAAAAAAAWv0+HFr9aV4AAAAAAAEAAAAAAAAAAAAAAAAAAA==");
        }
    }
}
