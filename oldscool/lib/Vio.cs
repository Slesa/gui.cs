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

    public static void Sz(int col, int row, char ch)
    {
      contents[col,row,0] = ch;
      Console.SetCursorPosition(col, row);
      Console.BackgroundColor = Colors.Background(contents[col, row, 1]);
      Console.ForegroundColor = Colors.Foreground(contents[col, row, 1]);
      Console.Write ((char)ch);
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

      for(var row=0; row<_rows; row++)
      {
        for(var col=0; col<_cols; col++)
        {
          Console.CursorTop = row;
          Console.CursorLeft = col;

          Vio.Sza( col, row, '\u2592', Colors.MakeAttr(ConsoleColor.Cyan, ConsoleColor.White));
          //Vio.Sz( col, row, '\u2592' );
        }
      }

      Console.CursorTop = savedRow;
      Console.CursorLeft = savedCol;
    }
  }
}