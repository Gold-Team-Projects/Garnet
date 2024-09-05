using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace GTC
{
	internal class MainWindow : Window
	{
		public MainWindow()
		{
			Title = "GTC v0.1";

			TextField input = new()
			{
				Height = Dim.Fill(),
				Width = Dim.Fill()
			};

			Add(input);
		}
	}
}
