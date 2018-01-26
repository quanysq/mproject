using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WX.Hook.Service.Model;

namespace WX.Hook.Service
{
    public class WeChatEngine : IWeChatEngine
    {
        private static WeChatEngine instance = null;
        private static Socket _SocketClient = null;

        private WeChatEngine()
        {
            InitSocket();
        }

        public static WeChatEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WeChatEngine();
                }
                return instance;
            }
        }

        /// <summary>
        /// 初始化 Socket
        /// </summary>
        private void InitSocket()
        {
            if (_SocketClient != null) return;

            LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("Init Socket...");
            try
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                EndPoint socketServer = new IPEndPoint(ip, 24777);

                _SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _SocketClient.DontFragment = true;
                _SocketClient.ReceiveTimeout = 3000;
                _SocketClient.Bind(socketServer);
            }
            catch (SocketException ex)
            {
                LogHelper.WXLogger.WXHOOKUI.Error("Init Socket Error: ", ex);
            }
            catch (Exception ex)
            {
                LogHelper.WXLogger.WXHOOKUI.Error("Init Socket Error: ", ex);
            }
        }

        List<WxInfoModel> _WxLoggedinList = new List<WxInfoModel>();
        public List<WxInfoModel> WxLoggedinList
        {
            get { return _WxLoggedinList; }
        }

        public WxInfoModel SelectedWx { get; set; }
        public GroupInfoModel SelectedGroup { get; set; }
        public FriendInfoModel SelectedFriend { get; set; }
        public FriendInfoModel SelectedMemberOfGroup { get; set; }

        public void CloseWeChat()
        {
            Process[] processList = Process.GetProcessesByName("WeChat");
            LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("WeChat process number: [{0}]", processList.Length);
            foreach (var process in processList)
            {
                process.Kill();
            }
        }

        private bool NetCheck(WxInfoModel wx)
        {
            int pID = CommonUtil.GetObjTranNull<int>(SelectedWx.WxProcessID);
            bool resultCheck1 = CheckWxExists(pID);
            LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("NetCheck CheckWxExists: [{0}]", resultCheck1);
            if (resultCheck1)
            {
                bool resultCheck2 = CheckWxOffline();
                LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("NetCheck CheckWxOffline: [{0}]", resultCheck2);
                return resultCheck2;
            }
            else
            {
                return false;
            }
        }

        public void GetWXFriendList(string msg = "0")
        {
            if (SelectedWx == null) return;
            bool resultCheck = NetCheck(SelectedWx);
            if (!resultCheck) return;
            WeDll.GetWXFriendList(_SocketClient, SelectedWx, msg);
        }

        public void GetWXGroupList(string msg = "0")
        {
            if (SelectedWx == null) return;
            bool resultCheck = NetCheck(SelectedWx);
            if (!resultCheck) return;
            WeDll.GetWXGroupList(_SocketClient, SelectedWx, msg);
        }

        public void GetWXGroupMemberList(string memberPosition = "0")
        {
            if (SelectedWx == null) return;
            bool resultCheck = NetCheck(SelectedWx);
            if (!resultCheck) return;
            if (SelectedGroup == null) return;
            string groupOrigID = SelectedGroup.Group_Orig_ID;
            WeDll.GetWXGroupMemberList(_SocketClient, SelectedWx, groupOrigID, memberPosition);
        }

        public int OpenWeChatAndInjectWeDll()
        {
            bool resultCheck = CheckWxOffline();
            LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("OpenWeChatAndInjectWeDll CheckWxOffline: [{0}]", resultCheck);
            if (resultCheck)
            {
                throw new Exception("Network connection failed, please try again later!");
            }

            int pID = WeDll.InjectWeDll();
            LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("OpenWeChatAndInjectWeDll WeChat PID: [{0}]", pID);
            return pID;
        }

        public void ReceiveWxMessage()
        {
            if (SelectedWx == null) return;
            bool resultCheck = NetCheck(SelectedWx);
            if (!resultCheck) return;
            WeDll.ReceiveWxMessage(_SocketClient, SelectedWx);
        }

        public void ReceiveDataFromWeDll(Action<CallBackType, object> callback)
        {
            WeDll.ReceiveDataFromWeDllUsingUdp(_SocketClient, (type, msg, receiveAddr) => 
            {
                LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("WX_CMD_TYPE_E_GET_WX_GROUP_MEMBER_INFO_ACK type is [{0}]", (int)WeDllCmd.WX_CMD_TYPE_E_GET_WX_GROUP_MEMBER_INFO_ACK);
                LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("ReceiveDataFromWeDllUsingUdp receiveAddr: [{2}], type: [{0}], msg: [{1}]", type, msg, receiveAddr);

                //微信登录
                if (type == (int)WeDllCmd.WX_CMD_TYPE_D_ONLINE)
                {
                    HandleWeDllMsgForLogin(msg, receiveAddr, callback);
                }

                //接收消息
                if (type == (int)WeDllCmd.WX_CMD_TYPE_E_MSG_READ_ACK)
                {
                    HandleWeDllMsgForReceiveMsg(msg, callback);
                }

                //好友列表
                if (type == (int)WeDllCmd.WX_CMD_TYPE_E_GET_WX_FRIEND_INFO_ACK)
                {
                    HandleWeDllMsgForFriendList(msg, callback);
                }

                //群列表
                if (type == (int)WeDllCmd.WX_CMD_TYPE_E_GET_WX_GROUP_INFO_ACK)
                {
                    HandleWeDllMsgForGroupList(msg, callback);
                }

                //群好友 
                if (type == (int)WeDllCmd.WX_CMD_TYPE_E_GET_WX_GROUP_MEMBER_INFO_ACK)
                {
                    HandleWeDllMsgForGroupMemberList(msg, callback);
                }
            });
        }

        #region Handle WeDll Message
        private void HandleWeDllMsgForLogin(string msg, EndPoint receiveAddr, Action<CallBackType, object> callback)
        {
            string[] wxInfoFromMsg = msg.Split('|');
            WxInfoModel wxInfo = new WxInfoModel()
            {
                WxID        = wxInfoFromMsg[0],
                WxOrigID    = wxInfoFromMsg[1],
                WxNickName  = wxInfoFromMsg[2],
                WxProcessID = wxInfoFromMsg[3],
                LoginTime   = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Addr        = receiveAddr
            };
            _WxLoggedinList.Add(wxInfo);
            callback(CallBackType.LoginSuccess, wxInfo);
        }

        private void HandleWeDllMsgForReceiveMsg(string msg, Action<CallBackType, object> callback)
        {
            if (!msg.Equals("END", StringComparison.OrdinalIgnoreCase))
            {
                callback(CallBackType.ReceiveMsg, msg);
            }
            if (SelectedWx == null) return;
            ReceiveWxMessage();
        }

        private void HandleWeDllMsgForFriendList(string msg, Action<CallBackType, object> callback)
        {
            if (msg.Equals("END", StringComparison.OrdinalIgnoreCase))
            {
                callback(CallBackType.Friends, null);
            }
            else
            {
                var friendList = msg.Split('|');
                int friendIndex = CommonUtil.GetObjTranNull<int>(friendList[0]);
                friendIndex = friendIndex + 1;
                FriendInfoModel friendInfo = new FriendInfoModel()
                {
                    Friend_Orig_ID = friendList[1],
                    Friend_ID      = friendList[2],
                    V_ID           = friendList[3],
                    Nick           = friendList[4],
                    Remark         = friendList[5],
                    Sex            = friendList[6]
                };
                SelectedWx.FriendList.Add(friendInfo);
                GetWXFriendList(friendIndex.ToString());
            }
        }

        private void HandleWeDllMsgForGroupList(string msg, Action<CallBackType, object> callback)
        {
            if (msg.Equals("END", StringComparison.OrdinalIgnoreCase))
            {
                callback(CallBackType.Groups, null);
            }
            else
            {
                var groupList  = msg.Split('|');
                int groupIndex = CommonUtil.GetObjTranNull<int>(groupList[0]);
                groupIndex     = groupIndex + 1;
                GroupInfoModel groupInfo = new GroupInfoModel()
                {
                    Group_Orig_ID = groupList[1],
                    Group_ID      = groupList[2],
                    V_ID          = groupList[3],
                    Nick          = groupList[4],
                    MemberNumber  = groupList[5]
                };
                SelectedWx.GroupList.Add(groupInfo);
                GetWXGroupList(groupIndex.ToString());
            }
        }

        private void HandleWeDllMsgForGroupMemberList(string msg, Action<CallBackType, object> callback)
        {
            if (msg.Equals("END", StringComparison.OrdinalIgnoreCase))
            {
                callback(CallBackType.GroupMembers, null);
            }
            else
            {
                var groupmemberList   = msg.Split('|');
                int groupmemberIndex  = CommonUtil.GetObjTranNull<int>(groupmemberList[0]);
                groupmemberIndex      = groupmemberIndex + 1;
                string groupID        = groupmemberList[1];
                string groupMemberMsg = string.Format("{0}|{1}", groupmemberIndex, groupID);
                var friendInfo        = new FriendInfoModel()
                {
                    Group_Orig_ID  = groupID,
                    Friend_Orig_ID = groupmemberList[2],
                    Friend_ID      = groupmemberList[2],
                    V_ID           = groupmemberList[4],
                    Nick           = groupmemberList[5],
                    Remark         = groupmemberList[6],
                    Sex            = groupmemberList[7]
                };
                SelectedGroup.MemberList.Add(friendInfo);
                GetWXGroupMemberList(groupMemberMsg);
            }
        }
        #endregion

        public void SendGroupMessage(string msgContent, string msgType = "0")
        {
            if (SelectedWx == null) return;
            if (SelectedGroup == null) return;
            string groupOrigID = SelectedGroup.Group_Orig_ID;
            WeDll.SendGroupMessage(_SocketClient, SelectedWx, groupOrigID, msgContent, msgType);
        }

        public void SendGroupMessageEx(string msgContent)
        {
            if (SelectedWx == null) return;
            if (SelectedGroup == null) return;
            if (SelectedMemberOfGroup == null) return;
            string groupOrigID = SelectedGroup.Group_Orig_ID;
            string memberOrigID = SelectedMemberOfGroup.Friend_Orig_ID;
            WeDll.SendGroupMessageEx(_SocketClient, SelectedWx, memberOrigID, groupOrigID, msgContent);
        }

        public bool CheckWxExists(int pID)
        {
            try
            {
                Process p = Process.GetProcessById(pID);
                LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("CheckWxOnline: [{0}]", p.Id);
                return true;
            }
            catch (ArgumentException ex)
            {
                LogHelper.WXLogger.WXHOOKSERVICE.Error("CheckWxOnline Error: ", ex);
                return false;
            }
        }

        public void CheckWxExistsLoop(int pID, Action<int> callback)
        {
            Task.Run(() => 
            {
                while (true)
                {
                    bool resultCheckWxOnline = CheckWxExists(pID);
                    if (!resultCheckWxOnline)
                    {
                        callback(pID);
                    }
                    Thread.Sleep(5000); //每5秒检测一次
                }
            });
            
        }

        public bool CheckWxOffline()
        {
            try
            {
                Ping objPingSender = new Ping();
                PingOptions objPinOptions = new PingOptions();
                objPinOptions.DontFragment = true;
                string data = "";
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int intTimeout = 120;
                PingReply objPinReply = objPingSender.Send("www.baidu.com", intTimeout, buffer, objPinOptions);
                string strInfo = objPinReply.Status.ToString();
                LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("CheckWxOffline result: [{0}]", strInfo);
                if (strInfo.Equals("Success", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WXLogger.WXHOOKUI.Error("CheckWxOffline Error: ", ex);
                return false;
            }
        }

        public void CheckWxOfflineLoop(Action callback)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    bool resultCheckWxOffline = CheckWxOffline();
                    if (!resultCheckWxOffline)
                    {
                        callback();
                    }
                    Thread.Sleep(1000 * 15); //每15秒检测一次
                }
            });
        }

        /// <summary>
        /// 关闭 Socket
        /// </summary>
        public void SafeCloseSocket()
        {
            if (_SocketClient == null)
            {
                LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("Socket is null.");
                return;
            }


            if (!_SocketClient.Connected)
            {
                LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("Socket is connected");
                return;
            }


            try
            {
                _SocketClient.Shutdown(SocketShutdown.Both);
            }
            catch (Exception ex)
            {
                LogHelper.WXLogger.WXHOOKSERVICE.Error("Error occurred when shutdown socket: ", ex);
            }

            try
            {
                _SocketClient.Close();
            }
            catch (Exception ex)
            {
                LogHelper.WXLogger.WXHOOKSERVICE.Error("Error occurred when close socket: ", ex);
            }
        }
    }
}
