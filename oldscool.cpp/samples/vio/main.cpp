#include <iostream>
#include <sstream>
#include <ncurses.h>
#include "Vio.h"
#include "Palette.h"

#include <iostream>
#include <fstream>

int main() {

	#define LABEL_POS 21
    #define VALUE_POS 51

	setlocale(LC_ALL, "");

	/* string line;
	ifstream myfile ("txt.txt");
	if (myfile.is_open())
	{
		while ( getline (myfile,line) )
		{
			cout << line << '\n';
		}
		myfile.close();
	} */
	//while(true) { auto tmp = getch(); if(tmp==27) break; }

	auto vio = new Vio();
	vio->clear();
	vio->doBackground();

	auto done = false;
	while( !done ) {

		//vio->status( line, AttribRole::LstStatusLine, AttribRole::LstStatusInvers );
		vio->status( " ~F1~ Please Select a driver  ~F2~ Искать \uE030", AttribRole::LstStatusLine, AttribRole::LstStatusInvers );

		std::ostringstream ssc;
		ssc << vio->getColumns();
		vio->ss( LABEL_POS, 2, "Columns.....:");
		vio->ssa( VALUE_POS, 2, ssc.str(), AttribRole::MsgFrame);

		std::ostringstream ssr;
		ssr << vio->getRows();
		vio->ss( LABEL_POS, 3, "Rows........:");
		vio->ssa( VALUE_POS, 3, ssr.str(), AttribRole::MsgFrame);

		vio->ss( LABEL_POS, 4, "Can color...:" );
		vio->ssa( VALUE_POS, 4, vio->canColors() ? "Да" : "Нет", AttribRole::MsgFrame);

		auto ret = getch();
		if(ret==27) done = true;
	}

	delete vio;

    return 0;
}