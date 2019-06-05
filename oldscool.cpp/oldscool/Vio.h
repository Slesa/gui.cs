#ifndef OLDSCOOL_VIO_H
#define OLDSCOOL_VIO_H
#include <string>
#include "Colors.h"

using namespace std;

namespace OldScool {

	class Vio {
    public:	// ---- ctor/dtor -----------------------------------------------------------------------------------------
		Vio();
		~Vio();
	public:	// ---- static funkcions ----------------------------------------------------------------------------------
		static void	debug(const string& str, bool newline=true);
		static void	beep();
		static void	flash();
		static void	sleep(int msecs);
		static int	getColumns();
		static int	getRows();
		static bool	hasColors();
		static int	getColorCount();
	public:
		void doBackground();
		int centerCol(int len=0)
		{
			return (getColumns()-len) / 2;
		}
		int centerRow(int height=0)
		{
			return (getRows()-height) / 2;
		}
		Colors& getColors()
		{
			return _colors;
		}
	public:
		void clear();
		void gotoxy(int x, int y);
		void sa(int x, int y, int col);
		void sz(int x, int y, char ch);
		void sza(int x, int y, char ch, int col);
		void ss(int x, int y, const string& str);
		void ssa(int x, int y, const string& str, int col);
		void status(const string& str, int colnorm, int colinv);
		void statusOff();
	private:
		void init();
		void done();
		Colors _colors;
	};

}

using namespace OldScool;

#endif //OLDSCOOL_VIO_H
