#ifndef OLDSCOOL_EVMOUSE_H
#define OLDSCOOL_EVMOUSE_H

namespace OldScool {

	class EvMouse {
	public:
		void init();
		void done();
		int getKey();
		//int getState();
	};
}

using namespace OldScool;


#endif //OLDSCOOL_EVMOUSE_H
