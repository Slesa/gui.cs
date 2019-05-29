using System.Linq;

namespace OldSchool 
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
			var x = Vio.CenterCol(width);
			var y = Vio.CenterRow(height);
			return ShowWindow(x, y, width, height, text, mode, status);
		}

 		public int ShowWindow(int x, int y, int width, int height, string text, MsgMode mode, string status=null)
		{
			if( mode==MsgMode.Hide ) {
				if( _win!=null )
					_win.Remove();
				return 0;
			}
			Vio.Status(status, Palette.Instance.Get(AttributeRole.MsgStatusLine), Palette.Instance.Get(AttributeRole.MsgStatusLine));
			if( _win==null ) { 
				_win = new Win(x, y, width+6, height+4);
				_win.CursorType = CursorType.Off;
			}
			// _win->setBackground(Palette.Instance.Get(AttributeRole.MsgText));
			// _win->Frame = Frame.Single; //  TWin::FrameSingle, TColors::MsgFrame);

			// _win->Print(text);
			// _win->Update();
			var ret = 0;
			switch( mode )
			{
				case MsgMode.Info:
					// TODO: use ev
					// _win->setTitle(" <Taste> ", TitlePosition.BottomRight); //, TColors::MsgTitle);
					do {
						ret = getch();
						if( !ret )
							continue;
					} while( ret==ERR );
					break;
				case MsgMode.YesNo:
					// TODO: use ev
					// _win->setTitle(" <Return>=JA <F10>=NEIN ",TitlePosition.BottomRight); //, TColors::MsgTitle);
					do {
						ret = getch();
						if( ret==KEY_CANCEL || ret==KEY_F(10) )
							break;
					} while( ret!='\n' );
					break;
				case MsgMode.F1_F2:
					do {
						ret = getch();
						if( ret==KEY_F(2) )
							break;
					} while( ret!=KEY_F(1) );
					break;
				case MsgMode.Show:
					return 0;                 // Show window and return immediately
			}
			if( mode!=Show ) {
				_win.Remove();
			}
			return 0;
		}

		int GetWidth(string text)
		{
			var lines = text.Split('\n');
			var maxwidth = lines.Max(x => x.Length);
			return maxwidth+4>Vio.Columns ? Vio.Columns-4 : maxwidth;
		}

		int GetHeight(string text)
		{
			var lines = text.Split('\n');
			var height = lines.Count();
			return height+2>Vio.Rows ? Vio.Rows-2 : height;
		}

		Win _win;
	}
}
