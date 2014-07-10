using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerPoint
{
	public class Program
	{
		static void Main(string[] args)
		{
			IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8000);
			Socket newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			newSocket.Bind(localEndPoint);

			newSocket.Listen(10);
			var client = newSocket.Accept();
			Console.WriteLine("连接成功");
		}
	}
}
