using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WX.Hook.Service;
using WX.Hook.Service.Model;

namespace WX.Hook.UI
{
    public partial class Form2 : Form
    {
        List<WxInfoModel> wxLoggedinList = new List<WxInfoModel>();
        Socket socket = null;

        public Form2()
        {
            InitializeComponent();

            this.Load += Form2_Load;
            this.FormClosing += Form2_FormClosing;
            this.btnOpenWeChat.Click += BtnOpenWeChat_Click;
        }
        
        #region Control Event
        private void Form2_Load(object sender, EventArgs e)
        {
            //开启一个线程接收微信信息
            WeChatEngine.Instance.ReceiveDataFromWeDll((dllCmd, obj) =>
            {
                SafeInvoke(() =>
                {
                    switch (dllCmd)
                    {
                        case WeDllCmd.WX_CMD_TYPE_D_ONLINE:
                            HandleMessage_WX_CMD_TYPE_D_ONLINE(obj);
                            break;
                        case WeDllCmd.WX_CMD_TYPE_E_MSG_READ:
                            HandleMessage_WX_CMD_TYPE_E_MSG_READ(obj);
                            break;
                        default:
                            break;

                    }
                });
            });
            
        }

        private void BtnOpenWeChat_Click(object sender, EventArgs e)
        {
            WeChatEngine.Instance.OpenWeChatAndInjectWeDll();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            WeChatEngine.Instance.CloseWeChat();
            WeChatEngine.Instance.SafeCloseSocket();
            Environment.Exit(0);
        }
        #endregion

        #region private method
        public void HandleMessage_WX_CMD_TYPE_D_ONLINE(object obj)
        {
            WxInfoModel wx = CommonUtil.GetObjTranNull<WxInfoModel>(obj);
            ListViewItem lviWxLoggedin = new ListViewItem();
            lviWxLoggedin.Text = wx.WxID;
            lviWxLoggedin.Tag = wx;
            lviWxLoggedin.SubItems.Add(wx.WxNickName);
            lviWxLoggedin.SubItems.Add(wx.WxProcessID);
            this.lsvWxLoggedin.BeginUpdate();
            this.lsvWxLoggedin.Items.Add(lviWxLoggedin);
            this.lsvWxLoggedin.EndUpdate();
        }
        
        public void HandleMessage_WX_CMD_TYPE_E_MSG_READ(object obj)
        {
            string msg = CommonUtil.GetObjTranNull<string>(obj);
            ListViewItem lviReciveData = new ListViewItem();
            lviReciveData.Text = msg;
            this.lsvReciveData.BeginUpdate();
            this.lsvReciveData.Items.Add(lviReciveData);
            this.lsvReciveData.EndUpdate();
        }


        private void SafeInvoke(Action action)
        {
            if (this.IsDisposed || !this.IsHandleCreated)
            {
                return;
            }

            if (this.InvokeRequired)
            {
                LogHelper.WXLogger.WXHOOKUI.Info("call action using BeginInvoke mode.");
                this.BeginInvoke(action);
            }
            else
            {
                LogHelper.WXLogger.WXHOOKUI.Info("call action using Invoke mode.");
                action.Invoke();
            }
        }
        #endregion
    }
}
