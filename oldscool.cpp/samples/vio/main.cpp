#include <iostream>
#include "oldscool/Vio.h"
#include "oldscool/Colors.h"

int main() {

	#define INFO_POS 61

	auto vio = new Vio();
	vio->Clear();

	auto done = false;
	while( !done ) {

		vio->Status( " ~F1~ Please Select a driver", ColorRole::, 115 );
		sprintf( cTemp, "Columns.....: %d", vio->getColumns() );
		vio->Ss( INFO_POS, 2, cTemp );
		sprintf( cTemp, "Rows........: %d", vio->getRows() );
		vio->Ss( INFO_POS, 3, cTemp );
		sprintf( cTemp, "Can color...: %s", vio->canColor()() ? "Yes" : "No" );
		vio->Ss( INFO_POS, 4, cTemp );

		auto ret = getkey();
		if(ret==27) done = true;
	}

	vio->Done();

    return 0;
}