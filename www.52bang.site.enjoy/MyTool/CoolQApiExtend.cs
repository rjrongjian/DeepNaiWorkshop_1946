﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using www_52bang_site_enjoy.MyModel;

namespace www_52bang_site_enjoy.MyTool
{
    public class CoolQApiExtend
    {
        /// <summary>
        /// 解析群列表
        /// </summary>
        /// <param name="groupInfo">密文</param>
        /// <returns></returns>
        public static List<GroupInfo> ParseGroupList(string groupInfo)
        {
            byte[] bt = Convert.FromBase64String(groupInfo);
            int weizhi = 0;
            double groups = 0;
            string groupname3 = "";
            List<GroupInfo> groupInfoList = new List<GroupInfo>();
            for (int i = 0; i < 4; i++)
            {
                groups += bt[i] * Math.Pow(256, 3 - i);
                weizhi++;
            }
            for (int j = 0; j < Convert.ToInt32(groups); j++)
            {
                double chang = 0;
                for (int i = 4; i < 6; i++)
                {
                    chang += bt[weizhi] * Math.Pow(256, 5 - i);
                    weizhi++;
                }
                double qunhao = 0;
                for (int i = 6; i < 14; i++)
                {
                    qunhao += bt[weizhi] * Math.Pow(256, 13 - i);
                    weizhi++;
                }
                long groupId = Convert.ToInt64(qunhao.ToString());
                Console.WriteLine("群号：" + qunhao.ToString());
                weizhi++;
                weizhi++;
                int namechang = Convert.ToInt32(chang) - 10;
                List<byte> listname = new List<byte>();
                for (int i = 0; i < namechang; i++)
                {
                    listname.Add(bt[weizhi]);
                    weizhi++;
                }
                byte[] byt = listname.ToArray();
                groupname3 = Encoding.Default.GetString(byt);
                Console.WriteLine("群名：" + groupname3.ToString());
                groupInfoList.Add(new GroupInfo(groupname3,groupId));
            }

            return groupInfoList;

        }
        /// <summary>
        /// 获取群成员列表
        /// </summary>
        /// <param name="groupMemberInfo"></param>
        /// <returns></returns>
        /*
        public static void ParseGroupMemberList(string groupMemberInfo)
        {
            byte[] bt = Convert.FromBase64String(groupMemberInfo);
            int weizhi = 0;
            double groups = 0;
            string groupname3 = "";
            List<GroupInfo> groupInfoList = new List<GroupInfo>();
            for (int i = 0; i < 4; i++)
            {
                groups += bt[i] * Math.Pow(256, 3 - i);
                weizhi++;
            }
            for (int j = 0; j < Convert.ToInt32(groups); j++)
            {
                double chang = 0;
                for (int i = 4; i < 6; i++)
                {
                    chang += bt[weizhi] * Math.Pow(256, 5 - i);
                    weizhi++;
                }
                double qunhao = 0;
                for (int i = 6; i < 14; i++)
                {
                    qunhao += bt[weizhi] * Math.Pow(256, 13 - i);
                    weizhi++;
                }
                long groupId = Convert.ToInt64(qunhao.ToString());
                Console.WriteLine("群号：" + qunhao.ToString());
                weizhi++;
                weizhi++;
                int namechang = Convert.ToInt32(chang) - 10;
                List<byte> listname = new List<byte>();
                for (int i = 0; i < namechang; i++)
                {
                    listname.Add(bt[weizhi]);
                    weizhi++;
                }
                byte[] byt = listname.ToArray();
                Console.WriteLine("群号：" + Encoding.Default.GetString(byt));
                //ParseGroupMemberList(byt);






                //groupInfoList.Add(new GroupInfo(groupname3, groupId));
            }

            

        }

        private static void ParseGroupMemberList(byte[] bt)
        {
            int weizhi = 0;
            double groups = 0;
            string groupname3 = "";
            List<GroupInfo> groupInfoList = new List<GroupInfo>();
            for (int i = 0; i < 4; i++)
            {
                groups += bt[i] * Math.Pow(256, 3 - i);
                weizhi++;
            }
            for (int j = 0; j < Convert.ToInt32(groups); j++)
            {
                double chang = 0;
                for (int i = 4; i < 6; i++)
                {
                    chang += bt[weizhi] * Math.Pow(256, 5 - i);
                    weizhi++;
                }
                double qunhao = 0;
                for (int i = 6; i < 14; i++)
                {
                    qunhao += bt[weizhi] * Math.Pow(256, 13 - i);
                    weizhi++;
                }
                long groupId = Convert.ToInt64(qunhao.ToString());
                Console.WriteLine("群成员qq：" + qunhao.ToString());
                weizhi++;
                weizhi++;
                int namechang = Convert.ToInt32(chang) - 10;
                List<byte> listname = new List<byte>();
                for (int i = 0; i < namechang; i++)
                {
                    listname.Add(bt[weizhi]);
                    weizhi++;
                }
                byte[] byt = listname.ToArray();
                groupname3 = Encoding.Default.GetString(byt);
                Console.WriteLine("群成员名：" + groupname3.ToString());
                groupInfoList.Add(new GroupInfo(groupname3, groupId));
            }
            
        }*/
    }
}
