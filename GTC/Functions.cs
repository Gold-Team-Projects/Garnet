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
		public static void ResizeScrollView(ref ScrollView view)
		{
			int w = 0;
			int h = 0;
			foreach (View v in view.Subviews)
			{
				w = v.Frame.Width > w ? v.Frame.Width : w;
				h += v.Frame.Height;
			}
			view.ContentSize = new(w, h);
		}

		public static int AboveZero(int i)
		{
			if (i > 0) return i;
			else return 0;
		}
	}
}
