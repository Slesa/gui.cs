	void TColors::configure()
	{
		init_pair(Background, COLOR_BLACK, COLOR_CYAN); m_Attrs[Background] = A_BOLD;
		// Nachrichten
		init_pair(MsgFrame, COLOR_WHITE, COLOR_BLUE);
		init_pair(MsgTitle, COLOR_WHITE, COLOR_BLUE);
		init_pair(MsgText, COLOR_WHITE, COLOR_BLUE);
		init_pair(MsgStatusLine, COLOR_BLACK, COLOR_WHITE);
		// Listen
		init_pair(LstFrame, COLOR_BLACK, COLOR_WHITE);
		init_pair(LstTitle, COLOR_BLACK, COLOR_WHITE); //m_Attrs[LstTitle] = A_BOLD;
		init_pair(LstText, COLOR_BLACK, COLOR_WHITE);
		init_pair(LstInvers, COLOR_WHITE, COLOR_BLUE); m_Attrs[LstInvers] = A_BOLD;
		init_pair(LstStatusLine, COLOR_BLACK, COLOR_WHITE);
		init_pair(LstStatusInvers, COLOR_RED, COLOR_WHITE);
		// Menüs
		init_pair(MnuFrame, COLOR_BLACK, COLOR_WHITE);
		init_pair(MnuTitle, COLOR_BLACK, COLOR_WHITE);
		init_pair(MnuText, COLOR_BLACK, COLOR_WHITE);
		init_pair(MnuInvers, COLOR_WHITE, COLOR_BLUE); m_Attrs[MnuInvers] = A_BOLD;
		init_pair(MnuHotkey, COLOR_RED, COLOR_WHITE);
		init_pair(MnuHotInvers, COLOR_RED, COLOR_BLUE); m_Attrs[MnuHotInvers] = A_BOLD;
		init_pair(MnuStatusLine, COLOR_BLACK, COLOR_WHITE);
		init_pair(MnuStatusInvers, COLOR_RED, COLOR_WHITE);
	}

#if 0
 PRIVATE SDlgPal DlgPalColor = { VH( SCHWARZ, WEISS )  /* Rahmenattribut    */
                               , VH( SCHWARZ, WEISS )  /* Titelattribut     */
                               , VH( SCHWARZ, WEISS )  /* Normaler Text     */
                               , VH( HELLWEISS, BLAU ) /* Inverser Text     */
                               , VH( ROT, WEISS )      /* Normaler Text der Hotkeys */
                               , VH( HELLROT, BLAU )   /* Inverser Text der Hotkeys */
                               , VH( SCHWARZ, WEISS )  /* Normaler Text der Statuszeile */
                               , VH( ROT, WEISS )      /* Inverser Text der Statuszeile */
                               , VH( SCHWARZ, WEISS )  /* Attribut eines nicht aktiven Eintrags */
                               , VH( GRAU, GRUEN )     /* Attribut eines nicht aktiven Push-Buttons */
                               , VH( ROT, GRUEN )      /* Attribut des Hotkeys eines Push-Buttons */
                               , VH( WEISS, GRUEN )    /* Attribut eines aktiven Push-Buttons */
                               , VH( GELB, BLAU )      /* Attribut eines aktiven Push-Buttons */
                               , VH( HELLROT, BLAU )   /* Attribut des Hotkeys eines Push-Buttons */
                               , VH( SCHWARZ, KOBALT ) /* Attribut des Editierfeldes */
                               , VH( BLAU, GELB )      /* Attribut des Markierten Textes */
                               };
#endif
