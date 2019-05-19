using System;
using OldSchool;
//using Terminal.Gui;
//using System;
//using Mono.Terminal;

class Demo {

  static int Main ()
  {
    //Application.UseSystemConsole = true;
    //Application.Init ();
    //Application.Run ();
    Vio.Init();                           // Bildschirm vorbereiten
    Vio.Background();                     // Hintergrund malen
    Vio.Done();                           // Bildschirm restaurieren
    Console.ReadKey();
    return 0;
  }

/*
 int            main         ( argc, argv )
 INT            argc;                            // Anzahl Parameter
 CHAR*          argv[];                          // Parameterliste
 {
  INT           ret;                             // Rueckgabe von Funktionen merken
  setbuf( stdout, NULL );
  #ifdef LANGUAGE
  __INTSTR = LngStrRead( FILE_LNG, LNG_FOREIGN );
  #endif
  if( ( ret=SetupHandleArg( argc, argv ) ) != 0 )// Fehlerhafter Schalter?
  {
   if( ret==-1 )
   SetupShowHelp( (ret==-1) ? NULL : argv[ret] );// Diesen anzeigen lassen mit Hilfe
   return( -1 );                                 // Fehler melden fuer Batch etc
  }
  VioInit( VIO_NONE );                           // Bildschirm vorbereiten
  EvInit();
#if( !defined IPC )
#if( ( !defined IPCOFFICE ) && ( !defined IPC80 ) )
  VioSsa( VioGetMaxCol()-20, 1, SetupGetName(), INVERS );
#endif
  SetupKralle();                                 // Unser Logo, wie immer
#endif
  EvSchonSetText( "* GastroFix *" );
  if( ( ret = SetupLoad() ) != ERR_NONE )        // Versuchen, die Daten einzulesen
  {
   if( ret!=ERR_MEM_ALLOC )                      // Speichermangel?
    WinMsgCenter( " Fehler beim Laden der Setup-Datei", MSG_INFO );
   else                                          // Meldung, dass Speicher fehlt
   {
    WinMsgCenter( " Es steht nicht gen�gend Speicher zur\n"
                  " Ausf�hrung des Setup-Programms zur\n"
                  " Verf�gung."
                , MSG_INFO
                );
    EvDone();
    VioDone();
    return( -1 );
   }
  }
  if( DskFileExist( "CODES.DAT" ) ) ArtLoadInfo( "CODES.DAT" );
  SetupMainMenu();                               // Hauptmenue ausfuehren
  switch( SetupSave() )                          // Versuchen, Daten zu sichern
  {
   case ERR_NONE:                                // Datei wurde gesichert
        break;
   case ERR_DISK_CREATE:                         // Fehler beim Oeffnen der Datei
        WinMsgCenter( " Fehler beim Anlegen der Setup-Datei!", MSG_INFO );
        break;
   case ERR_DISK_WRITE:                          // Fehler beim Schreiben in die Datei
        WinMsgCenter( " Fehler beim Schreiben in Setup-Datei!", MSG_INFO );
        break;
  }
#if( !defined IPC )
  WinEntfernen( Kralle );                        // Unser Logo, wie immer
#endif
  EvDone();
  VioDone();                                     // Bildschirm restaurieren
  return( 0 );                                   // Kein Fehler aufgetreten
*/
}