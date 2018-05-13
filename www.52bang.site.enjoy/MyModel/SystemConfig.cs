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
        public static string ResourceApi = "http://www.vipjiexi.com/yun.php?url=";
        //指定特殊关键词，给与的回复信息
        public static Hashtable keywords = new Hashtable();
        //可解析的视频平台
        public static List<string> CanParsePlatform = new List<string>();

        public static string msgWhenErr = "小主，我出问题啦，过会再为你服务";
        //分享的视频链接不能被转换
        public static string NoConvertPlatform = "不支持该链接转换";
        static SystemConfig(){

            keywords.Add("谢谢", "不客气");

            CanParsePlatform.Add("youku");
        }
    }
}
