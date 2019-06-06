#include "Vio.h"
#include <ncurses.h>
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

	void Vio::doBackground()const
	{
		if( has_colors() )
			::bkgd(_colors.getBackground());
	}

	void Vio::clear() const
	{
		::clear();
	}

	void Vio::gotoxy(int x, int y) const
	{
		::move(y, x);
	}

	void Vio::sa(int x, int y, int col) const
	{
	}

	void Vio::sz(int x, int y, char ch) const
	{
		mvaddch(y, x, ch);
	}

	void Vio::sza(int x, int y, char ch, int col) const
	{
		::move(y, x);
		::attrset(_colors.getColor(col));
		::addch(ch);
		::attroff(_colors.getColor(col));
	}

	void Vio::ss(int x, int y, const string& str) const
	{
		mvaddstr(y, x, str.c_str());
	}

	void Vio::ssa(int x, int y, string str, int col) const
	{
		::move(y, x);
		::attrset(_colors.getColor(col));
		::addstr(str.c_str());
		::attroff(_colors.getColor(col));
	}

	void Vio::status(string str, int colnorm, int colinv) const
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

	void Vio::statusOff() const
	{
		status(" ", ColorRole::Background, ColorRole::Background);
	}

}