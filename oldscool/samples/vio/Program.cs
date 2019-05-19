using System;
using OldSchool;

class Demo {

  static int Main ()
  {
    Vio.Init();                           // Bildschirm vorbereiten
    Vio.Background();                     // Hintergrund malen
    Vio.Status(" ~ESC~ Quit", 112, 115);
    sprintf(cTemp, "Max Y.....: %d", VioGetMaxY());
    VioSs(INFO_POS, 1, cTemp);
    sprintf(cTemp, "Max Col...: %d", VioGetMaxCol());
    VioSs(INFO_POS, 2, cTemp);

    while (true)
    {
        var ch = Console.ReadKey();
        if (ch.Key != ConsoleKey.Escape) break;
    }

    Console.ReadKey();
    Vio.Done();                           // Bildschirm restaurieren
    return 0;
  }
}
