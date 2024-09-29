using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace GTC
{
	internal static class Functions
	{
		internal static void SetColorScheme(View view)
		{
			view.ColorScheme = new()
			{
				Normal = new(Color.Green, Color.Black),
				HotFocus = new(Color.Black, Color.DarkGray),
				HotNormal = new(Color.White, Color.DarkGray)
			};
		}
	}
}
