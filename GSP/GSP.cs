using System.Net.Sockets;
using GSP.Messages;
using static GSP.Functions;
using static Garnet.Functions;

namespace GSP
{
	internal class GSP
	{
		public int N { get; set; }
		public List<Node> RoutingTable;
		public Dictionary<int, Node> publicGSPLookup { get; set; } = new();
		public bool ShutdownFlag { get; set; } = false;
		public Action<Message> OnMessage = (x) => { };

		private Dictionary<Node, PrivateNode> privateGSPLookup = new();
		private Dictionary<Node, TcpClient> clients = new();

		internal GSP(int n)
		{
			N = n;
			RoutingTable = CreateRoutingTable(N);

			foreach (var node in RoutingTable)
			{
				publicGSPLookup[node.GSP] = node;
			}
		}

		internal void Activate()
		{
			foreach (Node n in RoutingTable)
			{
				n.RefreshCache();
				if (!n.IsAvailable || n.IP == null) continue;

				TcpClient client = new(n.IP, 2044);

				Handshake(client, n);

				clients.Add(n, client);

				PingMessage ping = Construct<PingMessage>(n);
				ping.Subject = "ping!";
				ping.AddQuery(PingQueryType.AvailabilityCheck);
				Write(n, ping);
				Read(n);

				Task.Run(() => Handle(n));
			}
			TcpListener clientListener = new(GetIPAddress(), 2046);
			TcpListener gspListener = new(GetIPAddress(), 2044);
			clientListener.Start();
			gspListener.Start();
		}
		internal void Handle(Node n)
		{
			Span<byte> buffer = new();
			NetworkStream ns = clients[n].GetStream();
			while (!ShutdownFlag)
			{
				ns.Read(buffer);
			}
		}
		internal async Task ListenForClients(TcpListener listener)
		{

		}
		internal async Task ListenForGSPs(TcpListener listener)
		{

		}

		private void Handshake(TcpClient client, Node n)
		{
			NetworkStream ns = client.GetStream();
			byte[] secret = ECDH(ns, Construct<HandshakeMessage>, n);
			var (key1, key2, iv) = CreateKeys(secret);

			privateGSPLookup[n] = new PrivateNode(key1, iv, key2, n);
		}
		private T Construct<T>(Node n) where T : Message, new()
		{
			T output = new();
			Message.Construct(N, 0, n.GID, 0, ref output);
			output.Route = CreateOptimalRoute(N, n.GSP);
			return output;
		}
		private Message Read(Node n)
		{
			Span<byte> _buffer = new();
			Span<byte> signed = new();
			using (var ns = clients[n].GetStream()) 
			{ 
				ns.Read(_buffer);
				ns.Read(signed);
				ns.Flush();
			}

			byte[] buffer = _buffer.ToArray();
			byte[] decrypted = Decrypt(buffer, privateGSPLookup[n].AESKey, privateGSPLookup[n].AESIV);

			if (!Verify(decrypted, signed.ToArray(), privateGSPLookup[n].HMACKey))
			{

			}

			return ToMessageFromBytes(decrypted);
		}
		private void Write(Node n, Message x)
		{
			byte[] output = Transform(x, n);
			byte[] signed = Sign(output, privateGSPLookup[n].HMACKey);
			using (var ns = clients[n].GetStream())
			{
				ns.Write(output);
				ns.Write(signed);
				ns.Flush();
			}
		}
		private void Send(int gsp, Message x)
		{
			x.Route = CreateOptimalRoute(N, gsp);
			Write(publicGSPLookup[x.Route[1]], x);

			List<int> vroute = CreateOptimalRoutes(N, gsp)[GetRandom()];
			VerificationMessage v = Construct<VerificationMessage>(publicGSPLookup[vroute[1]]);
			v.Hash = Sign(Transform(x, publicGSPLookup[vroute[1]]), privateGSPLookup[publicGSPLookup[vroute[1]]].HMACKey);
			v.Route = vroute;
			v.RealRoute = x.Route;

			Write(publicGSPLookup[vroute[1]], x);
		}

		private byte[] Transform(Message x, Node n)
		{
			byte[] output = ToBytes(x);
			output = Encrypt(output, privateGSPLookup[n].AESKey, privateGSPLookup[n].AESIV);
			return output;
		}
	}
}
