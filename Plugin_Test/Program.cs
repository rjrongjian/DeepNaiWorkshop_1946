using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using www_52bang_site_enjoy;

namespace Plugin_Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mainForm = new MainForm();
            mainForm.displayMsg2("测试");
            mainForm.displayMsg2("测试");
            mainForm.displayMsg2("测试");
            mainForm.displayMsg2("测试");
            Application.Run(new Form1());
        }
    }
}
