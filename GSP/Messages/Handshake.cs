using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;

namespace GSP.Messages
{
	internal class HandshakeMessage : Message
	{
		public AsymmetricKeyParameter? PublicKey { get; set; }
	}
}
