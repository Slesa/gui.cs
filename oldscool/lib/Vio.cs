using System;
// using Mono.Terminal;

namespace OldSchool 
{
  public static class Vio
  {
    static int _rows;
    static int _cols;
    static int [,,] contents;

    public static void Init()
    {
      Console.Clear();
      _cols = Console.WindowWidth;
      _rows = Console.WindowHeight;

      contents = new int [_cols, _rows, 3];
      for (int r = 0; r < _rows; r++) {
        for (int c = 0; c < _cols; c++) {
          contents [c, r, 0] = ' ';
          contents [c, r, 1] = Colors.MakeAttr (ConsoleColor.Gray, ConsoleColor.Black);
          contents [c, r, 2] = 0;
        }
      }


#if NOT
  BYTE          bPages;                          /* Anzahl Seiten           */
  WORD          wLength;                         /* Groesse einer Bildseite */
  if( boVioSaving )
  {
   TimerInit();
   ScrGetInfo( &bPages, &wLength );               /* Anzahl Seiten und Bildgroesse ermitteln */
   ScrGetCurPos( &wOldX, &wOldY );                /* Alte Cursorposition merken */
   bLastmode = ScrGetMode();                      /* Alten Modus merken      */
   pbSaveScr = MemAlloc( sizeof( BYTE ) * wLength );
   if( pbSaveScr != NULL ) MemCopy( pbSaveScr, VioSetAdr(), wLength ); /* Bildschirminhalt sichern */
  }

  VioSetMode( iMode );
#endif
 
    }
    public static void Done()
    {
#if NOT

  BYTE          bPages;                          /* Anzahl Seiten           */
  WORD          wLength;                         /* Groesse einer Bildseite */
  BOOL          boFade       = (VioIsVga()) && (VioGetColors()==16) && (boVioFading==TRUE);
  if( !boVioSaving ) return;
  if( boFade )
  {
   VgaFadeDown();
   VgaSetTurning( VGA_TURN_MONITOR, TRUE );
   VgaSetTurning( VGA_TURN_PALETTE, TRUE );
  }
  if( VioDescr->Done!=NULL ) VioDescr->Done();
  ScrSetMode( bLastmode );                       /* Urspruenglichen Mode setzen */
  ScrGetInfo( &bPages, &wLength );               /* Anzahl Seiten und Bildgroesse ermitteln */
  if( pbSaveScr )                                /* Falls Bildschirm gesichert */
  {
   MemCopy( VioSetAdr(), pbSaveScr, wLength );   /* Inhalt wieder restaurieren */
   MemFree( pbSaveScr, wLength );
  }
  ScrSetCurPos( wOldX, wOldY );                  /* Alte Cursorposition restaurieren */
  if( boFade )
  {
   VgaSetTurning( VGA_TURN_MONITOR, FALSE );
   VgaSetTurning( VGA_TURN_PALETTE, FALSE );
   VgaFadeUp();
  }
  TimerDone();
#endif 

    }

    public static int Columns => _cols;
    public static int Rows => _rows;

		public static int CenterCol(int width=0)
		{
			return (Columns-width) / 2;
		}
    
		public static int CenterRow(int height=0)
		{
			return (Rows-height) / 2;
		}

    public static void Ss(int col, int row, string text)
    {
      for(int i=0; i<text.Length; i++)
        Sz(col+i, row, text[i]);
    }

    public static void Ssa(int col, int row, string text, int attr)
    {
      Console.SetCursorPosition(col, row);
      Console.BackgroundColor = Colors.Background(attr);
      Console.ForegroundColor = Colors.Foreground(attr);
      Console.Write (text);
      for(int i=0; i<text.Length; i++) {
        contents[col+i,row,0] = text[i];
        contents[col+i,row,1] = attr;
      }
    }

    public static int Lz(int col, int row)
    {
      return contents[col,row,0];
    }

    public static void Sz(int col, int row, char ch)
    {
      contents[col,row,0] = ch;
      Console.SetCursorPosition(col, row);
      Console.BackgroundColor = Colors.Background(contents[col, row, 1]);
      Console.ForegroundColor = Colors.Foreground(contents[col, row, 1]);
      Console.Write ((char)ch);
    }

    public static int La(int col, int row)
    {
      return contents[col,row,1];
    }

    public static void Sa(int col, int row, int attr)
    {
      contents[col, row, 1] = attr;
      Sz(col, row, (char) contents[col, row, 0]);
    }

    public static void Sza(int col, int row, char ch, int attr)
    {
      contents[col, row, 0] = ch;
      contents[col, row, 1] = attr;
      Console.SetCursorPosition(col, row);
      Console.BackgroundColor = Colors.Background(contents[col, row, 1]);
      Console.ForegroundColor = Colors.Foreground(contents[col, row, 1]);
      Console.Write ((char)ch);
    }

    public static void Background()
    {
      var savedRow = Console.CursorTop;
      var savedCol = Console.CursorLeft;
      var line = new string('\u2592', _cols);

      Console.CursorLeft = 0;
      for(var row=0; row<_rows; row++)
      {
        Console.CursorTop = row;
        Vio.Ssa(0, row, line, Colors.MakeAttr(ConsoleColor.Cyan, ConsoleColor.Black));
      }

      Console.CursorTop = savedRow;
      Console.CursorLeft = savedCol;
    }

    public static void Status(string text)
    {
      var normAttr = Colors.MakeAttr(ConsoleColor.Black, ConsoleColor.Gray);
      var invAttr = Colors.MakeAttr(ConsoleColor.DarkRed, ConsoleColor.Gray);
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
  }
}