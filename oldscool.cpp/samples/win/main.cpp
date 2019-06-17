#include <iostream>
#include <array>
#include <ncurses.h>
#include <sstream>
#include "Win.h"
#include "Vio.h"
#include "Palette.h"

static array<Win*, 5> _wins;
static auto _current = 0;

static string toString(string label, int value);
static bool execute(const Vio& vio, char ch);
static void createWindows(const Vio& vio);
static Win* createWindow1(const Vio& vio);
static Win* createWindow2(const Vio& vio);
static Win* createWindow3(const Vio& vio);
static Win* createWindow4(const Vio& vio);
static Win* createWindowM(const Vio& vio);

int main() {

	auto vio = new Vio();
	vio->doBackground();
	vio->status( "~F1~ Help ~F2~ Искать | ~1~st Win ~2~nd Win ~3~rd Win ~4~th Win ~m~iddle Win \uE030", AttribRole::LstStatusLine, AttribRole::LstStatusInvers);


	createWindows(*vio);
	auto running = true;
	while (running) {
		auto ch = getch();
		running = execute(*vio, ch);
	}

	delete vio;
	return 0;
}

static bool execute(const Vio& vio, char key) {

	switch(key) {
		case 27: return false; // ESC
		case 7: // TAB
			//if( (key.Modifiers&ConsoleModifiers.Shift)==ConsoleModifiers.Shift) {
			//	if( --_current<0 ) _current = _wins.Length-1;
			//} else {
				if( ++_current>=_wins.size() ) _current = 0;
			//}
			return true;
	}
//	auto val = toLower(key.KeyChar);
	switch(key) {
		case '+': if( ++_current>=0 ) _current = 0; break;
		case '-': if( --_current<0 ) _current = _wins.size()-1; break;
		case '1': _wins[0]->setVisible( !_wins[0]->isVisible() ); break;
		case '2': _wins[1]->setVisible( !_wins[1]->isVisible() ); break;
		case '3': _wins[2]->setVisible( !_wins[2]->isVisible() ); break;
		case '4': _wins[3]->setVisible( !_wins[3]->isVisible() ); break;
		case 'm': _wins[4]->setVisible( !_wins[4]->isVisible() ); break;
		case 's': _wins[_current]->setFrame( FrameType::Single, vio.getPalette().get(AttribRole::WinFrame) ); break;
		case 'd': _wins[_current]->setFrame( FrameType::Double, vio.getPalette().get(AttribRole::WinFrame) ); break;
		case 't': _wins[_current]->setFrame( FrameType::Thick, vio.getPalette().get(AttribRole::WinFrame) ); break;
		case 'b': _wins[_current]->setFrame( FrameType::Block, vio.getPalette().get(AttribRole::WinFrame) ); break;
		case 'n': _wins[_current]->setFrame( FrameType::None, vio.getPalette().get(AttribRole::WinFrame) ); break;
	}
	return true;
}

static void createWindows(const Vio& vio) {
	_wins[0] = createWindow1(vio);
	_wins[1] = createWindow2(vio);
	_wins[2] = createWindow3(vio);
	_wins[3] = createWindow4(vio);
	_wins[4] = createWindowM(vio);
	for(auto i=0; i<5; i++) {
		_wins[i]->print( toString("This is window...: ", i+1) );
		_wins[i]->print( toString("Vio columns......: ", vio.getColumns()) );
		_wins[i]->print( toString("Vio rows.........: ", vio.getRows()) );
		_wins[i]->print( toString("Win x............: ", _wins[i]->getX()) );
		_wins[i]->print( toString("Win y............: ", _wins[i]->getY()) );
		_wins[i]->print( toString("Win width........: ", _wins[i]->getWidth()) );
		_wins[i]->print( toString("Win height.......: ", _wins[i]->getHeight()) );
        _wins[i]->ss( 25, 1, "ss" );
        _wins[i]->ssa( 25, 2, "ssa", vio.getPalette().get(AttribRole::MnuHotkey));
        _wins[i]->sz( 25, 3, 'z' );
        _wins[i]->sza( 25, 4, 'A', vio.getPalette().get(AttribRole::MnuHotkey) );
	}
}

static string toString(string label, int value) {
	ostringstream oss;
	oss << label << value << '\n';
	return oss.str();
}
static int windowWidth(const Vio& vio) {
	return vio.getColumns()/2-5;
}
static int windowHeight(const Vio& vio) {
	return vio.getRows()/2-5;
}

static Win* createWindow1(const Vio& vio) {
	auto win = new Win(vio, 1, 1, windowWidth(vio), windowHeight(vio) );
	win->setFrame( FrameType::Single, AttribRole::WinFrame );
	win->setTitle( " Links oben ", TitlePos::TopLeft, AttribRole::WinTitle );
	return win;
}

static Win* createWindow2(const Vio& vio) {
	auto win = new Win(vio, vio.getColumns()-windowWidth(vio)-3, 1, windowWidth(vio), windowHeight(vio) );
	win->setFrame( FrameType::Single, AttribRole ::WinFrame );
	win->setTitle( " Rechts oben ", TitlePos::TopRight, AttribRole ::WinTitle );
	return win;
}

static Win* createWindow3(const Vio& vio) {
	auto win = new Win(vio, 1, vio.getRows()-windowHeight(vio)-4, windowWidth(vio), windowHeight(vio) );
	win->setFrame( FrameType::Single, AttribRole::WinFrame );
	win->setTitle( " Links unten ", TitlePos::BottomLeft, AttribRole::WinTitle );
	return win;
}

static Win* createWindow4(const Vio& vio) {
	auto win = new Win(vio, vio.getColumns()-windowWidth(vio)-3, vio.getRows()-windowHeight(vio)-4, windowWidth(vio), windowHeight(vio) );
	win->setFrame( FrameType::Single, AttribRole::WinFrame );
	win->setTitle( " Rechts unten ", TitlePos::BottomRight, AttribRole ::WinTitle );
	return win;
}

static Win* createWindowM(const Vio& vio) {
	auto win = new Win(vio, vio.centerCol(windowWidth(vio)), vio.centerRow(windowHeight(vio)), windowWidth(vio), windowHeight(vio) );
	win->setTitle( " Zentriert ", TitlePos::TopCenter, AttribRole ::WinTitle );
	return win;
}
