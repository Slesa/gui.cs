#include <ncursesw/ncurses.h>
#include "EvKeyboard.h"

namespace OldScool {

	void EvKeyboard::init() {
		keypad(stdscr, false);
		nodelay(stdscr, true);
	}

	void EvKeyboard::done() {

	}

	int EvKeyboard::getKey() {
		auto key = wgetch(stdscr);
		return( key==ERR) ? -1 : key;
	}

	int EvKeyboard::getState() {

	}

}