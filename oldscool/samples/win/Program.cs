using System;
using OldScool;

class Demo {

	static Win[] _wins = new Win[5];
	static int _current;

	static int Main ()
	{
		var vio = new Vio();
		vio.Clear();
		vio.DoBackground();
		vio.Status("~F1~ Help ~F2~ Искать \u2500 \u2501 | ~1~st Win ~2~nd Win ~3~rd Win ~4~th Win ~m~iddle Win", AttribRole.LstStatusLine, AttribRole.LstStatusInvers);

		CreateWindows(vio);

		var running = true;
		while (running)
		{
			var ch = Console.ReadKey(true);
			running = Execute(vio, ch);
		}

		Vio.Done();
		return 0;
	}

	static bool Execute(Vio vio, ConsoleKeyInfo key) 
	{
		switch(key.Key) {
			case ConsoleKey.Escape: return false;
			case ConsoleKey.Tab: 
				if( (key.Modifiers&ConsoleModifiers.Shift)==ConsoleModifiers.Shift) {
					if( --_current<0 ) _current = _wins.Length-1;
				} else {
					if( ++_current>=0 ) _current = 0;
				}
				return true;
		}
		var val = char.ToLower(key.KeyChar);
		switch(val) {
			case '+': if( ++_current>=0 ) _current = 0; break;
			case '-': if( --_current<0 ) _current = _wins.Length-1; break;
			case '1': _wins[0].Visible = !_wins[0].Visible; break;
			case '2': _wins[1].Visible = !_wins[1].Visible; break;
			case '3': _wins[2].Visible = !_wins[2].Visible; break;
			case '4': _wins[3].Visible = !_wins[3].Visible; break;
			case 'm': _wins[4].Visible = !_wins[4].Visible; break;
			case 's': _wins[_current].SetFrame( FrameType.Single ); break;
			case 'd': _wins[_current].SetFrame( FrameType.Double ); break;
			case 't': _wins[_current].SetFrame( FrameType.Thick ); break;
			case 'b': _wins[_current].SetFrame( FrameType.Block ); break;
			case 'n': _wins[_current].SetFrame( FrameType.None ); break;
		}
		return true;
	}

	static void CreateWindows(Vio vio) {
		_wins[0] = CreateWindow1(vio);
		_wins[1] = CreateWindow2(vio);
		_wins[2] = CreateWindow3(vio);
		_wins[3] = CreateWindow4(vio);
		_wins[4] = CreateWindowM(vio);
		for(var i=0; i<5; i++) {
			_wins[i].Printf($"This is window...: {i+1}\n");
			_wins[i].Printf($"Vio columns......: {vio.Columns}\n");
			_wins[i].Printf($"Vio rows.........: {vio.Rows}\n");
			_wins[i].Printf($"Win x............: {_wins[i].PosX}\n");
			_wins[i].Printf($"Win y............: {_wins[i].PosY}\n");
			_wins[i].Printf($"Win width........: {_wins[i].Width}\n");
			_wins[i].Printf($"Win height.......: {_wins[i].Height}\n");
			_wins[i].Ss( 25, 1, "ss" );
			_wins[i].Ssa( 25, 2, "ssa", AttribRole.MnuHotkey);
			_wins[i].Sz( 25, 3, 'z' );
			_wins[i].Sza( 25, 4, 'A', AttribRole.MnuHotkey );
		}
	}

	static int WindowWidth(Vio vio) {
		return vio.Columns/2-5;
	}
	static int WindowHeight(Vio vio) {
		return vio.Rows/2-5;
	}

	static Win CreateWindow1(Vio vio) {
		var win = new Win( vio, 1, 1, WindowWidth(vio), WindowHeight(vio) );
		win.SetFrame( FrameType.Single, AttribRole.WinFrame );
		win.SetTitle( " Links oben ", TitlePos.TopLeft );
		return win;
	}

	static Win CreateWindow2(Vio vio) {
		var win = new Win( vio, vio.Columns-WindowWidth(vio)-3, 1, WindowWidth(vio), WindowHeight(vio) );
		win.SetFrame( FrameType.Single );
		win.SetTitle( " Rechts oben ", TitlePos.TopRight );
		return win;
	}

	static Win CreateWindow3(Vio vio) {
		var win = new Win( vio, 1, vio.Rows-WindowHeight(vio)-4, WindowWidth(vio), WindowHeight(vio) );
		win.SetFrame( FrameType.Single );
		win.SetTitle( " Links unten ", TitlePos.BottomLeft );
		return win;
	}

	static Win CreateWindow4(Vio vio) {
		var win = new Win( vio, vio.Columns-WindowWidth(vio)-3, vio.Rows-WindowHeight(vio)-4, WindowWidth(vio), WindowHeight(vio) );
		win.SetFrame( FrameType.Single );
		win.SetTitle( " Rechts unten ", TitlePos.BottomRight );
		return win;
	}

	static Win CreateWindowM(Vio vio) {
		var win = new Win( vio, vio.CenterCol(WindowWidth(vio)), vio.CenterRow(WindowHeight(vio)), WindowWidth(vio), WindowHeight(vio) );
		win.SetTitle( " Zentriert ", TitlePos.TopCenter );
		win.SetBackground(AttribRole.LstStatusInvers);
		return win;
	}
}
