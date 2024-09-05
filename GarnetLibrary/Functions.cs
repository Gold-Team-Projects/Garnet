using System.Text;
using System.Net;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Garnet.Structures;

namespace Garnet;

public static class Functions
{
	public static string GenerateRandomString(int length)
	{
		const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		StringBuilder result	= new();
		Random random			= new();

		for (int i = 0; i < length; i++)
		{
			int rindex = random.Next(0, characters.Length);
			result.Append(characters[rindex]);
		}

		return result.ToString();
	}

	public static string HashSHA512(string input)
	{
		SHA512 sha512hash = SHA512.Create();
		byte[] src = Encoding.UTF8.GetBytes(input);
		byte[] hash = sha512hash.ComputeHash(src);
		sha512hash.Dispose();
		return BitConverter.ToString(hash).Replace("-", "");
	}

	public static string GetIPAddress()
	{
		var host = Dns.GetHostEntry(Dns.GetHostName());

		foreach (var ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork) return ip.ToString();
		}
		
		return "";
	}

	public static byte[] ToBytes<T>(T input) where T : struct
	{
		int size = Marshal.SizeOf(input);
		byte[] arr = new byte[size];
		IntPtr ptr = Marshal.AllocHGlobal(size);

		try
		{
			Marshal.StructureToPtr(input, ptr, false);
			Marshal.Copy(ptr, arr, 0, size);
		}
		finally { Marshal.FreeHGlobal(ptr); }
		return arr;
	}

	public static T FromBytes<T>(byte[] bytes) where T : struct
	{
		GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

		try
		{
			IntPtr ptr = handle.AddrOfPinnedObject();
			return Marshal.PtrToStructure<T>(ptr);
		}
		finally { handle.Free(); }
	}

	public static Header CreateResponseHeader(Header header, MessageType type)
	{
		return new()
		{
			Type = type,
			ReceiverGID = header.SenderGID,
			SenderGID = header.ReceiverGID,
			Time = DateTime.Now
		};
	}

	public static Header CreateHeader(MessageType type, string receiver, string sender)
	{
		return new Header
		{
			ReceiverGID = receiver,
			SenderGID = sender,
			Type = type,
			Time = DateTime.Now
		};
	}
}