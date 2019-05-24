using System;
using System.Collections.Generic;
using System.Linq;

namespace OldSchool 
{
  public class MnuEntry
  {
    public int X { get; }
    public int Y { get; }
    public ConsoleKeyInfo Hotkey { get; }
    public string Text { get; }
    public string Statusline { get; }

    public MnuEntry(int x, int y, string text, ConsoleKeyInfo hotkey=default(ConsoleKeyInfo), string statusline=null) {
      X = x;
      Y = y;
      Text = string.IsNullOrEmpty(text) ? "" : text;
      Hotkey = hotkey;
      Statusline = statusline;
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
      if( _win==null) CreateWindow(x, y, title);
      _win.Visible = false;
      return 0;
    }

    Win _win;
    Win CreateWindow(int x, int y, string title) 
    { 
      var win = new Win(x, y, _maxWidth+2, _maxHeight+2) { Title = title };
      return win;
    }

    List<MnuEntry> _mnuEntries = new List<MnuEntry>();
    int _current;
    int _maxWidth;
    int _maxHeight;
  }
}
