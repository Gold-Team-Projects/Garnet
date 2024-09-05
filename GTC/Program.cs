using Terminal.Gui;
using GTC;

class Program
{
	public static int Main(string[] args)
	{
		Application.Init();
		Application.Run<MainWindow>();

		Application.Shutdown();

		return 0;
	}
}