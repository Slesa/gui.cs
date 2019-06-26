#include <ncursesw/ncurses.h>
#include "EvKeyboard.h"

namespace OldScool {

	void EvKeyboard::init() {
		cbreak();
		keypad(stdscr, true);
		// cbreak();
		// raw();
		notimeout(stdscr, true);
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