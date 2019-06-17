#ifndef OLDSCOOL_PALETTE_H
#define OLDSCOOL_PALETTE_H
#include <map>

namespace OldScool {

	enum AttribRole {
		Background = 1,
		Shadow,
		WinFrame,
		WinTitle,
		WinText,
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


	class Palette {

	public:
		// Attrib Map is to declare light colours
		typedef std::map<AttribRole, int> AttribMap;

	public:
		Palette();
		void configure();

	public:
		int getBackground() const;
		int get(int index) const;

	private:
		int getAttrib(AttribRole index) const;
		int getAttrib(int index) const { return getAttrib((AttribRole)index); }
	private:
		AttribMap _attribMap;
	};

}

using namespace OldScool;

#endif //OLDSCOOL_PALETTE_H
