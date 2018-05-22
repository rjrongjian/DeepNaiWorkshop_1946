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
using www_52bang_site_enjoy.Service;

namespace www_52bang_site_enjoy.enjoy
{
    public class MyPlugin: PluginBase
    {
        private MainForm mainForm;
        private SystemConfigJson systemConfigJson;
        
        public MyPlugin(ICoolQApi coolQApi) : base(coolQApi)
        {
            

            //获取本地配置
            systemConfigJson = MySystemUtil.GetSystemConfigJson();
            if (systemConfigJson == null)
            {

                MessageBox.Show("不能加载"+ MySystemUtil.GetSystemConfigJsonPath()+",请重启");//程序即将退出

            }
            else
            {
                mainForm = new MainForm();
                mainForm.Show();
                mainForm.setCoolQApi(CoolQApi);
            }
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
            try { 
                mainForm.displayMsg2("处理私聊消息：" + subType + "," + sendTime + "," + msg+","+font);
                MemberService memberService = new MemberService();

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
                        CoolQApi.SendPrivateMsg(fromQQ,  "主人，这是你的观影地址：" + " " + myResponse.Msg.Url+ "，由于需要加载影片，请耐心等待，如果不能播放，请刷新或换浏览器，更多好玩的电影跟班，关注微信公众号[电影信封]");
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
                //判断是否收到转账
                //充值会员
                if (!string.IsNullOrWhiteSpace(msg) && msg.Contains("&#91;转账&#93;") && (msg.Contains("元已转账成功，请使用手机QQ查看。") || msg.Contains("元转账需收款，请使用手机QQ查看。")))
                {
                    try {
                        string value = msg.Replace("&#91;转账&#93;", "");
                        value = value.Replace("元已转账成功，请使用手机QQ查看。", "");
                        value = value.Replace("元转账需收款，请使用手机QQ查看。", "");
                        value = value.Trim();
                    
                        double money = Convert.ToDouble(value);
                        if (money == 3||money==10)
                        {
                            /*
                            String urlEncode = MyUrlTool.UrlEncode("http://www.52bang.site/dyxf/parse.html?url=http://hao.czybjz.com/20180507/EOxP9Flv/index.m3u8");
                            //复联3
                            String shortUrl = MyLinkCoverter.CovertUrlInSuoIm(urlEncode);
                            CoolQApi.SendPrivateMsg(fromQQ, "主人，这是你的观影地址，"+ shortUrl+"，由于需要加载影片，请耐心等待，如果不能播放，请刷新或换浏览器，感谢支持！更多好玩的电影跟班，关注微信公众号[电影信封]");
                            MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, "主人，这是你的观影地址，" + shortUrl + "，感谢支持！更多好玩的电影跟班，关注微信公众号[电影信封]");
                            return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                            */
                        
                            Member member = memberService.Recharge(money,fromQQ);

                            MyLogUtil.WriteZhuanZhangLog(fromQQ, "用户充值" + money);
                            CoolQApi.SendPrivateMsg(fromQQ, "充值成功，会员到期时间："+member.DateDesp+"，QQ回复“会员”，查看会员到期日");
                            MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, "充值成功，会员到期时间：" + member.DateDesp + "，QQ回复“会员”，查看会员到期日");
                            CoolQApi.SendPrivateMsg(fromQQ, "QQ回复“资源”或微信关注[电影信封]，获得观看会员资源");
                            MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, "QQ回复“资源”或微信关注[电影信封]，获得观看会员资源");
                            return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                        }
                        else
                        {
                            MyLogUtil.WriteZhuanZhangLog(fromQQ, "用户转账额度不符合，"+ money);
                            MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, "用户转账额度不符合，" + money);
                            CoolQApi.SendPrivateMsg(fromQQ, "主人，目前只支持3、10元的充值金额，更多好玩的电影跟班，关注微信公众号[电影信封]");
                            return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                        }
                    }catch(Exception e)
                    {
                        //解析转账出现问题
                        MyLogUtil.ErrToLog(fromQQ, "转账失败，原因：" + e);

                        MyLogUtil.WriteZhuanZhangLog(fromQQ, "转账失败"+e);
                        MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, "主人，不好意思，我现在生病啦，不能为你提供资源链接，转账金额1天后自动退还，更多好玩的电影跟班，关注微信公众号[电影信封]");
                        CoolQApi.SendPrivateMsg(fromQQ, "主人，不好意思，我现在生病啦，不能为你提供资源链接，转账金额1天后自动退还，更多好玩的电影跟班，关注微信公众号[电影信封]");
                        return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                    }

                }
                //会员到期日
                if ("会员".Equals(msg))
                {
                    Member member = memberService.GetMemberDate(fromQQ);
                    if (member.Type == 3)// 1不是会员 2 会员过期 3 正常会员
                    {
                        CoolQApi.SendPrivateMsg(fromQQ, "会员过期时间："+member.DateDesp);
                        CoolQApi.SendPrivateMsg(fromQQ, "会员价格：3元-7天，10元30天，请转账给此QQ，进行充值（不收红包）");
                    }
                    else if(member.Type == 2)
                    {
                        CoolQApi.SendPrivateMsg(fromQQ, "你的会员已过期，"+member.DateDesp+"，会员价格：3元-7天，10元30天，请转账给此QQ，进行充值（不收红包）");
                    }
                    else
                    {
                        CoolQApi.SendPrivateMsg(fromQQ, "你还不是会员，会员价格：3元-7天，10元30天，请转账给此QQ，进行充值（不收红包）");
                    }
                    return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                }
                //资源列表
                if ("资源".Equals(msg))
                {
                    //付费电影资源列表
                    CoolQApi.SendPrivateMsg(fromQQ,"查看所有资源资源码："+systemConfigJson.ResourceUrl);
                    return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);

                }

                //解析分享过来的是不是指定平台的链接(拦截形如xxxhttp://www.baidu.comxxx,但是有些新闻也是这种格式的)
                MyResponse<string> sharedUrl = null;
                sharedUrl = MyLinkCoverter.ParsePlatform(msg);
                if (sharedUrl.Code == 0)//正常链接
                {
                    sharedUrl.Msg = MyLinkCoverter.CovertUrlInSuoIm(sharedUrl.Msg, true);
                    //给用户回复的信息日志
                    MyLogUtil.WriteQQDialogueLogOfMe(fromQQ, "主人，这是你的观影地址：" + " " + sharedUrl.Msg + "，由于需要加载影片，请耐心等待，如果不能播放，请刷新或换浏览器，更多好玩的电影跟班，关注微信公众号[电影信封]");
                    CoolQApi.SendPrivateMsg(fromQQ, "主人，这是你的观影地址：" + " " + sharedUrl.Msg + "，由于需要加载影片，请耐心等待，如果不能播放，请刷新或换浏览器，更多好玩的电影跟班，关注微信公众号[电影信封]");
                    CoolQApi.SendPrivateMsg(fromQQ, "请确保发送给我的是主流视频平台的http或https链接，不要带其他信息，否则不能正常观看呦");
                    return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                }

                //付费资源KeyNotFoundException
                //目前资源识别是4位数字
                ResourceInfo resourceInfo = null;
                try
                {
                    resourceInfo = (ResourceInfo)systemConfigJson.ResourceKl[msg];
                }
                catch (KeyNotFoundException ke)
                {
                    resourceInfo = null;

                }

                if (resourceInfo != null)
                {
                    Member member = memberService.GetMemberDate(fromQQ);
                    if (member.Type == 3)// 1不是会员 2 会员过期 3 正常会员
                    {
                        String url = "";
                        try { 
                            if (resourceInfo.Type == 2)////资源类型 1 链接直接使用 2 需使用优酷转vip的接口
                            {
                                url = SystemConfig.ResourceApi+resourceInfo.Url;
                            }
                            else
                            {
                                url = resourceInfo.Url;
                            }
                            url = MyLinkCoverter.CovertUrlInSuoIm(url);
                            CoolQApi.SendPrivateMsg(fromQQ, "《"+resourceInfo.Name+"》"+" "+MyLinkCoverter.CovertUrlInSuoIm(resourceInfo.Url));
                            return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);

                        }
                        catch(Exception e2)
                        {
                            CoolQApi.SendPrivateMsg(fromQQ, "小喵出现问题，请过会再来尝试");
                            return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                        }
                    
                    }
                    else if (member.Type == 2)
                    {
                        CoolQApi.SendPrivateMsg(fromQQ, "你的会员已过期，会员价格：3元-7天，10元30天，请转账给此QQ，进行充值（不收红包）");
                        return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                    }
                    else
                    {
                        CoolQApi.SendPrivateMsg(fromQQ, "你还不是会员，会员价格：3元-7天，10元30天，请转账给此QQ，进行充值（不收红包）");
                        return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                    }
                }
                else
                {

                    //查看信息是否是4位数字
                    try
                    {
                        if (msg.Length == 4)
                        {
                            //如果是四位数字
                            int kl = Convert.ToInt32(msg);
                            CoolQApi.SendPrivateMsg(fromQQ, "此口令资源不存在，更多资源电影码：" + systemConfigJson.ResourceUrl);
                            return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                        }
                        
                        
                        
                    }
                    catch (Exception e)//如果是4位非数字
                    {
                        CoolQApi.SendPrivateMsg(fromQQ, "此口令资源不存在，更多资源电影码：" + systemConfigJson.ResourceUrl);
                        return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
                    }
                    
                }

                //以上所有资源都不匹配

                //

                return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
            }
            catch(Exception e)
            {
                MyLogUtil.ErrToLog(fromQQ, "发生不被期待的异常："+e);
                return base.ProcessPrivateMessage(subType, sendTime, fromQQ, msg, font);
            }
        }

        /// <summary>
        /// 好友添加请求
        /// </summary>
        /// <param name="subType">事件类型。固定为1。</param>
        /// <param name="sendTime">	事件发生时间的时间戳。</param>
        /// <param name="fromQq">事件来源QQ。</param>
        /// <param name="msg">附言内容。</param>
        /// <param name="responseFlag">反馈标识(处理请求用)</param>
        /// <returns></returns>
        public override int ProcessAddFriendRequest(int subType, int sendTime, long fromQq, string msg, string responseFlag)
        {
            //有人添加好友，直接加为我的好友
            CoolQApi.SetFriendAddRequest(responseFlag,1, System.DateTime.Now.ToString("yyyyMMddHHmmss"));

            return base.ProcessAddFriendRequest(subType, sendTime, fromQq, msg, responseFlag);
        }

        /// <summary>
        /// 处理好友已添加事件（此事件监听不到）
        /// </summary>
        /// <param name="subType">事件类型。固定为1</param>
        /// <param name="sendTime">事件发生时间的时间戳</param>
        /// <param name="fromQq">事件来源QQ</param>
        /// <returns></returns>
        public override int ProcessFriendsAdded(int subType, int sendTime, long fromQq)
        {

            mainForm.displayMsg2("处理好友已添加事件：" + subType+","+sendTime+","+fromQq);
            //给用户回复的信息日志
            MyLogUtil.WriteQQDialogueLogOfMe(fromQq, SystemConfig.MsgWhenFriendsAdded);
            CoolQApi.SendPrivateMsg(fromQq, SystemConfig.MsgWhenFriendsAdded);

            return base.ProcessFriendsAdded(subType, sendTime, fromQq);
        }

        /// <summary>
        /// 处理讨论组消息
        /// </summary>
        /// <param name="subType">消息类型，目前固定为1</param>
        /// <param name="sendTime">消息发送时间的时间戳</param>
        /// <param name="fromDiscuss">消息来源讨论组号</param>
        /// <param name="fromQq">发送此消息的QQ号码</param>
        /// <param name="msg">消息内容</param>
        /// <param name="font">消息所使用字体</param>
        /// <returns></returns>
        public override int ProcessDiscussGroupMessage(int subType, int sendTime, long fromDiscuss, long fromQq, string msg, int font)
        {

            mainForm.displayMsg2("处理讨论组消息：" + subType + "," + sendTime + ","+ fromDiscuss+"," + fromQq+","+msg+","+font);
            return base.ProcessDiscussGroupMessage(subType, sendTime, fromDiscuss, fromQq, msg, font);
        }
        
        /// <summary>
        /// 处理群管理员变动事件
        /// </summary>
        /// <param name="subType">事件类型。1为被取消管理员，2为被设置管理员</param>
        /// <param name="sendTime">事件发生时间的时间戳</param>
        /// <param name="fromGroup">事件来源群号</param>
        /// <param name="target">被操作的QQ</param>
        /// <returns></returns>
        public override int ProcessGroupAdminChange(int subType, int sendTime, long fromGroup, long target)
        {
            mainForm.displayMsg2("处理群管理员变动事件：" + subType + "," + sendTime + "," + fromGroup + "," + target);

            return base.ProcessGroupAdminChange(subType, sendTime, fromGroup, target);
        }
        /// <summary>
        /// 处理群成员数量减少事件
        /// </summary>
        /// <param name="subType">事件类型。1为群员离开；2为群员被踢为；3为自己(即登录号)被踢</param>
        /// <param name="sendTime">事件发生时间的时间戳</param>
        /// <param name="fromGroup">事件来源群号</param>
        /// <param name="fromQq">事件来源QQ</param>
        /// <param name="target">被操作的QQ</param>
        /// <returns></returns>
        public override int ProcessGroupMemberDecrease(int subType, int sendTime, long fromGroup, long fromQq, long target)
        {
            mainForm.displayMsg2("处理群成员数量减少事件：" + subType + "," + sendTime + "," + fromGroup +","+fromQq+ "," + target);

            return base.ProcessGroupMemberDecrease(subType, sendTime, fromGroup, fromQq, target);
        }
        /// <summary>
        /// 处理群成员添加事件
        /// </summary>
        /// <param name="subType">事件类型。1为管理员已同意；2为管理员邀请</param>
        /// <param name="sendTime">事件发生时间的时间戳</param>
        /// <param name="fromGroup">事件来源群号</param>
        /// <param name="fromQq">事件来源QQ</param>
        /// <param name="target">被操作的QQ</param>
        /// <returns></returns>
        public override int ProcessGroupMemberIncrease(int subType, int sendTime, long fromGroup, long fromQq, long target)
        {
            mainForm.displayMsg2("处理群成员添加事件：" + subType + "," + sendTime + "," + fromGroup + "," + fromQq + "," + target);

            //成员添加后，进行@用户，外加欢迎与
            if (subType == 1)//账号为管理员，同意群员入群后触发
            {
                CoolQApi.SendGroupMsg(fromGroup, CoolQCode.At(target) + "欢迎加入我们的大家庭");
                mainForm.displayMsg2("---给用户发送欢迎语："+ CoolQCode.At(target) + "欢迎加入我们的大家庭");
            }
            else if(subType == 2)//群员邀请好友，好友同意并入群后触发
            {
                CoolQApi.SendGroupMsg(fromGroup, CoolQCode.At(fromQq)+"邀请"+ CoolQCode.At(target) + "加入我们的大家庭，热烈欢迎");
                mainForm.displayMsg2("---给用户发送欢迎语：" + CoolQCode.At(fromQq) + "邀请" + CoolQCode.At(target) + "加入我们的大家庭，热烈欢迎");
            }

            return base.ProcessGroupMemberIncrease(subType, sendTime, fromGroup, fromQq, target);
        }
        /// <summary>
        /// 处理群聊消息
        /// </summary>
        /// <param name="subType">消息类型，目前固定为1</param>
        /// <param name="sendTime">消息发送时间的时间戳</param>
        /// <param name="fromGroup">消息来源群号</param>
        /// <param name="fromQq">发送此消息的QQ号码</param>
        /// <param name="fromAnonymous">发送此消息的匿名用户</param>
        /// <param name="msg">消息内容</param>
        /// <param name="font">消息所使用字体</param>
        /// <returns></returns>
        public override int ProcessGroupMessage(int subType, int sendTime, long fromGroup, long fromQq, string fromAnonymous, string msg, int font)
        {
            mainForm.displayMsg2("处理群聊消息：" + subType + "," + sendTime + "," + fromGroup + "," + fromQq + "," + fromAnonymous+","+ msg+","+font);

            return base.ProcessGroupMessage(subType, sendTime, fromGroup, fromQq, fromAnonymous, msg, font);
        }
        /// <summary>
        /// 处理群文件上传事件
        /// </summary>
        /// <param name="subType"></param>
        /// <param name="sendTime"></param>
        /// <param name="fromGroup"></param>
        /// <param name="fromQq"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public override int ProcessGroupUpload(int subType, int sendTime, long fromGroup, long fromQq, string file)
        {
            mainForm.displayMsg2("处理群文件上传事件：" + subType + "," + sendTime + "," + fromGroup + "," + fromQq + "," + file);

            return base.ProcessGroupUpload(subType, sendTime, fromGroup, fromQq, file);
        }
        /// <summary>
        /// 处理加群请求（有加群请求）
        /// </summary>
        /// <param name="subType">请求类型。1为他人申请入群；2为自己(即登录号)受邀入群</param>
        /// <param name="sendTime">请求发送时间戳</param>
        /// <param name="fromGroup">要加入的群的群号</param>
        /// <param name="fromQq">发送此请求的QQ号码</param>
        /// <param name="msg">附言内容</param>
        /// <param name="responseMark">用于处理请求的标识</param>
        /// <returns></returns>
        public override int ProcessJoinGroupRequest(int subType, int sendTime, long fromGroup, long fromQq, string msg, string responseMark)
        {
            mainForm.displayMsg2("处理加群请求：" + subType + "," + sendTime + "," + fromGroup + "," + fromQq + ","+msg+"," + responseMark);

            //自动加群处理
            //TODO 暂时屏蔽
            //CoolQApi.SetGroupAddRequest(responseMark, CoolQAddGroupRequestType.Normal, CoolQRequestResult.Allow);//请求通过
            mainForm.displayMsg2("---发起自动加群处理SetGroupAddRequest：" + responseMark + "," + CoolQAddGroupRequestType.Normal + "," + CoolQRequestResult.Allow);

            return base.ProcessJoinGroupRequest(subType, sendTime, fromGroup, fromQq, msg, responseMark);
        }

    }
}
