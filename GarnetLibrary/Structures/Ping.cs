using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Garnet.Structures
{
	[StructLayout(LayoutKind.Sequential)]
	public struct PingMessage : IMessage
	{
		public Header Header { get; set; }
		public string Message { get; set; }
	}

	public struct PongMessage : IMessage 
	{
		public Header Header { get; set; }
		public string Message { get; set; }
	}
}
