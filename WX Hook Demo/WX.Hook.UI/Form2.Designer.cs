namespace WX.Hook.UI
{
    partial class frmDemo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpWhole = new System.Windows.Forms.TableLayoutPanel();
            this.tlpUnder = new System.Windows.Forms.TableLayoutPanel();
            this.tlpLeft = new System.Windows.Forms.TableLayoutPanel();
            this.gbWxLoggedin = new System.Windows.Forms.GroupBox();
            this.lsvWxLoggedin = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbFriendList = new System.Windows.Forms.GroupBox();
            this.lsvFriendList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbGroupList = new System.Windows.Forms.GroupBox();
            this.lsvGroupList = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbGroupMemberList = new System.Windows.Forms.GroupBox();
            this.lsvGroupMember = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpRight = new System.Windows.Forms.TableLayoutPanel();
            this.gbReciveData = new System.Windows.Forms.GroupBox();
            this.lsvReciveData = new System.Windows.Forms.ListView();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_content = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpSendMsg = new System.Windows.Forms.TableLayoutPanel();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.tlpUnder2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.chkStopTest = new System.Windows.Forms.CheckBox();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.btnOpenWeChat = new System.Windows.Forms.Button();
            this.lbError = new System.Windows.Forms.Label();
            this.tlpWhole.SuspendLayout();
            this.tlpUnder.SuspendLayout();
            this.tlpLeft.SuspendLayout();
            this.gbWxLoggedin.SuspendLayout();
            this.gbFriendList.SuspendLayout();
            this.gbGroupList.SuspendLayout();
            this.gbGroupMemberList.SuspendLayout();
            this.tlpRight.SuspendLayout();
            this.gbReciveData.SuspendLayout();
            this.tlpSendMsg.SuspendLayout();
            this.tlpUnder2.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpWhole
            // 
            this.tlpWhole.ColumnCount = 1;
            this.tlpWhole.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpWhole.Controls.Add(this.tlpUnder, 0, 1);
            this.tlpWhole.Controls.Add(this.tlpTop, 0, 0);
            this.tlpWhole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpWhole.Location = new System.Drawing.Point(0, 0);
            this.tlpWhole.Name = "tlpWhole";
            this.tlpWhole.RowCount = 2;
            this.tlpWhole.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpWhole.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWhole.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpWhole.Size = new System.Drawing.Size(1481, 821);
            this.tlpWhole.TabIndex = 0;
            // 
            // tlpUnder
            // 
            this.tlpUnder.ColumnCount = 2;
            this.tlpUnder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpUnder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpUnder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpUnder.Controls.Add(this.tlpLeft, 0, 0);
            this.tlpUnder.Controls.Add(this.tlpRight, 1, 0);
            this.tlpUnder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUnder.Location = new System.Drawing.Point(3, 53);
            this.tlpUnder.Name = "tlpUnder";
            this.tlpUnder.RowCount = 1;
            this.tlpUnder.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpUnder.Size = new System.Drawing.Size(1475, 765);
            this.tlpUnder.TabIndex = 1;
            // 
            // tlpLeft
            // 
            this.tlpLeft.ColumnCount = 1;
            this.tlpLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpLeft.Controls.Add(this.gbWxLoggedin, 0, 0);
            this.tlpLeft.Controls.Add(this.gbFriendList, 0, 1);
            this.tlpLeft.Controls.Add(this.gbGroupList, 0, 2);
            this.tlpLeft.Controls.Add(this.gbGroupMemberList, 0, 3);
            this.tlpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLeft.Location = new System.Drawing.Point(3, 3);
            this.tlpLeft.Name = "tlpLeft";
            this.tlpLeft.RowCount = 4;
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpLeft.Size = new System.Drawing.Size(436, 759);
            this.tlpLeft.TabIndex = 1;
            // 
            // gbWxLoggedin
            // 
            this.gbWxLoggedin.Controls.Add(this.lsvWxLoggedin);
            this.gbWxLoggedin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbWxLoggedin.Location = new System.Drawing.Point(3, 3);
            this.gbWxLoggedin.Name = "gbWxLoggedin";
            this.gbWxLoggedin.Size = new System.Drawing.Size(430, 183);
            this.gbWxLoggedin.TabIndex = 0;
            this.gbWxLoggedin.TabStop = false;
            this.gbWxLoggedin.Text = "已登录微信";
            // 
            // lsvWxLoggedin
            // 
            this.lsvWxLoggedin.CheckBoxes = true;
            this.lsvWxLoggedin.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.lsvWxLoggedin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvWxLoggedin.FullRowSelect = true;
            this.lsvWxLoggedin.Location = new System.Drawing.Point(3, 16);
            this.lsvWxLoggedin.Name = "lsvWxLoggedin";
            this.lsvWxLoggedin.Size = new System.Drawing.Size(424, 164);
            this.lsvWxLoggedin.TabIndex = 28;
            this.lsvWxLoggedin.UseCompatibleStateImageBehavior = false;
            this.lsvWxLoggedin.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "微信ID";
            this.columnHeader7.Width = 120;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "微信昵称";
            this.columnHeader8.Width = 100;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "进程";
            // 
            // gbFriendList
            // 
            this.gbFriendList.Controls.Add(this.lsvFriendList);
            this.gbFriendList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFriendList.Location = new System.Drawing.Point(3, 192);
            this.gbFriendList.Name = "gbFriendList";
            this.gbFriendList.Size = new System.Drawing.Size(430, 183);
            this.gbFriendList.TabIndex = 1;
            this.gbFriendList.TabStop = false;
            this.gbFriendList.Text = "好友列表";
            // 
            // lsvFriendList
            // 
            this.lsvFriendList.CheckBoxes = true;
            this.lsvFriendList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lsvFriendList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvFriendList.FullRowSelect = true;
            this.lsvFriendList.Location = new System.Drawing.Point(3, 16);
            this.lsvFriendList.Name = "lsvFriendList";
            this.lsvFriendList.Size = new System.Drawing.Size(424, 164);
            this.lsvFriendList.TabIndex = 23;
            this.lsvFriendList.UseCompatibleStateImageBehavior = false;
            this.lsvFriendList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "好友呢称";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "好友ID";
            // 
            // gbGroupList
            // 
            this.gbGroupList.Controls.Add(this.lsvGroupList);
            this.gbGroupList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbGroupList.Location = new System.Drawing.Point(3, 381);
            this.gbGroupList.Name = "gbGroupList";
            this.gbGroupList.Size = new System.Drawing.Size(430, 183);
            this.gbGroupList.TabIndex = 2;
            this.gbGroupList.TabStop = false;
            this.gbGroupList.Text = "群列表";
            // 
            // lsvGroupList
            // 
            this.lsvGroupList.CheckBoxes = true;
            this.lsvGroupList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lsvGroupList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvGroupList.FullRowSelect = true;
            this.lsvGroupList.Location = new System.Drawing.Point(3, 16);
            this.lsvGroupList.Name = "lsvGroupList";
            this.lsvGroupList.Size = new System.Drawing.Size(424, 164);
            this.lsvGroupList.TabIndex = 25;
            this.lsvGroupList.UseCompatibleStateImageBehavior = false;
            this.lsvGroupList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "群聊名称";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "群ID";
            // 
            // gbGroupMemberList
            // 
            this.gbGroupMemberList.Controls.Add(this.lsvGroupMember);
            this.gbGroupMemberList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbGroupMemberList.Location = new System.Drawing.Point(3, 570);
            this.gbGroupMemberList.Name = "gbGroupMemberList";
            this.gbGroupMemberList.Size = new System.Drawing.Size(430, 186);
            this.gbGroupMemberList.TabIndex = 3;
            this.gbGroupMemberList.TabStop = false;
            this.gbGroupMemberList.Text = "群成员";
            // 
            // lsvGroupMember
            // 
            this.lsvGroupMember.CheckBoxes = true;
            this.lsvGroupMember.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.lsvGroupMember.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvGroupMember.FullRowSelect = true;
            this.lsvGroupMember.Location = new System.Drawing.Point(3, 16);
            this.lsvGroupMember.Name = "lsvGroupMember";
            this.lsvGroupMember.Size = new System.Drawing.Size(424, 167);
            this.lsvGroupMember.TabIndex = 26;
            this.lsvGroupMember.UseCompatibleStateImageBehavior = false;
            this.lsvGroupMember.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "好友呢称";
            this.columnHeader5.Width = 120;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "好友ID";
            // 
            // tlpRight
            // 
            this.tlpRight.ColumnCount = 1;
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRight.Controls.Add(this.gbReciveData, 0, 0);
            this.tlpRight.Controls.Add(this.tlpSendMsg, 0, 1);
            this.tlpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRight.Location = new System.Drawing.Point(445, 3);
            this.tlpRight.Name = "tlpRight";
            this.tlpRight.RowCount = 2;
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpRight.Size = new System.Drawing.Size(1027, 759);
            this.tlpRight.TabIndex = 2;
            // 
            // gbReciveData
            // 
            this.gbReciveData.Controls.Add(this.lsvReciveData);
            this.gbReciveData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbReciveData.Location = new System.Drawing.Point(3, 3);
            this.gbReciveData.Name = "gbReciveData";
            this.gbReciveData.Size = new System.Drawing.Size(1021, 563);
            this.gbReciveData.TabIndex = 0;
            this.gbReciveData.TabStop = false;
            this.gbReciveData.Text = "接收消息";
            // 
            // lsvReciveData
            // 
            this.lsvReciveData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader14,
            this.columnHeader12,
            this.columnHeader13,
            this.ch_content});
            this.lsvReciveData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvReciveData.Location = new System.Drawing.Point(3, 16);
            this.lsvReciveData.Name = "lsvReciveData";
            this.lsvReciveData.Size = new System.Drawing.Size(1015, 544);
            this.lsvReciveData.TabIndex = 13;
            this.lsvReciveData.UseCompatibleStateImageBehavior = false;
            this.lsvReciveData.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "微信号";
            this.columnHeader10.Width = 80;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "群名称";
            this.columnHeader11.Width = 80;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "群 ID";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "发送人名称";
            this.columnHeader12.Width = 80;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "发送人";
            this.columnHeader13.Width = 80;
            // 
            // ch_content
            // 
            this.ch_content.Text = "内容";
            this.ch_content.Width = 800;
            // 
            // tlpSendMsg
            // 
            this.tlpSendMsg.ColumnCount = 1;
            this.tlpSendMsg.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSendMsg.Controls.Add(this.txtMessage, 0, 0);
            this.tlpSendMsg.Controls.Add(this.tlpUnder2, 0, 1);
            this.tlpSendMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSendMsg.Location = new System.Drawing.Point(3, 572);
            this.tlpSendMsg.Name = "tlpSendMsg";
            this.tlpSendMsg.RowCount = 2;
            this.tlpSendMsg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSendMsg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpSendMsg.Size = new System.Drawing.Size(1021, 184);
            this.tlpSendMsg.TabIndex = 1;
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.Location = new System.Drawing.Point(3, 3);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(1015, 123);
            this.txtMessage.TabIndex = 1;
            // 
            // tlpUnder2
            // 
            this.tlpUnder2.ColumnCount = 5;
            this.tlpUnder2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpUnder2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpUnder2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpUnder2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpUnder2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUnder2.Controls.Add(this.label2, 2, 0);
            this.tlpUnder2.Controls.Add(this.label1, 0, 0);
            this.tlpUnder2.Controls.Add(this.txtTime, 1, 0);
            this.tlpUnder2.Controls.Add(this.chkStopTest, 3, 0);
            this.tlpUnder2.Controls.Add(this.btnSendMsg, 4, 0);
            this.tlpUnder2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUnder2.Location = new System.Drawing.Point(3, 132);
            this.tlpUnder2.Name = "tlpUnder2";
            this.tlpUnder2.RowCount = 1;
            this.tlpUnder2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUnder2.Size = new System.Drawing.Size(1015, 49);
            this.tlpUnder2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(103, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 49);
            this.label2.TabIndex = 3;
            this.label2.Text = "秒发送";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 49);
            this.label1.TabIndex = 1;
            this.label1.Text = "每隔";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTime
            // 
            this.txtTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTime.Location = new System.Drawing.Point(53, 14);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(44, 20);
            this.txtTime.TabIndex = 2;
            this.txtTime.Text = "3";
            // 
            // chkStopTest
            // 
            this.chkStopTest.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkStopTest.AutoSize = true;
            this.chkStopTest.Location = new System.Drawing.Point(163, 16);
            this.chkStopTest.Name = "chkStopTest";
            this.chkStopTest.Size = new System.Drawing.Size(134, 17);
            this.chkStopTest.TabIndex = 5;
            this.chkStopTest.Text = "自动测试发送群消息";
            this.chkStopTest.UseVisualStyleBackColor = true;
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendMsg.Location = new System.Drawing.Point(363, 3);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(649, 43);
            this.btnSendMsg.TabIndex = 0;
            this.btnSendMsg.Text = "发送群消息";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            // 
            // tlpTop
            // 
            this.tlpTop.ColumnCount = 2;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.Controls.Add(this.btnOpenWeChat, 0, 0);
            this.tlpTop.Controls.Add(this.lbError, 1, 0);
            this.tlpTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTop.Location = new System.Drawing.Point(3, 3);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.RowCount = 1;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.Size = new System.Drawing.Size(1475, 44);
            this.tlpTop.TabIndex = 2;
            // 
            // btnOpenWeChat
            // 
            this.btnOpenWeChat.Location = new System.Drawing.Point(3, 3);
            this.btnOpenWeChat.Name = "btnOpenWeChat";
            this.btnOpenWeChat.Size = new System.Drawing.Size(109, 38);
            this.btnOpenWeChat.TabIndex = 0;
            this.btnOpenWeChat.Text = "打开微信";
            this.btnOpenWeChat.UseVisualStyleBackColor = true;
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.Font = new System.Drawing.Font("Microsoft Sans Serif", 27F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbError.ForeColor = System.Drawing.Color.Red;
            this.lbError.Location = new System.Drawing.Point(118, 0);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(113, 40);
            this.lbError.TabIndex = 1;
            this.lbError.Text = "label1";
            // 
            // frmDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1481, 821);
            this.Controls.Add(this.tlpWhole);
            this.Name = "frmDemo";
            this.Text = "WX Hook Demo";
            this.tlpWhole.ResumeLayout(false);
            this.tlpUnder.ResumeLayout(false);
            this.tlpLeft.ResumeLayout(false);
            this.gbWxLoggedin.ResumeLayout(false);
            this.gbFriendList.ResumeLayout(false);
            this.gbGroupList.ResumeLayout(false);
            this.gbGroupMemberList.ResumeLayout(false);
            this.tlpRight.ResumeLayout(false);
            this.gbReciveData.ResumeLayout(false);
            this.tlpSendMsg.ResumeLayout(false);
            this.tlpSendMsg.PerformLayout();
            this.tlpUnder2.ResumeLayout(false);
            this.tlpUnder2.PerformLayout();
            this.tlpTop.ResumeLayout(false);
            this.tlpTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpWhole;
        private System.Windows.Forms.Button btnOpenWeChat;
        private System.Windows.Forms.TableLayoutPanel tlpUnder;
        private System.Windows.Forms.GroupBox gbWxLoggedin;
        private System.Windows.Forms.TableLayoutPanel tlpLeft;
        private System.Windows.Forms.ListView lsvWxLoggedin;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.GroupBox gbFriendList;
        private System.Windows.Forms.ListView lsvFriendList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox gbGroupList;
        private System.Windows.Forms.ListView lsvGroupList;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox gbGroupMemberList;
        private System.Windows.Forms.ListView lsvGroupMember;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private System.Windows.Forms.GroupBox gbReciveData;
        private System.Windows.Forms.ListView lsvReciveData;
        private System.Windows.Forms.ColumnHeader ch_content;
        private System.Windows.Forms.TableLayoutPanel tlpSendMsg;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.TableLayoutPanel tlpUnder2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.CheckBox chkStopTest;
    }
}