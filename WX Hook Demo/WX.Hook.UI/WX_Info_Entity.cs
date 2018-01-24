using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WX.Hook.UI
{
    public class WX_Info_Entity
    {
        public string WxID { get; set; }

        public string WxNickName { get; set; }

        public string WxProcessID { get; set; }

        public string LoginTime { get; set; }

        public EndPoint Addr { get; set; }

        private List<FriendInfo_Entity> m_friendList = new List<FriendInfo_Entity>();
        public List<FriendInfo_Entity> FriendList
        {
            get { return m_friendList; }
            set { m_friendList = value; }
        }

        private List<GroupInfo_Entity> m_groupList = new List<GroupInfo_Entity>();
        public List<GroupInfo_Entity> GroupList
        {
            get { return m_groupList; }
            set { m_groupList = value; }
        }
    }

    public class FriendInfo_Entity
    {
        public string Friend_Orig_ID { get; set; }
        public string Friend_ID { get; set; }
        public string V_ID { get; set; }
        public string Nick { get; set; }
        public string Remark { get; set; }
        public string Sex { get; set; }
        public string Group_Orig_ID { get; set; }
    }

    public class GroupInfo_Entity
    {
        public string Group_Orig_ID { get; set; }
        public string Group_ID { get; set; }
        public string V_ID { get; set; }
        public string Nick { get; set; }
        public string MemberNumber { get; set; }

        private List<FriendInfo_Entity> m_memberList = new List<FriendInfo_Entity>();
        public List<FriendInfo_Entity> MemberList
        {
            get { return m_memberList; }
            set { m_memberList = value; }
        }

        public bool Selected { get; set; }
    }

    public class SocketReceiveData
    {
        public byte[] ReceiveData { get; set; }
        public EndPoint ClientOfReceiveFrom { get; set; }
    }
}
