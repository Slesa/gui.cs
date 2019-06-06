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
		static bool	canColors();
		static int	getColorCount();
	public:
		void doBackground() const;
		int centerCol(int len=0) const
		{
			return (getColumns()-len) / 2;
		}
		int centerRow(int height=0) const
		{
			return (getRows()-height) / 2;
		}
		Colors& getColors()
		{
			return _colors;
		}
	public:
		void clear() const;
		void gotoxy(int x, int y) const;
		void sa(int x, int y, int col) const;
		void sz(int x, int y, char ch) const;
		void sza(int x, int y, char ch, int col) const;
		void ss(int x, int y, const string& str) const;
		void ssa(int x, int y, string str, int col) const;
		void status(string str, int colnorm, int colinv) const;
		void statusOff() const;
	private:
		void init();
		void done();
		Colors _colors;
	};

}

using namespace OldScool;

#endif //OLDSCOOL_VIO_H
