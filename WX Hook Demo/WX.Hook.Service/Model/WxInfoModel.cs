using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WX.Hook.Service.Model
{
    public class WxInfoModel
    {
        public string WxID { get; set; }

        public string WxOrigID { get; set; }

        public string WxNickName { get; set; }

        public string WxProcessID { get; set; }

        public string LoginTime { get; set; }

        public EndPoint Addr { get; set; }

        private List<FriendInfoModel> m_friendList = new List<FriendInfoModel>();
        public List<FriendInfoModel> FriendList
        {
            get { return m_friendList; }
            set { m_friendList = value; }
        }

        private List<GroupInfoModel> m_groupList = new List<GroupInfoModel>();
        public List<GroupInfoModel> GroupList
        {
            get { return m_groupList; }
            set { m_groupList = value; }
        }
    }

    public class FriendInfoModel
    {
        public string Friend_Orig_ID { get; set; }
        public string Friend_ID { get; set; }
        public string V_ID { get; set; }
        public string Nick { get; set; }
        public string Remark { get; set; }
        public string Sex { get; set; }
        public string Group_Orig_ID { get; set; }
    }

    public class GroupInfoModel
    {
        public string Group_Orig_ID { get; set; }
        public string Group_ID { get; set; }
        public string V_ID { get; set; }
        public string Nick { get; set; }
        public string MemberNumber { get; set; }

        private List<FriendInfoModel> m_memberList = new List<FriendInfoModel>();
        public List<FriendInfoModel> MemberList
        {
            get { return m_memberList; }
            set { m_memberList = value; }
        }
    }
}
