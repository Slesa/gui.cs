#ifndef OLDSCOOL_FRAME_H
#define OLDSCOOL_FRAME_H
#include "ncursesw/ncurses.h"

namespace OldScool {

	enum FrameType
	{
		None,
		Single,
		Double,
		Thick,
		Block
	};

	// https://jrgraphix.net/r/Unicode/2500-257F BoxDrawing
	class Frame {
	public:
		FrameType getType() const { return _type; }
		static Frame* createFrame(FrameType type);
		virtual ~Frame() {}
		virtual chtype hLine() = 0;
		virtual chtype vLine() = 0;
		virtual chtype topLeft() = 0;
		virtual chtype topRight() = 0;
		virtual chtype bottomLeft() = 0;
		virtual chtype bottomRight() = 0;
	protected:
		Frame(FrameType type) :_type(type) {}
	private:
		FrameType _type;
	};

	class NoFrame : public Frame {
	public:
		explicit NoFrame() : Frame(None) {}
		virtual	chtype hLine() { return ' '; }
		virtual chtype vLine() { return ' '; }
		virtual chtype topLeft() { return ' '; }
		virtual chtype topRight() { return ' ';  }
		virtual chtype bottomLeft() { return ' '; }
		virtual chtype bottomRight() { return ' '; }
	};

	class SingleFrame : public Frame {
	public:
		explicit SingleFrame() : Frame(Single) {}
		virtual	chtype hLine() { return ACS_HLINE; } // return u'\u2500'; }
		virtual chtype vLine() { return ACS_VLINE; } // return u'\u2502'; }
		virtual chtype topLeft() { return ACS_ULCORNER; }  // return u'\u250C'; }
		virtual chtype topRight() { return ACS_URCORNER; } // return u'\u2510';  }
		virtual chtype bottomLeft() { return ACS_LLCORNER; } // return u'\u2514'; }
		virtual chtype bottomRight() { return ACS_LRCORNER; } // return u'\u2518'; }
	};

	class DoubleFrame : public Frame {
	public:
		explicit DoubleFrame() : Frame(Double) {}
		virtual	chtype hLine() { return u'\u2550'; }
		virtual	chtype vLine() { return u'\u2551'; }
		virtual	chtype topLeft() { return u'\u2554'; }
		virtual	chtype topRight() { return u'\u2557'; }
		virtual	chtype bottomLeft() { return u'\u255A'; }
		virtual	chtype bottomRight() { return u'\u255D'; }
	};

	class ThickFrame : public Frame {
	public:
		explicit ThickFrame() : Frame(Thick) {}
		virtual	chtype hLine() { return u'\u2501'; }
		virtual	chtype vLine() { return u'\u2503'; }
		virtual	chtype topLeft() { return u'\u250F'; }
		virtual	chtype topRight() { return u'\u2513'; }
		virtual	chtype bottomLeft() { return u'\u2517'; }
		virtual	chtype bottomRight() { return u'\u251B'; }
	};

	class BlockFrame : public Frame {
	public:
		explicit BlockFrame() : Frame(Block) {}
		virtual	chtype hLine() { return u'\u2587'; }
		virtual	chtype vLine() { return u'\u2587'; }
		virtual	chtype topLeft() { return u'\u2587'; }
		virtual	chtype topRight() { return u'\u2587'; }
		virtual	chtype bottomLeft() { return u'\u2587'; }
		virtual	chtype bottomRight() { return u'\u2587'; }
	};

}

using namespace OldScool;

#endif //OLDSCOOL_FRAME_H
