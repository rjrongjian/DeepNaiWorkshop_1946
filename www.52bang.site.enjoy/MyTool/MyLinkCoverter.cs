using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using www_52bang_site_enjoy.MyModel;

namespace www_52bang_site_enjoy.MyTool
{
    /// <summary>
    /// 各大影视平台的链接转换器
    /// </summary>
    public class MyLinkCoverter
    {
        
        /// <summary>
        /// 转长链接
        /// </summary>
        /// <param name="cqMsg">返回的消息[CQ: rich, url=https://v.youku.com/v_show/id_XMzU5NDQzNzIxNg==.html?sharefrom=iphone,text=极限挑战 第四季20180510 会员解读版第2期:演技滑铁卢!内含男人帮最想删的片段 ]</param>
        /// <rturns></returns>
        public static MovieInfo Covert(string cqMsg)
        {
            //解析酷q消息体中的url链接
            MovieInfo movie = parseMsg(cqMsg);
            movie.OriginalUrl = SystemConfig.ResourceApi + movie.Url;
            //组成可观看的视频地址
            movie.Url = MyUrlTool.UrlEncode(SystemConfig.ResourceApi + movie.Url);
            
            
            return movie;
        }

        private static MovieInfo parseMsg(string cqMsg)
        {

            string movieName = cqMsg.Substring(cqMsg.IndexOf(",text="), cqMsg.IndexOf("]")- cqMsg.IndexOf(",text="));
            movieName = movieName.Replace(",text=","");
            string url = cqMsg.Substring(cqMsg.IndexOf("url="), cqMsg.IndexOf(",text=")- cqMsg.IndexOf("url="));
            url = url.Replace("url=", "");
            MovieInfo movieInfo = new MovieInfo(movieName, url, url);
            return movieInfo;
        }

        /// <summary>
        /// 判断用户发过来的信息是否是分享的视频的消息体
        /// </summary>
        /// <param name="cqMsg">返回的消息[CQ: rich, url=https://v.youku.com/v_show/id_XMzU5NDQzNzIxNg==.html?sharefrom=iphone,text=极限挑战 第四季20180510 会员解读版第2期:演技滑铁卢!内含男人帮最想删的片段 ]</param>
        /// <rturns></returns>
        public static bool JugePlatform(string cqMsg)
        {
            if((cqMsg.Contains("CQ: rich") || cqMsg.Contains("CQ:rich"))&&cqMsg.Contains("url=")&&cqMsg.Contains("text="))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 例用suo.im转短链接
        /// </summary>
        /// <param name="cqMsg"></param>
        /// <returns></returns>
        public static MyResponse<MovieInfo> CovertInSuoIm(string cqMsg)
        {
            //不做是否是支持的转换平台
            /*
            bool isContain = false;
            foreach(string canParsePlatform in SystemConfig.CanParsePlatform)
            {
                if (cqMsg.Contains(canParsePlatform))
                {
                    isContain = true;
                    break;
                }
            }

            if (!isContain)
            {

                return new MyResponse<MovieInfo>(1,null);
            }
            */

            MovieInfo movieInfo = Covert(cqMsg);
            //转成短网址，例用suo.im
            movieInfo.Url = MyUrlTool.HttpGet("http://suo.im/api.php?url=" + movieInfo.Url, "");
            

            MyResponse<MovieInfo> myResponse = new MyResponse<MovieInfo>(0, movieInfo);
            return myResponse;
        }
        /// <summary>
        /// 将长连接转为短链接
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string CovertUrlInSuoIm(string url)
        {
            //转成短网址，例用suo.im
            string result = MyUrlTool.HttpGet("http://suo.im/api.php?url=" + url, "");
            return result;
        }


    }
}
