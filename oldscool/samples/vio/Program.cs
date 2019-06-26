using System;
using OldScool;

class Demo {

	static int LABEL_POS = 21;
	static int VALUE_POS = 51;
	
	static int Main ()
	{
		var vio = new Vio();
		vio.Clear();
		vio.DoBackground();

		var done = false;
		while (!done) {
			
			vio.Status(" ~F1~ Please Select a driver  ~F2~ Искать \uE030", AttribRole.LstStatusLine, AttribRole.LstStatusInvers );
		
			vio.Ss( LABEL_POS, 2, "Columns.....:");
			vio.Ssa( VALUE_POS, 2, $"{vio.Columns}", AttribRole.MsgFrame);

			vio.Ss( LABEL_POS, 3, "Rows........:");
			vio.Ssa( VALUE_POS, 3, $"{vio.Rows}", AttribRole.MsgFrame);
		
			var ch = Console.ReadKey();
			if (ch.Key == ConsoleKey.Escape) done = true;
		}

		// vio.Done();
		return 0;
	}
}
