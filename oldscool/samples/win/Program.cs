using System;
using OldSchool;

class Demo {

	static Win[] _wins;
	static int _current;

	static int Main ()
	{
		_current = 0;
		_wins = new Win[5];
		Vio.Init();                           // Bildschirm vorbereiten
		Vio.Background();                     // Hintergrund malen
		Vio.Status(" ~F1~ Fenstertest ~F2~ Test des Fensters");

		CreateWindows();

		var running = true;
		while (running)
		{
			var ch = Console.ReadKey(true);
			running = Execute(ch);
		}

		Vio.Done();                           // Bildschirm restaurieren
		return 0;
	}

	static bool Execute(ConsoleKeyInfo key) 
	{
		switch(key.Key) {
			case ConsoleKey.Escape: return false;
			case ConsoleKey.Tab: 
				if( (key.Modifiers&ConsoleModifiers.Shift)==ConsoleModifiers.Shift) {
					if( --_current<0 ) _current = _wins.Length-1;
				} else {
					if( ++_current>=0 ) _current = 0;
				}
				return true;
		}
		var val = char.ToLower(key.KeyChar);
		switch(val) {
			case '+': if( ++_current>=0 ) _current = 0; break;
			case '-': if( --_current<0 ) _current = _wins.Length-1; break;
			case '1': _wins[0].Visible = !_wins[0].Visible; break;
			case '2': _wins[1].Visible = !_wins[1].Visible; break;
			case '3': _wins[2].Visible = !_wins[2].Visible; break;
			case '4': _wins[3].Visible = !_wins[3].Visible; break;
			case 'm': _wins[4].Visible = !_wins[4].Visible; break;
			case 's': _wins[_current].SetFrame( FrameType.Single, Palette.Instance.Get(AttributeRole.WinFrame) ); break;
			case 'd': _wins[_current].SetFrame( FrameType.Double, Palette.Instance.Get(AttributeRole.WinFrame) ); break;
			case 't': _wins[_current].SetFrame( FrameType.Thick, Palette.Instance.Get(AttributeRole.WinFrame) ); break;
			case 'b': _wins[_current].SetFrame( FrameType.Block, Palette.Instance.Get(AttributeRole.WinFrame) ); break;
			case 'n': _wins[_current].SetFrame( FrameType.None, Palette.Instance.Get(AttributeRole.WinFrame) ); break;
		}
		return true;
	}

	static void CreateWindows() {
		_wins[0] = CreateWindow1();
		_wins[1] = CreateWindow2();
		_wins[2] = CreateWindow3();
		_wins[3] = CreateWindow4();
		_wins[4] = CreateWindowM();
		for(var i=0; i<5; i++) {
			_wins[i].Printf($"This is window...: {i+1}\n");
			_wins[i].Printf($"Vio columns......: {Vio.Columns}\n");
			_wins[i].Printf($"Vio rows.........: {Vio.Rows}\n");
			_wins[i].Printf($"Win x............: {_wins[i].PosX}\n");
			_wins[i].Printf($"Win y............: {_wins[i].PosY}\n");
			_wins[i].Printf($"Win width........: {_wins[i].Width}\n");
			_wins[i].Printf($"Win height.......: {_wins[i].Height}\n");
		}
	}

	static int WindowWidth() {
		return Vio.Columns/2-5;
	}
	static int WindowHeight() {
		return Vio.Rows/2-5;
	}

	static Win CreateWindow1() {
		var win = new Win( 1, 1, WindowWidth(), WindowHeight() );
		win.SetFrame( FrameType.Single, Win.DefFrameAttr );
		win.SetTitle( " Links oben ", TitlePosition.TopLeft, Win.DefTitleAttr );
		return win;
	}

	static Win CreateWindow2() {
		var win = new Win( Vio.Columns-WindowWidth()-1, 1, WindowWidth(), WindowHeight() );
		win.SetFrame( FrameType.Single, Win.DefFrameAttr );
		win.SetTitle( " Rechts oben ", TitlePosition.TopRight, Win.DefTitleAttr );
		return win;
	}

	static Win CreateWindow3() {
		var win = new Win( 1, Vio.Rows-WindowHeight()-3, WindowWidth(), WindowHeight() );
		win.SetFrame( FrameType.Single, Win.DefFrameAttr );
		win.SetTitle( " Links unten ", TitlePosition.BottomLeft, Win.DefTitleAttr );
		return win;
	}

	static Win CreateWindow4() {
		var win = new Win( Vio.Columns-WindowWidth()-1, Vio.Rows-WindowHeight()-3, WindowWidth(), WindowHeight() );
		win.SetFrame( FrameType.Single, Win.DefFrameAttr );
		win.SetTitle( " Rechts unten ", TitlePosition.BottomRight, Win.DefTitleAttr );
		return win;
	}

	static Win CreateWindowM() {
		var win = new Win( Vio.CenterCol(WindowWidth()), Vio.CenterRow(WindowHeight()), WindowWidth(), WindowHeight() );
		win.SetTitle( " Zentriert ", TitlePosition.TopCenter, Win.DefTitleAttr );
		return win;
	}
}

/*
  CHAR          ch;
  SWin*         win;
  SWin*         winold       = NULL;
  BOOL          boFertig     = FALSE;

  WinAktivieren( winm );
  WinCls();
  WinSetTitel( winm, " Mittiges Fenster", WIN_TITEL_OZ );
  WinAktivieren( winm );
  WinPrintf( "Videomodus: %d\n", ScrGetMode() );
  WinPrintf( "Spalten...: %d\n", VioGetMaxCol() );
  WinPrintf( "Zeilen....: %d", VioGetMaxRow() );
  while( !boFertig )
  {
   switch( ch = getch() )
   {
    case '1': win = win1; break;
    case '2': win = win2; break;
    case '3': win = win3; break;
    case '4': win = win4; break;
    case 27: boFertig = TRUE;
   }
   if( winold ) WinVerstecken( winold );
   if( boFertig ) break;
   winold = win;
   WinAktivieren( win );
   WinCls();
   WinPrintf( " Dies ist Fenster %c", ch );
  }
  WinPrintf( "Dies ist Fenster Eins\n" );
  WinEntfernen( winm );
  WinEntfernen( win1 );
  WinEntfernen( win2 );
  WinEntfernen( win3 );
  WinEntfernen( win4 );
  VioDone();
}
*/