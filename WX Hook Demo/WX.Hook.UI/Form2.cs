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
    public partial class frmDemo : Form
    {
        public frmDemo()
        {
            InitializeComponent();

            this.Load += Form2_Load;
            this.FormClosing += Form2_FormClosing;
            this.btnOpenWeChat.Click += BtnOpenWeChat_Click;
            this.lsvWxLoggedin.ItemChecked += LsvWxLoggedin_ItemChecked;
            this.lsvFriendList.ItemChecked += LsvFriendList_ItemChecked;
            this.lsvGroupList.ItemChecked += LsvGroupList_ItemChecked;
            this.lsvGroupMember.ItemChecked += LsvGroupMember_ItemChecked;
            this.btnSendMsg.Click += BtnSendMsg_Click;
        }

        #region Control Event
        private void Form2_Load(object sender, EventArgs e)
        {
            lbError.Text = "";

            HandleMessageFromWeDll();
        }

        private void BtnOpenWeChat_Click(object sender, EventArgs e)
        {
            try
            {
                int pID = WeChatEngine.Instance.OpenWeChatAndInjectWeDll();
                if (pID > 0)
                {
                    WeChatEngine.Instance.CheckWxExistsLoop(pID, (wxPID) =>
                    {
                        SafeInvoke(() =>
                        {
                            lbError.Text = string.Format("微信进程 {0} 已经退出", wxPID);
                        });
                    });
                }
            } catch (Exception ex)
            {
                LogHelper.WXLogger.WXHOOKUI.Error("Error occurred when open new Wechat: ", ex);
                MessageBox.Show(ex.Message);
            }
        }
        
        private void LsvWxLoggedin_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            WeChatEngine.Instance.SelectedWx = null;
            if (e.Item.Checked)
            {
                LogHelper.WXLogger.WXHOOKUI.InfoFormat("Selected WX.");

                ResetListViewCheckedStatus(this.lsvWxLoggedin, e.Item.Index);
                
                WeChatEngine.Instance.SelectedWx = (WxInfoModel)e.Item.Tag;
                WeChatEngine.Instance.SelectedWx.FriendList.Clear();
                WeChatEngine.Instance.SelectedWx.GroupList.Clear();

                this.lsvGroupMember.Items.Clear();
                WeChatEngine.Instance.GetWXFriendList("0");
                WeChatEngine.Instance.GetWXGroupList("0");
                WeChatEngine.Instance.ReceiveWxMessage();
            }
            else
            {
                LogHelper.WXLogger.WXHOOKUI.InfoFormat("Unselected WX.");

                WeChatEngine.Instance.SelectedFriend = null;
                WeChatEngine.Instance.SelectedGroup = null;
                WeChatEngine.Instance.SelectedMemberOfGroup = null;
                this.lsvFriendList.Items.Clear();
                this.lsvGroupList.Items.Clear();
                this.lsvGroupMember.Items.Clear();
            }
        }
        
        private void LsvFriendList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            WeChatEngine.Instance.SelectedFriend = null;
            if (e.Item.Checked)
            {
                ResetListViewCheckedStatus(this.lsvFriendList, e.Item.Index);
                WeChatEngine.Instance.SelectedFriend = (FriendInfoModel)e.Item.Tag;
            }
        }
        
        private void LsvGroupList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            WeChatEngine.Instance.SelectedGroup = null;
            if (e.Item.Checked)
            {
                ResetListViewCheckedStatus(this.lsvGroupList, e.Item.Index);
                WeChatEngine.Instance.SelectedGroup = (GroupInfoModel)e.Item.Tag;
                WeChatEngine.Instance.SelectedGroup.MemberList.Clear();

                WeChatEngine.Instance.GetWXGroupMemberList("0");
            }
            else
            {
                WeChatEngine.Instance.SelectedMemberOfGroup = null;
                this.lsvGroupMember.Items.Clear();
            }
        }

        private void LsvGroupMember_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            WeChatEngine.Instance.SelectedMemberOfGroup = null;
            if (e.Item.Checked)
            {
                ResetListViewCheckedStatus(this.lsvGroupMember, e.Item.Index);
                WeChatEngine.Instance.SelectedMemberOfGroup = (FriendInfoModel)e.Item.Tag;
            }
        }

        private void BtnSendMsg_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();
            if (string.IsNullOrWhiteSpace(message)) return;

            if (WeChatEngine.Instance.SelectedMemberOfGroup != null)
            {
                WeChatEngine.Instance.SendGroupMessageEx(message);
                AfterSendMsg();
            }
            else if (WeChatEngine.Instance.SelectedGroup != null)
            {
                WeChatEngine.Instance.SendGroupMessage(message);
                AfterSendMsg();
            }
            else
            {
                MessageBox.Show("请先选择一个群或群里某个成员！");
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            WeChatEngine.Instance.SafeCloseSocket();
            WeChatEngine.Instance.CloseWeChat();
            Environment.Exit(0);
        }
        #endregion

        #region private method

        #region Handle WeDll message to control
        private void HandleMessageFromWeDll()
        {
            //开启一个线程接收并处理各种微信消息
            WeChatEngine.Instance.ReceiveDataFromWeDll((callbacktype, obj) =>
            {
                SafeInvoke(() =>
                {
                    switch (callbacktype)
                    {
                        case CallBackType.LoginSuccess:
                            LogHelper.WXLogger.WXHOOKUI.InfoFormat("lsvWxLoggedin.Items.Count: [{0}]", lsvWxLoggedin.Items.Count);
                            if (lsvWxLoggedin.Items.Count > 0)
                            {
                                //如果 lsvWxLoggedin 项大于 0，取消订阅 ItemChecked 事件，避免触发
                                lsvWxLoggedin.ItemChecked -= LsvWxLoggedin_ItemChecked;
                            }
                            //处理微信登录信息
                            HandleMessageForLoginSuccess(obj);
                            //添加微信之后再加此事件
                            if (lsvWxLoggedin.Items.Count > 1)
                            {
                                //如果 lsvWxLoggedin 项大于 1，重新订阅 ItemChecked 事件
                                lsvWxLoggedin.ItemChecked += LsvWxLoggedin_ItemChecked;
                            }
                            break;
                        case CallBackType.ReceiveMsg:
                            HandleMessageForReceiveMsg(obj);
                            break;
                        case CallBackType.Friends:
                            HandleMessageForFriends(obj);
                            break;
                        case CallBackType.Groups:
                            HandleMessageForGroups(obj);
                            break;
                        case CallBackType.GroupMembers:
                            HandleMessageForGroupMembers(obj);
                            break;
                        default:
                            break;

                    }
                });
            });
        }

        private void HandleMessageForLoginSuccess(object obj)
        {
            WxInfoModel wx             = CommonUtil.GetObjTranNull<WxInfoModel>(obj);
            ListViewItem lviWxLoggedin = new ListViewItem();
            lviWxLoggedin.Text         = wx.WxID;
            lviWxLoggedin.Tag          = wx;
            lviWxLoggedin.SubItems.Add(wx.WxNickName);
            lviWxLoggedin.SubItems.Add(wx.WxProcessID);
            this.lsvWxLoggedin.BeginUpdate();
            this.lsvWxLoggedin.Items.Add(lviWxLoggedin);
            this.lsvWxLoggedin.EndUpdate();

            if (lsvWxLoggedin.Items.Count == 1)
            {
                lsvWxLoggedin.Items[0].Checked = true;
            }
        }

        private void HandleMessageForReceiveMsg(object obj)
        {
            string msg                 = CommonUtil.GetObjTranNull<string>(obj);
            string[] msgInfo           = msg.Split('|');
            LogHelper.WXLogger.WXHOOKUI.InfoFormat("HandleMessageForReceiveMsg msgInfo length: [{0}]", msgInfo.Length);
            if (msgInfo.Length < 3) return;
            if (msgInfo[1].Equals("weixin", StringComparison.OrdinalIgnoreCase)) return;
            if (msgInfo[1].Equals(msgInfo[0], StringComparison.OrdinalIgnoreCase)) return;

            LogHelper.WXLogger.WXHOOKUI.DebugFormat("HandleMessageForReceiveMsg msgInfo 0: [{0}]", msgInfo[0]);
            LogHelper.WXLogger.WXHOOKUI.DebugFormat("HandleMessageForReceiveMsg msgInfo 1: [{0}]", msgInfo[1]);
            LogHelper.WXLogger.WXHOOKUI.DebugFormat("HandleMessageForReceiveMsg msgInfo 2: [{0}]", msgInfo[2]);

            ListViewItem lviReciveData = new ListViewItem();
            lviReciveData.Text         = msgInfo[0];        //微信号
            #region 获取群名称
            string groupNickName       = msgInfo[1];
            var group = WeChatEngine.Instance.SelectedWx.GroupList.Find(x => x.Group_Orig_ID.Equals(msgInfo[1], StringComparison.OrdinalIgnoreCase));
            if (group != null) groupNickName = group.Nick;
            #endregion
            lviReciveData.SubItems.Add(groupNickName);      //群名称
            lviReciveData.SubItems.Add(msgInfo[1]);         //群ID
            string[] noteInfo = msgInfo[2].Split(':');
            #region 获取发送人名称
            string sendNickName = noteInfo[0];
            if (group != null)
            {
                var member = group.MemberList.Find(x => x.Friend_Orig_ID.Equals(noteInfo[0], StringComparison.OrdinalIgnoreCase));
                if (member != null) sendNickName = noteInfo[0];
            }
            #endregion
            lviReciveData.SubItems.Add(sendNickName);       //发送人名称
            lviReciveData.SubItems.Add(noteInfo[0]);        //发送人ID
            lviReciveData.SubItems.Add(noteInfo[noteInfo.Length - 1]);     //内容
            this.lsvReciveData.BeginUpdate();
            this.lsvReciveData.Items.Add(lviReciveData);
            this.lsvReciveData.EndUpdate();
        }

        private void HandleMessageForFriends(object obj)
        {
            LogHelper.WXLogger.WXHOOKUI.InfoFormat("FriendList count: [{0}]", WeChatEngine.Instance.SelectedWx.FriendList.Count);
            lsvFriendList.Items.Clear();
            lsvFriendList.BeginUpdate();
            foreach (var friendInfo in WeChatEngine.Instance.SelectedWx.FriendList)
            {
                ListViewItem lviFriend = new ListViewItem();
                lviFriend.Text         = friendInfo.Nick;
                lviFriend.Tag          = friendInfo;
                lviFriend.SubItems.Add(friendInfo.Friend_Orig_ID);
                lsvFriendList.Items.Add(lviFriend);
            }
            lsvFriendList.EndUpdate();
            LogHelper.WXLogger.WXHOOKUI.InfoFormat("Load FriendList completed!");
        }

        private void HandleMessageForGroups(object obj)
        {
            LogHelper.WXLogger.WXHOOKUI.InfoFormat("GroupList count: [{0}]", WeChatEngine.Instance.SelectedWx.GroupList.Count);
            lsvGroupList.Items.Clear();
            lsvGroupList.BeginUpdate();
            foreach (var groupInfo in WeChatEngine.Instance.SelectedWx.GroupList)
            {
                ListViewItem lviGroup = new ListViewItem();
                lviGroup.Text         = string.Format("{0}({1})", groupInfo.Nick, groupInfo.MemberNumber);
                lviGroup.Tag          = groupInfo;
                lviGroup.SubItems.Add(groupInfo.Group_Orig_ID);
                lsvGroupList.Items.Add(lviGroup);
            }
            lsvGroupList.EndUpdate();
            LogHelper.WXLogger.WXHOOKUI.InfoFormat("Load GroupList completed!");
        }

        private void HandleMessageForGroupMembers(object obj)
        {
            LogHelper.WXLogger.WXHOOKUI.InfoFormat("Group ID: [{0}], member count: [{1}]", WeChatEngine.Instance.SelectedGroup.Group_Orig_ID, WeChatEngine.Instance.SelectedGroup.MemberList.Count);
            lsvGroupMember.Items.Clear();
            lsvGroupMember.BeginUpdate();
            foreach (var memberInfo in WeChatEngine.Instance.SelectedGroup.MemberList)
            {
                ListViewItem lviMember = new ListViewItem();
                lviMember.Text         = memberInfo.Nick;
                lviMember.Tag          = memberInfo;
                lviMember.SubItems.Add(memberInfo.Friend_Orig_ID);
                lsvGroupMember.Items.Add(lviMember);
            }
            lsvGroupMember.EndUpdate();
            LogHelper.WXLogger.WXHOOKUI.InfoFormat("Load GroupMemberList completed!");
        }
        #endregion

        private void ResetListViewCheckedStatus(ListView lsv, int currentItemIndex)
        {
            foreach (ListViewItem lvi in lsv.Items)
            {
                if (lvi.Selected && lvi.Index != currentItemIndex) lvi.Selected = false;
                if (lvi.Checked && lvi.Index != currentItemIndex) lvi.Checked = false;
            }
        }

        private void AfterSendMsg()
        {
            Thread.Sleep(100);
            txtMessage.Text = "";
            MessageBox.Show("消息已发送！");
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
