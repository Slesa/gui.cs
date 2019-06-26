using System;
// using Mono.Terminal;

namespace OldScool 
{

	public class Vio
	{
		public Vio()
		{
			Init();
		}
		~Vio()
		{
			Done();
		}

		public int Columns => _cols;
		public int Rows => _rows;
		public IPalette Palette => _palette;

		public int CenterCol(int width=0)
		{
			return (Columns-width) / 2;
		}

		public int CenterRow(int height=0)
		{
			return (Rows-height) / 2;
		}
		
		public void DoBackground()
		{
			var savedRow = Console.CursorTop;
			var savedCol = Console.CursorLeft;
			var line = new string('\u2592', _cols);

			Console.CursorLeft = 0;
			for(var row=0; row<_rows; row++)
			{
				Console.CursorTop = row;
				Ssa(0, row, line, AttribRole.Background);
			}
			Gotoxy(savedCol, savedRow);
		}

		public void Clear()
		{
			Console.Clear();
		}

		public void Gotoxy(int x, int y)
		{
			Console.SetCursorPosition(x, y);
		}

		public void Sa(int col, int row, AttribRole attr)
		{
			_contents[col, row, 1] = _palette.Get(attr);
			Sz(col, row, (char) _contents[col, row, 0]);
		}
		public int La(int col, int row)
		{
			return _contents[col,row,1];
		}

		public void Sz(int col, int row, char ch)
		{
			_contents[col,row,0] = ch;
			Gotoxy(col, row);
			Console.BackgroundColor = Colors.Background(_contents[col, row, 1]);
			Console.ForegroundColor = Colors.Foreground(_contents[col, row, 1]);
			Console.Write ((char)ch);
		}
		public int Lz(int col, int row)
		{
			return _contents[col,row,0];
		}

		public void Sza(int col, int row, char ch, AttribRole attr)
		{
			var a = _palette.Get(attr); 
			_contents[col, row, 0] = ch;
			_contents[col, row, 1] = a;
			Gotoxy(col, row);
			Console.BackgroundColor = Colors.Background(a);
			Console.ForegroundColor = Colors.Foreground(a);
			Console.Write ((char)ch);
		}

		public void Ss(int col, int row, string text)
		{
			for(var i=0; i<text.Length; i++)
				Sz(col+i, row, text[i]);
		}

		public void Ssa(int col, int row, string text, AttribRole attr)
		{
			var a = _palette.Get(attr); 
			Gotoxy(col, row);
			Console.BackgroundColor = Colors.Background(a);
			Console.ForegroundColor = Colors.Foreground(a);
			Console.Write (text);
			for(int i=0; i<text.Length; i++) {
				_contents[col+i,row,0] = text[i];
				_contents[col+i,row,1] = a;
			}
		}

		public void Status(string text)
		{
			Status(text, AttribRole.MnuStatusLine, AttribRole.MnuStatusInvers);
		}

		public void Status(string text, AttribRole normAttr, AttribRole invAttr)
		{
			var attr = normAttr;
			var col = 0;
			var strings = text.Split('~');
			for(int i=0; i<strings.Length; i++)
			{
				Ssa(col, _rows-1, strings[i], attr);
				attr = attr==normAttr ? invAttr : normAttr;
				col += strings[i].Length;
			}
			var left = new string(' ', _cols-col);
			Ssa(col, _rows-1, left, normAttr);
		}


		void Init()
		{
			_cols = Console.WindowWidth;
			_rows = Console.WindowHeight;

			_contents = new int [_cols, _rows, 3];
			for (var r = 0; r < _rows; r++) {
				for (var c = 0; c < _cols; c++) {
					_contents [c, r, 0] = ' ';
					_contents [c, r, 1] = Colors.MakeAttr (ConsoleColor.Gray, ConsoleColor.Black);
					_contents [c, r, 2] = 0;
				}
			}
			_palette = new Palette();
			/*
			BYTE          bPages; 
			WORD          wLength;
			if( boVioSaving ) {
				TimerInit();
				ScrGetInfo( &bPages, &wLength );    
				ScrGetCurPos( &wOldX, &wOldY );     
				bLastmode = ScrGetMode();           
				pbSaveScr = MemAlloc( sizeof( BYTE ) * wLength );
				if( pbSaveScr != NULL ) MemCopy( pbSaveScr, VioSetAdr(), wLength );
			}

			VioSetMode( iMode );
			*/
		}

		public static void Done()
		{
			/*
			BYTE          bPages;
			WORD          wLength;
			BOOL          boFade       = (VioIsVga()) && (VioGetColors()==16) && (boVioFading==TRUE);
			if( !boVioSaving ) return;
			if( boFade ) {
				VgaFadeDown();
				VgaSetTurning( VGA_TURN_MONITOR, TRUE );
				VgaSetTurning( VGA_TURN_PALETTE, TRUE );
			}
			if( VioDescr->Done!=NULL ) VioDescr->Done();
			ScrSetMode( bLastmode );
			ScrGetInfo( &bPages, &wLength );
			if( pbSaveScr ) {
				MemCopy( VioSetAdr(), pbSaveScr, wLength );
				MemFree( pbSaveScr, wLength );
			}
			ScrSetCurPos( wOldX, wOldY );    
			if( boFade ) {
				VgaSetTurning( VGA_TURN_MONITOR, FALSE );
				VgaSetTurning( VGA_TURN_PALETTE, FALSE );
				VgaFadeUp();
			}
			TimerDone();
			*/ 
		}

		int _rows;
		int _cols;
		int [,,] _contents;
		IPalette _palette;
	} 
}
