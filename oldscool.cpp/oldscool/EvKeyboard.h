#ifndef OLDSCOOL_EVKEYBOARD_H
#define OLDSCOOL_EVKEYBOARD_H

namespace OldScool {

	class EvKeyboard {
	public:
		void init();
		void done();
		int getKey();
		int getState();
	};

}

using namespace OldScool;

#endif //OLDSCOOL_EVKEYBOARD_H
