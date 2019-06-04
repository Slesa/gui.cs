#ifndef OLDSCOOL_WIN_H
#define OLDSCOOL_WIN_H

#include <string>
#include "Vio.h"

using namespace std;

namespace OldScool {

	enum TitlePosition
	{
		TopLeft,
		TopCenter,
		TopRight,
		BottomLeft,
		BottomCenter,
		BottomRight
	};


	enum FrameType
	{
		FrameNone,
		FrameSingle,
		FrameDouble,
		FrameThick
	};

	enum CursorType
	{
		CurOff,
		CurLine,
		CurBlock
	};

	class Win {

	public:	// ---- ctor/dtor --------------------------------------------------------------------------------------
		Win(const Vio& vio, int x, int y, int width, int height, string name);
		~Win();

	private:
		const Vio& _vio;

	};

}

using namespace OldScool;

#endif //OLDSCOOL_WIN_H
