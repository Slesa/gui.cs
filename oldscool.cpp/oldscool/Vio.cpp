#include "Vio.h"
#include <curses.h>
#include <fstream>

namespace OldScool {

	Vio::Vio()
	{
		debug("-----------------------------------------------------------------------------------------------------");
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
		_colors.configure();
		cbreak();
		keypad(stdscr, TRUE);
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

	bool Vio::hasColors()
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

	void Vio::doBackground()
	{
		if( has_colors() )
			::bkgd(_colors.getBackground());
	}

	void Vio::clear()
	{
		::clear();
	}

	void Vio::gotoxy(int x, int y)
	{
		::move(y, x);
	}

	void Vio::sa(int x, int y, int col)
	{
	}

	void Vio::sz(int x, int y, char ch)
	{
		mvaddch(y, x, ch);
	}

	void Vio::sza(int x, int y, char ch, int col)
	{
		::move(y, x);
		::attrset(_colors.getColor(col));
		::addch(ch);
		::attroff(_colors.getColor(col));
	}

	void Vio::ss(int x, int y, const string& str)
	{
		mvaddstr(y, x, str.c_str());
	}

	void Vio::ssa(int x, int y, const string& str, int col)
	{
		::move(y, x);
		::attrset(_colors.getColor(col));
		::addstr(str.c_str());
		::attroff(_colors.getColor(col));
	}

	void Vio::status(const string& str, int colnorm, int colinv)
	{
		int x, y;
		getyx(stdscr, y, x);
		int col = colnorm;
		int column = 0;
		int maxcol = getColumns();
		int row = getRows()-1;

		for (std::string::const_iterator i=str.begin(); i<str.end(); ++i) {
//	debug(QString("TVio::status, row %1 status(%2)").arg(row).arg(cp));
			if( *i=='~' ) {
				col = (col==colnorm) ? colinv : colnorm;
				continue;
			}
//	debug(QString("TVio::status, cp[%1] = %2").arg(i).arg(cp[i]));
			sza(column++, row, *i, col);
			if( column==maxcol )
				break;
		}
		for(int n=column; n<maxcol; n++)
			sza(n, row, ' ', col);
		::move(y, x);
		::refresh();
	}

	void Vio::statusOff()
	{
		status(" ", ColorRole::Background, ColorRole::Background);
	}

}