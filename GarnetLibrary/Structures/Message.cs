using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Garnet.Structures
{
	public enum MessageType
	{
		Ping, Pong, UnknownType
	}

	public struct Header
	{
		public MessageType Type;
		public string SenderGID;
		public string ReceiverGID;
		public DateTime Time;
	}

	public interface IMessage
	{
		public Header Header { get; set; }
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Message : IMessage
	{
		public Header Header { get; set; }
	}
}
