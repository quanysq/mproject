using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WX.Hook.Service.Model;

namespace WX.Hook.Service
{
    /// <summary>
    /// 封装第三方的Hook DLL
    /// </summary>
    internal sealed class WeDll
    {
        #region Win32 API Const
        // privileges
        const int PROCESS_CREATE_THREAD = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_READ = 0x0010;

        // used for memory allocation
        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;
        #endregion

        #region Win32 API Struct For CreateProcess
        [StructLayout(LayoutKind.Sequential)]
        private class SECURITY_ATTRIBUTES
        {
            public int nLength;
            public string lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct STARTUPINFO
        {
            public int cb;
            public string lpReserved;
            public string lpDesktop;
            public int lpTitle;
            public int dwX;
            public int dwY;
            public int dwXSize;
            public int dwYSize;
            public int dwXCountChars;
            public int dwYCountChars;
            public int dwFillAttribute;
            public int dwFlags;
            public int wShowWindow;
            public int cbReserved2;
            public byte lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        /// <summary>
        /// 创建一个新的进程和它的主线程，这个新进程运行指定的可执行文件
        /// </summary>
        /// <param name="lpApplicationName">指向一个NULL结尾的、用来指定可执行模块的字符串</param>
        /// <param name="lpCommandLine">指向一个以NULL结尾的字符串，该字符串指定要执行的命令行</param>
        /// <param name="lpProcessAttributes">指向一个SECURITY_ATTRIBUTES结构体，这个结构体决定是否返回的句柄可以被子进程继承。如果lpProcessAttributes参数为空（NULL），那么句柄不能被继承。</param>
        /// <param name="lpThreadAttributes">同lpProcessAttribute,不过这个参数决定的是线程是否被继承.通常置为NULL</param>
        /// <param name="bInheritHandles">指示新进程是否从调用进程处继承了句柄。</param>
        /// <param name="dwCreationFlags">指定附加的、用来控制优先类和进程的创建的标志。</param>
        /// <param name="lpEnvironment">指向一个新进程的环境块。如果此参数为空，新进程使用调用进程的环境。</param>
        /// <param name="lpCurrentDirectory">指向一个以NULL结尾的字符串，这个字符串用来指定子进程的工作路径。</param>
        /// <param name="lpStartupInfo">指向一个用于决定新进程的主窗体如何显示的STARTUPINFO结构体。</param>
        /// <param name="lpProcessInformation">指向一个用来接收新进程的识别信息的PROCESS_INFORMATION结构体。</param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Ansi)]
        private static extern bool CreateProcess(
            string lpApplicationName,
            string lpCommandLine,
            SECURITY_ATTRIBUTES lpProcessAttributes,
            SECURITY_ATTRIBUTES lpThreadAttributes,
            bool bInheritHandles,
            int dwCreationFlags,
            string lpEnvironment,
            string lpCurrentDirectory,
            ref STARTUPINFO lpStartupInfo,
            ref PROCESS_INFORMATION lpProcessInformation
            );
        #endregion

        #region Win32 API For Inject WeDll
        /// <summary>
        /// 打开一个已存在的进程对象,并返回进程的句柄
        /// </summary>
        /// <param name="dwDesiredAccess">访问权限</param>
        /// <param name="bInheritHandle">继承标志</param>
        /// <param name="dwProcessId">进程ID</param>
        /// <returns>返回进程的句柄</returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        /// <summary>
        /// 获取一个应用程序或动态链接库的模块句柄 
        /// </summary>
        /// <param name="lpModuleName">指定模块名，这通常是与模块的文件名相同的一个名字</param>
        /// <returns>如执行成功成功，则返回模块句柄。零表示失败</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// 检索指定的动态链接库(DLL)中的输出库函数地址 
        /// </summary>
        /// <param name="hModule">DLL模块句柄 包含此函数的DLL模块的句柄。LoadLibrary或者GetModuleHandle函数可以返回此句柄。</param>
        /// <param name="procName">函数名 包含函数名的以NULL结尾的字符串，或者指定函数的序数值。如果此参数是一个序数值，它必须在一个字的底字节，高字节必须为0。</param>
        /// <returns>调用成功，返回DLL中的输出函数地址，调用失败，返回0。得到进一步的错误信息，调用函数GetLastError。</returns>
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        /// <summary>
        /// 在指定进程的虚拟地址空间中保留或开辟一段区域.除非MEM_RESET被使用，否则将该内存区域初始化为0.
        /// </summary>
        /// <param name="hProcess">需要在其中分配空间的进程的句柄.这个句柄必须拥有PROCESS_VM_OPERATION访问权限</param>
        /// <param name="lpAddress">想要获取的地址区域.一般用NULL自动分配</param>
        /// <param name="dwSize">要分配的内存大小.字节单位.注意实际分 配的内存大小是页内存大小的整数倍</param>
        /// <param name="flAllocationType">内存分配的类型</param>
        /// <param name="flProtect">内存页保护</param>
        /// <returns>执行成功就返回分配内存的首地址，失败返回0。</returns>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        /// <summary>
        /// 写入某一进程的内存区域。入口区必须可以访问，否则操作将失败
        /// </summary>
        /// <param name="hProcess">进程句柄</param>
        /// <param name="lpBaseAddress">要写的内存首地址</param>
        /// <param name="lpBuffer">指向要写的数据的指针(数据当前存放地址)。</param>
        /// <param name="nSize">要写入的字节数。</param>
        /// <param name="lpNumberOfBytesWritten">实际数据的长度</param>
        /// <returns>非零表示成功，零表示失败</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        /// <summary>
        /// 创建一个在其它进程地址空间中运行的线程(也称:创建远程线程). 
        /// </summary>
        /// <param name="hProcess">目标进程的句柄</param>
        /// <param name="lpThreadAttributes">指向线程的安全描述结构体的指针，一般设置为0，表示使用默认的安全级别</param>
        /// <param name="dwStackSize">线程堆栈大小，一般设置为0，表示使用默认的大小，一般为1M</param>
        /// <param name="lpStartAddress">线程函数的地址</param>
        /// <param name="lpParameter">传给线程函数的参数</param>
        /// <param name="dwCreationFlags">线程的创建方式(0表示线程创建后﻿立即运行 CREATE_SUSPENDED 0x00000004以挂起方式创建</param>
        /// <param name="lpThreadId">指向所创建线程句柄的指针,如果创建失败,该参数为0</param>
        /// <returns>如果调用成功,返回新线程句柄,失败返回0</returns>
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        
        /// <summary>
        /// 获得一个顶层窗口的句柄，该窗口的类名和窗口名与给定的字符串相匹配
        /// </summary>
        /// <param name="lpClassName">指向一个指定了类名的空结束字符串，或一个标识类名字符串的成员的指针</param>
        /// <param name="lpWindowName">指向一个指定了窗口名（窗口标题）的空结束字符串</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        #endregion

        /// <summary>
        /// 获取微信的安装路径
        /// </summary>
        /// <returns></returns>
        private static string GetWechatPath()
        {
            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Tencent\\WeChat"))
            {
                if (regKey == null)
                {
                    throw new Exception("No found the install path of WeChat!");
                }
                string wechatInstallPath = CommonUtil.GetObjTranNull<string>(regKey.GetValue("InstallPath"));
                string wechatPath = Path.Combine(wechatInstallPath, "WeChat.exe");
                LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("The path of wechat is [{0}]", wechatPath);
                return wechatPath;
            }
        }

        /// <summary>
        /// 通过系统API启动微信
        /// </summary>
        /// <returns></returns>
        private static int OpenWechat(string wechatPath)
        {
            STARTUPINFO sInfo = new STARTUPINFO();
            PROCESS_INFORMATION pInfo = new PROCESS_INFORMATION();
            bool resultOpenWechat = CreateProcess(wechatPath, null, null, null, false, 0, null, null, ref sInfo, ref pInfo);
            if (!resultOpenWechat)
            {
                throw new Exception("Open Wechat.exe failed!");
            }
            else
            {
                return pInfo.dwProcessId;
            }
        }

        /// <summary>
        /// 注入WeDll.dll
        /// </summary>
        /// <returns>返回 WeChat 进程ID</returns>
        public static int InjectWeDll()
        {
            string dllName      = "WeDll.dll";
            uint dllLength      = (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char)));
            string wechatPath   = GetWechatPath();
            int wechatProcessID = OpenWechat(wechatPath);
            //下面开始注入 WeDll.dll 到 WeChat
            if (wechatProcessID > 0)
            {
                //获取 WeChat 进程的处理权限
                IntPtr procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, wechatProcessID);
                if (procHandle == IntPtr.Zero)
                {
                    throw new Exception("Geting the handle of the process with required privileges failed！");
                }

                //取得 LoadLibraryA 在 kernek32.dll 中地址
                IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                if (loadLibraryAddr == IntPtr.Zero)
                {
                    throw new Exception("Searching for the address of LoadLibraryA and storing it in a pointer failed！");
                }

                //申请内存空间
                IntPtr allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, dllLength, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
                if (allocMemAddress == IntPtr.Zero)
                {
                    throw new Exception("Apply memory space failed!");
                }

                //写内存
                UIntPtr bytesWritten;
                bool writeProcessMemory = WriteProcessMemory(procHandle, allocMemAddress, Encoding.UTF8.GetBytes(dllName), dllLength, out bytesWritten);
                if (!writeProcessMemory)
                {
                    throw new Exception("Write memory failed!");
                }

                //创建远程线程
                IntPtr createRemoteThread = CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);
                if (createRemoteThread == IntPtr.Zero)
                {
                    throw new Exception("Create the remote thread for WeDll failed！");
                }

                LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("Injected WeDll successfully!");
                return wechatProcessID;
            }
            else
            {
                throw new Exception("Does not found WeChat window!");
            }
        }

        private static Task SendWeDllCmd(Socket socket, WxInfoModel wx, WeDllCmd dllCmd, string msg)
        {
            Task task = Task.Run(() => 
            {
                try
                {
                    byte[] buff = new byte[1004];

                    int type = (int)dllCmd;
                    byte[] b1 = BitConverter.GetBytes(type);
                    byte[] b2 = Encoding.UTF8.GetBytes(msg);

                    Array.Copy(b1, buff, 4);
                    Array.Copy(b2, 0, buff, 4, b2.Length);

                    socket.SendTo(buff, wx.Addr);
                } 
                catch (Exception ex)
                {
                    LogHelper.WXLogger.WXHOOKSERVICE.Error("Error occurred when execute SendWeDllCmd method: ", ex);
                }
            });
            return task;
        }

        public static Task ReceiveDataFromWeDllUsingUdp(Socket socket, Action<int, string, EndPoint> handleMessage)
        {
            Task task = Task.Run(() => 
            {
                while (true)
                {
                    try
                    {
                        bool SocketCanRead = socket.Poll(100, SelectMode.SelectRead);
                        LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("Socket can read? [{0}]", SocketCanRead);
                        if (!SocketCanRead) continue;

                        byte[] data = new byte[1004];
                        EndPoint remoteAddr = new IPEndPoint(IPAddress.Any, 0);
                        int count = socket.ReceiveFrom(data, ref remoteAddr);
                        LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("Remote Address: [{0}], Receive Data Size: [{1}]", remoteAddr, count);
                        if (count == 0) continue;

                        //byte[] ret = new byte[count];
                        //Array.Copy(data, ret, count);

                        byte[] b1 = new byte[4];
                        byte[] b2 = new byte[1001];

                        Array.Copy(data, b1, 4);
                        Array.Copy(data, 4, b2, 0, 1000);

                        int type = BitConverter.ToInt32(b1, 0);
                        string content = Encoding.UTF8.GetString(b2);
                        int _index = content.IndexOf("\0");
                        content = content.Substring(0, _index);

                        handleMessage(type, content, remoteAddr);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WXLogger.WXHOOKSERVICE.Warn("Error occurred when execute ReceiveDataFromWeDllUsingUdp method: ", ex);
                    }
                }
            });
            return task;
        }

        #region Methods for WeDLL CMD 
        /// <summary>
        /// 接收微信消息
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="wx"></param>
        /// <param name="msg"></param>
        public static void ReceiveWxMessage(Socket socket, WxInfoModel wx)
        {
            SendWeDllCmd(socket, wx, WeDllCmd.WX_CMD_TYPE_E_MSG_READ, "");
        }

        /// <summary>
        /// 获取微信好友列表
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="wx"></param>
        /// <param name="msg">消息格式：0，1，2这样的数字，获取的是第几个好友。默认从0开始获取</param>
        public static void GetWXFriendList(Socket socket, WxInfoModel wx, string msg = "0")
        {
            SendWeDllCmd(socket, wx, WeDllCmd.WX_CMD_TYPE_E_GET_WX_FRIEND_INFO, msg);
        }

        /// <summary>
        /// 获取微信群聊列表
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="wx"></param>
        /// <param name="msg">消息格式：0，1，2这样的数字，获取的是第几个群。默认从0开始获取</param>
        public static void GetWXGroupList(Socket socket, WxInfoModel wx, string msg = "0")
        {
            SendWeDllCmd(socket, wx, WeDllCmd.WX_CMD_TYPE_E_GET_WX_GROUP_INFO, "0");
        }

        /// <summary>
        /// 获取微信群成员列表
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="wx"></param>
        /// <param name="groupOrigID">群ID</param>
        /// <param name="memberPosition">0，1，2这样的数字，获取的是第几个成员。默认从0开始获取</param>
        public static void GetWXGroupMemberList(Socket socket, WxInfoModel wx, string groupOrigID, string memberPosition = "0")
        {
            string groupMemberMsg = string.Format("{0}|{1}", groupOrigID, memberPosition);
            SendWeDllCmd(socket, wx, WeDllCmd.WX_CMD_TYPE_E_GET_WX_GROUP_MEMBER_INFO, groupMemberMsg);
        }

        /// <summary>
        /// 发送群消息
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="wx"></param>
        /// <param name="groupOrigID">群ID</param>
        /// <param name="msgContent">消息内容</param>
        /// <param name="msgType">消息类型：0位文字，1位图片，2为文件。默认为0</param>
        public static void SendGroupMessage(Socket socket, WxInfoModel wx, string groupOrigID, string msgContent, string msgType = "0")
        {
            string msg = string.Format("{0}|{1}|{2}", groupOrigID, msgContent, msgType);
            SendWeDllCmd(socket, wx, WeDllCmd.WX_CMD_TYPE_E_SEND_MSG_2, msg);
        }

        /// <summary>
        /// 发送群消息扩展，支持 @群成员 + 消息
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="wx"></param>
        /// <param name="memberOrigID"></param>
        /// <param name="groupOrigID"></param>
        /// <param name="msgContent"></param>
        public static void SendGroupMessageEx(Socket socket, WxInfoModel wx, string memberOrigID, string groupOrigID, string msgContent)
        {
            string msg = string.Format("{0}|{1}|{2}", groupOrigID, memberOrigID, msgContent);
            LogHelper.WXLogger.WXHOOKSERVICE.InfoFormat("@Someone message: [{0}]", msg);
            SendWeDllCmd(socket, wx, WeDllCmd.WX_CMD_TYPE_E_AT_GROUP_MEMBER_MSG, msg);
        }
        #endregion
    }
}
