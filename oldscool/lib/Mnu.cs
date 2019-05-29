using System;
using System.Collections.Generic;
using System.Linq;

namespace OldSchool 
{
	// One entry within the menu
	public class MnuEntry
	{
		public int X { get; }
		public int Y { get; }
		public ConsoleKeyInfo Hotkey { get; }
		public string Text { get; }
		public string StatusLine { get; }

		public MnuEntry(int x, int y, string text, ConsoleKeyInfo hotkey=default(ConsoleKeyInfo), string statusLine=null) 
		{
			X = x;
			Y = y;
			Text = string.IsNullOrEmpty(text) ? "" : text;
			Hotkey = hotkey;
			StatusLine = statusLine;
		}
	}

	public class Mnu
	{
		public Mnu() {}

		public void Add(MnuEntry entry) 
		{
			_maxWidth = Math.Max(entry.X+entry.Text.Length, _maxWidth);
			_maxHeight = Math.Max(entry.Y, _maxHeight);
			_mnuEntries.Add(entry);
		}

		public void Add(int x, int y, string text, ConsoleKeyInfo hotkey=default(ConsoleKeyInfo), string statusline=null)
		{
			var mnuEntry = new MnuEntry(x, y, text, hotkey, statusline);
			Add(mnuEntry);
		}

		public int Handle(string title, int x, int y, ConsoleKeyInfo[] exitKeys=null)
		{
			if( !_mnuEntries.Any() ) return -1;
			if( _win==null) _win = CreateWindow(x, y, title);
			
			_win.Cls();
	    /*
			foreach(var entry in _mnuEntries) {
				_win->Hot(entry.X, entry.Y, entry.Text, TColors::MnuText, TColors::MnuHotkey);
			}
			_win.Visible = false;

			var finished = false;
			while ( !finished ) {
				var entry = _mnuEntries[_current];
				_win->Hot(entry.X, entry.Y, entry.Text, TColors::MnuInvers, TColors::MnuHotInvers);
				Vio.Status(entry.Status, TColors::MnuStatusLine, TColors::MnuStatusInvers);
				_win->Update();

				var ret = getch();
				if( exitKeys.Contains(ret) )
					return ret;
			}
			*/
			return 0;
		}

		Win _win;
		Win CreateWindow(int x, int y, string title) 
		{ 
			// m_Win->cursor(TWin::CurOff);						// Cursor abschalten
			// m_Win->setBackground(TColors::MnuText);
			// m_Win->setFrame(TWin::FrameSingle, TColors::MnuFrame);
			//m_Win->setTitle(title, TWin::TitleOZ, TColors::MnuTitle);

			var win = new Win(x, y, _maxWidth+2, _maxHeight+2);
			win.SetTitle(title, TitlePosition.TopCenter, Palette.Instance.Get(AttributeRole.MnuTitle));
			// win.setBackground()
			// win.Cursor = Cursor.Off;
			win.SetFrame(FrameType.Single, Palette.Instance.Get(AttributeRole.MnuFrame));
			return win;
		}

		List<MnuEntry> _mnuEntries = new List<MnuEntry>();
		int _current;
		int _maxWidth;
		int _maxHeight;
	}
}
