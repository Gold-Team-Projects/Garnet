using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Sockets;
using GSP.Messages;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace GSP
{
	internal static class Functions
	{
		internal static int N { get; set; } = Exponent(2, 8);

		internal static int Exponent(int x, int y) 
		{ 
			return (int)Math.Pow(x, y); 
		}

		internal static int GetDistance(int gspA, int gspB)
		{
			if (gspA <= gspB) return gspB - gspA;
			else return gspA + N - gspB;
		}
		internal static int AddDistance(int gsp, int dist)
		{
			if (gsp + dist < N) return gsp + dist;
			else return gsp + dist - N;
		}
		internal static int GetClosest(int gspX, int gspY)
		{
			var table = CreatePartialTable(gspX);
			int lowest = gspX;

			foreach (var gsp in table) // gspX is 2, gspY 14
			{
				if (GetDistance(gsp, gspY) < GetDistance(lowest, gspY)) lowest = gsp; 
			}

			return lowest;
		}

		internal static List<Node> CreateRoutingTable(int gsp)
		{
			List<Node> table = new();

			for (int i = 0; i < 8; i++) 
			{
				var g = AddDistance(gsp, Exponent(2, i));
				table.Add(new(null, g, 0));
			}

			return table;
		}
		internal static List<int> CreatePartialTable(int gsp)
		{
			List<int> output = [];
			foreach (var n in CreateRoutingTable(gsp)) output.Add(n.GSP);
			return output;
		}

		internal static int GetRandom(int min = 0, int max = 255)
		{
			return new Random().Next(min, max);
		}

		internal static List<int> CreateOptimalRoute(int gsp, int target)
		{
			List<int> route = [gsp];
			for (int i = 1; i < 100; i++)
			{
				route.Add(GetClosest(route[i - 1], target));
				if (route[i] == target) break;
			}
			return route;
		}
		internal static List<int>[] CreateOptimalRoutes(int gsp, int target, int maxLength = 8)
		{
			var table = CreatePartialTable(gsp);
			List<List<int>> routes = [];

			foreach (var g in table)
			{
				var output = CreateOptimalRoute(g, target);
				output.Insert(0, gsp);
				routes.Add(output);
			}

			Random rand = new();
			routes.RemoveAll(x => x.Count > maxLength);
			return routes.OrderBy(r => r.Count).ToArray();
		}

		public static byte[] SHA256Hash(string input)
		{
			SHA256 sha256hash = SHA256.Create();
			byte[] src = Encoding.UTF8.GetBytes(input);
			byte[] hash = sha256hash.ComputeHash(src);
			sha256hash.Dispose();
			return hash;
		}
		public static string SHA256HashAsString(string input)
		{
			return ToStringFromBytes(SHA256Hash(input));
		}
		public static int SHA256HashAsInt(string input)
		{
			SHA256 sha256hash = SHA256.Create();
			byte[] src = Encoding.UTF8.GetBytes(input);
			byte[] hash = sha256hash.ComputeHash(src);
			sha256hash.Dispose();
			return BitConverter.ToInt32(hash, 0);
		}

		public static byte[] ToBytes<T>(T input, Func<string, string>? mod = null)
		{
			string output = JsonConvert.SerializeObject(input);
			output = mod?.Invoke(output) ?? output;
			return Encoding.UTF8.GetBytes(output);
		}
		public static T FromBytes<T>(byte[] bytes)
		{
			string output = Encoding.UTF8.GetString(bytes);
			return (T)JsonConvert.DeserializeObject(output)!;
		}

		public static string ToStringFromBytes(byte[] input)
		{
			return Encoding.UTF8.GetString(input);
		}
		public static byte[] ToBytesFromString(string input)
		{
			return Encoding.UTF8.GetBytes(input);
		}

		public static byte[] ECDH(NetworkStream ns, Func<Node, HandshakeMessage> construct, Node n)
		{
			X9ECParameters ecParams = ECNamedCurveTable.GetByName("K-283");
			ECKeyGenerationParameters ecKeyGenParam = new(new ECDomainParameters(ecParams.Curve, ecParams.G, ecParams.N, ecParams.H, ecParams.GetSeed()), new());
			ECKeyPairGenerator ecKeyPairGen = new();

			ecKeyPairGen.Init(ecKeyGenParam);
			AsymmetricCipherKeyPair keyPair = ecKeyPairGen.GenerateKeyPair();

			ECDHBasicAgreement agreement = new();
			agreement.Init(keyPair.Private);

			HandshakeMessage msg1 = construct(n);

			msg1.PublicKey = keyPair.Public;
			ns.Write(ToBytes(msg1));

			Span<byte> msg2b = new();
			ns.Read(msg2b);
			HandshakeMessage msg2 = FromBytes<HandshakeMessage>(msg2b.ToArray());

			BigInteger secret = agreement.CalculateAgreement(msg2.PublicKey);

			return secret.ToByteArray();
		}
		public static (byte[] encryptionKey, byte[] hmacKey, byte[] iv) CreateKeys(byte[] secret)
		{
			int keySize = secret.Length / 3;

			return (
				SHA256Hash(ToStringFromBytes(secret[..keySize])), 
				SHA256Hash(ToStringFromBytes(secret[keySize..(keySize * 2)])),
				SHA256Hash(ToStringFromBytes(secret[(keySize * 2)..]))
			);
		}

		public static byte[] Encrypt(byte[] input, byte[] key, byte[] iv) 
		{
			byte[] output = [];
			Aes aes = Aes.Create();

			aes.Key = key;
			aes.IV = iv;
			ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

			MemoryStream ms = new();
			CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write);
			StreamWriter sw = new(cs);

			sw.Write(input);

			output = ms.ToArray();

			aes.Dispose();
			ms.Dispose();
			cs.Dispose();
			sw.Dispose();

			return output;
		}
		public static byte[] Decrypt(byte[] input, byte[] key, byte[] iv) 
		{
			byte[] output = [];
			Aes aes = Aes.Create();

			aes.Key = key;
			aes.IV = iv;
			ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

			MemoryStream ms = new(input);
			CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
			StreamReader sr = new(cs);

			sr.ReadToEnd();

			output = ms.ToArray();

			aes.Dispose();
			ms.Dispose();
			cs.Dispose();
			sr.Dispose();

			return output;
		}

		public static byte[] Sign(byte[] input, byte[] key)
		{
			using (HMACSHA256 hmac = new(key)) return hmac.ComputeHash(input);
		}
		public static bool Verify(byte[] input, byte[] received, byte[] key)
		{
			using (HMACSHA256 hmac = new(key)) return EvaluateByteArrayEquivalence(received, Sign(input, key));
		}

		public static bool EvaluateByteArrayEquivalence(byte[] x, byte[] y)
		{
			if (x.Length != y.Length) return false;
			for (int i = 0; i < x.Length; i++)
			{
				if (x[i] != y[i]) return false;
			}
			return true;
		}

		public static Message ToMessageFromBytes(byte[] x)
		{
			Message output = FromBytes<Message>(x);

			switch (output.Type)
			{
				case MessageType.Handshake: return FromBytes<HandshakeMessage>(x);
				default: return FromBytes<UnknownMessage>(x);
			}
		}
	}
}