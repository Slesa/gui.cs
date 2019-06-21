#ifndef OLDSCOOL_EVPROCESS_H
#define OLDSCOOL_EVPROCESS_H
#include <chrono>

using namespace std;

namespace OldScool {
	class EvProcess {

	public:
		bool shouldRun();
		virtual void run() = 0;

	protected:
		EvProcess(int delay);
		virtual ~EvProcess() {}

	private:
		void reset();

	private:
		int _delay;
		chrono::time_point<chrono::system_clock> _startTime;
	};

}

using namespace OldScool;

#endif //OLDSCOOL_EVPROCESS_H
