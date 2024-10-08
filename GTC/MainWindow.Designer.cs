
//------------------------------------------------------------------------------

//  <auto-generated>
//      This code was generated by:
//        TerminalGuiDesigner v1.1.0.0
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// -----------------------------------------------------------------------------
namespace GTC {
    using System;
    using Terminal.Gui;
    
    
    public partial class MainWindow : Terminal.Gui.Window {
        
        private Terminal.Gui.ColorScheme greenOnBlack;
        
        private Terminal.Gui.ColorScheme redOnBlack;
        
        private Terminal.Gui.ColorScheme greyOnBlack;
        
        private Terminal.Gui.TabView tabView;
        
        private Terminal.Gui.ScrollView messagesView;
        
        private Terminal.Gui.FrameView frameView;
        
        private Terminal.Gui.ScrollView scrollingMenu;
        
        private Terminal.Gui.TextView textView;
        
        private Terminal.Gui.StatusBar statusBar;
        
        private Terminal.Gui.StatusItem f1EditMe;
        
        private Terminal.Gui.MenuBar menuBar;
        
        private Terminal.Gui.MenuBarItem fileF9Menu;
        
        private Terminal.Gui.MenuItem editMeMenuItem;
        
        private void InitializeComponent() {
            this.menuBar = new Terminal.Gui.MenuBar();
            this.statusBar = new Terminal.Gui.StatusBar();
            this.textView = new Terminal.Gui.TextView();
            this.scrollingMenu = new Terminal.Gui.ScrollView();
            this.frameView = new Terminal.Gui.FrameView();
            this.messagesView = new Terminal.Gui.ScrollView();
            this.tabView = new Terminal.Gui.TabView();
            this.greenOnBlack = new Terminal.Gui.ColorScheme();
            this.greenOnBlack.Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Green, Terminal.Gui.Color.Black);
            this.greenOnBlack.HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightGreen, Terminal.Gui.Color.Black);
            this.greenOnBlack.Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Green, Terminal.Gui.Color.Magenta);
            this.greenOnBlack.HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightGreen, Terminal.Gui.Color.Magenta);
            this.greenOnBlack.Disabled = new Terminal.Gui.Attribute(Terminal.Gui.Color.Gray, Terminal.Gui.Color.Black);
            this.redOnBlack = new Terminal.Gui.ColorScheme();
            this.redOnBlack.Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Red, Terminal.Gui.Color.Black);
            this.redOnBlack.HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.Black);
            this.redOnBlack.Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Red, Terminal.Gui.Color.Brown);
            this.redOnBlack.HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.Brown);
            this.redOnBlack.Disabled = new Terminal.Gui.Attribute(Terminal.Gui.Color.Gray, Terminal.Gui.Color.Black);
            this.greyOnBlack = new Terminal.Gui.ColorScheme();
            this.greyOnBlack.Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.DarkGray, Terminal.Gui.Color.Black);
            this.greyOnBlack.HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.DarkGray, Terminal.Gui.Color.Black);
            this.greyOnBlack.Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.DarkGray);
            this.greyOnBlack.HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.Black, Terminal.Gui.Color.DarkGray);
            this.greyOnBlack.Disabled = new Terminal.Gui.Attribute(Terminal.Gui.Color.DarkGray, Terminal.Gui.Color.Black);
            this.Width = Dim.Fill(0);
            this.Height = Dim.Fill(0);
            this.X = 0;
            this.Y = 0;
            this.Visible = true;
            this.ColorScheme = this.greenOnBlack;
            this.Modal = false;
            this.IsMdiContainer = false;
            this.Border.BorderStyle = Terminal.Gui.BorderStyle.Single;
            this.Border.Effect3D = false;
            this.Border.Effect3DBrush = null;
            this.Border.DrawMarginFrame = true;
            this.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Title = "GTC v0.0";
            this.tabView.Width = Dim.Percent(20f);
            this.tabView.Height = Dim.Percent(65f);
            this.tabView.X = 0;
            this.tabView.Y = 1;
            this.tabView.Visible = true;
            this.tabView.Data = "tabView";
            this.tabView.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.tabView.MaxTabTextWidth = 30u;
            this.tabView.Style.ShowBorder = true;
            this.tabView.Style.ShowTopLine = true;
            this.tabView.Style.TabsOnBottom = false;
            Terminal.Gui.TabView.Tab tabViewchannels;
            tabViewchannels = new Terminal.Gui.TabView.Tab("Channels", new View());
            tabViewchannels.View.Width = Dim.Fill();
            tabViewchannels.View.Height = Dim.Fill();
            tabView.AddTab(tabViewchannels, false);
            Terminal.Gui.TabView.Tab tabViewservers;
            tabViewservers = new Terminal.Gui.TabView.Tab("Servers", new View());
            tabViewservers.View.Width = Dim.Fill();
            tabViewservers.View.Height = Dim.Fill();
            tabView.AddTab(tabViewservers, false);
            Terminal.Gui.TabView.Tab tabViewnetworks;
            tabViewnetworks = new Terminal.Gui.TabView.Tab("Networks", new View());
            tabViewnetworks.View.Width = Dim.Fill();
            tabViewnetworks.View.Height = Dim.Fill();
            tabView.AddTab(tabViewnetworks, false);
            this.tabView.ApplyStyleChanges();
            this.Add(this.tabView);
            this.messagesView.Width = Dim.Percent(80f);
            this.messagesView.Height = Dim.Percent(80f);
            this.messagesView.X = Pos.Right(tabView);
            this.messagesView.Y = 1;
            this.messagesView.Visible = true;
            this.messagesView.ContentSize = new Size(20,10);
            this.messagesView.Data = "messagesView";
            this.messagesView.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.messagesView);
            this.frameView.Width = Dim.Percent(20f);
            this.frameView.Height = Dim.Percent(15f) + 1;
            this.frameView.X = 0;
            this.frameView.Y = Pos.Bottom(tabView);
            this.frameView.Visible = true;
            this.frameView.Data = "frameView";
            this.frameView.Border.BorderStyle = Terminal.Gui.BorderStyle.Single;
            this.frameView.Border.Effect3D = false;
            this.frameView.Border.Effect3DBrush = null;
            this.frameView.Border.DrawMarginFrame = true;
            this.frameView.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.frameView.Title = "Menu";
            this.Add(this.frameView);
            this.scrollingMenu.Width = Dim.Fill(0);
            this.scrollingMenu.Height = Dim.Fill(0);
            this.scrollingMenu.X = 0;
            this.scrollingMenu.Y = 0;
            this.scrollingMenu.Visible = true;
            this.scrollingMenu.ContentSize = new Size(20,10);
            this.scrollingMenu.Data = "scrollingMenu";
            this.scrollingMenu.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.frameView.Add(this.scrollingMenu);
            this.textView.Width = Dim.Fill(0);
            this.textView.Height = Dim.Fill(1);
            this.textView.X = 0;
            this.textView.Y = Pos.Bottom(messagesView);
            this.textView.Visible = true;
            this.textView.ColorScheme = this.greyOnBlack;
            this.textView.AllowsTab = true;
            this.textView.AllowsReturn = true;
            this.textView.WordWrap = false;
            this.textView.Data = "textView";
            this.textView.Text = "";
            this.textView.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.Add(this.textView);
            this.statusBar.Width = Dim.Fill(0);
            this.statusBar.Height = 1;
            this.statusBar.X = 0;
            this.statusBar.Y = Pos.AnchorEnd(1);
            this.statusBar.Visible = true;
            this.statusBar.Data = "statusBar";
            this.statusBar.Text = "";
            this.statusBar.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.f1EditMe = new Terminal.Gui.StatusItem(((Terminal.Gui.Key)(1048588u)), "F1 - Edit Me", null);
            this.statusBar.Items = new Terminal.Gui.StatusItem[] {
                    this.f1EditMe};
            this.Add(this.statusBar);
            this.menuBar.Width = Dim.Fill(0);
            this.menuBar.Height = 1;
            this.menuBar.X = 0;
            this.menuBar.Y = 0;
            this.menuBar.Visible = true;
            this.menuBar.Data = "menuBar";
            this.menuBar.TextAlignment = Terminal.Gui.TextAlignment.Left;
            this.fileF9Menu = new Terminal.Gui.MenuBarItem();
            this.fileF9Menu.Title = "_File (F9)";
            this.editMeMenuItem = new Terminal.Gui.MenuItem();
            this.editMeMenuItem.Title = "Edit Me";
            this.editMeMenuItem.Data = "editMeMenuItem";
            this.fileF9Menu.Children = new Terminal.Gui.MenuItem[] {
                    this.editMeMenuItem};
            this.menuBar.Menus = new Terminal.Gui.MenuBarItem[] {
                    this.fileF9Menu};
            this.Add(this.menuBar);
        }
    }
}
