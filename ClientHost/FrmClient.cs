using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aviad.Utils.Communication.Core;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace ClientHost
{
	public partial class FrmClient : Form
	{
		ClientTerminal m_terminal = new ClientTerminal();

		public FrmClient()
		{
			InitializeComponent();
			m_terminal.Connected += m_TerminalClient_Connected;
			m_terminal.Disconncted += m_TerminalClient_ConnectionDroped;
			m_terminal.MessageRecived += m_TerminalClient_MessageRecived;
		}

		private void cmdConnect_Click(object sender, EventArgs e)
		{
			try
			{
				string szIPSelected = txtIPAddress.Text;
				string szPort = txtPort.Text;
				int alPort = System.Convert.ToInt16(szPort, 10);
				IPAddress remoteIPAddress = System.Net.IPAddress.Parse(szIPSelected);

				m_terminal.Connect(remoteIPAddress, alPort);
			}
			catch (SocketException se)
			{
				MessageBox.Show(se.Message);
			}
		}

		private void cmdSendData_Click(object sender, EventArgs e)
		{
			try
			{
				m_terminal.SendMessage(txtData.Text);
			}
			catch (SocketException se)
			{
				MessageBox.Show(se.Message);
			}
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			m_terminal.Close();

			cmdConnect.Enabled = true;
			cmdClose.Enabled = false;
		}

		private void m_btnNew_Click(object sender, EventArgs e)
		{
			new FrmClient().Show();
		}

		void m_TerminalClient_Connected(Socket socket)
		{
			m_terminal.SendMessage("Hello There");

			cmdConnect.Enabled = false;
			cmdClose.Enabled = true;
			PublishMessage(listLog, "Connection Opened!");

			m_terminal.StartListen();
			PublishMessage(listLog, "Start listening to server messages");
		}

		void m_TerminalClient_ConnectionDroped(Socket socket)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new TCPTerminal_DisconnectDel(m_TerminalClient_ConnectionDroped), socket);
				return;
			}

			cmdConnect.Enabled = true;
			cmdClose.Enabled = false;

			PublishMessage(listLog, "Server has been disconnected!");
		}

		void m_TerminalClient_MessageRecived(string message, Socket socket)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new TCPTerminal_MessageRecivedDel(m_TerminalClient_MessageRecived), message, socket);
				return;
			}

			PublishMessage(listMessages, message);
		}

		private void PublishMessage(ListBox listBox, string mes)
		{
			if (InvokeRequired)
			{
				BeginInvoke((ThreadStart)delegate { PublishMessage(listBox, mes); });
				return;
			}

			listBox.Items.Add(mes);
		}
	}
}
