using System;
using OldSchool;

class Demo {

  static Win[] _wins;

  static int Main ()
  {
    _wins = new Win[5];
    Vio.Init();                           // Bildschirm vorbereiten
//    Vio.Background();                     // Hintergrund malen
    Vio.Status(" ~[F1]~ Fenstertest ~[F2]~ Test des Fensters");

    CreateWindows();

    var running = true;
    while (running)
    {
        var ch = Console.ReadKey();
        running = Execute(ch);
    }

    Vio.Done();                           // Bildschirm restaurieren
    return 0;
  }

  static bool Execute(ConsoleKeyInfo key) {
    switch(key.Key) {
      case ConsoleKey.Escape: return false;
    }
    var val = char.ToLower(key.KeyChar);
    switch(val) {
      case '1': _wins[0].Visible = !_wins[0].Visible; break;
      case '2': _wins[1].Visible = !_wins[1].Visible; break;
      case '3': _wins[2].Visible = !_wins[2].Visible; break;
      case '4': _wins[3].Visible = !_wins[3].Visible; break;
      case 'm': _wins[4].Visible = !_wins[4].Visible; break;
      case 's': for(var i=0; i<5; i++) _wins[i].Frame = Frame.Single; break;
      case 'd': for(var i=0; i<5; i++) _wins[i].Frame = Frame.Double; break;
      case 't': for(var i=0; i<5; i++) _wins[i].Frame = Frame.Thick; break;
      case 'b': for(var i=0; i<5; i++) _wins[i].Frame = Frame.Block; break;
      case 'n': for(var i=0; i<5; i++) _wins[i].Frame = Frame.None; break;
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
    var win = new Win( 1, 1, WindowWidth(), WindowHeight() ) {
      Frame = Frame.Single,
      Title = " Links oben ",
    };
    return win;
  }

  static Win CreateWindow2() {
    var win = new Win( Vio.Columns-WindowWidth()-1, 1, WindowWidth(), WindowHeight() ) {
      Frame = Frame.Single,
      Title = " Rechts oben ",
    };
    return win;
  }

  static Win CreateWindow3() {
    var win = new Win( 1, Vio.Rows-WindowHeight()-3, WindowWidth(), WindowHeight() ) {
      Frame = Frame.Single,
      Title = " Links unten ",
    };
    return win;
  }

  static Win CreateWindow4() {
    var win = new Win( Vio.Columns-WindowWidth()-1, Vio.Rows-WindowHeight()-3, WindowWidth(), WindowHeight() ) {
      Frame = Frame.Single,
      Title = " Rechts unten ",
    };
    return win;
  }

  static Win CreateWindowM() {
    var win = new Win( Vio.CenterCol(WindowWidth()), Vio.CenterRow(WindowHeight()), WindowWidth(), WindowHeight() ) {
      Title = " Zentriert ",
    };
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