#ifndef OLDSCOOL_EVENTS_H
#define OLDSCOOL_EVENTS_H
#include <vector>

using namespace std;

namespace OldScool {

	class EvProcess;
	class EvKeyboard;
	class EvMouse;


	class Events {
	public:
		Events();
		~Events();
		void init();
		void done();
		int getKey();
		//void hideMouse(bool off);
		void waitForKey(bool wait) { _waitForkey = wait; }
		void addMulti(EvProcess *proc);
		void delMulti(EvProcess *proc);

	private:
		void doMulti();

	private:
		EvKeyboard* _keyboard;
		EvMouse* _mouse;
		vector<EvProcess*> _processes;
		bool _waitForkey;
	};
}

using namespace OldScool;

#endif
