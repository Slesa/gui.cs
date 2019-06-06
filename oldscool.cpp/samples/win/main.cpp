#include <iostream>
#include <array>
#include <ncurses.h>
#include "Win.h"
#include "Vio.h"
#include "Colors.h"

static array<Win*, 5> _wins;
static auto _current = 0;

static bool execute(char ch);
static void createWindows(Vio* vio);
static Win* createWindow1(Vio* vio);
static Win* createWindow2(Vio* vio);
static Win* createWindow3(Vio* vio);
static Win* createWindow4(Vio* vio);
static Win* createWindowM(Vio* vio);

int main() {

	auto vio = new Vio();
	vio->doBackground();
	vio->status( "~F1~ Fenstertest ~F2~ Искать", ColorRole::LstStatusLine, ColorRole::LstStatusInvers);


	createWindows(vio);
	auto running = true;
	while (running) {
		auto ch = getch();
		running = execute(ch);
	}

	delete vio;
	return 0;
}

static bool execute(char key) {

	switch(key) {
		case 27: return false; // ESC
		case 7: // TAB
			//if( (key.Modifiers&ConsoleModifiers.Shift)==ConsoleModifiers.Shift) {
			//	if( --_current<0 ) _current = _wins.Length-1;
			//} else {
				if( ++_current>=0 ) _current = 0;
			//}
			return true;
	}
	auto val = char.ToLower(key.KeyChar);
	switch(val) {
		case '+': if( ++_current>=0 ) _current = 0; break;
		case '-': if( --_current<0 ) _current = _wins.size()-1; break;
		case '1': _wins[0].Visible = !_wins[0].Visible; break;
		case '2': _wins[1].Visible = !_wins[1].Visible; break;
		case '3': _wins[2].Visible = !_wins[2].Visible; break;
		case '4': _wins[3].Visible = !_wins[3].Visible; break;
		case 'm': _wins[4].Visible = !_wins[4].Visible; break;
		case 's': _wins[_current].setFrame( FrameType::Single, Palette.Instance.Get(ColorRole::WinFrame) ); break;
		case 'd': _wins[_current].setFrame( FrameType::Double, Palette.Instance.Get(ColorRole::WinFrame) ); break;
		case 't': _wins[_current].setFrame( FrameType::Thick, Palette.Instance.Get(ColorRole::WinFrame) ); break;
		case 'b': _wins[_current].setFrame( FrameType::Block, Palette.Instance.Get(ColorRole::WinFrame) ); break;
		case 'n': _wins[_current].setFrame( FrameType::None, Palette.Instance.Get(ColorRole::WinFrame) ); break;
	}
	return true;
}

static void createWindows(Vio* vio) {
	_wins[0] = createWindow1(vio);
	_wins[1] = createWindow2(vio);
	_wins[2] = createWindow3(vio);
	_wins[3] = createWindow4(vio);
	_wins[4] = createWindowM(vio);
	for(auto i=0; i<5; i++) {
		_wins[i].printf($"This is window...: {i+1}\n");
		_wins[i].printf($"Vio columns......: {Vio.Columns}\n");
		_wins[i].printf($"Vio rows.........: {Vio.Rows}\n");
		_wins[i].printf($"Win x............: {_wins[i].PosX}\n");
		_wins[i].printf($"Win y............: {_wins[i].PosY}\n");
		_wins[i].printf($"Win width........: {_wins[i].Width}\n");
		_wins[i].printf($"Win height.......: {_wins[i].Height}\n");
	}
}

static int windowWidth(Vio* vio) {
	return vio->getColumns()/2-5;
}
static int windowHeight(Vio* vio) {
	return vio->getRows()/2-5;
}

static Win* createWindow1(Vio* vio) {
	auto win = new Win( 1, 1, windowWidth(vio), windowHeight(vio) );
	win.SetFrame( FrameType::Single, Win::DefFrameAttr );
	win.SetTitle( " Links oben ", TitlePosition::TopLeft, Win::DefTitleAttr );
	return win;
}

static Win* createWindow2(Vio* vio) {
	var win = new Win( vio->getColumns()-windowWidth(vio)-1, 1, windowWidth(vio), windowHeight(vio) );
	win.SetFrame( FrameType::Single, Win::DefFrameAttr );
	win.SetTitle( " Rechts oben ", TitlePosition::TopRight, Win::DefTitleAttr );
	return win;
}

static Win createWindow3(Vio* vio) {
	var win = new Win( 1, Vio.Rows-windowHeight(vio)-3, windowWidth(vio), windowHeight(vio) );
	win.SetFrame( FrameType.Single, Win.DefFrameAttr );
	win.SetTitle( " Links unten ", TitlePosition.BottomLeft, Win.DefTitleAttr );
	return win;
}

static Win createWindow4(Vio* vio) {
	var win = new Win( vio->getColumns()-WindowWidth()-1, vio.getRows()-windowHeight(vio)-3, windowWidth(), windowHeight(vio) );
	win.SetFrame( FrameType::Single, Win::DefFrameAttr );
	win.SetTitle( " Rechts unten ", TitlePosition::BottomRight, Win::DefTitleAttr );
	return win;
}

static Win CreateWindowM(Vio* vio) {
	var win = new Win( vio->centerCol(windowWidth(vio)), vio->centerRow(windowHeight(vio)), windowWidth(vio), windowHeight(vio) );
	win.SetTitle( " Zentriert ", TitlePosition::TopCenter, Win::DefTitleAttr );
	return win;
}
