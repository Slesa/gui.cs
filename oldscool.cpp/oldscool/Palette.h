#ifndef OLDSCOOL_PALETTE_H
#define OLDSCOOL_PALETTE_H
#include <map>

namespace OldScool {

	enum AttribRole {
		Background = 1,             // Background of the program
		WinFrame,                   // Windows, frame
		WinTitle,                   // Windows, title
		WinText,                    // Windows, text
		WinShadow,                  // Windows, shadow
		MsgFrame,                   // Messages, frame
		MsgTitle,                   // Messages, title
		MsgText,                    // Messages, text
		MsgStatusLine,              // Messages, text in status line
		LstFrame,                   // Lists, frame
		LstTitle,                   // Lists, title
		LstText,                    // Lists, text
		LstInvers,                  // Lists, highlighted text
		LstStatusLine,              // Lists, text in status line
		LstStatusInvers,            // Lists, highlighted text in status line
		MnuFrame,                   // Menus, frame
		MnuTitle,                   // Menus, title
		MnuText,                    // Menus, text
		MnuInvers,                  // Menus, highlighted text
		MnuHotkey,                  // Menus, hotkey text
		MnuHotInvers,               // Menus, highlighted hotkey text
		MnuStatusLine,              // Menus, text in status line
		MnuStatusInvers,            // Menus, highlighted text in status line
		DlgFrame,                   // Dialogs, frame
		DlgTitle,                   // Dialogs, title
		DlgText,                    // Dialogs, text
		DlgInvers,                  // Dialogs, highlighted text
		DlgHotkey,                  // Dialogs, hotkeys
		DlgHotInvers,               // Dialogs, highlighted hotkeys
		DlgStatusLine,              // Dialogs, status line
		DlgStatusInvers,            // Dialogs, highlighted text in status line
		DlgEdit,                    // Dialogs, editable text
		DlgEditMark,                // Dialogs, marked editable text
// #define        DLG_PAL_DISABLE     8            /* Disableter Eintrag      */
// #define        DLG_PAL_PUSHDIS     9            /* Push-Button, disabled   */
// #define        DLG_PAL_PUSHNORMAL  10
// #define        DLG_PAL_PUSHINVERS  11
// #define        DLG_PAL_PUSHHOTNORM 12
// #define        DLG_PAL_PUSHHOTINV  13
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
