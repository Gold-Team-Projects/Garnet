using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GSP
{
	internal enum MessageType
	{
		Unknown, // Used to classify messages of an unknown type.
		Ping, // Sent to ask questions about a node
		Pong, // Sent to give answers in response to Pings.
		Handshake, // Sent to agree on conditions for connections
	}
	internal enum Header
	{
		UserAgent, // The software sending the message
	}

	internal class Message
	{
		public int SenderGSP { get; set; } = -1;
		public int SenderGID { get; set; } = -1;
		public int ReceiverGSP { get; set; } = -1;
		public int ReceiverGID { get; set; } = -1;
		public string Subject { get; set; } = "n/a";
		public DateTime Time { get; set; } = DateTime.Now;
		public MessageType Type { get; set; } = MessageType.Unknown;
		public Dictionary<Header, object> Headers { get; set; } = new();
		public List<int> Route { get; set; } = new();

		public static void Construct<T>(int sGSP, int sID, int rGSP, int rID, ref T obj, string? subject = null) where T : Message
		{
			obj.SenderGSP = sGSP;
			obj.SenderGID = sID;
			obj.ReceiverGSP = rGSP;
			obj.ReceiverGID = rID;
			if (subject is not null) obj.Subject = subject;
		}
	}
}
