using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WX.Hook.Service.Model;

namespace WX.Hook.Service
{
    public class WeChatEngine : IWeChatEngine
    {
        private static WeChatEngine instance = null;

        private WeChatEngine()
        {

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

        public void CloseWeChat()
        {
            throw new NotImplementedException();
        }

        public void GetWXFriendList(Socket socket, WxInfoModel wx, string msg = "0")
        {
            throw new NotImplementedException();
        }

        public void GetWXGroupList(Socket socket, WxInfoModel wx, string msg = "0")
        {
            throw new NotImplementedException();
        }

        public void GetWXGroupMemberList(Socket socket, WxInfoModel wx, string groupOrigID, string memberPosition = "0")
        {
            throw new NotImplementedException();
        }

        public Socket InitSocket()
        {
            throw new NotImplementedException();
        }

        public int OpenWeChatAndInjectWeDll()
        {
            throw new NotImplementedException();
        }

        public Task ReceiveDataFromWeDll(Socket socket, Action<int, string, EndPoint> handleMessage)
        {
            throw new NotImplementedException();
        }

        public void SendGroupMessage(Socket socket, WxInfoModel wx, string groupOrigID, string msgContent, string msgType = "0")
        {
            throw new NotImplementedException();
        }

        public void SendGroupMessageEx(Socket socket, WxInfoModel wx, string friendOrigID, string groupOrigID, string msgContent)
        {
            throw new NotImplementedException();
        }
    }
}
