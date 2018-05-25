using Newbe.CQP.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace www_52bang_site_enjoy.MyModel
{
    public class SystemConfig
    {
        //75个视频网站地址，http://www.zuanke8.com/archiver/?tid-4515701.html
        //public static string ResourceApi = "http://www.vipjiexi.com/yun.php?url=";
        //public static string ResourceApi = "http://mlxztz.com/player.php?url=";
        public static string ResourceApi = "http://www.52bang.site/dyxf/parse.html?url=";
        //指定特殊关键词，给与的回复信息
        public static Hashtable keywords = new Hashtable();
        //可解析的视频平台
        public static List<string> CanParsePlatform = new List<string>();

        public static string msgWhenErr = "小主，我出问题啦，过会再为你服务";
        //用户添加时的回复语，附加表情，卖萌+微笑
        public static string MsgWhenFriendsAdded = "主人，将优酷、腾讯、爱奇艺等vip视频分享给我，可直接观看哦！！"+CoolQCode.Expression(62)+ CoolQCode.Expression(14);
        //分享的视频链接不能被转换
        public static string NoConvertPlatform = "不支持该链接转换";

        public static int MoneyForWeekPay = 4;//7天价格
        public static int MoneyForMonthPay = 15;//30天价格

        static SystemConfig(){

            keywords.Add("谢谢", "不客气");

            CanParsePlatform.Add("youku");
        }
    }
}
