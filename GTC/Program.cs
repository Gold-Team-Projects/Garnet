﻿using GTC;
using Terminal.Gui;

Application.Init();

try
{
    Application.Run<MainWindow>();
}
finally
{
    Application.Shutdown();
}