#include "Palette.h"
#include <ncursesw/ncurses.h>

namespace OldScool {

	Palette::Palette() {

	}

	void Palette::configure() {
		init_pair(Background, COLOR_CYAN, COLOR_BLACK); _attribMap[Background] = A_BOLD;
		// Windows
		init_pair(WinFrame, COLOR_WHITE, COLOR_BLUE);
		init_pair(WinTitle, COLOR_WHITE, COLOR_BLUE);
		init_pair(WinText, COLOR_WHITE, COLOR_BLUE);
		// Message Windows
		init_pair(MsgFrame, COLOR_WHITE, COLOR_BLUE);
		init_pair(MsgTitle, COLOR_WHITE, COLOR_BLUE);
		init_pair(MsgText, COLOR_WHITE, COLOR_BLUE);
		init_pair(MsgStatusLine, COLOR_BLACK, COLOR_WHITE);
		// List Windows
		init_pair(LstFrame, COLOR_BLACK, COLOR_WHITE);
		init_pair(LstTitle, COLOR_BLACK, COLOR_WHITE); // _attribMap[LstTitle] = A_BOLD;
		init_pair(LstText, COLOR_BLACK, COLOR_WHITE);
		init_pair(LstInvers, COLOR_WHITE, COLOR_BLUE); _attribMap[LstInvers] = A_BOLD;
		init_pair(LstStatusLine, COLOR_BLACK, COLOR_WHITE);
		init_pair(LstStatusInvers, COLOR_RED, COLOR_WHITE);
		// Menu Windows
		init_pair(MnuFrame, COLOR_BLACK, COLOR_WHITE);
		init_pair(MnuTitle, COLOR_BLACK, COLOR_WHITE);
		init_pair(MnuText, COLOR_BLACK, COLOR_WHITE);
		init_pair(MnuInvers, COLOR_WHITE, COLOR_BLUE); _attribMap[MnuInvers] = A_BOLD;
		init_pair(MnuHotkey, COLOR_RED, COLOR_WHITE);
		init_pair(MnuHotInvers, COLOR_RED, COLOR_BLUE); _attribMap[MnuHotInvers] = A_BOLD;
		init_pair(MnuStatusLine, COLOR_BLACK, COLOR_WHITE);
		init_pair(MnuStatusInvers, COLOR_RED, COLOR_WHITE);

	}

	int Palette::getBackground() const {
		return COLOR_PAIR(Background) | getAttrib(Background);
	}

	int Palette::get(int index) const {
		return COLOR_PAIR(index) | getAttrib(index);
	}

	int Palette::getAttrib(AttribRole index) const {
		auto it = _attribMap.find(index);
		if( it!=_attribMap.end() )
			return (*it).second;
		return 0;
	}
}