using Newbe.CQP.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            /*
             * [CQ:rich,url=https://v.youku.com/v_show/id_XMzU5NDQzNzIxNg==.html?sharefrom=iphone,text=极限挑战 第四季20180510 会员解读版第2期:演技滑铁卢!内含男人帮最想删的片段 ]
             */

            //说明用户回复的是指定的关键词
            Hashtable keywords = SystemConfig.keywords;
            string keywordsValue = (string)keywords[msg];
            if (!string.IsNullOrWhiteSpace(keywordsValue))
            {
                CoolQApi.SendPrivateMsg(fromQQ, keywordsValue);
                return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
            }

            //判断是否是指定的视频平台链接
            if (MyLinkCoverter.JugePlatform(msg))
            {
                MyResponse<MovieInfo> myResponse = MyLinkCoverter.CovertInSuoIm(msg);
                if (myResponse.Code == 0)//获取成功了
                {
                    CoolQApi.SendPrivateMsg(fromQQ, sendTime + " " + myResponse.Msg.MovieName + " " + myResponse.Msg.Url);
                    return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                }
                else
                {
                    CoolQApi.SendPrivateMsg(fromQQ,SystemConfig.NoConvertPlatform);
                    return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                }
                
            }
            
            //使用CoolQApi将信息回发给发送者
            //CoolQApi.SendPrivateMsg(fromQQ, "头号玩家电影 http://suo.im/4zgAuL");
            
            return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
        }

        
    }
}
