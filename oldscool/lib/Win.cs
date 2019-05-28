using System;

namespace OldSchool 
{
  public enum TitlePosition
  {
    TopLeft,      // oben links
    TopCenter,    // oben zentriert
    TopRight,     // oben rechts
    BottomLeft,   // unten links 
    BottomCenter, // unten zentriert
    BottomRight   // unten rechts
  }

  public enum Frame
  {
    None,     // Kein Rahmen
    Single,   // Einfacher Rahmen
    Double,   // Doppelter Rahmen
    Thick,    // Dicker Rahmen
    Block,    // Blockrahmen
  }

  public enum Cursor
  {
	  Off,        // Kein Cursor
  }

  // Struktur eines Fensters
  public class Win
  {
    int _curX; // X-Position des Cursors
    int _curY; // Y-Position des Cursors
    int [,,] _content;
    int [,,] _background;
    int _frameAttr;
    int _titleAttr;
    int _textAttr;
//  INT           iTitlePos;                       /* Position des Titels     */
//  WORD          wFlags;                          /* Die Fenster-Flags       */
//  WORD          wAttrShadow;                     /* Attribut des Fensterschattens */

    public int PosX { get; } 
    public int PosY { get; } 
    public int Width { get; }
    public int Height { get; }
	public Cursor Cursor { get; set; }

    string _title;
    public string Title {
      get => _title;
      set { _title =value; DrawFrame(); DrawTitle(); }
    }

    Frame _frame;
    public Frame Frame { 
      get => _frame;
      set { _frame = value; DrawFrame(); DrawTitle(); }
    }

    bool _visible;
    public bool Visible {
      get => _visible;
      set {
        if( _visible==value ) return; 
        _visible = value;
        if( _visible ) { 
          SaveWin();
          DrawFrame(); 
          DrawTitle(); 
          Redraw();
        } else {
          RestoreWin();
        }
      }
    }

	public void Remove() {}

    public Win(int x, int y, int w, int h)
    {
      PosX = x;
      PosY = y;
      Width = w;
      Height = h;
      _visible = false;
      _content = new int [w, h, 2];
      _background = new int [w, h, 2];
      _frameAttr = Colors.MakeAttr(ConsoleColor.White, ConsoleColor.DarkBlue);
      _titleAttr = Colors.MakeAttr(ConsoleColor.White, ConsoleColor.DarkBlue);
      _textAttr = Colors.MakeAttr(ConsoleColor.White, ConsoleColor.DarkBlue);
      Frame = Frame.Double;

      Cls();
    }

		public int CenterCol(int width=0)
		{
			return (Width-width) / 2;
		}
    
		public int CenterRow(int height=0)
		{
			return (Height-height) / 2;
		}

    public void Cls()
    {
      var line = new string(' ', Width-2);
      for(var i=0; i<Height-2; i++) {
        Ssa(0, i, line, _textAttr);  
      }
    }

    public void Sza(int col, int row, char ch, int attr)
    {
      if( Visible )
        Vio.Sza(PosX+col+1, PosY+row+1, ch, attr);
      _content[col,row,0] = ch;
      _content[col,row,1] = attr;
    }

    public void Ssa(int col, int row, string text, int attr)
    {
      if( Visible )
        Vio.Ssa(PosX+col+1, PosY+row+1, text, attr);
      for(int i=0; i<text.Length; i++) {
        _content[col+i,row,0] = text[i];
        _content[col+i,row,1] = attr;
      }
    }

    public void Printf(string text)
    {
      var maxCol = Width-2;
      var maxRow = Height-2;
      var col = _curX;
      var row = _curY;
      for(var i=0; i<text.Length; i++)
      {
        switch( text[i] )
        {
          case '\b':
            if( col>0 ) col--;
            break;
          case '\r':
            col = 0;
            break;
          case '\n':
            if( row==maxRow ) 
              ; //ScrollUp();
            else
              row++;
              col = 0;
            break;
          default:
            Sza( col, row, (char)text[i], _textAttr );
            if( ++col == maxCol ) {
              col = 0;
              if( row==maxRow )
                ; //ScrollUp();
              else
                row++;
            }
            break;
        }
      }
      _curX = col;
      _curY =row;
    }

    void Redraw() 
    {
      for(var i=0; i<Width-2; i++) {
        for(var n=0; n<Height-2; n++) {
          Vio.Sza(PosX+i+1, PosY+n+1, (char)_content[i,n,0], _content[i,n,1]);
        }
      }
    }

    void DrawTitle()
    {
      if( !Visible ) return;
      if( string.IsNullOrEmpty(Title) ) return;
      var pos = PosX + CenterCol(Title.Length);
/*  WORD          wLen;
  WORD          wSpalte;
  WORD          wZeile;
  BOOL          boMouse      = MouHide();
  wLen = strlen( win->pcTitle );
  if( win->iTitlePos < 3 )
   wZeile = win->wPosY - 1;
  else
   wZeile = win->wPosY + win->wHeight - 1;
  switch( win->iTitlePos )
  {
   case WIN_TITEL_OL:
   case WIN_TITEL_UL:
        wSpalte = win->wPosX;
        break;
   case WIN_TITEL_OR:
   case WIN_TITEL_UR:
        wSpalte = win->wPosX+win->wWidth-wLen-1;
        break;
   case WIN_TITEL_OZ:
   case WIN_TITEL_UZ:
        wSpalte = win->wPosX+(win->wWidth-wLen)/2;
        break;
  } */
      Vio.Ssa( pos, PosY, Title, _titleAttr );
    }

    void DrawFrame()
    {
      if( !Visible ) return;
      var width = PosX+Width-1;
      var height = PosY+Height-1;
      var chars = GetFrameChars() ;
      Vio.Sza( PosX, PosY, chars.TopLeft, _frameAttr );
      Vio.Sza( width, PosY, chars.TopRight, _frameAttr );
      Vio.Sza( PosX, height, chars.BottomLeft, _frameAttr );
      Vio.Sza( width, height, chars.BottomRight, _frameAttr );
      for(var i=PosX+1; i<width; i++)
      {
        Vio.Sza( i, PosY, chars.HLine, _frameAttr );
        Vio.Sza( i, height, chars.HLine, _frameAttr );
      }
      for(var i=PosY+1; i<height; i++)
      {
        Vio.Sza( PosX, i, chars.VLine, _frameAttr );
        Vio.Sza( width, i, chars.VLine, _frameAttr );
      }
    }

    void RestoreWin()
    {
      for(var i=0; i<Width; i++) {
        for(var n=0; n<Height; n++) {
           Vio.Sza(i+PosX, n+PosY, (char)_background[i, n, 0], _background[i, n, 1]);
        }
      }
    }

    void SaveWin()
    {
      for(var i=0; i<Width; i++) {
        for(var n=0; n<Height; n++) {
          _background[i, n, 0] = Vio.Lz(i+PosX, n+PosY);
          _background[i, n, 1] = Vio.La(i+PosX, n+PosY);
        }
      }
    }

    IFrame GetFrameChars() 
    {
      switch(Frame)
      {
        case Frame.None: return NoFrame.Instance;
        case Frame.Single: return SingleFrame.Instance;
        case Frame.Double: return DoubleFrame.Instance;
        case Frame.Thick: return ThickFrame.Instance;
        case Frame.Block: return BlockFrame.Instance;
      }
      return SingleFrame.Instance;
    }
  }
}

#if NOT
 VOID  CDECL    WinEntfernen    PARAM(( SWin* ));
 VOID  CDECL    WinAktivieren   PARAM(( SWin* ));
 VOID  CDECL    WinVerstecken   PARAM(( SWin* ));
 VOID  CDECL    WinSchattenEin  PARAM(( SWin* ));
 VOID  CDECL    WinSchattenAus  PARAM(( SWin* ));
 VOID  CDECL    WinSetRahmentyp PARAM(( SWin*, INT ));
 VOID  CDECL    WinSetTitel     PARAM(( SWin*, CHAR*, INT ));
 VOID  CDECL    WinSetAttrib    PARAM(( SWin*, WORD, WORD, WORD )); 

  SWin* CDECL    WinGetAktivPwkb PARAM(( void ));
 VOID  CDECL    WinCls          PARAM(( void ));
 VOID  CDECL    WinSa           PARAM(( WORD, WORD, WORD ));
 VOID  CDECL    WinSz           PARAM(( WORD, WORD, CHAR ));
 VOID  CDECL    WinSza          PARAM(( WORD, WORD, CHAR, WORD ));
 VOID  CDECL    WinSs           PARAM(( WORD, WORD, CHAR* ));
 VOID  CDECL    WinSsa          PARAM(( WORD, WORD, CHAR*, WORD ));
 VOID  CDECL    WinSwa          PARAM(( WORD, WORD, WORD, WORD, WORD ));
 VOID  CDECL    WinSwz          PARAM(( WORD, WORD, WORD, WORD, CHAR ));
 VOID  CDECL    WinSwza         PARAM(( WORD, WORD, WORD, WORD, CHAR, WORD ));
 INT   CDECL    WinPrintf       PARAM(( CHAR* pcFormat, CPPARGS ));
 VOID  CDECL    WinScrollUp     PARAM(( void ));
 VOID  CDECL    WinScrollDown   PARAM(( void ));

 VOID  CDECL    WinCursor       PARAM(( INT ));
 VOID  CDECL    WinSetCursorPos PARAM(( WORD, WORD ));
 VOID  CDECL    WinGetCursorPos PARAM(( WORD*, WORD* ));
 VOID  CDECL    WinHot          PARAM(( WORD, WORD, CHAR*, WORD, WORD ));
 INT   CDECL    WinGetHotkey    PARAM(( CHAR* )); 
  };
}


PRIVATE SWin*  WinCurrent   = NULL;             /* Aktuelles Fenster       */

 /* ----------- Interne Prototypen ---------------------------------------- */
 PRIVATE VOID CDECL WinSave       PARAM(( SWin* ));
 PRIVATE VOID CDECL WinRestore    PARAM(( SWin* ));
 PRIVATE VOID CDECL WinShowShadow PARAM(( SWin* ));
 PRIVATE VOID CDECL WinShowTitle  PARAM(( SWin* ));
 PRIVATE VOID CDECL WinShowFrame  PARAM(( SWin* ));

 SWin* CDECL    WinEinrichten( wX, wY, wWidth, wHeight )
 WORD           wX;                              /* Spalte des Fensters     */
 WORD           wY;                              /* Zeile des Fensters      */
 WORD           wWidth;                          /* Breite des Fensters     */
 WORD           wHeight;                         /* Hoehe des Fensters      */
 {
  SWin*         temp         = NULL;             /* Hilfshandle fuers Fenster */
  temp   = (SWin*) MemAlloc( sizeof( SWin ) );   /* Speicher fuer Fenster anfordern */
  if( temp==NULL ) return( NULL );               /* Keiner mehr da?         */
  temp->wPosX     = wX+1;                        /* X-Position merken       */
  temp->wPosY     = wY+1;                        /* Y-Position merken       */
  temp->wWidth    = wWidth-1;                    /* Breite merken           */
  temp->wHeight   = wHeight-1;                   /* Hoehe merken            */
  temp->wCurX     = 0;                           /* Cursorpos X resettieren */
  temp->wCurY     = 0;                           /* Cursorpos Y resettieren */
  temp->wFlags    = 0;
  temp->pcTitle   = NULL;                        /* Standard: Kein Titel    */
  temp->pwBuffer  = NULL;                        /* Vorerst kein Puffer     */
  temp->iTitlePos = WIN_TITEL_OZ;                /* Titelposition: Oben zentriert */
  temp->iFrame    = WIN_FRAME_DOUBLE;            /* Standard: Kein Rahmen   */
  temp->pwBuffer  = MemAlloc( (temp->wWidth+3) * (temp->wHeight+2) * 2 );
  if( VioIsMono() )
  {
   temp->wAttrText   = NORMAL;
   temp->wAttrFrame  = INVERS;
   temp->wAttrTitle  = INVERS;
   temp->wAttrShadow = NORMAL;
  }
  else
  {
   temp->wAttrText   = VH( WEISS, BLAU );
   temp->wAttrFrame  = VH( WEISS, BLAU );
   temp->wAttrTitle  = VH( WEISS, BLAU );
   temp->wAttrShadow = GRAU;
  }
  return( temp );                                /* Fensterdeskriptor zurueckliefern */
 }


 VOID CDECL     WinAktivieren( win )
 SWin*          win;                             /* Zu aktivierendes Fenster */
 {
  if( win==NULL ) return;                        /* Falls ungueltiges handle: raus */
  if( !( win->wFlags&WIN_FLG_SAVE) ) WinSave( win ); /* Fensterinhalt sichern   */
  if( win->wFlags&WIN_FLG_SHADOW ) WinShowShadow( win );/* Evtl Schatten malen     */
  if( win->iFrame!=0 ) WinShowFrame( win );      /* Fensterrahmen malen     */
  if( win->pcTitle!=NULL ) WinShowTitle( win );  /* Evtl Titel anzeigen     */
  WinCurrent = win;                              /* Aktuelles Fenster anpassen */
  if( !( win->wFlags&WIN_FLG_1ST ) ) { WinCls(); win->wFlags|=WIN_FLG_1ST; }
 }

 VOID CDECL     WinVerstecken( win )
 SWin*          win;                             /* Zu versteckendes Fenster */
 {
  if( win==NULL ) return;                        /* Falsches Handle: Fehler melden */
  WinRestore( win );                             /* Hintergrund ueberbuegeln */
 }

 VOID CDECL     WinSchattenEin( win )
 SWin*          win;                             /* Das Fenster will Schatten */
 {
  if( win==NULL ) return;                        /* Falls Handle ungueltig, gibts gar nix */
  win->wFlags |= WIN_FLG_SHADOW;                 /* Schattenflag setzen     */
  if( win==WinCurrent ) WinShowShadow( win );    /* Schatten aktualisieren  */
 }

 VOID CDECL     WinSchattenAus( win )
 SWin*          win;                             /* Das Fenster will Schatten */
 {
  if( win==NULL ) return;                        /* Falls Handle ungueltig, gibts gar nix */
  win->wFlags &= !WIN_FLG_SHADOW;                 /* Schattenflag setzen     */
  if( win==WinCurrent ) WinShowShadow( win );    /* Schatten aktualisieren  */
 }

 VOID CDECL     WinSetRahmentyp( win, iTyp )
 SWin*          win;                             /* Zu aenderndes Fenster   */
 INT            iTyp;                            /* Neuer Rahemntyp         */
 {
  if( ( win==NULL ) || ( iTyp<WIN_FRAME_NONE ) || ( iTyp>WIN_FRAME_THICK ) ) return;
  win->iFrame = iTyp;                            /* Typ merken              */
  if( win==WinCurrent )
  {
   WinShowFrame( win );                          /* Rahmen aktualisieren    */
   if( win->pcTitle ) WinShowTitle( win );
  }
 }

 VOID CDECL     WinSetTitel  ( win, pcStr, iPos )
 SWin*          win;                             /* Zu bearbeitendes Fenster */
 CHAR*          pcStr;                           /* Neuer Titel des Fensters */
 INT            iPos;                            /* Position des Titels     */
 {
  if( ( win==NULL ) || ( iPos<WIN_TITEL_OL ) || ( iPos>WIN_TITEL_UR ) ) return;
  win->iTitlePos = iPos;                         /* Position merken          */
  if( win->pcTitle ) MemFree( win->pcTitle, 0L );
  if( pcStr!=NULL ) win->pcTitle   = strdup( pcStr );
  if( win==WinCurrent )
  {
   WinShowFrame( win );
   WinShowTitle( win );
  }
 }

 VOID CDECL     WinSetAttrib ( win, wText, wFrame, wTitle )
 SWin*          win;
 WORD           wText;
 WORD           wFrame;
 WORD           wTitle;
 {
  if( win==NULL ) return;
  win->wAttrText  = wText;
  win->wAttrFrame = wFrame;
  win->wAttrTitle = wTitle;
  if( win==WinCurrent )
  {
   WinSwa( 0, 0, win->wWidth, win->wHeight, win->wAttrText );
   WinShowFrame( win );
   WinShowTitle( win );
  }
 }

 SWin* CDECL    WinGetAktivPwkb( void )
 {
  return( WinCurrent );
 }

 
 VOID  CDECL    WinSa        ( wX, wY, wAttr )
 WORD           wX;                              /* X-Position des Attributs */
 WORD           wY;                              /* Y-Position des Attributs */
 WORD           wAttr;                           /* Zu setzendes Attribut   */
 {
  BOOL          boMouse      = MouHide();
  if( WinCurrent==NULL )                         /* Falls kein Fenster aktiv */
   VioSa( wX, wY, wAttr );                       /* ist die Position relativ zum Bildschirm */
  else                                           /* Falls Fenster aktiv relativ zum Fenster */
   VioSa( WinCurrent->wPosX+wX, WinCurrent->wPosY+wY, wAttr );
  if( boMouse ) MouShow();
 }

 VOID CDECL     WinSz        ( wX, wY, cCh )
 WORD           wX;                              /* X-Position des Zeichens */
 WORD           wY;                              /* Y-Position des Zeichens */
 CHAR           cCh;                             /* Zu setzendes Zeichen    */
 {
  BOOL          boMouse      = MouHide();
  if( WinCurrent==NULL )                         /* Falls kein Fenster aktiv */
   VioSz( wX, wY, cCh );                         /* Koordinaten relativ zum Bildschirm */
  else                                           /* Falls Fenster aktiv     */
   VioSz( WinCurrent->wPosX+wX, WinCurrent->wPosY+wY, cCh ); /* relativ zum Fenster */
  if( boMouse ) MouShow();
 }

 VOID CDECL     WinSza       ( wX, wY, cCh, wAttr )
 WORD           wX;                              /* X-Position von Zeichen/Attribut */
 WORD           wY;                              /* Y-Position von Zeichen/Attribut */
 CHAR           cCh;                             /* Zu setzendes Zeichen    */
 WORD           wAttr;                           /* Zu setzendes Attribut   */
 {
  BOOL          boMouse      = MouHide();
  if( WinCurrent==NULL )                         /* Falls Kein Fenster aktiv */
   VioSza( wX, wY, cCh, wAttr );                 /* Koordinaten relativ zum Bildschirm */
  else                                           /* Falls Fenster aktiv zum Fenster */
   VioSza( WinCurrent->wPosX+wX, WinCurrent->wPosY+wY, cCh, wAttr );
  if( boMouse ) MouShow();
 }

 VOID CDECL     WinSs        ( wX, wY, pcStr )
 WORD           wX;                              /* X-Position des Strings  */
 WORD           wY;                              /* Y-Position des Strings  */
 CHAR*          pcStr;                           /* Zu schreibender String  */
 {
  WORD          wMaxx;                           /* Horizontaler Rand fuer String */
  WORD          wMaxy;                           /* Vertikaler Rand fuer String */
  BOOL          boMouse      = MouHide();
  if( WinCurrent==NULL )                         /* Falls kein Fenster aktiv */
  {
   wMaxx=VioGetMaxCol()-1;                       /* Rechte Grenze = Rechter Bildschirmrand */
   wMaxy=VioGetMaxRow()-1;                       /* Untere Grenze = Unterer Bildschirmrand */
   for ( ; *pcStr; pcStr++ )                     /* String abarbeiten       */
   {                                             /* solange unterer Rand nicht erreicht */
    VioSz( wX++, wY, *pcStr );                   /* Relativ zum Bildschirm setzen */
    wX %= wMaxx;                                 /* X-Position erhoehen     */
    if( wX==0 )                                  /* Rechter Rand erreicht?  */
    {
     wY += 1;
     wY %= wMaxy;                                /* Y-Position erhoehen     */
     if( wY==0 ) break;                          /* Falls unterer Rand erreicht */
    }                                            /* aufhoeren               */
   }
  }
  else                                           /* Falls Fenster aktiv      */
  {
   wMaxx=WinCurrent->wWidth;                     /* Rechte Grenze = Rechter Fenster */
   wMaxy=WinCurrent->wHeight;                    /* Untere Grenze = Unterer Fensterrand */
   for ( ; *pcStr; pcStr++ )                     /* String abarbeiten       */
   {                                             /* solange unterer Rand nicht erreicht */
    VioSz( WinCurrent->wPosX+wX++, WinCurrent->wPosY+wY, *pcStr ); /* relativ zum Fenster */
    wX %= wMaxx;                                 /* X-Position erhoehen     */
    if( wX==0 )                                  /* Rechter Rand erreicht?  */
    {
     wY += 1;
     wY %= wMaxy;                                /* Y-Position erhoehen     */
     if( wY==0 ) break;                          /* Falls unterer Rand erreicht */
    }                                            /* aufhoeren               */
   }
  }
  if( boMouse ) MouShow();
 }

 VOID CDECL     WinSsa        ( wX, wY, pcStr, wAttr )
 WORD           wX;                              /* X-Position des Strings  */
 WORD           wY;                              /* Y-Position des Strings  */
 CHAR*          pcStr;                           /* Zu schreibender String  */
 WORD           wAttr;                           /* Attribut des Strings    */
 {
  WORD          wMaxx;                           /* Horizontaler Rand fuer String */
  WORD          wMaxy;                           /* Vertikaler Rand fuer String */
  BOOL          boMouse      = MouHide();
  if( WinCurrent==NULL )                         /* Falls kein Fenster aktiv */
  {
   wMaxx=VioGetMaxCol()-1;                       /* Rechte Grenze = Rechter Bildschirmrand */
   wMaxy=VioGetMaxRow()-1;                       /* Untere Grenze = Unterer Bildschirmrand */
   for ( ; *pcStr; pcStr++ )                     /* String abarbeiten        */
   {
    VioSza( wX++, wY, *pcStr, wAttr );           /* Relativ zum Bildschirm setzen */
    wX %= wMaxx;                                 /* X-Position erhoehen      */
    if( wX==0 )                                  /* Rechter Rand erreicht?   */
    {
     wY+=1;
     wY %= wMaxy;                                /* Y-Position erhoehen      */
     if( wY==0 ) break;                          /* Falls unterer Rand erreicht */
    }
   }
  }
  else
  {
   wMaxx=WinCurrent->wWidth;                     /* Rechte Grenze = Rechter Fenster */
   wMaxy=WinCurrent->wHeight;                    /* Untere Grenze = Unterer Fensterrand */
   for ( ; *pcStr; pcStr++ )                     /* String abarbeiten        */
   {                                             /* solange unterer Rand nicht erreicht */
    VioSza( WinCurrent->wPosX+wX++, WinCurrent->wPosY+wY, *pcStr, wAttr ); /* relativ zum Fenster */
    wX %= wMaxx;                                  /* X-Position erhoehen      */
    if( wX==0 )                                   /* Rechter Rand erreicht?   */
    {
     wY+=1;
     wY %= wMaxy;                                 /* Y-Position erhoehen      */
     if( wY==0 ) break;
    }
   }
  }
  if( boMouse ) MouShow();
 }

 VOID CDECL     WinSwa       ( wX, wY, wWidth, wHeight, wAttr )
 WORD           wX;                              /* X-Position des Bereiches */
 WORD           wY;                              /* Y-Position des Bereiches */
 WORD           wWidth;                          /* Breite des Bereiches    */
 WORD           wHeight;                         /* Hoehe des Bereiches     */
 WORD           wAttr;                           /* Fuellattribut           */
 {
  BOOL          boMouse      = MouHide();
  if( WinCurrent==NULL )                         /* Falls kein Fenster aktiv */
   VioSwa( wX, wY, wWidth, wHeight, wAttr );     /* Koordinaten relativ zum Bildschirm */
  else                                           /* Falls Fenster aktiv zum Fenster */
   VioSwa( WinCurrent->wPosX+wX, WinCurrent->wPosY+wY, wWidth, wHeight, wAttr );
  if( boMouse ) MouShow();
 }

 VOID CDECL     WinSwz       ( wX, wY, wWidth, wHeight, cCh )
 WORD           wX;                              /* X-Position des Bereiches */
 WORD           wY;                              /* Y-Position des Bereiches */
 WORD           wWidth;                          /* Breite des Bereiches    */
 WORD           wHeight;                         /* Hoehe des Bereiches     */
 CHAR           cCh;                             /* Fuellzeichen            */
 {
  BOOL          boMouse      = MouHide();
  if( WinCurrent==NULL )                         /* Falls kein Fenster aktiv  */
   VioSwz( wX, wY, wWidth, wHeight, cCh );       /* Koordinaten relativ zum Bildschirm */
  else                                           /* Falls Fenster aktiv zum Fenster */
   VioSwz( WinCurrent->wPosX+wX, WinCurrent->wPosY+wY, wWidth, wHeight, cCh );
  if( boMouse ) MouShow();
 }

 VOID CDECL     WinSwza      ( wX, wY, wWidth, wHeight, cCh, wAttr )
 WORD           wX;                              /* X-Position des Bereiches */
 WORD           wY;                              /* Y-Position des Bereiches */
 WORD           wWidth;                          /* Breite des Bereiches    */
 WORD           wHeight;                         /* Hoehe des Bereiches     */
 CHAR           cCh;                             /* Fuellzeichen            */
 WORD           wAttr;                           /* Fuellattribut           */
 {
  BOOL          boMouse      = MouHide();
  if( WinCurrent==NULL )                         /* Falls kein Fenster aktiv */
   VioSwza( wX, wY, wWidth, wHeight, cCh, wAttr ); /* Koordinaten relativ zum Bildschirm */
  else                                           /* Falls Fenster aktiv zum Fenster */
   VioSwza( WinCurrent->wPosX+wX, WinCurrent->wPosY+wY, wWidth, wHeight, cCh, wAttr );
  if( boMouse ) MouShow();
 }


 VOID CDECL     WinScrollUp  ( void )
 {
  BOOL          boMouse      = MouHide();
  if( WinCurrent==NULL )
  {
   ScrScrUp( 0, 0, (BYTE)(VioGetMaxCol()-1), (BYTE)(VioGetMaxRow()-1), 1, NORMAL );
  }
  else
  {
   VioScrUp( WinCurrent->wPosX, WinCurrent->wPosY, WinCurrent->wPosX+WinCurrent->wWidth-2, WinCurrent->wPosY+WinCurrent->wHeight-2, WinCurrent->wAttrText );
  }
  if( boMouse ) MouShow();
 }

 VOID CDECL     WinScrollDown( void )
 {
  BOOL          boMouse      = MouHide();
  if( WinCurrent==NULL )
  {
   ScrScrDown( 0, 0, (BYTE)(VioGetMaxCol()-1), (BYTE)(VioGetMaxRow()-1), 1, NORMAL );
  }
  else
  {
   VioScrDown( WinCurrent->wPosX, WinCurrent->wPosY, WinCurrent->wPosX+WinCurrent->wWidth-2, WinCurrent->wPosY+WinCurrent->wHeight-2, WinCurrent->wAttrText );
  }
  if( boMouse ) MouShow();
 }

 VOID CDECL     WinCursor    ( iMode )
 INT            iMode;
 {
  switch( iMode )
  {
   case WIN_CUR_HIDE:
        ScrSetCurTyp( 1, 0 );
        return;
   case WIN_CUR_LINE:
        if( !VioIsMono() )
         ScrSetCurTyp( 7, 8 );
        else
         ScrSetCurTyp( 11, 12 );
        break;
   case WIN_CUR_BLOCK:
        if( !VioIsMono() )
         ScrSetCurTyp( 1, 8 );
        else
         ScrSetCurTyp( 1, 12 );
        break;
  }
  if( WinCurrent!=NULL )
   ScrSetCurPos( WinCurrent->wCurX+WinCurrent->wPosX, WinCurrent->wCurY+WinCurrent->wPosY );
 }

 VOID CDECL     WinSetCursorPos( wX, wY )
 WORD           wX;
 WORD           wY;
 {
  if( WinCurrent==NULL )
   ScrSetCurPos( wX, wY );
  else
  {
   WinCurrent->wCurX = wX;
   WinCurrent->wCurY = wY;
  }
 }

 VOID CDECL     WinGetCursorPos( pwX, pwY )
 WORD*          pwX;
 WORD*          pwY;
 {
  if( WinCurrent==NULL )
   ScrGetCurPos( pwX, pwY );
  else
  {
   *pwX = WinCurrent->wCurX;
   *pwY = WinCurrent->wCurY;
  }
 }

 VOID CDECL     WinHot       ( wX, wY, pcText, wNorm, wHot )
 WORD           wX;
 WORD           wY;
 CHAR*          pcText;
 WORD           wNorm;
 WORD           wHot;
 {
  INT           i;
  INT           iPos         = 0;
  WORD          wAttr        = wNorm;
  for( i=0; i<strlen( pcText ); i++ )
   if( pcText[i]=='~' ) wAttr = ( wAttr==wNorm ) ? wHot : wNorm;
   else
    WinSza( wX+iPos++, wY, pcText[i], wAttr );
 }

 VOID CDECL     WinSave      ( win )
 SWin*          win;
 {
  if( ( win==NULL ) || ( win->pwBuffer==NULL ) ) return;
  VioSw2b( win->wPosX-1, win->wPosY-1, win->wWidth+3, win->wHeight+2, win->pwBuffer );
  win->wFlags |= WIN_FLG_SAVE;
 }

 VOID CDECL     WinRestore   ( win )
 SWin*          win;
 {
  BOOL          boMouse      = MouHide();
  if( ( win==NULL ) || ( win->pwBuffer==NULL ) ) /* Falsches Handle/Kein Puffer? */
   return;                                       /* ignorieren              */
  VioSb2w( win->wPosX-1, win->wPosY-1, win->wWidth+3, win->wHeight+2, win->pwBuffer );
  win->wFlags ^= WIN_FLG_SAVE;
  if( boMouse ) MouShow();
 }

 VOID CDECL     WinShowShadow( win )
 SWin*          win;
 {
  INT           i;                               /* Laufzaehler fuer Hoehe und Breite */
  WORD          wStop;                           /* Endpos fuer Schatten    */
  WORD          wStart         = win->wPosX+win->wWidth; /* Startpos fuer Schatten  */
  BOOL          boMouse      = MouHide();
  if( wStart < VioGetMaxCol() )                  /* Falls ueberhaupt sichtbar */
  {
   wStop  = win->wPosY+win->wHeight;             /* Ende des Schattens bestimmen */
   if( VioIsMono() )
   {
    for( i=win->wPosY+1; i<=wStop && i<VioGetMaxRow(); i++ )
    {
     VioSza( wStart, i, '±', win->wAttrShadow );
     if( wStart<VioGetMaxCol()-1 )
      VioSza( wStart+1, i, '±', win->wAttrShadow );
    }
    wStart = win->wPosY+win->wHeight;
    if( wStart < VioGetMaxRow() )
    {
     wStop  = win->wPosX+win->wWidth;
     for( i=win->wPosX+1; i<wStop && i< VioGetMaxCol(); i++ )
      VioSza( i, wStart, '±', win->wAttrShadow );
    }
   }
   else
   {
    wStop  = win->wPosY+win->wHeight;
    for( i=win->wPosY+1; i<=wStop && i<VioGetMaxRow(); i++ )
    {
     VioSa( wStart, i, win->wAttrShadow );
     if( wStart<VioGetMaxCol()-1 )
      VioSa( wStart+1, i, win->wAttrShadow );
    }
    wStart = win->wPosY+win->wHeight;
    if( wStart < VioGetMaxRow() )
    {
     wStop  = win->wPosX+win->wWidth;
     for( i=win->wPosX+1; i<wStop && i< VioGetMaxCol(); i++ )
      VioSa( i, wStart, win->wAttrShadow );
    }
   }
  }
  if( boMouse ) MouShow();
 }



 INT   CDECL    WinGetHotkey    ( pcText )
 CHAR*          pcText;
 {
  INT           i;
  for( i=0; pcText[i]!=0; i++ )
  {
   if( pcText[i]=='~' )
   {
    if( pcText[i+2]=='~' ) return( toupper( pcText[i+1] ) );
    if( pcText[i+1]=='F' ) switch( pcText[i+2] )
    {
     case '1': switch( pcText[i+3] )
               {
                case '0': return( T_F10 );
                case '1': return( T_F11 );
                case '2': return( T_F12 );
               }
               return( T_F1 );
     case '2': return( T_F2 );
     case '3': return( T_F3 );
     case '4': return( T_F4 );
     case '5': return( T_F5 );
     case '6': return( T_F6 );
     case '7': return( T_F7 );
     case '8': return( T_F8 );
     case '9': return( T_F9 );
    }
   }
  }
  return( -1 );
 }

#endif
