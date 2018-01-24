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
            foreach (var wxInfo in _WxLoggedinList)
            {
                int pID = CommonUtil.GetObjTranNull<int>(wxInfo.WxProcessID);
                Process process = Process.GetProcessById(pID);
                if (process != null) process.Kill();
            }
        }

        public void GetWXFriendList(string msg = "0")
        {
            WeDll.GetWXFriendList(_SocketClient, SelectedWx, msg);
        }

        public void GetWXGroupList(string msg = "0")
        {
            WeDll.GetWXGroupList(_SocketClient, SelectedWx, msg);
        }

        public void GetWXGroupMemberList(string memberPosition = "0")
        {
            string groupOrigID = SelectedGroup.Group_Orig_ID;
            WeDll.GetWXGroupMemberList(_SocketClient, SelectedWx, groupOrigID, memberPosition);
        }

        public int OpenWeChatAndInjectWeDll()
        {
            int pID = WeDll.InjectWeDll();
            LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("WeChat PID: [{0}]", pID);
            return pID;
        }

        public void ReceiveDataFromWeDll(Action<WeDllCmd, object> callback)
        {
            WeDll.ReceiveDataFromWeDllUsingUdp(_SocketClient, (type, msg, receiveAddr) => 
            {
                LogHelper.WXLogger.WXHOOKUI.InfoFormat("ReceiveDataFromWeDllUsingUdp receiveAddr: [{2}], type: [{0}], msg: [{1}]", type, msg, receiveAddr);

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

                }

                //群列表
                if (type == (int)WeDllCmd.WX_CMD_TYPE_E_GET_WX_GROUP_INFO_ACK)
                {

                }

                //群好友
                if (type == (int)WeDllCmd.WX_CMD_TYPE_E_GET_WX_GROUP_MEMBER_INFO_ACK)
                {

                }
            });
        }

        #region Handle WeDll Message

        #region WX Login
        private void HandleWeDllMsgForLogin(string msg, EndPoint receiveAddr, Action<WeDllCmd, object> callback)
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
            if (_WxLoggedinList.Count == 0)
            {
                SelectedWx = wxInfo;
            }
            _WxLoggedinList.Add(wxInfo);
            callback(WeDllCmd.WX_CMD_TYPE_D_ONLINE, wxInfo);
            WeDll.ReceiveWxMessage(_SocketClient, SelectedWx);
        }

        private void HandleWeDllMsgForReceiveMsg(string msg, Action<WeDllCmd, object> callback)
        {
            if (!msg.Equals("END", StringComparison.OrdinalIgnoreCase))
            {
                callback(WeDllCmd.WX_CMD_TYPE_E_MSG_READ, msg);
            }
            Thread.Sleep(1000);
            WeDll.ReceiveWxMessage(_SocketClient, SelectedWx);
        }
        #endregion

        #endregion

        public void SendGroupMessage(string msgContent, string msgType = "0")
        {
            string groupOrigID = SelectedGroup.Group_Orig_ID;
            WeDll.SendGroupMessage(_SocketClient, SelectedWx, groupOrigID, msgContent, msgType);
        }

        public void SendGroupMessageEx(string msgContent)
        {
            string groupOrigID = SelectedGroup.Group_Orig_ID;
            string memberOrigID = SelectedMemberOfGroup.Friend_Orig_ID;
            WeDll.SendGroupMessageEx(_SocketClient, SelectedWx, memberOrigID, groupOrigID, msgContent);
        }

        public bool CheckWxOnline(WxInfoModel wx)
        {
            int pID = CommonUtil.GetObjTranNull<int>(wx.WxProcessID);
            Process p = Process.GetProcessById(pID);
            if (p == null)
            {
                return false;
            }

            return true;
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
                if (strInfo == "Success")
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
                LogHelper.WXLogger.WXHOOKUI.Error("PingIpOrDomainName Error: ", ex);
                return false;
            }
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
