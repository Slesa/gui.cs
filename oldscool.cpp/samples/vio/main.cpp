#include <iostream>
#include <sstream>
#include <ncurses.h>
#include "Vio.h"
#include "Colors.h"

int main() {

	#define LABEL_POS 21
    #define VALUE_POS 51

	auto vio = new Vio();
	vio->clear();
	vio->doBackground();

	auto done = false;
	std::ostringstream ss;
	while( !done ) {

		vio->status( " ~F1~ Please Select a driver  ~F2~ Искать", ColorRole::LstStatusLine, ColorRole::LstStatusInvers );

		ss << vio->getColumns();
		vio->ss( LABEL_POS, 2, "Columns.....:");
		vio->ssa( VALUE_POS, 2, ss.str(), ColorRole::MsgFrame);

		ss << vio->getRows();
		vio->ss( LABEL_POS, 3, "Rows........:");
		vio->ssa( VALUE_POS, 3, ss.str(), ColorRole::MsgFrame);

		vio->ss( LABEL_POS, 4, "Can color...:" );
		vio->ssa( VALUE_POS, 4, vio->canColors() ? "Yes" : "No", ColorRole::MsgFrame);

		auto ret = getch();
		if(ret==27) done = true;
	}

	delete vio;

    return 0;
}