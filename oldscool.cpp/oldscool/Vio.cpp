#include "Vio.h"
#include <ncursesw/ncurses.h>
#include <fstream>

namespace OldScool {

	// http://www.melvilletheatre.com/articles/ncurses-extended-characters/index.html

	Vio::Vio()
	{
		debug(L"-----------------------------------------------------------------------------------------------------");
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

	void Vio::debug(const wstring& str, bool newLine)
	{
		wofstream os;
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
		if( has_colors() ) {
			::bkgd(_palette.getBackground() | ACS_CKBOARD);
			refresh();
		}
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
		//int x, y;
		//getyx(stdscr, y, x);
		int col = colnorm;
		int column = 0;
		int maxcol = getColumns();
		int row = getRows()-1;
/*
		for (std::string::const_iterator i=str.begin(); i<str.end(); ++i) {
//	debug(QString("TVio::status, row %1 status(%2)").arg(row).arg(cp));
			if( *i=='~' ) {
				col = (col==colnorm) ? colinv : colnorm;
				continue;
			}
//	debug(QString("TVio::status, cp[%1] = %2").arg(i).arg(cp[i]));
			_sza(column++, row, *i, col);
			if( column==maxcol )
				break;
		}
		for(int n=column; n<maxcol; n++)
			_sza(n, row, ' ', col);
			*/
		mvaddstr(row, 0, str.c_str());
//		::printw(str.c_str());
		//_ssa(0, row, str, col);

		//::move(y, x);
		::refresh();
	}

	void Vio::statusOff() const
	{
		status(" ", AttribRole::Background, AttribRole::Background);
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