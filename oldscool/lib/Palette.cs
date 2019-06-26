using System;
using System.Collections.Generic;

namespace OldScool 
{
	// Colors of element's parts
	public enum AttribRole
	{
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
	}

	public interface IPalette
	{
		int Get(AttribRole role);
	}

	public class Palette : IPalette
	{
		public Palette()
		{
			Configure();
		}

		public int GetBackground() {
			return Get(AttribRole.Background);
		}
		
		public int Get(AttribRole role) {
			return _colors[role];
		}

		Dictionary<AttribRole, int> _colors;
		void Configure() {
			_colors = new Dictionary<AttribRole, int>();
			_colors[AttribRole.Background] = Colors.MakeAttr(Color.Black, Color.Cyan);
			_colors[AttribRole.WinFrame] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttribRole.WinTitle] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttribRole.WinText] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttribRole.WinShadow] = Colors.MakeAttr(Color.Gray, Color.Cyan);
			_colors[AttribRole.MsgFrame] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttribRole.MsgTitle] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttribRole.MsgText] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttribRole.MsgStatusLine] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttribRole.LstFrame] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttribRole.LstTitle] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttribRole.LstText] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttribRole.LstInvers] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttribRole.LstStatusLine] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttribRole.LstStatusInvers] = Colors.MakeAttr(Color.Red, Color.White);
			_colors[AttribRole.MnuFrame] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttribRole.MnuTitle] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttribRole.MnuText] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttribRole.MnuInvers] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttribRole.MnuHotkey] = Colors.MakeAttr(Color.Red, Color.White);
			_colors[AttribRole.MnuHotInvers] = Colors.MakeAttr(Color.Red, Color.Blue);
			_colors[AttribRole.MnuStatusLine] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttribRole.MnuStatusInvers] = Colors.MakeAttr(Color.Red, Color.White);
			_colors[AttribRole.DlgFrame] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttribRole.DlgTitle] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttribRole.DlgText] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttribRole.DlgInvers] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttribRole.DlgHotkey] = Colors.MakeAttr(Color.Red, Color.White);
			_colors[AttribRole.DlgHotInvers] = Colors.MakeAttr(Color.Red, Color.Blue);
			_colors[AttribRole.DlgStatusLine] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttribRole.DlgStatusInvers] = Colors.MakeAttr(Color.Red, Color.White);
			_colors[AttribRole.DlgEdit] = Colors.MakeAttr(Color.Black, Color.Cyan);
			_colors[AttribRole.DlgEditMark] = Colors.MakeAttr(Color.BrightYellow, Color.Blue);
		}
	}
}
