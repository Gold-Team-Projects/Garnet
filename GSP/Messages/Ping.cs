using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSP.Messages
{
	internal class PingMessage : Message
	{
		public List<PingQuery> Queries { get; set; }

		public PingMessage()
		{
			Queries = [];
		}
		public void AddQuery(PingQueryType type, object[]? args = null)
		{
			Queries.Append(new() { Query = type, Args = args ?? [] });
		}
	}
	internal class PongMessage : Message
	{
		public Dictionary<PingQueryType, object> Responses { get; set; }

		public PongMessage()
		{
			Responses = new();
		}
	}

	internal enum PingQueryType
	{
		AvailabilityCheck, GetRoutingTable
	}
	internal struct PingQuery
	{
		public PingQueryType Query;
		public object[] Args;
	}
}
