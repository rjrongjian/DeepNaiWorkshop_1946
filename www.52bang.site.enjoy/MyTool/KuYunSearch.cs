﻿using HttpCodeLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using www_52bang_site_enjoy.MyModel;

namespace www_52bang_site_enjoy.MyTool
{
    public class KuYunSearch
    {

        public static List<KunyunInfo> Search(string key)
        {
            string detailPre = "http://kuyunzy.cc";
            if (string.IsNullOrWhiteSpace(key))
            {
                return new List<KunyunInfo>();
            }
            List<KunyunInfo> list = new List<KunyunInfo>();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("searchword", key);
            string content = HttpPost("http://kuyunzy.cc/search.asp", dic);
            //Console.WriteLine("响应的内容" + content);
            MatchCollection mList;
            string regex = "<td height=\"20\" align=\"left\"><a href=\"(.+?)\" target=\"_blank\">(.+?)</a></td>";

            mList = Regex.Matches(content, regex);
            if (mList.Count>1)//说明匹配到多个资源，先让用户选择具体的资源，防止搜出来的是多个电视剧资源，炸屏
            {
                foreach (Match m in mList)
                {
                    string name = m.Groups[2].ToString();
                    list.Add(new KunyunInfo(name, null, 2));
                }
            }
            else if(mList.Count == 1){
                foreach (Match m in mList)
                {
                    string url = detailPre + m.Groups[1].ToString().Trim();
                    string name = m.Groups[2].ToString();
                    //获取链接
                    string strRes = HttpGet(url, "");
                    //<h1>来源:kkm3u8</h1> //存在就不再获取
                    MatchCollection match = Regex.Matches(strRes, "<h1>来源:kkm3u8</h1>[\\s\\S]+?checked />全选");
                    if (match.Count > 0)
                    {
                        Console.WriteLine("匹配后：" + match[0].ToString());
                        string kkm3u8 = match[0].ToString();
                        //解析串，取第一个就行链接
                        mList = Regex.Matches(kkm3u8, "<input type='checkbox' name='copy_yah' id='copy_yah' value='(.+?)'  checked/>");
                        List<string> urlList = new List<string>();
                       
                        foreach (Match m3u8Math in mList)
                        {
                            urlList.Add(m3u8Math.Groups[1].ToString());
                           
                        }
                        list.Add(new KunyunInfo(name, urlList, 1));

                    }
                    else
                    {
                        //<h1>来源:kkyun</h1>,[\\s\\S]+? 包含换行符的匹配
                        match = Regex.Matches(strRes, "<h1>来源:kkyun</h1>[\\s\\S]+?checked />全选");
                        if (match.Count > 0)
                        {
                            string kkyun = match[0].ToString();
                            //解析串，取第一个就行链接
                            mList = Regex.Matches(kkyun, "<input type='checkbox' name='copy_yah' id='copy_yah' value='(.+?)'  checked/>");
                            List<string> urlList = new List<string>();

                            foreach (Match m3u8Math in mList)
                            {
                                urlList.Add(m3u8Math.Groups[1].ToString());

                            }
                            list.Add(new KunyunInfo(name, urlList, 1));
                        }
                        else
                        {
                            //如果该资源没有链接，则忽略
                            continue;
                        }
                    }
                }
            }

            
            
            return list;
        }

        /// <summary>  
        /// 指定Post地址使用Get 方式获取全部字符串  
        /// </summary>  
        /// <param name="url">请求后台地址</param>  
        /// <returns></returns>  
        public static string HttpPost(string url, Dictionary<string, string> dic)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            #region 添加Post 参数  
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            
            byte[] data = Encoding.GetEncoding("gb2312").GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容  
            using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("gb2312")))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (string.IsNullOrWhiteSpace(postDataStr) ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=gb2312";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

    }
}
