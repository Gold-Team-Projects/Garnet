using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GSP
{
	internal class Node
	{
		public string? IP { get; set; }
		public int GSP { get; set; }
		public int GID { get; set; }

		private bool availabilityCache = true;

		public Node(string? ip, int gsp, int gid)
		{
			IP = ip;
			GSP = gsp;
			GID = gid;
		}

		public bool IsAvailable => availabilityCache;

		public bool RefreshCache()
		{
			try
			{
				TcpClient client = new();
				client.Connect(IP ?? "", 2046);

				if (!client.Connected)
				{
					client.Close();
					client.Dispose();
					availabilityCache = false;
					return false;
				}
				else
				{
					client.Close();
					client.Dispose();
					availabilityCache = true;
					return true;
				}
			}
			catch (SocketException)
			{
				availabilityCache = false;
				return false;
			}
		}
	}

	internal class PrivateNode
	{
		public byte[] AESKey { get; set; }
		public byte[] AESIV { get; set; }
		public byte[] HMACKey { get; set; }
		public Node PublicNode { get; set; }

		public PrivateNode(byte[] aesKey, byte[] aesIV, byte[] hmacKey, Node n)
		{
			AESKey = aesKey;
			AESIV = aesIV;
			HMACKey = hmacKey;
			PublicNode = n;
		}
	}
}
