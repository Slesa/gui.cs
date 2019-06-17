#ifndef OLDSCOOL_WIN_H
#define OLDSCOOL_WIN_H

#include <string>
#include "Vio.h"

using namespace std;

struct _win_st;

namespace OldScool {

	enum TitlePos
	{
		TopLeft,
		TopCenter,
		TopRight,
		BottomLeft,
		BottomCenter,
		BottomRight
	};


	enum FrameType
	{
		None,
		Single,
		Double,
		Thick,
		Block
	};

	enum CursorType
	{
		CurOff,
		CurLine,
		CurBlock
	};

	class Win {

	public:	// ---- ctor/dtor --------------------------------------------------------------------------------------
		Win(const Vio& vio, int x, int y, int width, int height);
		~Win();

		int getX();
		int getY();
		int getWidth();
		int getHeight();
		int centerCol(int len=0)
		{
			return (getWidth()-len) / 2;
		}
		int centerRow(int height=0)
		{
			return (getHeight()-height) / 2;
		}
		bool isVisible();
		void setVisible(bool doShow) { if (doShow) show(); else hide(); }
		void setFrame(FrameType frame, int attr);
		void setTitle(const string& title, TitlePos pos, int attr);
		void show();
		void hide();
		void setBackground(int col);
		void update();
		void clear();
		void sa(int x, int y, int col);
		void sz(int x, int y, char ch);
		void sza(int x, int y, char ch, int col);
		void ss(int x, int y, const string& str);
		void ssa(int x, int y, const string& str, int col);
		void print(const string& str);
		void hot(int x, int y, const string& str, int colnorm, int colinv);
	public:	// ---- Cursor-Functions ---------------------------------------------------------------------------------
		void cursor(CursorType mode);
		void setCursorPos(int x, int y);
		void getCursorPos(int& x, int& y);
	private:
		void showWins();
		void showFrame(bool refresh=true);
		void showTitle(bool refresh=true);

	private:
		const Vio& _vio;
		_win_st*	_window; //! Windw handle
		_win_st*	_content; //! Inner window due to frame
		int			_textAttr;
		FrameType	_frame;
		int			_frameAttr;
		string		_title;
		TitlePos	_titlePos;
		int			_titleAttr;
	};

}

using namespace OldScool;

#endif //OLDSCOOL_WIN_H
