
//------------------------------------------------------------------------------

//  <auto-generated>
//      This code was generated by:
//        TerminalGuiDesigner v1.1.0.0
//      You can make changes to this file and they will not be overwritten when saving.
//  </auto-generated>
// -----------------------------------------------------------------------------
namespace GTC {
	using Terminal.Gui;
	using static GTC.Functions; 
	
	public partial class MainWindow 
	{
		private int messagesY = 0;	

		public MainWindow() {
			InitializeComponent();
			Initialize();

			AddMessage("Hello");
			AddMessage("Hello 12345677889900987654312345678909876553221");
		}

		private void Initialize()
		{
			messagesView.ShowVerticalScrollIndicator = true;
			messagesView.ShowHorizontalScrollIndicator = true;

			textView.KeyUp += (k) =>
			{
				if (k.KeyEvent.Key == Key.Enter)
				{
					AddMessage((string)textView.Text.Substring(0, textView.Text.Length - 1));
					textView.Text = "";
				}
			};
		}

		private void AddMessage(string msg)
		{
			Label label = new Label(msg)
			{
				X = 0,
				Y = messagesY,
			};
			messagesView.ContentSize = new(
				messagesView.ContentSize.Width + AboveZero(messagesView.ContentSize.Width - label.Bounds.Width), 
				messagesView.ContentSize.Height + label.Bounds.Height
			);
			label.Width = Dim.Fill();
			messagesView.Add(label);
			messagesY += label.Bounds.Height;
		}
	}
}
