#ifndef				HILEVEL_TVIO_H
#define				HILEVEL_TVIO_H

namespace Hilevel
{
	#define			KEY_RETURN		10
	#define			KEY_ESC			27

}

#endif

#if 0
/****************************************************************************/
/* VIO.H                                                                    */
/* ------------------------------------------------------------------------ */
/* Definition der Funktionen zum Zugriff auf den PC-Bildschirm.             */
/****************************************************************************/
#ifndef         _VIO_

 #define        _VIO_

 /* ----------- Funktionstypen -------------------------------------------- */
 typedef VOID (CDECL *FVioSza)   PARAM(( WORD, WORD, CHAR, WORD ));
 typedef WORD (CDECL *FVioLza)   PARAM(( WORD, WORD ));
 typedef VOID (CDECL *FVioSs)    PARAM(( WORD, WORD, CHAR* ));
 typedef VOID (CDECL *FVioSsa)   PARAM(( WORD, WORD, CHAR*, WORD ));
 typedef VOID (CDECL *FVioSwa)   PARAM(( WORD, WORD, WORD, WORD, WORD ));
 typedef VOID (CDECL *FVioSwz)   PARAM(( WORD, WORD, WORD, WORD, BYTE ));
 typedef VOID (CDECL *FVioSwza)  PARAM(( WORD, WORD, WORD, WORD, BYTE, WORD ));
 typedef VOID (CDECL *FVioCopy)  PARAM(( WORD, WORD, WORD, WORD, WORD* ));
 typedef VOID (CDECL *FVioScroll)PARAM(( WORD, WORD, WORD, WORD, WORD ));
 typedef VOID (CDECL *FVioPutp)  PARAM(( WORD, WORD, LONG ));
 typedef LONG (CDECL *FVioGetp)  PARAM(( WORD, WORD ));

 /* ----------- Bildschirmdeskriptor -------------------------------------- */
 #ifndef UNIX
 #pragma        pack(1)
 #endif
 typedef        struct
 {
  INT           iMode;                           /* Aktueller Modus         */
  WORD          wMaxX;                           /* Grafik: X-Aufloesung    */
  WORD          wMaxY;                           /* Grafik: Y-Aufloesung    */
  BYTE*         bpChars;                         /* Grafik: Zeichentabelle  */
  BYTE*         bpChars2;                        /* Grafik: Zeichentabelle  */
  BYTE          bChHeight;                       /* Grafik: Zeichenhoehe    */
  WORD          wFlags;                          /* Diverse Flags           */
  BYTE*         pbScreen;                        /* Bildschirmzeiger        */
  FVioLza       Lza;                             /* Zeichen/Attrib. lesen   */
  FVioSa        Sa;                              /* Attribut setzen         */
  FVioSz        Sz;                              /* Zeichen setzen          */
  FVioSza       Sza;                             /* Attribut mit Zeichen setzen */
  FVioSs        Ss;                              /* String schreiben        */
  FVioSsa       Ssa;                             /* String mit Attribut schreiben */
  FVioSwa       Swa;                             /* Fenster mit Attribut fuellen */
  FVioSwz       Swz;                             /* Fenster mit Zeichen fuellen */
  FVioSwza      Swza;                            /* Fenster mit Attribut und Zeichen fuellen */
  FVioCopy      Sb2w;                            /* Buffer auf Schirm schreiben */
  FVioCopy      Sw2b;                            /* Fensterinhalt speichern */
  FVioScroll    ScrollUp;                        /* Fensterinhalt nach oben scrollen */
  FVioScroll    ScrollDown;                      /* Fensterinhalt nach untern scrollen */
  FVioPutp      Putpixel;                        /* Grafik: Punkt setzen    */
  FVioGetp      Getpixel;                        /* Grafik: Punkt lesen     */
 } SVio;

 /* ----------- Prototypen ------------------------------------------------ */
 DECLSTART
#if( !defined UNIX )
 BYTE* CDECL    VioSetAdr       PARAM(( void ));           /* Bildadresse setzen      */
#endif
 DECLEND

 /* ----------- Mappings fuer Default-Fall -------------------------------- */
 #define        VioLza( x, y )                      VioDescr->Lza( x, y )
 #define        VioSa( x, y, attr )                 VioDescr->Sa( x, y, attr )
 #define        VioSz( x, y, ch )                   VioDescr->Sz( x, y, ch )
 #define        VioSza( x, y, ch, attr )            VioDescr->Sza( x, y, ch, attr )
 #define        VioSs( x, y, str )                  VioDescr->Ss( x, y, str )
 #define        VioSsa( x, y, str, attr )           VioDescr->Ssa( x, y, str, attr )
 #define        VioSwa( x1, y1, x2, y2, attr )      VioDescr->Swa( x1, y1, x2, y2, attr )
 #define        VioSwz( x1, y1, x2, y2, ch )        VioDescr->Swz( x1, y1, x2, y2, ch )
 #define        VioSwza( x1, y1, x2, y2, ch, attr ) VioDescr->Swza( x1, y1, x2, y2, ch, attr )
 #define        VioSb2w( x1, y1, x2, y2, buff )     VioDescr->Sb2w( x1, y1, x2, y2, buff )
 #define        VioSw2b( x1, y1, x2, y2, buff )     VioDescr->Sw2b( x1, y1, x2, y2, buff )
 #define        VioScrUp( x1, y1, x2, y2, attr )    VioDescr->ScrollUp( x1, y1, x2, y2, attr )
 #define        VioScrDown( x1, y1, x2, y2, attr )  VioDescr->ScrollDown( x1, y1, x2, y2, attr )



#endif

#endif
