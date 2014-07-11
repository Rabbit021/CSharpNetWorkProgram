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
			//IPEndPoint ipend = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
			//var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			//server.Connect(ipend);
		}
		private Socket sock = null;
		public string ServerIP = "127.0.0.1";
		public void Connect()
		{
			try
			{
				if (sock != null && sock.Connected)
				{
					sock.Shutdown(SocketShutdown.Both);
					System.Threading.Thread.Sleep(10);
					sock.Close();
				}
				sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				IPEndPoint epserver = new IPEndPoint(IPAddress.Parse(ServerIP), 8000);

				sock.Blocking = false;
				sock.BeginConnect(epserver, new AsyncCallback(OnConnect), sock);
			}
			catch (Exception exp)
			{

			}
		}

		private void OnConnect(IAsyncResult ar)
		{
			var sock = ar.AsyncState as Socket;
			try
			{
				if (sock.Connected)
				{
					SetupRecieveCallback(sock);
				}
				else
				{
					//失败
				}
			}
			catch (Exception)
			{

			}
		}

		private byte[] buff = new byte[256];
		private void SetupRecieveCallback(Socket sock)
		{
			try
			{
				sock.BeginReceive(buff, 0, buff.Length, SocketFlags.None, OnRecievedData, sock);
			}
			catch (Exception)
			{

			}
		}

		private void OnRecievedData(IAsyncResult ar)
		{
			var sock = ar.AsyncState as Socket;
			try
			{
				int rec = sock.EndReceive(ar);
				if (rec > 0)
				{
					string str = Encoding.ASCII.GetString(buff, 0, rec);
					SetupRecieveCallback(sock);
				}
				else
				{
					Console.WriteLine("Client{0},disConnected",sock.RemoteEndPoint);
					sock.Shutdown(SocketShutdown.Both);
					sock.Close();
				}
			}
			catch (Exception)
			{

			}
		}
	}
}
