using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Client
{
	class Program
	{
		const int qtdClient = 1;
		static void Main(string[] args)
		{
			ManualResetEvent[] connectDoneEvents = new ManualResetEvent[qtdClient];
			ManualResetEvent[] sendDoneEvents = new ManualResetEvent[qtdClient];
			ManualResetEvent[] receiveDoneEvents = new ManualResetEvent[qtdClient];
			AsynchronousClient[] clients = new AsynchronousClient[qtdClient];
			for (int i = 0; i < qtdClient; i++)
			{
				connectDoneEvents[i] = new ManualResetEvent(false);
				sendDoneEvents[i] = new ManualResetEvent(false);
				receiveDoneEvents[i] = new ManualResetEvent(false);
				AsynchronousClient c = new AsynchronousClient(i, connectDoneEvents[i], sendDoneEvents[i], receiveDoneEvents[i]);
				clients[i] = c;
				ThreadPool.QueueUserWorkItem(c.StartClient, i);
			}
			Console.WriteLine("Sair");
			Console.ReadKey();
		}
	}
}
