using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace www_52bang_site_enjoy.MyModel
{
    public class GroupInfo
    {
        public string GroupName;//群名
        public long GroupId;//群号
        public GroupInfo(string groupName,long groupId)
        {
            this.GroupName = groupName;
            this.GroupId = groupId;
        }
    }
}
