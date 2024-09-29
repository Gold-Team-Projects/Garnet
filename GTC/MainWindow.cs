using System.Net.NetworkInformation;
using Terminal.Gui;
using static GTC.Functions;

namespace GTC;

public class MainWindow : Window
{
	private ScrollView channelsView;
	private ScrollView messagesView;

	private MenuBar menuBar;
	private StatusBar statusBar;

	public MainWindow() : base("GTC v0.0")
	{
		Height = Dim.Fill();
		Width = Dim.Fill();

		menuBar = new(new MenuBarItem[]
		{
			new("File", new MenuItem[]
			{
				new("Quit", "", Exit, shortcut: (Key.CtrlMask | Key.Q)),
			}),
			new("Edit", new MenuItem[]
			{
				
			})
		});

		statusBar = new()
		{
			Items = new StatusItem[]
			{
				new(Key.Null, "🌐 Not Connected ", () => {}),
				new(Key.Null, "👤 Not Selected ", () => {}),
				new(Key.Null, "💳 000-000@000 ", () => {}),
				new(Key.Null, "○ Disconnected ", () => {}),
				new(Key.Null, "↑ 0.00 kb/s ", () => {}),
				new(Key.Null, "↓ 0.00 kb/s ", () => {})
			}
		};

		SetColorScheme(statusBar);
		SetColorScheme(menuBar);

		//Application.Top.Add(menuBar, statusBar);

		channelsView = new()
		{
			X = 0,
			Y = 0,
			Width = Dim.Percent(15),
			Height = Dim.Percent(80),
			Text = "No Server Selected",
			Border = new() { BorderThickness = new(2) },
			Visible = true,
		};
		messagesView = new()
		{
			X = Pos.Right(channelsView),
			Y = 0,
			Width = Dim.Percent(85),
			Height = Dim.Percent(80),
			Text = "No Channel Selected",
			Visible = true
		};

		channelsView.Add(new Label("No Network Selected!"));
		messagesView.Add(new Label("No Channel Selected!"));

		SetColorScheme(channelsView);
		SetColorScheme(messagesView);

		Add(channelsView, messagesView, menuBar, statusBar);

		ColorScheme.Normal = new(Color.Green, Color.Black);
	}

	private void Exit()
	{

	}
}