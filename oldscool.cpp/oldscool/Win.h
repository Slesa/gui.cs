#ifndef OLDSCOOL_WIN_H
#define OLDSCOOL_WIN_H

#include <string>
#include "Vio.h"
#include "Frame.h"

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
		void setFrame(FrameType frame, AttribRole attribRole);
		void setTitle(const string& title, TitlePos pos, AttribRole attribRole);
		void show();
		void hide();
		void setBackground(AttribRole attribRole);
		void update();
		void clear();
		void sz(int x, int y, char ch);
		void sza(int x, int y, char ch, AttribRole attribRole);
		void ss(int x, int y, const string& str);
		void ssa(int x, int y, const string& str, AttribRole attribRole);
		void print(const string& str);
		void hot(int x, int y, const string& str, AttribRole normRole, AttribRole invRole);
		static int getHotkey(const string& text);
	public:	// ---- Cursor-Functions ---------------------------------------------------------------------------------
		void cursor(CursorType mode);
		void setCursorPos(int x, int y);
		void getCursorPos(int& x, int& y);
	private:
		void showWins();
		Frame* createFrame(FrameType type);
		void showFrame(bool refresh=true);
		void showTitle(bool refresh=true);

	private:
		const Vio& _vio;
		_win_st*	_window; //! Windw handle
		_win_st*	_content; //! Inner window due to frame
		AttribRole	_textAttr;
		AttribRole	_frameAttr;
		Frame*		_frame;
		string		_title;
		TitlePos	_titlePos;
		AttribRole	_titleAttr;
	};

}

using namespace OldScool;

#endif //OLDSCOOL_WIN_H
