using Newbe.CQP.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using www_52bang_site_enjoy.MyModel;
using www_52bang_site_enjoy.MyTool;

namespace www_52bang_site_enjoy.enjoy
{
    public class MyPlugin: PluginBase
    {
        private MainForm mainForm;
        public MyPlugin(ICoolQApi coolQApi) : base(coolQApi)
        {
            mainForm = new MainForm();
            mainForm.Show();

            string str = Assembly.GetExecutingAssembly().CodeBase;

            mainForm.displayMsg2(str);
            int start = 8;// 去除file:///  
            int end = str.LastIndexOf('/');// 去除文件名xxx.dll及文件名前的/  
            str = str.Substring(start, end - start);
            mainForm.displayMsg2(str);
            str = Path.GetDirectoryName(str);
            mainForm.displayMsg2(str);
        }
        /// <summary>
        /// AppId需要与程序集名称相同
        /// </summary>
        public override string AppId => "www.52bang.site.enjoy";
        /// <summary>
        /// 监听私聊事件
        /// </summary>
        /// <param name="subType"></param>
        /// <param name="sendTime"></param>
        /// <param name="fromQQ"></param>
        /// <param name="msg"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        public override int ProcessPrivateMessage(int subType, int sendTime, long fromQQ, string msg, int font)
        {
            //用户发来的消息日志
            MyLogUtil.WriteQQDialogueLog(fromQQ, msg);
            //获取的消息
            mainForm.displayMsg(msg);

            /*
             * [CQ:rich,url=https://v.youku.com/v_show/id_XMzU5NDQzNzIxNg==.html?sharefrom=iphone,text=极限挑战 第四季20180510 会员解读版第2期:演技滑铁卢!内含男人帮最想删的片段 ]
             */

            //说明用户回复的是指定的关键词
            Hashtable keywords = SystemConfig.keywords;
            string keywordsValue = (string)keywords[msg];
            if (!string.IsNullOrWhiteSpace(keywordsValue))
            {
                //给用户回复的信息日志
                MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, keywordsValue);
                //发送消息
                CoolQApi.SendPrivateMsg(fromQQ, keywordsValue);
                return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
            }

            //判断是否是指定的视频平台链接
            if (MyLinkCoverter.JugePlatform(msg))
            {
                MyResponse<MovieInfo> myResponse = MyLinkCoverter.CovertInSuoIm(msg);
                if (myResponse.Code == 0)//获取成功了
                {
                    //给用户回复的信息日志
                    MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, myResponse.Msg.MovieName + " " + myResponse.Msg.Url);
                    CoolQApi.SendPrivateMsg(fromQQ,  myResponse.Msg.MovieName + " " + myResponse.Msg.Url);
                    return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                }
                else
                {
                    //给用户回复的信息日志
                    MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, SystemConfig.NoConvertPlatform);
                    CoolQApi.SendPrivateMsg(fromQQ,SystemConfig.NoConvertPlatform);
                    return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                }
                
            }

            //收到转账时消息体
            //&#91;转账&#93; 0.01元转账需收款，请使用手机QQ查看。
            //红包没有消息
            return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
        }

        
    }
}
