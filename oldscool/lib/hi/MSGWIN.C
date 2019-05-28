
 /* ------------ Interne Daten -------------------------------------------- */
 SWin*          MsgWin       = NULL;             /* Informationsfenster     */
 SWin*          MsgSave      = NULL;             /* Urspr. aktuelles Fenster*/

 INT CDECL      WinMsg       ( wX, wY, pcText, iMode )
 WORD           wX;
 WORD           wY;
 CHAR*          pcText;
 INT            iMode;
 {
  INT           iMaxlen      = 0;
  INT           iZeilenzahl  = 1;
  INT           iTaste;
  INT           iLen         = 0;
  CHAR*         cp;
  /* Wenn mode=MELDUNG_DAUER2, dann einfach Fenster schlie�en und raus      */
  if( ( iMode==MSG_DAUER2 ) && ( MsgWin!=NULL ) )
  {
   WinEntfernen( MsgWin ); MsgWin = NULL;        /* Info-Fenster schliessen */
   WinAktivieren( MsgSave ); MsgSave=NULL;       /* urspr Fenster aktivieren */
   return( 0 );                                  /* und raus                */
  }
  WinCursor( FALSE );                            /* Cursor abschalten       */
  MsgSave = WinGetAktivPwkb();                   /* Aktuelles Fenster merken */
  /* Ermittle l�ngste Zeile                                                 */
  cp   = pcText;
  iLen = 0;
  while( *pcText )
  {
   iLen++;
   if( *pcText++=='\n' )
   {
    if( iLen>iMaxlen ) iMaxlen=iLen;
    iLen=0;
    iZeilenzahl++;
   }
  }
  if( iLen>iMaxlen ) iMaxlen=iLen;
/*  win = WinEinrichten( VioCenterX( iMaxlen+6 ), VioCenterY( iZeilenzahl+4 ), iMaxlen+6, iZeilenzahl+4 ); */
  MsgWin = WinEinrichten( wX, wY, iMaxlen+6, iZeilenzahl+4 );
  if( MsgWin==NULL ) return( 0 );
  WinSchattenEin( MsgWin );
  WinSetRahmentyp( MsgWin, WIN_FRAME_SINGLE );
  WinSetAttrib( MsgWin, MsgGetPal( MSG_PAL_TEXT ), MsgGetPal( MSG_PAL_FRAME ), MsgGetPal( MSG_PAL_TITLE ) );
  WinAktivieren( MsgWin );
  WinCls();
  pcText = cp;
  WinPrintf( "\n%s", pcText );
  /*
  while( *sCp )
  {
   WinPrintf( "%c", *sCp++ );
   if( *sCp=='\n' )
   {
    WinPrintf( "\n  " );
   }
  }
  */
  /* Die Modes handlen                                                      */
  switch( iMode )
  {
   case MSG_INFO:
        WinSetTitel( MsgWin, " <Taste> ", WIN_TITEL_UR );
        do
        {
         iTaste=EvGetKey();
        } while( ( iTaste==0 ) || ( iTaste==MOU_MOVED ) );
        break;
   case MSG_YESNO:
   case MSG_YESNO2:
        if( iMode==MSG_YESNO )
         WinSetTitel( MsgWin, " <Return>=JA <ESC>=NEIN ", WIN_TITEL_UR );
        while( TRUE )
        {
         iTaste=EvGetKey();
         if( ( iTaste==T_ESC ) || ( iTaste==T_RETURN ) ) break;
        }
        break;
   case MSG_F1F2:
        while( TRUE )
        {
         iTaste=EvGetKey();
         if( ( iTaste==T_F1 ) || ( iTaste==T_F2 ) ) break;
        }
        break;
   case MSG_DAUER:
        return( 0 );                             /* Ohne Fenster zu schlie�en wieder raus */
  }
  if( iMode!=MSG_DAUER )
  {
   WinEntfernen( MsgWin );
   WinAktivieren( MsgSave );
   MsgSave=NULL;
  }
  WinCursor( FALSE );
  return( iTaste );
 }

