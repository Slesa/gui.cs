#include <ncursesw/ncurses.h>
#include "EvMouse.h"

namespace OldScool {

	// https://invisible-island.net/ncurses/man/curs_mouse.3x.html
	void EvMouse::init() {
		mousemask(ALL_MOUSE_EVENTS, NULL);

	}

	void EvMouse::done() {

	}

	int EvMouse::getKey() {

	}
//int getState();
}