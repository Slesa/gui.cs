#include <iostream>
#include <ncursesw/ncurses.h>
#include <sstream>
#include "Events.h"
#include "EvProcess.h"

class MyProcessBase : public EvProcess {

public:
	MyProcessBase(int x, int y) : EvProcess(1000), _x(x), _y(y), _state(0) {}
	void run() {
		mvaddch(_y, _x, _states[_state++]);
		if(_state>3) _state = 0;
		::refresh();
	}
private:
	int _x, _y;
	int _state;
	char _states[4]     = { '\\', '|', '/', '-' };
};

class MyProcess1 : public MyProcessBase {
public:
	MyProcess1() : MyProcessBase(1, 1) {}
};
class MyProcess2 : public MyProcessBase {
public:
	MyProcess2() : MyProcessBase(4, 1) {}
};
class MyProcess3 : public MyProcessBase {
public:
	MyProcess3() : MyProcessBase(7, 1) {}
};

int main() {

	setlocale(LC_ALL,"");
	initscr();
	noecho();
	cbreak();
	curs_set(0);
	//keypad(stdscr, true);
	notimeout(stdscr, true);
	mousemask(ALL_MOUSE_EVENTS|REPORT_MOUSE_POSITION, NULL);

	auto events = new Events();
	events->init();
	events->addMulti(new MyProcess1());
	events->addMulti(new MyProcess2());
	events->addMulti(new MyProcess3());


	while(true) {
		auto key = events->getKey();
		if(key<0) break;

		ostringstream oss;
		oss << "key " << key << ", name " << key_name(key) << std::endl;
		mvaddstr(1, 10, oss.str().c_str());
	}

	endwin();
	return 0;
}
