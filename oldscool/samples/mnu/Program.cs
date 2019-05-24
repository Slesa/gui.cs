using System;
using OldSchool;

class Demo {

  static int Main ()
  {
    Vio.Init();                           // Bildschirm vorbereiten
    Vio.Background();                     // Hintergrund malen
    Vio.Status(" ~[F1]~ Fenstertest ~[F2]~ Test des Fensters");
    
    var mnu = new Mnu();
    mnu.Add(1, 1, "Eintrag ~1~" ); //, " [~F1~] Dies ist Eintrag 1" );
    mnu.Add(1, 2, "Eintrag ~2~" ); //, " [~F2~] Dies ist Eintrag 2" );
    mnu.Add(1, 3, "Eintrag ~3~" ); //, " [~F3~] Dies ist Eintrag 3" );
    mnu.Add(1, 4, "Eintrag ~4~" ); //, " [~F4~] Dies ist Eintrag 4" );
    mnu.Add(1, 5, "Eintrag ~5~" ); //, " [~F5~] Dies ist Eintrag 5" );
    mnu.Add(1, 6, "Eintrag ~6~" ); //, " [~F6~] Dies ist Eintrag 6" );
    mnu.Add(1, 8, "Eintrag ~7~" ); //, " [~F7~] Dies ist Eintrag 7" );


        var running = true;
    while (running)
    {
        var ret = mnu.Handle( "Eintragswahl", Vio.CenterCol( 20 ), Vio.CenterRow( 11 ) /*, MenuHelp*/ );
        if( ret == -1 ) break;
        //var ch = Console.ReadKey();
        //running = Execute(ch);
    }

    Vio.Done();                           // Bildschirm restaurieren
    return 0;
  }

}
