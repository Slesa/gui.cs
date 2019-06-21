#include <iostream>
#include "EvProcess.h"

namespace OldScool {

	EvProcess::EvProcess(int delay)
	: _delay(delay)
	{

	}

	bool EvProcess::shouldRun() {
		auto current = chrono::system_clock::now();
		int msecs = chrono::duration_cast<chrono::milliseconds>(current-_startTime).count();
		//std::cout << "msecs " << msecs << std::endl;
		if(msecs>_delay) {
			//std::cout << "resetting" << std::endl;
			reset();
			return true;
		}
		return false;
	}

	void EvProcess::reset() {
		_startTime = chrono::system_clock::now();
	}

}