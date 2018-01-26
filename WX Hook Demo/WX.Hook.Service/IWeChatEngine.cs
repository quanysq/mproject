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
        /// <summary>
        /// 打开微信并注入 WeDll
        /// </summary>
        /// <returns></returns>
        int OpenWeChatAndInjectWeDll();

        /// <summary>
        /// 关闭微信
        /// </summary>
        void CloseWeChat();
        
        /// <summary>
        /// 获取微信好友列表
        /// </summary>
        /// <param name="msg"></param>
        void GetWXFriendList(string msg = "0");

        /// <summary>
        /// 获取微信群列表
        /// </summary>
        /// <param name="msg"></param>
        void GetWXGroupList(string msg = "0");

        /// <summary>
        /// 获取微信群成员
        /// </summary>
        /// <param name="memberPosition"></param>
        void GetWXGroupMemberList(string memberPosition = "0");

        /// <summary>
        /// 发送普通群消息
        /// </summary>
        /// <param name="delayTime">延时时间。单位：毫秒</param>
        /// <param name="msgContent"></param>
        /// <param name="msgType"></param>
        void SendGroupMessage(int delayTime, string msgContent, string msgType = "0");

        /// <summary>
        /// 发送群消息：@某人 + 消息
        /// </summary>
        /// <param name="delayTime">延时时间。单位：毫秒</param>
        /// <param name="msgContent"></param>
        void SendGroupMessageEx(int delayTime, string msgContent);

        /// <summary>
        /// 接收微信消息
        /// </summary>
        /// <param name="callback"></param>
        void ReceiveDataFromWeDll(Action<CallBackType, object> callback);

        /// <summary>
        /// 读取微信消息
        /// </summary>
        void ReceiveWxMessage();

        /// <summary>
        /// 通过线程检查微信闪退(单次)
        /// </summary>
        /// <param name="pID"></param>
        bool CheckWxExists(int pID);

        /// <summary>
        /// 通过线程检查微信闪退(循环)
        /// </summary>
        /// <param name="pID"></param>
        void CheckWxExistsLoop(int pID, Action<int> callback);

        /// <summary>
        /// 通过网络检查微信是否掉线(单次)
        /// </summary>
        /// <param name="callback"></param>
        bool CheckWxOffline();

        /// <summary>
        /// 通过网络检查微信是否掉线(循环)
        /// </summary>
        /// <param name="strIpOrDName"></param>
        /// <returns></returns>
        void CheckWxOfflineLoop(Action callback);

    }
}
