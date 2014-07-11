using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;
using System.Threading;

namespace ServerPoint
{
	public class Program
	{
		#region Field
		Socket client = null;
		string serverIp = "127.0.0.1";
		int port = 8008;
		#endregion

		private static Program instance = new Program();

		static void Main(string[] args)
		{

			IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8000);
			Socket newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			newSocket.Bind(localEndPoint);

			newSocket.Listen(10);
			var client = newSocket.BeginAccept(new AsyncCallback(instance.OnConnectRequest), newSocket);
			Console.ReadKey();

		}
		public void OnConnectRequest(IAsyncResult ar)
		{
			var listener = ar.AsyncState as Socket;
			client = listener.EndAccept(ar);
			Console.WriteLine("Clinet{0},joined", client.RemoteEndPoint);

			var nowTime = DateTime.Now;
			string strDateLine = string.Format("Welcome{0}\n\r", nowTime.ToString("G"));
			Byte[] byteDateLine = Encoding.ASCII.GetBytes(strDateLine);
			client.Send(byteDateLine, byteDateLine.Length, 0);

			listener.BeginAccept(new AsyncCallback(this.OnConnectRequest), listener);
		}
	}



}
