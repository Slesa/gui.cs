#include "Vio.h"
#include <ncursesw/ncurses.h>
#include <fstream>
#include <iostream>

namespace OldScool {

	// http://www.melvilletheatre.com/articles/ncurses-extended-characters/index.html

	Vio::Vio()
	: _currAttrib(0)
	{
		// debug(L"-----------------------------------------------------------------------------------------------------");
		init();
	}

	Vio::~Vio()
	{
		done();
	}

	void Vio::init() {
		initscr();
		noecho();
		if( has_colors() ) {
			start_color();
		}
		_palette.configure();
		cbreak();
		curs_set(0);
		keypad(stdscr, TRUE);
		notimeout(stdscr, TRUE);
		mousemask(ALL_MOUSE_EVENTS, NULL);
	}

	void Vio::done() {
		endwin();
	}

	void Vio::debug(const string& str, bool newLine)
	{
		ofstream os;
		os.open("viodebug.txt");
		os << str;
		if (newLine) os << "\n";
#if 0
#endif
	}

	void Vio::beep()
	{
		::beep();
	}

	void Vio::flash()
	{
		::flash();
	}

	void Vio::sleep(int msecs)
	{
		::napms(msecs);
	}

	bool Vio::canColors()
	{
		return has_colors();
	}

	int Vio::getColumns()
	{
		return COLS;
	}

	int Vio::getRows()
	{
		return LINES;
	}

	int Vio::getColorCount()
	{
		return COLORS;
	}

//	wchar_t bgChar = L'\u2592';

	void Vio::doBackground()const
	{
		string bg;
		bg.append(getColumns(), ACS_CKBOARD);

		move(0, 0);
	// cout << "Background: " << bg; getch();
		// if( has_colors() )
		//auto attr = _palette.get(AttribRole::Background);
		::attrset(_palette.get(AttribRole::Background));
//        ::attrset(_palette.get(AttribRole::Background));
		for(auto i=0; i<getRows(); i++) {
			for (auto n = 0; n < getColumns(); n++) {
				addch(ACS_CKBOARD);
			}
		}
			//mvaddstr(i, 0, bg.c_str());
		//if( has_colors() ) {
		//	::bkgd(/*_palette.getBackground() |*/ /*ACS_CKBOARD*/ '*');
//		::attrset(_palette.get(AttribRole::MnuHotInvers));
		::refresh();
		//}
	}

	void Vio::clear() const
	{
		::clear();
	}

	void Vio::gotoxy(int x, int y) const
	{
		::move(y, x);
	}

/*	void Vio::sa(int x, int y, int col) const
	{
		_sa(x, y, col);
		::refresh();
	} */

	void Vio::sz(int x, int y, wchar_t ch) const
	{
		_sz(x, y, ch);
		::refresh();
	}

	void Vio::sza(int x, int y, wchar_t ch, int col) const
	{
		_sza(x, y, ch, col);
		::refresh();
	}

	void Vio::ss(int x, int y, const string& str) const
	{
		_ss(x, y, str);
		::refresh();
	}

	void Vio::ssa(int x, int y, const string& str, int col) const
	{
		_ssa(x, y, str, col);
		::refresh();
	}

	void Vio::status(const string& str, int colnorm, int colinv) const
	{
		int x, y;
		getyx(stdscr, y, x);
		int column = 0;
		int maxcol = getColumns();
		int row = getRows()-1;

		move(row, 0);

		auto attr = colnorm;
		auto text = str;
		while( !text.empty() ) {
			::attrset(_palette.get(attr));
			auto pos = text.find('~');
			if( pos!=string::npos ) {
				auto substr = text.substr(0, pos);
				// cout << "Substr " << substr << '\n';
				::addstr(substr.c_str());
				column += substr.length();
				attr = (attr==colnorm) ? colinv : colnorm;
				text = text.substr(pos+1);
				// cout << "Text " << text << '\n';
				// getch();
			}
			else {
				if( column+text.length()>maxcol)
					text = text.substr(0, maxcol-column);
				else
					text.append(maxcol-text.length(), ' ');
				::addstr(text.c_str());
				// cout << "Remain " << text << '\n';
				break;
			}
		}

		::move(y, x);
		::refresh();
	}

	void Vio::statusOff() const
	{
		status(" ", AttribRole::Background, AttribRole::Background);
	}

	int Vio::getAttr() const {
		return _currAttrib;
	}

	void Vio::setAttr(int attrib) {
		_currAttrib = attrib;
		::attrset(_currAttrib);
	}

	/* void Vio::_sa(int x, int y, int col) const
	{
	} */

	void Vio::_sz(int x, int y, wchar_t ch) const
	{
		mvaddch(y, x, ch);
	}

	void Vio::_sza(int x, int y, wchar_t ch, int col) const
	{
		::move(y, x);
		::attrset(_palette.get(col));
		::addch(ch);
		::attroff(_palette.get(col));
	}

	void Vio::_ss(int x, int y, const string& str) const
	{
		mvaddstr(y, x, str.c_str());
	}

	void Vio::_ssa(int x, int y, const string& str, int col) const
	{
		::move(y, x);
		::attrset(_palette.get(col));
		::addstr(str.c_str());
		::attroff(_palette.get(col));
	}
}