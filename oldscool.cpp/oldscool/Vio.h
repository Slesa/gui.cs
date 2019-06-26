#ifndef OLDSCOOL_VIO_H
#define OLDSCOOL_VIO_H
#include <string>
#include <set>
#include "Palette.h"

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
		static bool	canColors();
		static int	getColorCount();
		static int	getColumns();
		static int	getRows();
		const Palette& getPalette() const
		{
			return _palette;
		}
	public:
		int centerCol(int len=0) const
		{
			return (getColumns()-len) / 2;
		}
		int centerRow(int height=0) const
		{
			return (getRows()-height) / 2;
		}
	public:
		void doBackground();
		void clear() const;
		void gotoxy(int x, int y) const;
		void sz(int x, int y, wchar_t ch) const;
		void sza(int x, int y, wchar_t ch, int col);
		void ss(int x, int y, const string& str) const;
		void ssa(int x, int y, const string& str, int col);
		void status(const string& str, int colnorm, int colinv);
		void statusOff();
	private:
		int getAttr() const;
		void setAttr(int attrib);
		void clrAttr(int attrib);
		void _sa(int x, int y, int col);
		void _sz(int x, int y, wchar_t ch) const;
		void _sza(int x, int y, wchar_t ch, int col);
		void _ss(int x, int y, const string& str) const;
		void _ssa(int x, int y, const string& str, int col);
	private:
		void init();
		void done();
		Palette _palette;
		std::set<int> _usedAttribs;
		friend class Win;
	};

}

using namespace OldScool;

#endif //OLDSCOOL_VIO_H
