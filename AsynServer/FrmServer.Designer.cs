﻿namespace AsynServer
{
	partial class FrmServer
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmdClose = new System.Windows.Forms.Button();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmdConnect = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.listLog = new System.Windows.Forms.ListBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.listMessages = new System.Windows.Forms.ListBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.cbClients = new System.Windows.Forms.ComboBox();
			this.txtData = new System.Windows.Forms.TextBox();
			this.cmdSendData = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cmdClose);
			this.groupBox1.Controls.Add(this.txtPort);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cmdConnect);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(2, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(438, 93);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Setting";
			// 
			// cmdClose
			// 
			this.cmdClose.Enabled = false;
			this.cmdClose.Location = new System.Drawing.Point(175, 49);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(120, 24);
			this.cmdClose.TabIndex = 5;
			this.cmdClose.Text = "Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// txtPort
			// 
			this.txtPort.Location = new System.Drawing.Point(151, 19);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(120, 20);
			this.txtPort.TabIndex = 4;
			this.txtPort.Text = "8221";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(95, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Port :";
			// 
			// cmdConnect
			// 
			this.cmdConnect.Location = new System.Drawing.Point(63, 49);
			this.cmdConnect.Name = "cmdConnect";
			this.cmdConnect.Size = new System.Drawing.Size(96, 24);
			this.cmdConnect.TabIndex = 0;
			this.cmdConnect.Text = "Connect";
			this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tabControl1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(2, 218);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.panel1.Size = new System.Drawing.Size(438, 216);
			this.panel1.TabIndex = 13;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 5);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(438, 211);
			this.tabControl1.TabIndex = 11;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.listLog);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(430, 185);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Log";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// listLog
			// 
			this.listLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listLog.FormattingEnabled = true;
			this.listLog.Location = new System.Drawing.Point(3, 3);
			this.listLog.Name = "listLog";
			this.listLog.Size = new System.Drawing.Size(424, 179);
			this.listLog.TabIndex = 10;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.listMessages);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(430, 185);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Messages";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// listMessages
			// 
			this.listMessages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listMessages.FormattingEnabled = true;
			this.listMessages.Location = new System.Drawing.Point(3, 3);
			this.listMessages.Name = "listMessages";
			this.listMessages.Size = new System.Drawing.Size(424, 179);
			this.listMessages.TabIndex = 11;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.cbClients);
			this.panel3.Controls.Add(this.txtData);
			this.panel3.Controls.Add(this.cmdSendData);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(2, 95);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(438, 123);
			this.panel3.TabIndex = 17;
			// 
			// cbClients
			// 
			this.cbClients.FormattingEnabled = true;
			this.cbClients.Location = new System.Drawing.Point(5, 12);
			this.cbClients.Name = "cbClients";
			this.cbClients.Size = new System.Drawing.Size(368, 21);
			this.cbClients.TabIndex = 11;
			// 
			// txtData
			// 
			this.txtData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtData.Location = new System.Drawing.Point(1, 39);
			this.txtData.Multiline = true;
			this.txtData.Name = "txtData";
			this.txtData.Size = new System.Drawing.Size(367, 56);
			this.txtData.TabIndex = 10;
			this.txtData.Text = "enter data to send here...";
			// 
			// cmdSendData
			// 
			this.cmdSendData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSendData.Location = new System.Drawing.Point(374, 39);
			this.cmdSendData.Name = "cmdSendData";
			this.cmdSendData.Size = new System.Drawing.Size(55, 56);
			this.cmdSendData.TabIndex = 9;
			this.cmdSendData.Text = "Tx";
			this.cmdSendData.Click += new System.EventHandler(this.cmdSendData_Click);
			// 
			// FrmServer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(442, 436);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox1);
			this.Name = "FrmServer";
			this.Padding = new System.Windows.Forms.Padding(2);
			this.Text = "Server - TCP Socket based Communication";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmServer_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.TextBox txtPort;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cmdConnect;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ListBox listLog;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ListBox listMessages;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ComboBox cbClients;
		private System.Windows.Forms.TextBox txtData;
		private System.Windows.Forms.Button cmdSendData;
	}
}

