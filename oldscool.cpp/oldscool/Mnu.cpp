#include <sys/param.h>
#include <algorithm>
#include <sstream>
#include "Mnu.h"
#include "ncursesw/ncurses.h"

namespace OldScool {
	MnuEntry::MnuEntry()
	: _x(0)
	, _y(0)
	, _hotkey(0)
	{
	}

	MnuEntry::MnuEntry(int x, int y, const string &text, const string& status, int hotkey)
	: _x(x)
	, _y(y)
	, _hotkey(hotkey)
	, _text(text)
	, _status(status)
	{
	}


	Mnu::Mnu(Vio &vio)
	: _vio(vio)
	, _current(0)
	, _maxWidth(0)
	, _maxHeight(0)
	, _win(nullptr)
	{
	}

	Mnu::~Mnu() {
		for(auto entry : _entries)
			delete entry;
		if (_win)
			delete _win;
	}

	void Mnu::add(const string& text, const string& status, int hotkey) {
		if( hotkey==0 ) hotkey = Win::getHotkey(text);
		add(1, _entries.size()+1, text, status, hotkey);
	}

	void Mnu::add(int x, int y, const string& text, const string& status, int hotkey)
	{
		MnuEntry* entry = new MnuEntry(x, y, text, status, hotkey);
		add(entry);
	}

	void Mnu::add(MnuEntry* entry)
	{
		_maxWidth = MAX(entry->getX()+entry->getText().length(), _maxWidth);
		_maxHeight = MAX(entry->getY(), _maxHeight);
//		Vio::debug(string("maxw=%1 | %2, maxh=%3 | %4")
//							.arg(entry->m_X+entry->m_Text.length()).arg(m_MaxWidth).arg(entry->m_Y).arg(m_MaxHeight));
		_entries.push_back(entry);
	}

	int Mnu::handle(const string& title, const vector<int>& exitCodes) {
		return handle(title, _vio.centerCol(_maxWidth+2), _vio.centerRow(+_maxHeight+2));
	}

	int Mnu::handle(const string& title, int x, int y, const vector<int>& exitCodes)
	{
		if( !_entries.size() )
			return -1;

		auto win = getWindow(x, y, title);

		auto ret = 0;
		auto done = false;
		while( !done ) {
			auto entry = _entries[_current];
			win->hot(entry->getX(), entry->getY(), entry->getText(), AttribRole::MnuInvers, AttribRole::MnuHotInvers);
			//if (entry->getStatus().length())
			//	_vio.status(entry->getStatus(), AttribRole::MnuStatusLine, AttribRole::MnuStatusInvers);

			//win->update();
			ret = getch(); // TODO: evGetKey()

			ostringstream oss;
			oss << "Key is " << key_name(ret);
			_vio.status(oss.str(), AttribRole::MnuStatusLine, AttribRole::MnuStatusInvers);

			if (std::find(exitCodes.begin(), exitCodes.end(), ret) != exitCodes.end())
				return ret;
AGAIN:
			switch (ret) {
				case KEY_UP:
					win->hot(entry->getX(), entry->getY(), entry->getText(), AttribRole::MnuText, AttribRole::MnuHotkey);
					if (--_current < 0) _current = _entries.size() - 1;
					break;
				case KEY_DOWN:
					win->hot(entry->getX(), entry->getY(), entry->getText(), AttribRole::MnuText, AttribRole::MnuHotkey);
					if (++_current >= _entries.size()) _current = 0;
					break;
				case 27:
					nodelay(stdscr, true);
					ret = getch();
					oss << "Key is now " << key_name(ret);
					_vio.status(oss.str(), AttribRole::MnuStatusLine, AttribRole::MnuStatusInvers);
					nodelay(stdscr, false);
					if( ret==ERR )
						return -1;
					goto AGAIN;
				case '\n':
				case KEY_ENTER:
					// case KEY_RETURN:
					// 	return _current;
				case KEY_F(9):
					done = true;
					break;
				default: {
					auto index = 0;
					for (auto newEntry: _entries) {
						if (newEntry->getHotkey() == ret) {
							win->hot(entry->getX(), entry->getY(), entry->getText(), AttribRole::MnuText, AttribRole::MnuHotkey);
							_current = index;
							win->hot(newEntry->getX(), newEntry->getY(), newEntry->getText(), AttribRole::MnuInvers, AttribRole::MnuHotInvers);
							done = true;
							break;
						}
						index++;
					}
				}
			}
		}
		return _current;
	}

	Win* Mnu::getWindow(int x, int y, const string& title) {
		if( !_win ) {
			auto height = _maxHeight+2;
			auto width = _maxWidth+2;
			_win = new Win(_vio, x, y, width, height);
			_win->cursor(CurOff);
			_win->setBackground(AttribRole::MnuText);
			_win->setFrame(FrameType::Single, AttribRole::MnuFrame);
			_win->setTitle(title, TitlePos::TopCenter, AttribRole::MnuTitle);
			_win->clear();
			_maxWidth = MAX(_maxWidth, title.length()+2);

			for(auto entry: _entries) {
				_win->hot(entry->getX(), entry->getY(), entry->getText(), AttribRole::MnuText, AttribRole::MnuHotkey);
			}
			_win->show();
		}
		return _win;
	}


}