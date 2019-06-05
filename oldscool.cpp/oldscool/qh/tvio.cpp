#include			"hilevel/tvio.h"
#include			<ncurses.h> 
#include			<QFile>
#include			<QTextStream>

namespace Hilevel
{
	void TVio::init()
	{
		if( m_Init )
			return;
		initscr();
		noecho();
		if( has_colors() ) {
			start_color();
		}
		m_Colors.configure();
		cbreak();
		keypad(stdscr, TRUE);
		mousemask(ALL_MOUSE_EVENTS, NULL);
		m_Init = true;
	}




}

#if 0
 VOID  CDECL    VioStatus    ( pcText, wNorm, wInvers )
 CHAR*          pcText;                          /* Darzustellender Text    */
 WORD           wNorm;                           /* Attribut fuer normalen Text */
 WORD           wInvers;                         /* Attribut fuer Hotkeys   */
 {
  INT           i;                               /* Schleifenzaehler        */
  INT           a            = 0;                /* Positionszaehler        */
  WORD          wAttr        = wNorm;            /* Anfangsattribut NORMAL  */
  for( i=0; pcText[i]!=0; i++ )                  /* Gesamten Text scannen   */
   if( pcText[i]=='~' )                          /* Hotkey-Farbe ein-/aus?  */
    wAttr = ( wAttr == wNorm ) ? wInvers : wNorm;/* Je nachdem Attribut aendern */
   else                                          /* Falls nich Text schreiben */
    VioDescr->Sza( a++, VioGetMaxRow()-1, pcText[i], wAttr );/* und Position erhoehen   */
  for( i=a; i < VioGetMaxCol(); i++ )            /* Rest der Statuszeile    */
   VioDescr->Sza( i, VioGetMaxRow()-1, ' ', wNorm );
 }
 
 #include       "hilevel/Vio.h"
 #include       "hilevel/VioBios.h"
 #include       "lolevel/ScrBios.h"
 #include       "defs/Bitdefs.h"
 #include       "defs/Colordef.h"
 #include       <stdlib.h>

 WORD CDECL     VioBiosLa    ( wX, wY )
 WORD           wX;                              /* Spalte                  */
 WORD           wY;                              /* Zeile                   */
 {
  BYTE          bAttr        = VioDescr->pbScreen[((wY*VioGetMaxCol()+wX)<<1)+1];
  return( (WORD) bAttr );
 }

 WORD CDECL     VioBiosLza   ( wX, wY )
 WORD           wX;                              /* Spalte                  */
 WORD           wY;                              /* Zeile                   */
 {
  WORD*         pwVioTemp    = (WORD*) VioDescr->pbScreen;
  WORD          wChAttr      = pwVioTemp[wY*VioGetMaxCol()+wX];
  return( wChAttr );
 }

 CHAR CDECL     VioBiosLz    ( wX, wY )
 WORD           wX;                              /* Spalte                  */
 WORD           wY;                              /* Zeile                   */
 {
  WORD*         pwVioTemp     = (WORD*) VioDescr->pbScreen;
  WORD          ch            = pwVioTemp[ wY*VioGetMaxCol()+wX ];
//  BYTE          ch           = VioDescr->pbScreen[(wY*VioGetMaxCol()+wX)<<1];
  return( ch&0xFF );
 }

 /***************************************************************************/
 /* VioBiosSsa                                                              */
 /* ----------------------------------------------------------------------- */
 /* Schreibt einen String mit Attribut an Position x/y durch direkten       */
 /* Bildschirmspeicherzugriff.                                              */
 /***************************************************************************/
 VOID CDECL     VioBiosSsa   ( wX, wY, pcStr, wAttr )
 WORD           wX;                              /* Spalte                  */
 WORD           wY;                              /* Zeile                   */
 CHAR*          pcStr;                           /* Der String              */
 WORD           wAttr;                           /* Attribut des Strings    */
 {
  BYTE*         pcTmpScreen  = &VioDescr->pbScreen[ ( wY*VioGetMaxCol()+wX ) << 1 ];
  CHAR*         pc           = pcStr;
  while( *pc )
  {
   *pcTmpScreen++ = *pc++;
   *pcTmpScreen++ = (CHAR) wAttr;
  }
 }

 /***************************************************************************/
 /* VioBiosSwa                                                              */
 /* ----------------------------------------------------------------------- */
 /* Fuellt ein Fenster mit dem Attribut attr durch direkten Bildschirmspei- */
 /* cherzugriff. Die Zeichen in dem Fenster werden nicht beeinflusst.       */
 /***************************************************************************/
 VOID CDECL     VioBiosSwa   ( wX, wY, wWidth, wHeight, wAttr )
 WORD           wX;                              /* Spalte                  */
 WORD           wY;                              /* Zeile                   */
 WORD           wWidth;                          /* Breite                  */
 WORD           wHeight;                         /* Hoehe                   */
 WORD           wAttr;                           /* Attribut                */
 {
  BYTE*         pbTmpScreen  = &VioDescr->pbScreen[ ( ( wY*VioGetMaxCol()+wX ) << 1 ) + 1 ];
  WORD          wDelta       = ( VioGetMaxCol()-wWidth ) << 1;
  INT           i;
  for( ; wHeight>0; wHeight--, pbTmpScreen += wDelta )
   for( i=wWidth; i>0; i--, pbTmpScreen += 2 )
    *pbTmpScreen = (BYTE) wAttr;
 }

 /***************************************************************************/
 /* VioBiosSwz                                                              */
 /* ----------------------------------------------------------------------- */
 /* Fuellt ein Fenster mit dem Zeichen ch durch direkten Bildschirmspei-    */
 /* cherzugriff. Die Attribute in dem Fenster werden nicht beeinflusst.     */
 /***************************************************************************/
 VOID CDECL     VioBiosSwz   ( wX, wY, wWidth, wHeight, bCh )
 WORD           wX;                              /* Spalte                  */
 WORD           wY;                              /* Zeile                   */
 WORD           wWidth;                          /* Breite                  */
 WORD           wHeight;                         /* Hoehe                   */
 BYTE           bCh;                             /* Zeichen                 */
 {
  BYTE*         pbTmpScreen = &VioDescr->pbScreen[ ( wY*VioGetMaxCol()+wX ) << 1 ];
  WORD          wDelta      = ( VioGetMaxCol()-wWidth ) << 1;
  INT           i;
  for( ; wHeight>0; wHeight--, pbTmpScreen += wDelta )
   for( i=wWidth; i>0; i--, pbTmpScreen += 2 )
    *pbTmpScreen = (BYTE) bCh;
 }

 /***************************************************************************/
 /* VioBiosSwza                                                             */
 /* ----------------------------------------------------------------------- */
 /* Fuellt ein Fenster mit dem Zeichen ch und dem Attribut attr durch di-   */
 /* rekten Bildschirmspeicherzugriff.                                       */
 /***************************************************************************/
 VOID CDECL     VioBiosSwza  ( wX, wY, wWidth, wHeight, cCh, wAttr )
 WORD           wX;                              /* Spalte                  */
 WORD           wY;                              /* Zeile                   */
 WORD           wWidth;                          /* Breite                  */
 WORD           wHeight;                         /* Hoehe                   */
 BYTE           cCh;                             /* Zeichen                 */
 WORD           wAttr;                           /* Attribut                */
 {
  WORD*         pwTmpScreen  = (WORD*) &VioDescr->pbScreen[ ( wY*VioGetMaxCol()+wX ) << 1 ];
  WORD          wDelta       = VioGetMaxCol()-wWidth;
  WORD          wChattr      = (WORD) ( ( wAttr<<8 ) | (cCh&0x0FF) );
  INT           i;
  for( ; wHeight>0; wHeight--, pwTmpScreen += wDelta )
   for( i=wWidth; i>0; i-- )
    *pwTmpScreen++ = wChattr;
 }

 /***************************************************************************/
 /* VioBiosSb2w                                                             */
 /* ----------------------------------------------------------------------- */
 /* Muellt einen Pufferinhalt durch direkten Bildschirmspeicherzugriff in   */
 /* ein Fenster.                                                            */
 /***************************************************************************/
 VOID CDECL     VioBiosSb2w  ( wX, wY, wWidth, wHeight, pwBuff )
 WORD           wX;                              /* Spalte                  */
 WORD           wY;                              /* Zeile                   */
 WORD           wWidth;                          /* Breite                  */
 WORD           wHeight;                         /* Hoehe                   */
 WORD*          pwBuff;                          /* Puffer                  */
 {
  WORD*         pwTmpScreen  = (WORD*) &VioDescr->pbScreen[ ( wY*VioGetMaxCol()+wX ) << 1 ];
  WORD          wDelta       = VioGetMaxCol()-wWidth;
  INT           i;
  for( ; wHeight>0; wHeight--, pwTmpScreen += wDelta )
   for( i=wWidth; i>0; i-- )
    *pwTmpScreen++ = *pwBuff++;
 }

 /***************************************************************************/
 /* VioBiosSw2b                                                             */
 /* ----------------------------------------------------------------------- */
 /* Speichert einen Fensterinhalt durch direkten Bildschirmspeicherzugriff  */
 /* in einem Puffer.                                                        */
 /***************************************************************************/
 VOID CDECL     VioBiosSw2b  ( wX, wY, wWidth, wHeight, pwBuff )
 WORD           wX;                              /* Spalte                  */
 WORD           wY;                              /* Zeile                   */
 WORD           wWidth;                          /* Breite                  */
 WORD           wHeight;                         /* Hoehe                   */
 WORD*          pwBuff;                          /* Puffer                  */
 {
  WORD*         pwTmpScreen  = (WORD*) &VioDescr->pbScreen[ ( wY*VioGetMaxCol()+wX ) << 1 ];
  WORD          wDelta       = VioGetMaxCol()-wWidth;
  INT           i;
  for( ; wHeight>0; wHeight--, pwTmpScreen += wDelta )
   for( i=wWidth; i>0; i-- )
    *pwBuff++ = *pwTmpScreen++;
 }

 VOID CDECL     VioBiosScrUp ( wX1, wY1, wX2, wY2, wAttr )
 WORD           wX1;
 WORD           wY1;
 WORD           wX2;
 WORD           wY2;
 WORD           wAttr;
 {
  ScrScrUp( 1, (BYTE)wX1, (BYTE)wY1, (BYTE)wX2, (BYTE)wY2, (BYTE)wAttr );
 }

 VOID CDECL     VioBiosScrDown( wX1, wY1, wX2, wY2, wAttr )
 WORD           wX1;
 WORD           wY1;
 WORD           wX2;
 WORD           wY2;
 WORD           wAttr;
 {
  ScrScrDown( 1, (BYTE)wX1, (BYTE)wY1, (BYTE)wX2, (BYTE)wY2, (BYTE)wAttr );
 }
#endif
