#include <list>
#include <algorithm>
#include <ncursesw/ncurses.h>
#include <cassert>
#include "Win.h"
#include "Palette.h"

namespace OldScool {

	std::list<Win*> _windows;

	Win::Win(const Vio& vio, int x, int y, int width, int height)
	: _vio(vio)
	, _window(0)
	, _content(0)
	, _textAttr(AttribRole::WinText)
	, _frame(FrameType::None)
	, _frameAttr(AttribRole::WinFrame)
	, _titlePos(TitlePos::TopCenter)
	, _titleAttr(AttribRole::WinTitle)
	{
		assert( x>0 );
		assert( y>0 );
		if( x+width>vio.getColumns()-1 ) width = vio.getColumns()-1;
		if( y+height>vio.getRows()-1 ) height = vio.getRows()-1;
		_window = newwin(height+2, width+2, y, x);
		_content = subwin(_window, height, width, y+1, x+1);
		clear();
	}

	Win::~Win() {
		if( _content )
			delwin(_content);
		if( _window )
			delwin(_window);
		hide();
	}

	int Win::getX()
	{
		return _content->_begx;
	}

	int Win::getY()
	{
		return _content->_begy;
	}

	int Win::getWidth()
	{
		return _content->_maxx;
	}

	int Win::getHeight()
	{
		return _content->_maxy;
	}

	bool Win::isVisible() {
		bool ret = (std::find(_windows.begin(), _windows.end(), this) != _windows.end());
		// TVio::debug(QString("TWin::isVisible %1: %2").arg(objectName()).arg(ret));
		return ret;
	}

	void Win::setFrame(FrameType frame, int attr) {
		_frame = frame;
		_frameAttr = attr;
		showFrame( isVisible() );
		showTitle( isVisible() );
	}

	void Win::setTitle(const string& title, TitlePos pos, int attr) {
		_title = title;
		_titlePos = pos;
		_titleAttr = attr;
		showFrame( isVisible() );
		showTitle( isVisible() );
	}

	void Win::show()
	{
		// TVio::debug(QString("TWin::show, showing %1").arg(objectName()));
		_windows.push_back(this);
		update();
//		showWins();
	}

	void Win::hide()
	{
		// TVio::debug(QString("TWin::hide, hiding %1").arg(objectName()));
		::touchwin(stdscr);
		::refresh();
		_windows.remove(this);
		showWins();
/*
		if( m_Parent && m_Parent->m_Win ) {
//			TVio::debug("TWin::hide, touching parent");
			::touchwin(m_Parent->m_Win);
			::wrefresh(m_Parent->m_Win);
		} else {
//			TVio::debug("TWin::hide, touching screen");
			::touchwin(stdscr);
			::refresh();
		}
		m_Hidden = true;
*/
	}

	void Win::clear()
	{
		//::wattrset(_window, _vio.getPalette().get(_textAttr));
		//::wclear(_window);
		// ::wrefresh(_window);
		::wbkgdset(_content, _vio.getPalette().get(_textAttr));
		//::wattrset(_content, _vio.getPalette().get(_textAttr));
		::wclear(_content);
		//if( isVisible() ) {
		//	::wrefresh(_content);
		//}
		showFrame( isVisible() );
	}

	void Win::print(const string& str)
	{
//		wattrset(m_Sub, m_Win->);
		::waddstr(_content, str.c_str());
        if( isVisible() ) ::wrefresh(_content);
	}

    void Win::sz(int x, int y, char ch)
    {
        mvwaddch(_content, y, x, ch);
        if( isVisible() ) ::wrefresh(_content);
    }

    void Win::sza(int x, int y, char ch, int col)
    {
        // ::wattrset(_content, _vio.getPalette().get(col));
        // wmove(_Sub, y, x);
        ::wattron(_content, col);
        mvwaddch(_content, y, x, ch);
        ::wattroff(_content, col);
        if( isVisible() ) ::wrefresh(_content);
    }

    void Win::ss(int x, int y, const string& str)
    {
        mvwaddstr(_content, y, x, str.c_str());
        if( isVisible() ) ::wrefresh(_content);
    }

    void Win::ssa(int x, int y, const string& str, int col)
    {
        // ::wattrset(_content, _vio.getPalette().get(col));
        // :: wattron(_content, _vio.getPalette().get(col));
        ::wattron(_content, col);
        // wmove(m_Sub, y, x);
        // color_set(_vio.getPalette().get(col), 0);
        //::color_set(col, 0);
        mvwaddstr(_content, y, x, str.c_str());
        ::wattroff(_content, col);
        if( isVisible() ) ::wrefresh(_content);
        // wattroff(m_Sub, m_Vio.getColors().getUser(col));
    }

	void Win::showWins()
	{
		for(auto const& win : _windows)
			win->update();
	}

	void Win::showFrame(bool refresh)
	{
		//::wattrset(_window, _vio.getPalette().get(_frameAttr));
		::wattron(_window, _frameAttr);
        // :: wborder(_window, '\u2502', '\u2502', '\u2500', '\u2500', '\u250C', '\u2510', '\u2514', '\u2518');
        // wborder(WINDOW *win, chtype ls, chtype rs, chtype ts, chtype bs, chtype tl, chtype tr, chtype bl, chtype br);
		::box(_window, 0, 0);
        ::wattroff(_window, _frameAttr);
		// ::wattroff(_window, _vio.getPalette().get(_frameAttr));
		if( _title.empty() )
			showTitle(false);
		if( refresh )
			wrefresh(_window);
	}

	void Win::showTitle(bool refresh)
	{
		int spalte = 2;
		int zeile = _titlePos&1 ? _window->_maxy : 0;
		switch( _titlePos )
		{
			default:
			case TitlePos::TopLeft:
			case TitlePos::BottomLeft:
				break;
			case TitlePos::TopRight:
			case TitlePos::BottomRight:
				spalte = _window->_maxx-_title.length()-1;
				break;
			case TitlePos::TopCenter:
			case TitlePos::BottomCenter:
				spalte = (_window->_maxx-_title.length())/2;
				break;
		}
		// Achtung, sa benutzt m_Sub, nicht m_Win!
		::wmove(_window, zeile, spalte);
		::wattrset(_window, _vio.getPalette().get(_titleAttr));
		::waddstr(_window, _title.c_str());
		//::wattroff(_window, _vio.getPalette().get(_titleAttr));
		if( refresh )
			::wrefresh(_window);
/* PDCurses
  if( boMouse ) MouShow();
*/
	}

	void Win::update()
	{
		// TVio::debug(QString("TWin::update, updating %1").arg(objectName()));
		::touchwin(_window);
		::wrefresh(_window);
		::touchwin(_content);
		::wrefresh(_content);
	}

}