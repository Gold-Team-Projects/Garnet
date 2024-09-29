using Terminal.Gui;
using GTC;

Application.Top.Add(new MainWindow());
Application.Run();

// Before the application exits, reset Terminal.Gui for clean shutdown
Application.Shutdown();
