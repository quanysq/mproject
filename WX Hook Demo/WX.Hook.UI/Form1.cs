using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using WX.Hook.Service;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;

namespace WX.Hook.UI
{
    public partial class Form1 : Form
    {
        List<WX_Info_Entity> wxLoggedinList = new List<WX_Info_Entity>();
        IPEndPoint g_serverEndPort = null;
        Socket socketClient = null;
        MyThread threadRecv = null;
        ManualResetEvent eventExitThread;

        WX_Info_Entity currentWxInfoEntity = null;  //当前选择的微信
        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            this.btnOpenWechat.Click += BtnOpenWechat_Click;
            this.btnSendGroupMsg.Click += BtnSendGroupMsg_Click;
            this.lsvGroup.Click += LsvGroup_Click;
            this.btnSendSomeone.Click += BtnSendSomeone_Click;
            this.btnSendSomeoneMsg.Click += BtnSendSomeoneMsg_Click;
            this.lsvWxLoggedin.Click += LsvWxLoggedin_Click;
        }

        private void LsvWxLoggedin_Click(object sender, EventArgs e)
        {
            currentWxInfoEntity = (WX_Info_Entity)lsvWxLoggedin.SelectedItems[0].Tag;

            SendWeDllCmd(currentWxInfoEntity, (int)WX_CMD.WX_CMD_TYPE_E_GET_WX_FRIEND_INFO, "0");
            SendWeDllCmd(currentWxInfoEntity, (int)WX_CMD.WX_CMD_TYPE_E_GET_WX_GROUP_INFO, "0");
        }

        private void BtnSendSomeoneMsg_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lsvGroupMember.Items)
            {
                if (lvi.Checked)
                {
                    try
                    {
                        if (currentWxInfoEntity == null) return;
                        string friendOrigID = lvi.SubItems[1].Text;
                        string groupOrigID = MyCommonUtil.TranNull<string>(lvi.SubItems[1].Tag);
                        string msg = string.Format("{0}|{1}|测试Test", groupOrigID, friendOrigID);
                        LogHelper.WXLogger.WXHOOKUI.InfoFormat("@Someone message: [{0}]", msg);
                        SendWeDllCmd(currentWxInfoEntity, (int)WX_CMD.WX_CMD_TYPE_E_AT_GROUP_MEMBER_MSG, msg);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WXLogger.WXHOOKUI.Error("Error occurred when @ sb.: ", ex);
                    }
                }
            }
        }

        private void BtnSendSomeone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lsvGroupMember.Items)
            {
                if (lvi.Checked)
                {
                    try
                    {
                        if (currentWxInfoEntity == null) return;
                        string friendOrigID = lvi.SubItems[1].Text;
                        string groupOrigID = MyCommonUtil.TranNull<string>(lvi.SubItems[1].Tag);
                        string msg = string.Format("{0}|{1}", groupOrigID, friendOrigID);
                        LogHelper.WXLogger.WXHOOKUI.InfoFormat("@Someone message: [{0}]", msg);
                        SendWeDllCmd(currentWxInfoEntity, (int)WX_CMD.WX_CMD_TYPE_E_AT_GROUP_MEMBER, msg);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WXLogger.WXHOOKUI.Error("Error occurred when @ sb.: ", ex);
                    }
                }
            }
        }

        private void LsvGroup_Click(object sender, EventArgs e)
        {
            if (currentWxInfoEntity == null) return;
            ListViewItem lvi = lsvGroup.SelectedItems[0];
            string groupOrigID = lvi.SubItems[1].Text;
            string groupMemberMsg = string.Format("0|{0}", groupOrigID);
            SendWeDllCmd(currentWxInfoEntity, (int)WX_CMD.WX_CMD_TYPE_E_GET_WX_GROUP_MEMBER_INFO, groupMemberMsg);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var wxInfo in wxLoggedinList)
            {
                int pID = MyCommonUtil.TranNull<int>(wxInfo.WxProcessID);
                Process process = Process.GetProcessById(pID);
                if (process != null) process.Kill();
            }

            SafeCloseSocket();

            Environment.Exit(0);
        }

        private void BtnOpenWechat_Click(object sender, EventArgs e)
        {
            try
            {
                WxService.InjectWeDll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private SocketReceiveData TimeoutRead()
        {
            try
            {
                byte[] data = new byte[1004];
                EndPoint client = new IPEndPoint(IPAddress.Any, 0);
                int count = socketClient.ReceiveFrom(data, ref client);
                LogHelper.WXLogger.WXHOOKUI.InfoFormat("client: [{0}]", client);
                byte[] ret = new byte[count];
                Array.Copy(data, ret, count);

                SocketReceiveData socketReceiveData = new SocketReceiveData();
                socketReceiveData.ReceiveData = data;
                socketReceiveData.ClientOfReceiveFrom = client;

                return socketReceiveData;
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode == SocketError.TimedOut)
                {
                    return null;
                }
                throw;
            }
        }
        private delegate void HandleMessageCB(int type, string msg, EndPoint clientOfReceiveFrom);

        private void SendWeDllCmd(WX_Info_Entity wx, int type, string msg)
        {
            byte[] buff = new byte[1004];

            byte[] b1 = BitConverter.GetBytes(type);
            byte[] b2 = Encoding.UTF8.GetBytes(msg);

            Array.Copy(b1, buff, 4);
            Array.Copy(b2, 0, buff, 4, b2.Length);

            socketClient.SendTo(buff, wx.Addr);
        }

        private void get_msg_rcv(object index)
        {
            if (currentWxInfoEntity == null) return;
            Thread.Sleep(2000);
            SendWeDllCmd(currentWxInfoEntity, (int)WX_CMD.WX_CMD_TYPE_E_MSG_READ, "");
        }

        private void HandMessagecb(int type, string msg, EndPoint clientOfReceiveFrom)
        {
            LogHelper.WXLogger.WXHOOKUI.InfoFormat("HandMessagecb ClientOfReceiveFrom: [{2}], type: [{0}], msg: [{1}]", type, msg, clientOfReceiveFrom);

            ListViewItem lvi = new ListViewItem();
            lvi.Text = "0";
            lvi.SubItems.Add(type.ToString());
            lvi.SubItems.Add(msg);
            this.lv_recv_msg.Items.Add(lvi);

            if (type == 2)
            {
                string[] wxinfoDetail = msg.Split('|');
                WX_Info_Entity wxInfoEntity = new WX_Info_Entity()
                {
                    WxID = wxinfoDetail[wxinfoDetail.Length - 3],
                    WxNickName = wxinfoDetail[wxinfoDetail.Length - 2],
                    WxProcessID = wxinfoDetail[wxinfoDetail.Length-1],
                    LoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Addr = clientOfReceiveFrom
                };
                if (wxLoggedinList.Count == 0 && currentWxInfoEntity == null)
                {
                    currentWxInfoEntity = wxInfoEntity;
                }
                wxLoggedinList.Add(wxInfoEntity);

                ListViewItem lviWxLoggedin = new ListViewItem();
                lviWxLoggedin.Text = wxInfoEntity.WxID;
                lviWxLoggedin.Tag = wxInfoEntity;
                lviWxLoggedin.SubItems.Add(wxInfoEntity.WxNickName);
                lviWxLoggedin.SubItems.Add(wxInfoEntity.WxProcessID);
                lsvWxLoggedin.BeginUpdate();
                lsvWxLoggedin.Items.Add(lviWxLoggedin);
                lsvWxLoggedin.EndUpdate();
                
                lb_online.Text = "当前在线微信：" + wxLoggedinList.Count;
                SendWeDllCmd(currentWxInfoEntity, (int)WX_CMD.WX_CMD_TYPE_E_MSG_READ, "");
            }

            if (type == (int)WX_CMD.WX_CMD_TYPE_E_MSG_READ_ACK)
            {
                if (msg == "END")
                {
                    MyThread tmp = new MyThread(new ParameterizedThreadStart(get_msg_rcv));
                    tmp.Start(0);
                }
                else
                {
                    SendWeDllCmd(currentWxInfoEntity, (int)WX_CMD.WX_CMD_TYPE_E_MSG_READ, "");
                }
            }

            //好友列表
            if (type == (int)WX_CMD.WX_CMD_TYPE_E_GET_WX_FRIEND_INFO_ACK)
            {
                if (msg.Equals("END", StringComparison.OrdinalIgnoreCase))
                {

                    LogHelper.WXLogger.WXHOOKUI.InfoFormat("FriendList count: [{0}]", currentWxInfoEntity.FriendList.Count);
                    lsvFriend.Items.Clear();
                    lsvFriend.BeginUpdate();
                    foreach (FriendInfo_Entity friend_Info in currentWxInfoEntity.FriendList)
                    {
                        ListViewItem lviFriend = new ListViewItem();
                        lviFriend.Text = friend_Info.Nick;
                        lviFriend.SubItems.Add(friend_Info.Friend_Orig_ID);
                        lsvFriend.Items.Add(lviFriend);
                    }
                    lsvFriend.EndUpdate();
                }
                else
                {
                    var friendList = msg.Split('|');
                    int friendIndex = MyCommonUtil.TranNull<int>(friendList[0]);
                    friendIndex = friendIndex + 1;
                    FriendInfo_Entity friendInfo = new FriendInfo_Entity()
                    {
                        Friend_Orig_ID = friendList[1],
                        Friend_ID = friendList[2],
                        V_ID = friendList[3],
                        Nick = friendList[4],
                        Remark = friendList[5],
                        Sex = friendList[6]
                    };
                    currentWxInfoEntity.FriendList.Add(friendInfo);
                    SendWeDllCmd(currentWxInfoEntity, (int)WX_CMD.WX_CMD_TYPE_E_GET_WX_FRIEND_INFO, friendIndex.ToString());
                }
            }

            //群列表
            if (type == (int)WX_CMD.WX_CMD_TYPE_E_GET_WX_GROUP_INFO_ACK)
            {
                if (msg.Equals("END", StringComparison.OrdinalIgnoreCase))
                {
                    LogHelper.WXLogger.WXHOOKUI.InfoFormat("GroupList count: [{0}]", currentWxInfoEntity.GroupList.Count);
                    lsvGroup.Items.Clear();
                    lsvGroup.BeginUpdate();
                    foreach (GroupInfo_Entity groupInfo in currentWxInfoEntity.GroupList)
                    {
                        ListViewItem lviGroup = new ListViewItem();
                        lviGroup.Text = string.Format("{0} ({1})", groupInfo.Nick, groupInfo.MemberNumber);
                        lviGroup.SubItems.Add(groupInfo.Group_Orig_ID);
                        lsvGroup.Items.Add(lviGroup);
                    }
                    lsvGroup.EndUpdate();
                }
                else
                {
                    var groupList = msg.Split('|');
                    int groupIndex = MyCommonUtil.TranNull<int>(groupList[0]);
                    groupIndex = groupIndex + 1;
                    GroupInfo_Entity groupInfo = new GroupInfo_Entity()
                    {
                        Group_Orig_ID = groupList[1],
                        Group_ID = groupList[2],
                        V_ID = groupList[3],
                        Nick = groupList[4],
                        MemberNumber = groupList[5]
                    };
                    currentWxInfoEntity.GroupList.Add(groupInfo);
                    SendWeDllCmd(currentWxInfoEntity, (int)WX_CMD.WX_CMD_TYPE_E_GET_WX_GROUP_INFO, groupIndex.ToString());
                }
            }

            //群好友
            if (type == (int)WX_CMD.WX_CMD_TYPE_E_GET_WX_GROUP_MEMBER_INFO_ACK)
            {
                if (msg.Equals("END", StringComparison.OrdinalIgnoreCase))
                {
                    List<GroupInfo_Entity> selectedGroupList = currentWxInfoEntity.GroupList.FindAll(x => x.Selected == true);
                    LogHelper.WXLogger.WXHOOKUI.InfoFormat("GroupList selected count: [{0}]", selectedGroupList.Count);
                    GroupInfo_Entity currentGroup = selectedGroupList[0];
                    LogHelper.WXLogger.WXHOOKUI.InfoFormat("Group ID: [{0}], member count: [{1}]", currentGroup.Group_Orig_ID, currentGroup.MemberList.Count);
                    lsvGroupMember.Items.Clear();
                    lsvGroupMember.BeginUpdate();
                    foreach (FriendInfo_Entity friend_Info in currentGroup.MemberList)
                    {
                        ListViewItem lviFriend = new ListViewItem();
                        lviFriend.Text = friend_Info.Nick;
                        ListViewItem.ListViewSubItem lsvs = new ListViewItem.ListViewSubItem()
                        {
                            Text = friend_Info.Friend_Orig_ID,
                            Tag = friend_Info.Group_Orig_ID
                        };
                        LogHelper.WXLogger.WXHOOKUI.InfoFormat("lsvs.Tag of friend_Info.Group_Orig_ID: {0}", friend_Info.Group_Orig_ID);
                        LogHelper.WXLogger.WXHOOKUI.InfoFormat("lsvs.Tag: {0}", lsvs.Tag);
                        lviFriend.SubItems.Add(lsvs);
                        lsvGroupMember.Items.Add(lviFriend);
                    }
                    lsvGroupMember.EndUpdate();
                }
                else
                {
                    var groupmemberList = msg.Split('|');
                    int groupmemberIndex = MyCommonUtil.TranNull<int>(groupmemberList[0]);
                    groupmemberIndex = groupmemberIndex + 1;
                    string groupID = groupmemberList[1];
                    string groupMemberMsg = string.Format("{0}|{1}", groupmemberIndex, groupID);
                    FriendInfo_Entity friendInfo = new FriendInfo_Entity()
                    {
                        Group_Orig_ID = groupID,
                        Friend_Orig_ID = groupmemberList[2],
                        Friend_ID = groupmemberList[2],
                        V_ID = groupmemberList[4],
                        Nick = groupmemberList[5],
                        Remark = groupmemberList[6],
                        Sex = groupmemberList[7]
                    };
                    currentWxInfoEntity.GroupList.ForEach(x => x.Selected = false);
                    GroupInfo_Entity currentGroup = currentWxInfoEntity.GroupList.Find(x => x.Group_Orig_ID.Equals(groupID, StringComparison.OrdinalIgnoreCase));
                    if (currentGroup != null)
                    {
                        currentGroup.Selected = true;
                        currentGroup.MemberList.Add(friendInfo);
                    }
                    SendWeDllCmd(currentWxInfoEntity, (int)WX_CMD.WX_CMD_TYPE_E_GET_WX_GROUP_MEMBER_INFO, groupMemberMsg);
                }
            }
        }
        private void HandleRecvByte(SocketReceiveData socketReceiveData)
        {
            byte[] b = socketReceiveData.ReceiveData;

            byte[] b1 = new byte[4];
            byte[] b2 = new byte[1001];

            System.Array.Copy(b, b1, 4);
            System.Array.Copy(b, 4, b2, 0, 1000);

            int type = BitConverter.ToInt32(b1, 0);
            string content = Encoding.UTF8.GetString(b2);
            int _index = content.IndexOf("\0");
            content = content.Substring(0, _index);

            if (this.InvokeRequired)
            {
                HandleMessageCB stcb = new HandleMessageCB(HandMessagecb);
                this.Invoke(stcb, new object[] { type, content, socketReceiveData.ClientOfReceiveFrom});
            }
        }

        private void ReceiveUdp(object obj)
        {
            try
            {

                while (!eventExitThread.WaitOne(0))
                {
                    try
                    {
                        var socketReceiveData = TimeoutRead();
                        if (socketReceiveData.ReceiveData != null)
                        {
                            HandleRecvByte(socketReceiveData);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        LogHelper.WXLogger.WXHOOKUI.Warn("ReceiveUdp Warnning: ", ex);
                        if (ex.Message == "正在中止线程。")
                        {
                            return;
                        }
                        socketClient.Close();
                        socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        socketClient.DontFragment = true;
                        socketClient.ReceiveTimeout = 3000;
                        socketClient.Bind(g_serverEndPort);
                        Thread.Sleep(5000);
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            finally
            {
                lock (this)
                {
                    eventExitThread.Dispose();
                    eventExitThread = null;
                    threadRecv = null;
                    socketClient.Close();
                    socketClient = null;
                }
            }
        }

        private void init_socket()
        {
            SafeCloseSocket();

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            g_serverEndPort = new IPEndPoint(ip, 24777);

            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socketClient.DontFragment = true;
            socketClient.ReceiveTimeout = 3000;
            socketClient.Bind(g_serverEndPort);
            
            /* 开始收发包线程 */
            eventExitThread = new ManualResetEvent(false);
            threadRecv = new MyThread(new ParameterizedThreadStart(ReceiveUdp));
            threadRecv.Start(0);
        }

        private void init()
        {
            init_socket();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            init();
        }

        private void BtnSendGroupMsg_Click(object sender, EventArgs e)
        {
            try
            {
                GroupInfo_Entity currentGroup = currentWxInfoEntity.GroupList.Find(x => x.Selected == true);
                if (currentGroup == null)
                {
                    MessageBox.Show("未选择群！");
                }
                string msg = string.Format("{0}|{1}|0", currentGroup.Group_Orig_ID, this.tb_content.Text);
                SendWeDllCmd(currentWxInfoEntity, (int)WX_CMD.WX_CMD_TYPE_E_SEND_MSG_2, msg);
            }
            catch (Exception ex)
            {
                LogHelper.WXLogger.WXHOOKUI.Error("Error occurred when send message to group.", ex);
            }
        }

        void SafeCloseSocket()
        {
            if (socketClient == null)
            {
                LogHelper.WXLogger.WXHOOKUI.InfoFormat("Socket is null.");
                return;
            }
                

            if (!socketClient.Connected)
            {
                LogHelper.WXLogger.WXHOOKUI.InfoFormat("Socket is connected");
                return;
            }
                

            try
            {
                socketClient.Shutdown(SocketShutdown.Both);
            }
            catch(Exception ex)
            {
                LogHelper.WXLogger.WXHOOKUI.Error("Error occurred when shutdown socket: ", ex);
            }

            try
            {
                socketClient.Close();
            }
            catch(Exception ex)
            {
                LogHelper.WXLogger.WXHOOKUI.Error("Error occurred when close socket: ", ex);
            }
        }

        /// <summary>
        /// 通过进程判断微信是否闪退
        /// </summary>
        /// <param name="wxList"></param>
        /// <returns></returns>
        private bool CheckWxOnline(WX_Info_Entity wx)
        {
            int pID = MyCommonUtil.TranNull<int>(wx.WxProcessID);
            Process p = Process.GetProcessById(pID);
            if (p == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 通过网络判断微信是否掉线
        /// </summary>
        /// <param name="strIpOrDName"></param>
        /// <returns></returns>
        private bool PingIpOrDomainName(string strIpOrDName)
        {
            try
            {
                Ping objPingSender = new Ping();
                PingOptions objPinOptions = new PingOptions();
                objPinOptions.DontFragment = true;
                string data = "";
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int intTimeout = 120;
                PingReply objPinReply = objPingSender.Send(strIpOrDName, intTimeout, buffer, objPinOptions);
                string strInfo = objPinReply.Status.ToString();
                if (strInfo == "Success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
