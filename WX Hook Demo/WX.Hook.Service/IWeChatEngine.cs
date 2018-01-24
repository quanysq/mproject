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
    internal interface IWeChatEngine
    {
        int OpenWeChatAndInjectWeDll();

        void CloseWeChat();

        Socket InitSocket();

        void GetWXFriendList(Socket socket, WxInfoModel wx, string msg = "0");

        void GetWXGroupList(Socket socket, WxInfoModel wx, string msg = "0");

        void GetWXGroupMemberList(Socket socket, WxInfoModel wx, string groupOrigID, string memberPosition = "0");

        void SendGroupMessage(Socket socket, WxInfoModel wx, string groupOrigID, string msgContent, string msgType = "0");

        void SendGroupMessageEx(Socket socket, WxInfoModel wx, string friendOrigID, string groupOrigID, string msgContent);

        Task ReceiveDataFromWeDll(Socket socket, Action<int, string, EndPoint> handleMessage);
    }
}
