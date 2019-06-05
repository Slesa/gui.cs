#ifndef OLDSCOOL_COLORS_H
#define OLDSCOOL_COLORS_H
#include <map>

namespace OldScool {

	enum ColorRole {
		Background,
		MsgFrame,
		MsgTitle,
		MsgText,
		MsgStatusLine,
		LstFrame,
		LstTitle,
		LstText,
		LstInvers,
		LstStatusLine,
		LstStatusInvers,
		MnuFrame,
		MnuTitle,
		MnuText,
		MnuInvers,
		MnuHotkey,
		MnuHotInvers,
		MnuStatusLine,
		MnuStatusInvers,
	};

	class Colors {

	public:
		typedef std::map<ColorRole, int> AttribMap;

	public:
		Colors();
		void configure();

	public:
		int getBackground();
		int getColor(int index);

	private:
		AttribMap _attribMap;
	};

}

using namespace OldScool;

#endif //OLDSCOOL_COLORS_H
