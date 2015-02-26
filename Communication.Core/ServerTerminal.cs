using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Communication.Core.Sockets
{
	public class ServerTerminal
	{
		private class Client
		{
			public Socket socket { get; set; }
			public bool isWebSocket { get; set; }
		}

		Socket m_socket;
		SocketListener m_listener;

		#region private members
		private string webSocketOrigin;     // location for the protocol handshake
		private string webSocketLocation;   // location for the protocol handshake

		private bool m_Closed;
		private Socket m_socWorker;

		private List<Client> m_connectedClient = new List<Client>();

		private static ManualResetEvent allDone = new ManualResetEvent(false);

		#endregion

		#region Events
		public event TCPTerminal_MessageRecivedDel MessageRecived;
		public event TCPTerminal_ConnectDel ClientConnect;
		public event TCPTerminal_DisconnectDel ClientDisconnect;
		#endregion

		// Thread signal.


		#region public void StartListen
		public void StartListen(int port, IPAddress ipAddress)
		{
			IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, port);

			m_socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			//bind to local IP Address...
			//if ip address is allready being used write to log log
			try
			{
				m_socket.Bind(ipLocal);
			}
			catch (Exception ex)
			{
				Debug.Fail(ex.ToString(), string.Format("Can't connect to port {0}!", port));
				return;
			}
			//start listening...
			m_socket.Listen(4);

			webSocketLocation = "ws://" + ipAddress.ToString() + "/service";
			webSocketOrigin = "http://" + ipAddress.ToString();

			new Thread(() =>
			{
				while (true)
				{
					// Set the event to nonsignaled state.
					allDone.Reset();

					// Start an asynchronous socket to listen for connections.
					//Console.WriteLine("Waiting for a connection...");
					// create the call back for any client connections...
					if (m_socket != null)
						m_socket.BeginAccept(new AsyncCallback(OnClientConnection), null);

					// Wait until a connection is made before continuing.
					allDone.WaitOne();
				}
			}).Start();
		}

		public void StartListen(int port, string ipAddress)
		{
			IPAddress ip = Dns.GetHostEntry(ipAddress).AddressList[0];
			StartListen(port, ip);
		}

		public void StartListen(int port)
		{
			StartListen(port, IPAddress.Any);
		}
		#endregion

		private void OnClientConnection(IAsyncResult asyn)
		{
			if (m_Closed)
			{
				return;
			}

			try
			{
				allDone.Set();

				m_socWorker = m_socket.EndAccept(asyn);

				bool WebSocket = ShakeHands(m_socWorker);

				m_connectedClient.Add(new Client() { socket = m_socWorker, isWebSocket = WebSocket });

				RaiseClientConnected(m_socWorker);

				m_listener = new SocketListener();
				m_listener.MessageRecived += OnMessageRecived;
				m_listener.Disconnected += OnClientDisconnection;

				m_listener.StartReciving(m_socWorker);
			}
			catch (ObjectDisposedException odex)
			{
				Debug.Fail(odex.ToString(), "OnClientConnection: Socket has been closed");
			}
			catch (Exception sex)
			{
				Debug.Fail(sex.ToString(), "OnClientConnection: Socket failed");
			}

		}

		/// <summary>
		/// Takes care of the initial handshaking between the the client and the server
		/// </summary>
		private bool ShakeHands(Socket conn)
		{
			using (var stream = new NetworkStream(conn))
			using (var reader = new StreamReader(stream))
			using (var writer = new StreamWriter(stream))
			{
				Byte[] bytes = new Byte[conn.Available];
				stream.Read(bytes, 0, bytes.Length);
				String data = Encoding.UTF8.GetString(bytes);

				if (!new Regex("WebSocket").IsMatch(data))
					return false;

				writer.WriteLine("HTTP/1.1 101 Web Socket Protocol Handshake");
				writer.WriteLine("Upgrade: WebSocket");
				writer.WriteLine("Connection: Upgrade");
				writer.WriteLine("WebSocket-Origin: " + webSocketOrigin);
				writer.WriteLine("WebSocket-Location: " + webSocketLocation);
				writer.WriteLine("Sec-WebSocket-Accept: " + Convert.ToBase64String(
									SHA1.Create().ComputeHash(
										Encoding.UTF8.GetBytes(new Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11")
								)));


				writer.WriteLine("");
			}
			return true;
		}

		private void OnClientDisconnection(Socket socket)
		{
			RaiseClientDisconnected(socket);

			// Try to re-establish connection
			m_socket.BeginAccept(new AsyncCallback(OnClientConnection), null);

		}

		private void SendWebSocketMessage(byte[] data)
		{
			try
			{
				byte[] mask = new byte[2];
				mask[0] = 0x81;
				mask[1] = Convert.ToByte(data.Length);

				byte[] send = new byte[data.Length + 2];
				mask.CopyTo(send, 0);
				data.CopyTo(send, 2);

				m_socWorker.Send(send);
				//m_socWorker.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), m_socWorker);
			}
			catch (SocketException se)
			{
				Debug.Fail(se.ToString(), "Message could not be sent");
			}
		}

		private void SendWebSocketMessage(string str)
		{
			try
			{
				SendWebSocketMessage(Encoding.UTF8.GetBytes(str));
			}
			catch (SocketException se)
			{
				Debug.Fail(se.ToString(), "Message could not be sent");
			}
		}

		public void SendMessage(string mes, EndPoint endpoint)
		{
			byte[] byData = System.Text.Encoding.ASCII.GetBytes(mes.ToString());
			SendMessage(byData, endpoint);
		}

		public void SendMessage(byte[] data, EndPoint endpoint)
		{
			bool found = false;
			Client client = null;
			foreach (var cli in m_connectedClient)
			{
				if (cli.socket.RemoteEndPoint == endpoint)
				{
					client = cli;
					m_socWorker = client.socket;
					found = true;
					break;
				}
			}

			if (found)
			{
				if (m_socWorker == null)
				{
					return;
				}

				try
				{
					if (client.isWebSocket)
						SendWebSocketMessage(data);
					else
						m_socWorker.Send(data);

					//m_socWorker.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), m_socWorker);
				}
				catch (SocketException se)
				{
					Debug.Fail(se.ToString(), "Message could not be sent");
				}
			}
		}

		/*private static void SendCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the socket from the state object.
				Socket handler = (Socket)ar.AsyncState;

				// Complete sending the data to the remote device.
				int bytesSent = handler.EndSend(ar);
				Console.WriteLine("Sent {0} bytes to client.", bytesSent);

				handler.Shutdown(SocketShutdown.Both);
				handler.Close();

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}*/

		public void Close()
		{
			try
			{
				if (m_socket != null)
				{
					m_Closed = true;

					if (m_listener != null)
					{
						foreach (var client in m_connectedClient)
							m_listener.StopListening(client.socket);

						m_connectedClient.Clear();
					}

					m_socket.Close();

					m_listener = null;
					m_socWorker = null;
					m_socket = null;
				}
			}
			catch (ObjectDisposedException odex)
			{
				Debug.Fail(odex.ToString(), "Stop failed");
			}
		}

		private void OnMessageRecived(string message, Socket socket)
		{
			if (MessageRecived != null)
			{
				MessageRecived(message, socket);
			}
		}

		private void RaiseClientConnected(Socket socket)
		{
			if (ClientConnect != null)
			{
				ClientConnect(socket);
			}
		}

		private void RaiseClientDisconnected(Socket socket)
		{
			if (ClientDisconnect != null)
			{
				Client client = null;
				foreach (var cli in m_connectedClient)
				{
					if (cli.socket == socket)
					{
						client = cli;
						break;
					}
				}
				m_connectedClient.Remove(client);
				ClientDisconnect(socket);
			}
		}
	}
}
