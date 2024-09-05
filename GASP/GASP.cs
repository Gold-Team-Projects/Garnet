using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Garnet.Structures;
using System.Net;
using System.Net.Sockets;
using static Garnet.Functions;
using System.Runtime.InteropServices.Marshalling;

namespace Garnet
{
	internal class GASP
	{
		public string IP { get; set; }
		public DataFile DataFile
		{
			get
			{
				return _dataFile;
			}
			set
			{
				_dataFile = value;
				File.WriteAllText("./data.txt", JsonSerializer.Serialize(_dataFile));
			}
		}

		private object _lock = new();
		private DataFile _dataFile;
		
		public GASP()
		{
			_dataFile = JsonSerializer.Deserialize<DataFile>(File.ReadAllText("./data.txt"));
			
			IP = GetIPAddress();
		}

		public void Start()
		{
			using (TcpListener server = new(IPAddress.Any, 2046))
			{
				server.Start();
				Task[] tasks = [];
				int i = 0;

				while (true)
				{
					TcpClient client = server.AcceptTcpClient();
					tasks[i] = new(async () => await Handle(client));
					tasks[i].Start();
				}
			}
		}

		public async Task Handle(TcpClient client)
		{
			using (client)
			{
				NetworkStream ns = client.GetStream();

				while (client.Connected)
				{
					byte[] buffer = new byte[1024];
					await ns.ReadAsync(buffer, 0, 1024);

					Header header = FromBytes<Message>(buffer).Header;
					if (header.ReceiverGID == DataFile.GID)
					{
						switch (header.Type)
						{
							case MessageType.Ping:
								PingMessage ping = FromBytes<PingMessage>(buffer.Skip(1).ToArray());
								PongMessage pong = new PongMessage { Message = "Pong!" };

								pong.Header = CreateResponseHeader(ping.Header, MessageType.Pong);

								await ns.WriteAsync(ToBytes(pong));
								break;

							default:
								await ns.WriteAsync(ToBytes(new Unknown { Message = "Unknown Message Type" }));
								break;
						}
					}

					else
					{
						
					}
				}
				ns.Dispose();
			}
		}
	
	
	}
}
