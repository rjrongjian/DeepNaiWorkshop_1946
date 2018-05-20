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
            msg = "&#91;转账&#93; 1.00元已转账成功，请使用手机QQ查看。";


            Hashtable keywords = SystemConfig.keywords;
            string keywordsValue = (string)keywords[msg];
            if (!string.IsNullOrWhiteSpace(keywordsValue))
            {
                //给用户回复的信息日志
                //MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, keywordsValue);
                //发送消息
                //CoolQApi.SendPrivateMsg(fromQQ, keywordsValue);
                //return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
            }

            //判断是否是指定的视频平台链接
            if (MyLinkCoverter.JugePlatform(msg))
            {
                MyResponse<MovieInfo> myResponse = MyLinkCoverter.CovertInSuoIm(msg);
                if (myResponse.Code == 0)//获取成功了
                {
                    //给用户回复的信息日志
                    //MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, myResponse.Msg.MovieName + " " + myResponse.Msg.Url);
                   // CoolQApi.SendPrivateMsg(fromQQ, "主人，这是你的观影地址：" + " " + myResponse.Msg.Url + "，更多好玩的电影跟班，关注微信公众号[电影信封]");
                   // return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                }
                else
                {
                    //给用户回复的信息日志
                    //MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, SystemConfig.NoConvertPlatform);
                    //CoolQApi.SendPrivateMsg(fromQQ, SystemConfig.NoConvertPlatform);
                   // return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                }

            }
            //判断是否收到转账
            if (!string.IsNullOrWhiteSpace(msg) && msg.Contains("&#91;转账&#93;") && (msg.Contains("元已转账成功，请使用手机QQ查看。")||msg.Contains("元转账需收款，请使用手机QQ查看。")))
            {
                //try
                //{
                    string value = msg.Replace("&#91;转账&#93;", "");
                    value = value.Replace("元已转账成功，请使用手机QQ查看。", "");
                    value = value.Replace("元转账需收款，请使用手机QQ查看。", "");
                    value = value.Trim();

                    double money = Convert.ToDouble(value);
                    //mainForm.displayMsg2("看看金额：" + (money >= 1));
                    if (money >= 1)
                    //if(money>0)
                    {
                        String urlEncode = MyUrlTool.UrlEncode("http://www.52bang.site/dyxf/vip.html?url=http://hao.czybjz.com/20180507/EOxP9Flv/index.m3u8");
                        //复联3
                        String shortUrl = MyLinkCoverter.CovertUrlInSuoIm(urlEncode);
                        //CoolQApi.SendPrivateMsg(fromQQ, "主人，这是你的观影地址，" + shortUrl + "，感谢支持！更多好玩的电影跟班，关注微信公众号[电影信封]");
                        //MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, "主人，这是你的观影地址，" + shortUrl + "，感谢支持！更多好玩的电影跟班，关注微信公众号[电影信封]");
                        //return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                    }
                    else
                    {
                        //MyLogUtil.WriteZhuanZhangLog(fromQQ, "用户转账额度不足，" + money);
                        //MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, "用户转账额度不足，" + money);
                       // CoolQApi.SendPrivateMsg(fromQQ, "主人，你转账的金额不足，1天后会自动退还，更多好玩的电影跟班，关注微信公众号[电影信封]");
                        //return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                    }
                //}
                //catch (Exception e)
                //{
                    //解析转账出现问题

                    //MyLogUtil.WriteZhuanZhangLog(fromQQ, "转账失败" + e);
                    //MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, "主人，不好意思，我现在生病啦，不能为你提供资源链接，转账金额1天后自动退还，更多好玩的电影跟班，关注微信公众号[电影信封]");
                    //CoolQApi.SendPrivateMsg(fromQQ, "主人，不好意思，我现在生病啦，不能为你提供资源链接，转账金额1天后自动退还，更多好玩的电影跟班，关注微信公众号[电影信封]");
                    //return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                //}

            }
            //收到转账时消息体
            //&#91;转账&#93; 0.01元转账需收款，请使用手机QQ查看。
            //红包没有消息
            //return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);


        }
    }
}
