#include <algorithm>
#include <ncursesw/ncurses.h>
#include "Events.h"
#include "EvProcess.h"
#include "EvKeyboard.h"
#include "EvMouse.h"

namespace OldScool {

	Events::Events()
	: _keyboard(nullptr)
	, _mouse(nullptr)
	, _waitForkey(true)
	{
	}

	Events::~Events() {
		done();
	}

	void Events::init() {
		_keyboard = new EvKeyboard();
		_keyboard->init();
		_mouse = new EvMouse();
		_mouse->init();
//		EvSchonInit();
//		EvSpoolInit();
	}

	void Events::done() {
		if( _keyboard!=nullptr ) {
			_keyboard->done();
			delete _keyboard;
		}
		if(_mouse!=nullptr) {
			_mouse->done();
			delete _mouse;
		}
//		EvSchonDone();
//		EvSpoolDone();
	}

	int Events::getKey()
	{
		bool done = false;
		while( !done )
		{
			doMulti();
			auto key = _keyboard->getKey();
			if( key>=0 ) return key;
//			if( EvKeybGetch( &iRet ) ) { EvSchonReset(); break; }
//			if( !boHideMouse )
//				if( EvMouGetch( &iRet ) )  { EvSchonReset(); break; }
//			if( EvSchoner() ) return( 0 );
			if( !_waitForkey )
				break;
		}
		return -1;
	}
/*
	VOID CDECL     EvHideMouse   ( BOOL flag )
{
	boHideMouse = flag;
}
*/

	void Events::addMulti(EvProcess *proc) {
		_processes.push_back(proc);
	}

	void Events::delMulti(EvProcess *proc) {
		auto pos = std::find(_processes.begin(), _processes.end(), proc);
		if (pos!=_processes.end())
			_processes.erase(pos);
	}

	void Events::doMulti()
	{
		for(auto proc: _processes) {
			if(proc->shouldRun())
				proc->run();
		}
	}

}