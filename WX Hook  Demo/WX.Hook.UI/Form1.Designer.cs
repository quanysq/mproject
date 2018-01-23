namespace WX.Hook.UI
{
    partial class Form1
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
            this.btnOpenWechat = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lv_recv_msg = new System.Windows.Forms.ListView();
            this.ch_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_content = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lb_online = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSendGroupMsg = new System.Windows.Forms.Button();
            this.tb_content = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lsvFriend = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsvGroup = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label6 = new System.Windows.Forms.Label();
            this.lsvGroupMember = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label7 = new System.Windows.Forms.Label();
            this.btnSendSomeone = new System.Windows.Forms.Button();
            this.btnSendSomeoneMsg = new System.Windows.Forms.Button();
            this.lsvWxLoggedin = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOpenWechat
            // 
            this.btnOpenWechat.Location = new System.Drawing.Point(12, 600);
            this.btnOpenWechat.Name = "btnOpenWechat";
            this.btnOpenWechat.Size = new System.Drawing.Size(113, 23);
            this.btnOpenWechat.TabIndex = 0;
            this.btnOpenWechat.Text = "打开微信";
            this.btnOpenWechat.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "收到的消息";
            // 
            // lv_recv_msg
            // 
            this.lv_recv_msg.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_id,
            this.ch_type,
            this.ch_content});
            this.lv_recv_msg.Location = new System.Drawing.Point(12, 267);
            this.lv_recv_msg.Name = "lv_recv_msg";
            this.lv_recv_msg.Size = new System.Drawing.Size(1120, 310);
            this.lv_recv_msg.TabIndex = 12;
            this.lv_recv_msg.UseCompatibleStateImageBehavior = false;
            this.lv_recv_msg.View = System.Windows.Forms.View.Details;
            // 
            // ch_id
            // 
            this.ch_id.Text = "id";
            // 
            // ch_type
            // 
            this.ch_type.Text = "type";
            // 
            // ch_content
            // 
            this.ch_content.Text = "content";
            this.ch_content.Width = 1100;
            // 
            // lb_online
            // 
            this.lb_online.AutoSize = true;
            this.lb_online.Location = new System.Drawing.Point(1033, 586);
            this.lb_online.Name = "lb_online";
            this.lb_online.Size = new System.Drawing.Size(97, 13);
            this.lb_online.TabIndex = 20;
            this.lb_online.Text = "当前在线微信：0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 584);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "群文本消息";
            // 
            // btnSendGroupMsg
            // 
            this.btnSendGroupMsg.Location = new System.Drawing.Point(1011, 600);
            this.btnSendGroupMsg.Name = "btnSendGroupMsg";
            this.btnSendGroupMsg.Size = new System.Drawing.Size(121, 25);
            this.btnSendGroupMsg.TabIndex = 16;
            this.btnSendGroupMsg.Text = "发送群文本消息";
            this.btnSendGroupMsg.UseVisualStyleBackColor = true;
            // 
            // tb_content
            // 
            this.tb_content.Location = new System.Drawing.Point(131, 603);
            this.tb_content.Name = "tb_content";
            this.tb_content.Size = new System.Drawing.Size(874, 20);
            this.tb_content.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(312, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "好友列表";
            // 
            // lsvFriend
            // 
            this.lsvFriend.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lsvFriend.FullRowSelect = true;
            this.lsvFriend.Location = new System.Drawing.Point(315, 26);
            this.lsvFriend.Name = "lsvFriend";
            this.lsvFriend.Size = new System.Drawing.Size(217, 220);
            this.lsvFriend.TabIndex = 22;
            this.lsvFriend.UseCompatibleStateImageBehavior = false;
            this.lsvFriend.View = System.Windows.Forms.View.Details;
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
            // lsvGroup
            // 
            this.lsvGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lsvGroup.FullRowSelect = true;
            this.lsvGroup.Location = new System.Drawing.Point(541, 26);
            this.lsvGroup.Name = "lsvGroup";
            this.lsvGroup.Size = new System.Drawing.Size(217, 220);
            this.lsvGroup.TabIndex = 24;
            this.lsvGroup.UseCompatibleStateImageBehavior = false;
            this.lsvGroup.View = System.Windows.Forms.View.Details;
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(538, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "群列表";
            // 
            // lsvGroupMember
            // 
            this.lsvGroupMember.CheckBoxes = true;
            this.lsvGroupMember.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.lsvGroupMember.FullRowSelect = true;
            this.lsvGroupMember.Location = new System.Drawing.Point(767, 26);
            this.lsvGroupMember.Name = "lsvGroupMember";
            this.lsvGroupMember.Size = new System.Drawing.Size(217, 220);
            this.lsvGroupMember.TabIndex = 25;
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(764, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "群好友";
            // 
            // btnSendSomeone
            // 
            this.btnSendSomeone.Location = new System.Drawing.Point(1005, 26);
            this.btnSendSomeone.Name = "btnSendSomeone";
            this.btnSendSomeone.Size = new System.Drawing.Size(125, 25);
            this.btnSendSomeone.TabIndex = 26;
            this.btnSendSomeone.Text = "@ 群里成员";
            this.btnSendSomeone.UseVisualStyleBackColor = true;
            // 
            // btnSendSomeoneMsg
            // 
            this.btnSendSomeoneMsg.Location = new System.Drawing.Point(1005, 57);
            this.btnSendSomeoneMsg.Name = "btnSendSomeoneMsg";
            this.btnSendSomeoneMsg.Size = new System.Drawing.Size(125, 25);
            this.btnSendSomeoneMsg.TabIndex = 26;
            this.btnSendSomeoneMsg.Text = "@ 群里成员+消息";
            this.btnSendSomeoneMsg.UseVisualStyleBackColor = true;
            // 
            // lsvWxLoggedin
            // 
            this.lsvWxLoggedin.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.lsvWxLoggedin.FullRowSelect = true;
            this.lsvWxLoggedin.Location = new System.Drawing.Point(12, 26);
            this.lsvWxLoggedin.Name = "lsvWxLoggedin";
            this.lsvWxLoggedin.Size = new System.Drawing.Size(297, 220);
            this.lsvWxLoggedin.TabIndex = 27;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "已登录微信";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 635);
            this.Controls.Add(this.lsvWxLoggedin);
            this.Controls.Add(this.btnSendSomeoneMsg);
            this.Controls.Add(this.btnSendSomeone);
            this.Controls.Add(this.lsvGroupMember);
            this.Controls.Add(this.lsvGroup);
            this.Controls.Add(this.lsvFriend);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lb_online);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSendGroupMsg);
            this.Controls.Add(this.tb_content);
            this.Controls.Add(this.lv_recv_msg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnOpenWechat);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenWechat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lv_recv_msg;
        private System.Windows.Forms.ColumnHeader ch_id;
        private System.Windows.Forms.ColumnHeader ch_type;
        private System.Windows.Forms.ColumnHeader ch_content;
        private System.Windows.Forms.Label lb_online;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSendGroupMsg;
        private System.Windows.Forms.TextBox tb_content;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView lsvFriend;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView lsvGroup;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView lsvGroupMember;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSendSomeone;
        private System.Windows.Forms.Button btnSendSomeoneMsg;
        private System.Windows.Forms.ListView lsvWxLoggedin;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader9;
    }
}

