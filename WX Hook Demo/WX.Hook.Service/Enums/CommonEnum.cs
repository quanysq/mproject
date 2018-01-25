using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.Hook.Service
{
    public enum CallBackType
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        LoginSuccess,

        /// <summary>
        /// 接收消息
        /// </summary>
        ReceiveMsg,

        /// <summary>
        /// 好友列表
        /// </summary>
        Friends,

        /// <summary>
        /// 群列表
        /// </summary>
        Groups,

        /// <summary>
        /// 群成员列表
        /// </summary>
        GroupMembers
    }
}
