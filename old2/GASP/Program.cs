using System.Runtime.InteropServices;
using System.Text.Json;
using Spectre.Console;
using Garnet;
using Garnet.Structures;
using static Garnet.Functions;

static class Program
{
	public static int Main(string[] args)
	{
		string name = "[red]GASP[/] ???-??? ([red]??.??.??.??@????[/])";

		AnsiConsole.MarkupLine($"[[{name}]] Welcome to the [red]Garnet Address and Service Provider[/] application!");

		try
		{
			if (args[0] == "-t")
			{
				AnsiConsole.MarkupLine("Welcome to the GASP Application Test Program.");


			}
		}
		catch { }

		if (File.Exists("./data.txt"))
		{
			DataFile dataFile = JsonSerializer.Deserialize<DataFile>(File.ReadAllText("./data.txt"));

			name = $"[red]GASP[/] {dataFile.GID} ([red]{GetIPAddress()}@{dataFile.NetworkName}[/])";

			AnsiConsole.MarkupLine($"[[{name}]] [red]'data.txt'[/] found!");
			AnsiConsole.MarkupLine($"[[{name}]] Starting GASP...");

			GASP self = new();
			self.Start();
		}
		else
		{
			AnsiConsole.MarkupLine($"[[{name}]] [red]'data.txt'[/] not found!\n");

			var newNetwork = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title($"[[{name}]] Would you like to [red]create[/] a [red]new network[/]?")
					.AddChoices(["Yes", "No"])
					.HighlightStyle(Color.Red)
				);

			if (newNetwork == "Yes")
			{
				var netname = AnsiConsole.Prompt(new TextPrompt<string>($"[[{name}]] Enter the network name: "));
				var ip = AnsiConsole.Prompt(new TextPrompt<string>($"[[{name}]] Enter an IP or address for this machine: (press 'enter' for {GetIPAddress()}) ").AllowEmpty());

				if (ip == "") ip = GetIPAddress();

				name = $"[red]GASP[/] 000-000 ([red]{ip}@{netname}[/])";

				string key = GenerateRandomString(16);
				string hash = HashSHA512(key);

				AnsiConsole.MarkupLine($"[[{name}]] Creating network [red]{netname}[/]...");

				DataFile dataFile = new()
				{
					GID = "000-000",
					NetworkName = netname,
					NetworkKey = hash,
					Children = [],
					Siblings = []
				};
				File.WriteAllText("./data.txt", JsonSerializer.Serialize(dataFile));

				AnsiConsole.MarkupLine($"[[{name}]] Created network [red]{netname}[/]!");
				AnsiConsole.MarkupLine($"[[{name}]] Starting GASP...");

				GASP self = new();
				self.Start();
			}
			else
			{
				var ip = AnsiConsole.Prompt(new TextPrompt<string>($"[[{name}]] Enter the [red]IP[/] of a [red]connected GASP[/]: "));
				var key = AnsiConsole.Prompt(new TextPrompt<string>($"[[{name}]] Enter the [red]network key[/]: "));
			}
		}

		return 0;
	}
}