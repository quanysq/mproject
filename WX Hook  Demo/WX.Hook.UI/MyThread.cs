using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WX.Hook.UI
{
    public enum WX_CMD
    {
        WX_CMD_TYPE_E_SEND_MSG,								/* 发送消息，消息格式：原始id|内容|类型（0位文字，1位图片）。必须有发送按钮*/
        WX_CMD_TYPE_E_SEND_MSG_ACK,
        WX_CMD_TYPE_D_ONLINE,								/* 微信模块上线通告，只有这个是dll主动去找exe服务端（这里通告内容原始ID|ID|昵称|进程ID） */
        WX_CMD_TYPE_D_ONLINE_ACK,
        WX_CMD_TYPE_E_MSG_READ,								/* 收取一条消息，消息格式： 微信原始id|发送方原始id|内容 */
        WX_CMD_TYPE_E_MSG_READ_ACK,
        WX_CMD_TYPE_E_TO_SEND_PAGE,							/* 切换到指定页面，消息格式：页面原始ID */
        WX_CMD_TYPE_E_TO_SEND_PAGE_ACK,
        WX_CMD_TYPE_E_ADD_GROUP,							/* 添加群聊 消息格式：群ID|好友ID */
        WX_CMD_TYPE_E_ADD_GROUP_ACK,
        WX_CMD_TYPE_E_DEL_GROUP,							/* 删除群聊 消息格式：群ID|好友ID */
        WX_CMD_TYPE_E_DEL_GROUP_ACK,
        WX_CMD_TYPE_E_GET_WX_FRIEND_INFO,					/* 获取好友 消息格式：0，1，2这样的数字，获取的是第几个好友 */
        WX_CMD_TYPE_E_GET_WX_FRIEND_INFO_ACK,
        WX_CMD_TYPE_E_GET_WX_GROUP_INFO,					/* 获取群组 消息格式：0，1，2这样的数字，获取的是第几个群 */
        WX_CMD_TYPE_E_GET_WX_GROUP_INFO_ACK,
        WX_CMD_TYPE_E_GET_WX_PUBLIC_INFO,					/* 获取公众号 消息格式：0，1，2这样的数字，获取的是第几个公众号 */
        WX_CMD_TYPE_E_GET_WX_PUBLIC_INFO_ACK,
        WX_CMD_TYPE_E_GET_WX_GROUP_MEMBER_INFO,				/* 获取群聊成员 消息格式：0，1，2|群ID这样的数字，获取的是第几个群成员（该接口为网络接口，速度较慢，因为群成员的详细信息不保存在本地 */
        WX_CMD_TYPE_E_GET_WX_GROUP_MEMBER_INFO_ACK,
        WX_CMD_TYPE_E_AT_GROUP_MEMBER,						/* @群成员 消息格式：群ID|好友ID */
        WX_CMD_TYPE_E_AT_GROUP_MEMBER_ACK,
        WX_CMD_TYPE_E_AT_GROUP_ALL,							/* @所有人，发群公告 消息格式：群ID|群主ID|内容 */
        WX_CMD_TYPE_E_AT_GROUP_ALL_ACK,
        WX_CMD_TYPE_E_CHANGE_REMARK,						/* 修改备注名 消息格式：好友原始ID|备注 */
        WX_CMD_TYPE_E_CHANGE_REMARK_ACK,
        WX_CMD_TYPE_E_GET_WX_FRIEND_INFO_ID,				/* 通过wxid获取用户信息 消息格式：好友原始ID */
        WX_CMD_TYPE_E_GET_WX_FRIEND_INFO_ID_ACK,
        WX_CMD_TYPE_E_GET_WX_GROUP_INFO_ID,					/* 通过群ID获取群信息 消息格式：群原始ID */
        WX_CMD_TYPE_E_GET_WX_GROUP_INFO_ID_ACK,
        WX_CMD_TYPE_E_GET_WX_GROUP_MEMBER_INFO_ID,			/* 通过群成员信息获取群成员信息 消息格式：群成员原始ID */
        WX_CMD_TYPE_E_GET_WX_GROUP_MEMBER_INFO_ID_ACK,
        WX_CMD_TYPE_E_EXIT_GROUP,							/* 退出群聊，自己退群，不是踢人 消息格式：群ID */
        WX_CMD_TYPE_E_EXIT_GROUP_ACK,
        WX_CMD_TYPE_E_SEND_FRIEND_ADD_GROUP,				/* 发送群邀请 消息格式：群ID|好友ID */
        WX_CMD_TYPE_E_SEND_FRIEND_ADD_GROUP_ACK,
        WX_CMD_TYPE_E_SEND_ID_CARD,							/* 发送名片 消息格式：发送对象ID|名片人ID */
        WX_CMD_TYPE_E_SEND_ID_CARD_ACK,
        WX_CMD_TYPE_E_SEND_ID_CARD_PUBLIC,					/* 发送公众号 消息格式：发送对象ID|公众号原始ID */
        WX_CMD_TYPE_E_SEND_ID_CARD_PUBLIC_ACK,
        WX_CMD_TYPE_E_ADD_FRIEND,							/* 添加好友  消息格式：添加好友的ID|打招呼内容 */
        WX_CMD_TYPE_E_ADD_FRIEND_ACK,
        WX_CMD_TYPE_E_OPEN_LINK,							/* 打开连接 消息格式：链接（改结构需要先手动打开连接，不然就会崩溃）*/
        WX_CMD_TYPE_E_OPEN_LINK_ACK,
        WX_CMD_TYPE_E_GET_WX_GROUP_MEMBER_ID_INFO,			/* 获取群聊成员 消息格式：0，1，2|群ID 获取的仅为群成员的原始ID */
        WX_CMD_TYPE_E_GET_WX_GROUP_MEMBER_ID_INFO_ACK,
        WX_CMD_TYPE_E_ADD_FRIEND_V_ID,						/* 用V_ID添加好友 消息格式：v1|v2 */
        WX_CMD_TYPE_E_ADD_FRIEND_V_ID_ACK,
        WX_CMD_TYPE_E_NICK_MSG_READ,						/* 微信部分群不推送的ID消息，可以通过这个获取 */
        WX_CMD_TYPE_E_NICK_MSG_READ_ACK,
        WX_CMD_TYPE_E_SEND_MSG_2,							/* 发送消息，消息格式：原始id|内容|类型（0位文字，1位图片，2为文件）。需要按钮 */
        WX_CMD_TYPE_E_SEND_MSG_2_ACK,
        WX_CMD_TYPE_E_DEL_FRIEND,							/* 删除好友 消息格式：原始ID */
        WX_CMD_TYPE_E_DEL_FRIEND_ACK,
        WX_CMD_TYPE_E_SHARE_LINK,							/* 发送链接 消息格式：原始id|标题|简介|链接|图片*/
        WX_CMD_TYPE_E_SHARE_LINK_ACK,
        WX_CMD_TYPE_E_ADD_FRIEND_2,							/* 关闭验证的同意方式  消息格式：收到的对方id */
        WX_CMD_TYPE_E_ADD_FRIEND_2_ACK,
        WX_CMD_TYPE_E_AT_MEMBER_ALL,								/* 非群主@所有人 */
        WX_CMD_TYPE_E_AT_MEMBER_ALL_ACK,
        WX_CMD_TYPE_E_AT_GROUP_MEMBER_MSG,                  /* @群成员并附加消息 消息格式：群ID|好友ID|内容 */
        WX_CMD_TYPE_E_AT_GROUP_MEMBER_MSG_ACK,
        WX_CMD_TYPE_E_TEST,
        WX_CMD_TYPE_E_TEST_ACK
    }
    
    class MyThread
    {
        public MyThread(ThreadStart start)
        {
            threadFunc1 = start;
            _thread = null;
        }

        public MyThread(ParameterizedThreadStart start)
        {
            threadFunc2 = start;
            _thread = null;
        }

        public void Start()
        {
            if (_thread == null)
                _thread = new Thread(new ThreadStart(OnProxy1));
            _thread.Name = threadFunc1.Method.Name;
            _thread.Start();
        }

        public void Start(object parameter)
        {
            if (_thread == null)
                _thread = new Thread(new ParameterizedThreadStart(OnProxy2));
            _thread.Name = threadFunc2.Method.Name;
            _thread.Start(parameter);
        }

        public void Stop()
        {
            if (_thread != null)
            {
                _thread.Abort();
            }
        }
        static int threadCounter = 0;
        public int index = 0;

        void OnProxy1()
        {
            threadCounter++;
            try
            {
                threadFunc1();
            }
            catch (Exception e)
            {
                throw e;

            }

            threadCounter--;
        }

        void OnProxy2(object obj)
        {
            threadCounter++;
            try
            {
                threadFunc2(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
            threadCounter--;
        }

        public void Join()
        {
            if (_thread != null)
                _thread.Join();
        }
        ThreadStart threadFunc1;
        ParameterizedThreadStart threadFunc2;
        Thread _thread;


        public MyThread(WaitCallback callBack, object state)
        {
            threadFunc3 = callBack;
            threadState = state;
        }
        static public void QueueUserWorkItem(WaitCallback callBack)
        {
            QueueUserWorkItem(callBack, new Object());
        }

        static public void QueueUserWorkItem(WaitCallback callBack, object state)
        {
            MyThread tt = new MyThread(callBack, state);
            tt.ThreadPoolQueueUserWorkItem();
        }

        void ThreadPoolQueueUserWorkItem()
        {
            if (_thread == null)
                _thread = new Thread(new ParameterizedThreadStart(OnProxy3));
            _thread.Name = threadFunc3.Method.Name;
            _thread.Start(threadState);
        }

        void OnProxy3(object state)
        {
            threadCounter++;
            try
            {
                threadFunc3(state);
            }
            catch (Exception e)
            {
                throw e;
            }
            threadCounter--;
        }

        WaitCallback threadFunc3;
        object threadState;
    }

    public static class CommonUtil
    {
        public static T TranNull<T>(object obj)
        {
            if (obj == null) return default(T);
            if (obj is T) return (T)obj;
            try
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }
    }
}
