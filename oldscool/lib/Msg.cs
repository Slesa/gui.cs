using System;
using System.Linq;

namespace OldScool 
{
	public enum MsgMode
	{
		Info,           // Information only
		YesNo,          // Ask for yes or no
		Show,           // Popup message window, call with Hide to remove
		Hide,           // Hide message window, shown with Show
		F1_F2,          // Ask for F1 or F2
	}

	public class Msg
	{
		Msg(Vio vio)
		{
			_vio = vio;
		}
		
 		public int Show(int x, int y, string text, MsgMode mode, string status=null)
		{
			var width = GetWidth(text);
			var height = GetHeight(text);
			return ShowWindow(x, y, width, height, text, mode, status);
		}

		public int ShowCentered(string text, MsgMode mode, string status=null)
		{
			var width = GetWidth(text);
			var height = GetHeight(text);
			var x = _vio.CenterCol(width);
			var y = _vio.CenterRow(height);
			return ShowWindow(x, y, width, height, text, mode, status);
		}

 		public int ShowWindow(int x, int y, int width, int height, string text, MsgMode mode, string status=null)
		{
			if( mode==MsgMode.Hide ) {
				if( _win!=null )
					_win.Remove();
				return 0;
			}
			_vio.Status(status, AttribRole.MsgStatusLine, AttribRole.MsgStatusLine);
			if( _win==null ) { 
				_win = new Win(_vio, x, y, width+6, height+4);
				_win.CursorType = CursorType.Off;
			}
			// _win->setBackground(Palette.Instance.Get(AttributeRole.MsgText));
			// _win->Frame = Frame.Single; //  TWin::FrameSingle, TColors::MsgFrame);

			// _win->Print(text);
			// _win->Update();
			ConsoleKeyInfo key;
			switch( mode )
			{
				case MsgMode.Info:
					// TODO: use ev
					// _win->setTitle(" <Taste> ", TitlePosition.BottomRight); //, TColors::MsgTitle);
					//do {
						key = Console.ReadKey(true);
					//} while( key<0 );
					break;
				case MsgMode.YesNo:
					// TODO: use ev
					// _win->setTitle(" <Return>=JA <F10>=NEIN ",TitlePosition.BottomRight); //, TColors::MsgTitle);
					do {
						key = Console.ReadKey(true);
						if( key.Key==ConsoleKey.Escape || key.Key==ConsoleKey.F10 )
							break;
					} while( key.Key!=ConsoleKey.Enter );
					break;
				case MsgMode.F1_F2:
					do {
						key = Console.ReadKey(true);
						if( key.Key==ConsoleKey.F2 )
							break;
					} while( key.Key==ConsoleKey.F1 );
					break;
				case MsgMode.Show:
					return 0;                 // Show window and return immediately
			}
			if( mode!=MsgMode.Show ) {
				_win.Remove();
			}
			return 0;
		}

		int GetWidth(string text)
		{
			var lines = text.Split('\n');
			var maxwidth = lines.Max(x => x.Length);
			return maxwidth+4>_vio.Columns ? _vio.Columns-4 : maxwidth;
		}

		int GetHeight(string text)
		{
			var lines = text.Split('\n');
			var height = lines.Count();
			return height+2>_vio.Rows ? _vio.Rows-2 : height;
		}

		Vio _vio;
		Win _win;
	}
}
