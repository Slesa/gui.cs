using System;
using OldSchool;

class Demo {

  static int Main ()
  {
    Vio.Init();                           // Bildschirm vorbereiten
    Vio.Background();                     // Hintergrund malen
    Vio.Status(" [~ESC~] Quit  [~F1~] Добрый день");
    var colInfo = $"Column..: {Vio.Columns}";
    var rowInfo = $"Rows....: {Vio.Rows}";
    Vio.Ss(3, 3, colInfo);
    Vio.Ssa(3, 4, rowInfo, Colors.MakeAttr(ConsoleColor.Yellow, ConsoleColor.Magenta));

    while (true)
    {
        var ch = Console.ReadKey();
        if (ch.Key == ConsoleKey.Escape) break;
    }

    Vio.Done();                           // Bildschirm restaurieren
    return 0;
  }
}
