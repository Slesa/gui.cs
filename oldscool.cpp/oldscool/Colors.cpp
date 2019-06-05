#include "Colors.h"
#include <curses.h>

namespace OldScool {

	Colors::Colors() {

	}

	void Colors::configure() {
		init_pair(Background, COLOR_BLACK, COLOR_CYAN); _attribMap[Background] = A_BOLD;
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

	int Colors::getBackground() {
		return COLOR_PAIR(Background)|_attribMap[Background]/*|ACS_BOARD*/;
	}

	int getColor(int index) {
		return COLOR_PAIR(index); //|_attribMap[index]/*|ACS_BOARD*/;
	}

}