using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

namespace Communication.Core
{
    public class ServerTerminal
    {
        Socket m_socket;
        SocketListener m_listener;
        
        private bool m_Closed;
        private Socket m_socWorker;

        public event TCPTerminal_MessageRecivedDel MessageRecived;
        public event TCPTerminal_ConnectDel ClientConnect;
        public event TCPTerminal_DisconnectDel ClientDisconnect;

		private List<Socket> m_socWorkers = new List<Socket>();

		// Thread signal.
		public static ManualResetEvent allDone = new ManualResetEvent(false);
        
        public void StartListen(int port)
        {
            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, port);

            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            //bind to local IP Address...
            //if ip address is allready being used write to log log
            try
            {
                m_socket.Bind(ipLocal);
            }
            catch(Exception ex)
            {
                Debug.Fail(ex.ToString(),string.Format("Can't connect to port {0}!", port));
                return;
            }
            //start listening...
            m_socket.Listen(4);

			new Thread(( ) => {
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

        private void OnClientDisconnection(Socket socket)
        {
            RaiseClientDisconnected(socket);

            // Try to re-establish connection
            m_socket.BeginAccept(new AsyncCallback(OnClientConnection), null);

        }

        public void SendMessage(string mes)
        {
            if (m_socWorker == null)
            {
                return;
            }

            try
            {
                Object objData = mes;
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString());
                m_socWorker.Send(byData);
            }
            catch (SocketException se)
            {
                Debug.Fail(se.ToString(), string.Format("Message '{0}' could not be sent", mes));
            }
        }

		public void SendMessage(string mes, IPEndPoint endpoint)
		{
			bool found = false;
			foreach (var soc in m_socWorkers)
			{
				if (soc.RemoteEndPoint == endpoint)
				{
					m_socWorker = soc;
					found = true;
					break;
				}
			}

			if (found)
			{
				SendMessage(mes);
			}
		}

		public void SendMessage(byte[] data, IPEndPoint endpoint)
		{
			bool found = false;
			foreach (var soc in m_socWorkers)
			{
				if (soc.RemoteEndPoint == endpoint)
				{
					m_socWorker = soc;
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
                        m_listener.StopListening(ref m_socWorkers);
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
				m_socWorkers.Add(socket);
                ClientConnect(socket);
            }
        }

        private void RaiseClientDisconnected(Socket socket)
        {
            if (ClientDisconnect != null)
            {
				m_socWorkers.Remove(socket);
                ClientDisconnect(socket);
            }
        }
    }
}
