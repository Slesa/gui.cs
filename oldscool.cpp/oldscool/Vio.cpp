#include "Vio.h"
 #include <ncurses.h>

namespace OldScool {

	Vio::Vio()
	{
		debug("-----------------------------------------------------------------------------------------------------");
		init();
	}

	Vio::~Vio()
	{
		done();
	}

	void Vio::init() {

	}

	void Vio::done() {
		endwin();
	}

}