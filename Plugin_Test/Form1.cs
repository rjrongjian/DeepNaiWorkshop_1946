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
            //MyResponse<MovieInfo> myResponse = MyLinkCoverter.CovertInSuoIm("[CQ: rich, url=https://v.youku.com/v_show/id_XMzU5NDQzNzIxNg==.html?sharefrom=iphone,text=极限挑战 第四季20180510 会员解读版第2期:演技滑铁卢!内含男人帮最想删的片段 ]");

            String msg = "[CQ: rich, url=https://v.youku.com/v_show/id_XMzU5NDQzNzIxNg==.html?sharefrom=iphone,text=极限挑战 第四季20180510 会员解读版第2期:演技滑铁卢!内含男人帮最想删的片段 ]";

            //说明用户回复的是指定的关键词
            Hashtable keywords = SystemConfig.keywords;
            string keywordsValue = (string)keywords[msg];
            if (!string.IsNullOrWhiteSpace(keywordsValue))
            {
                //CoolQApi.SendPrivateMsg(fromQQ, keywordsValue);
                //return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                Console.WriteLine("进入指定关键词列表");
                return;
            }


            //判断是否是指定的视频平台链接
            if (MyLinkCoverter.JugePlatform(msg))
            {
                MyResponse<MovieInfo> myResponse = MyLinkCoverter.CovertInSuoIm(msg);
                if (myResponse.Code == 0)//获取成功了
                {
                    /*
                    CoolQApi.SendPrivateMsg(fromQQ, myResponse.Msg.MovieName + " " + myResponse.Msg.Url);
                    CoolQApi.SendPrivateMsg(fromQQ, myResponse.Msg.MovieName + " " + myResponse.Msg.OriginalUrl);
                    CoolQApi.SendPrivateMsg(fromQQ, sendTime + "");
                    return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                    */
                    Console.WriteLine("转视频平台："+ myResponse.Msg.Url);
                    Console.WriteLine("转视频平台：" + myResponse.Msg.OriginalUrl);
                    return;
                }
                else
                {
                    //CoolQApi.SendPrivateMsg(fromQQ, SystemConfig.NoConvertPlatform);
                    //return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                    Console.WriteLine(SystemConfig.NoConvertPlatform);
                    return;
                }

            }



        }
    }
}
