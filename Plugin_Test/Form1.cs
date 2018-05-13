using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using www_52bang_site_enjoy.MyModel;
using www_52bang_site_enjoy.MyTool;

namespace Plugin_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyResponse<MovieInfo> myResponse = MyLinkCoverter.CovertInSuoIm("[CQ: rich, url=https://v.youku.com/v_show/id_XMzU5NDQzNzIxNg==.html?sharefrom=iphone,text=极限挑战 第四季20180510 会员解读版第2期:演技滑铁卢!内含男人帮最想删的片段 ]");
        }
    }
}
