using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSP.Messages
{
	internal class VerificationMessage : Message
	{
		public byte[] Hash { get; set; } = new byte[0];
		public List<int> RealRoute { get; set; } = new();
	}
}
