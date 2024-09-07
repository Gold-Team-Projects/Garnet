using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garnet.Structures
{
	internal struct DataFile
	{
		public string GID { get; set; }
		public string NetworkName { get; set; }
		public string NetworkKey { get; set; }
		public KeyValuePair<string, string>[] Children { get; set; }
		public KeyValuePair<string, string>[] Siblings { get; set; }
	}
}
