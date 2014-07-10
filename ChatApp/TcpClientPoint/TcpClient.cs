using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TcpClientPoint
{
	public class TcpClient
	{
		public TcpClient()
		{
			IPEndPoint ipend = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
			var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			server.Connect(ipend);
		}
	}
}
