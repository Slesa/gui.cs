#include <iostream>
#include <sstream>
#include "Mnu.h"

/*
void  MenuHelp() {
	VioSs( 1, 1, "Help was chosen" );
//  WinMeldung( 10, 10, "Dies ist eine Hilfe\nWeil das jeder will!.", MELDUNG_INFO );
}
*/

int main() {
	auto vio = new Vio();
	vio->clear();
	vio->doBackground();

	auto mnu = new Mnu(*vio);
	mnu->add( "  Eintrag ~1~                ", " ~F1~ Help ~F2~ Search | This is the first entry" );
	mnu->add( "  Eintrag ~2~  ", " ~F1~ Help ~F2~ Search | This is the second entry" );
	mnu->add( "  Eintrag ~3~  ", " ~F1~ Help ~F2~ Search | This is the third entry" );
	mnu->add( "  Eintrag ~4~  ", " ~F1~ Help ~F2~ Search | This is the fourth entry" );
	mnu->add( "  Eintrag ~5~  ", " ~F1~ Help ~F2~ Search | This is the fifth entry" );
	mnu->add( "  Eintrag ~6~  ", " ~F1~ Help ~F2~ Search | This is the sixth entry" );
	mnu->add( "  Eintrag ~7~  ", " ~F1~ Help ~F2~ Search | This is the seventh entry" );
	while( true )
	{
		auto ret = mnu->handle( " Your Choice " /*, MenuHelp*/ );
		if(ret<0) break;
		ostringstream oss;
		oss << "Choice " << ret;
		auto msg = oss.str();
		vio->ss( vio->getColumns()-msg.length()-2, 12, msg );
	}
	delete mnu;
	delete vio;
	return 0;
}