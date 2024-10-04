using GTC;
using Terminal.Gui;

Application.Init();

try
{
    Application.Run(new MainWindow());
}
finally
{
    Application.Shutdown();
}