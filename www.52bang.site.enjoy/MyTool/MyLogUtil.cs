using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace www_52bang_site_enjoy.MyTool
{
    public class MyLogUtil
    {
        public static void WriteQQDialogueLog(long qq, string str)
        {

            StreamWriter streamWriter = new StreamWriter(MySystemUtil.GetQQDialogueDir() + qq + ".txt", true);
            streamWriter.WriteLine(qq+" "+DateTime.Now.ToString("hh:mm:ss") + "=>" + str);
            //streamWriter.WriteLine("---------------------------------------------------------");
            streamWriter.Close();
        }
        /// <summary>
        /// 我给与的回复
        /// </summary>
        /// <param name="str"></param>
        public static void WriteQQDialogueLogOfMe(long qq, string str)
        {

            StreamWriter streamWriter = new StreamWriter(MySystemUtil.GetQQDialogueDir() + qq + ".txt", true);
            streamWriter.WriteLine("我 "+DateTime.Now.ToString("hh:mm:ss") + "=>" + str);
            //streamWriter.WriteLine("---------------------------------------------------------");
            streamWriter.Close();
        }
    }
}
