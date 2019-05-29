using System;
using System.Collections.Generic;

namespace OldSchool 
{
	// Colors of element's parts
	public enum AttributeRole
	{
		Background,                 // Background of the program
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
		int Get(AttributeRole role);
	}

	public class Palette : IPalette
	{
		private Palette()
		{
			CreatePalette();
		}

		public int Get(AttributeRole role) {
			return _colors[role];
		}

		static Palette _instance;
		public static Palette Instance { get {
			if( _instance==null ) _instance = new Palette();
			return _instance;
		} }

		Dictionary<AttributeRole, int> _colors;
		void CreatePalette() {
			_colors = new Dictionary<AttributeRole, int>();
			_colors[AttributeRole.Background] = Colors.MakeAttr(Color.Black, Color.Cyan);
			_colors[AttributeRole.WinFrame] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttributeRole.WinTitle] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttributeRole.WinText] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttributeRole.WinShadow] = Colors.MakeAttr(Color.Gray, Color.Cyan);
			_colors[AttributeRole.MsgFrame] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttributeRole.MsgTitle] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttributeRole.MsgText] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttributeRole.MsgStatusLine] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttributeRole.LstFrame] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttributeRole.LstTitle] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttributeRole.LstText] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttributeRole.LstInvers] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttributeRole.LstStatusLine] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttributeRole.LstStatusInvers] = Colors.MakeAttr(Color.Red, Color.White);
			_colors[AttributeRole.MnuFrame] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttributeRole.MnuTitle] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttributeRole.MnuText] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttributeRole.MnuInvers] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttributeRole.MnuHotkey] = Colors.MakeAttr(Color.Red, Color.White);
			_colors[AttributeRole.MnuHotInvers] = Colors.MakeAttr(Color.Red, Color.Blue);
			_colors[AttributeRole.MnuStatusLine] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttributeRole.MnuStatusInvers] = Colors.MakeAttr(Color.Red, Color.White);
//                               , VH( SCHWARZ, WEISS )  /* Attribut eines nicht aktiven Eintrags */
//                               , VH( GRAU, GRUEN )     /* Attribut eines nicht aktiven Push-Buttons */
//                               , VH( ROT, GRUEN )      /* Attribut des Hotkeys eines Push-Buttons */
//                               , VH( WEISS, GRUEN )    /* Attribut eines aktiven Push-Buttons */
//                               , VH( GELB, BLAU )      /* Attribut eines aktiven Push-Buttons */
//                               , VH( HELLROT, BLAU )   /* Attribut des Hotkeys eines Push-Buttons */
			_colors[AttributeRole.DlgFrame] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttributeRole.DlgTitle] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttributeRole.DlgText] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttributeRole.DlgInvers] = Colors.MakeAttr(Color.White, Color.Blue);
			_colors[AttributeRole.DlgHotkey] = Colors.MakeAttr(Color.Red, Color.White);
			_colors[AttributeRole.DlgHotInvers] = Colors.MakeAttr(Color.Red, Color.Blue);
			_colors[AttributeRole.DlgStatusLine] = Colors.MakeAttr(Color.Black, Color.White);
			_colors[AttributeRole.DlgStatusInvers] = Colors.MakeAttr(Color.Red, Color.White);
			_colors[AttributeRole.DlgEdit] = Colors.MakeAttr(Color.Black, Color.Cyan);
			_colors[AttributeRole.DlgEditMark] = Colors.MakeAttr(Color.BrightYellow, Color.Blue);

		}
	}
}
