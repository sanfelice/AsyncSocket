using System.Net.Sockets;

namespace Communication.Core
{
    public delegate void TCPTerminal_MessageRecivedDel(string message, Socket socket);
    public delegate void TCPTerminal_ConnectDel(Socket socket);
    public delegate void TCPTerminal_DisconnectDel(Socket socket);
}
