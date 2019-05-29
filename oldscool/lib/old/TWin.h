namespace Hilevel
{
	class	TWin;
	typedef QList<TWin*>	TWinList;

	class			TWin
	: public QObject
	{
	public:	// ---- ctor/dtor -----------------------------------------------------------------------------------------
		TWin(TVio& vio, int x, int y, int width, int height, const char* name);
		~TWin();
	public:
		bool		isVisible();
		void		show();
		void		hide();
		void		setBackground(int col);
		void		update();
		/*!	Löscht den Fensterinhalt. Hierbei bleibt die Hintergrundfarbe unangetastet.
		*/
		void		clear();
		/*!	Setzte die Farbe mit dem Farbenindex col an der Position (x,y). Das Zeichen wird dabei nicht geändert.
			\brief Attribut setzen
		*/
		void		sa(int x, int y, int col);
		/*! Setzt das Zeichen ch an die Position (x,y). Das Attribut wird dabei nicht geändert.
			\brief Zeichen setzen
		*/
		void		sz(int x, int y, char ch);
		/*! Setzt das Zeichen ch mit dem Farbenindex col an die Position (x,y).
			\brief Zeichen und Attribut setzen
		*/
		void		sza(int x, int y, char ch, int col);
		/*! Schreibt den String str an die Position (x,y). Die Attribute werden dabei nicht geändert.
			\brief String schreiben
		*/
		void		ss(int x, int y, const QString& str);
		/*! Schreibt den String str mit dem Farbenindex col an die Position (x,y)
			\brief String und Attribut schreiben
		*/
		void		ssa(int x, int y, const QString& str, int col);
		void		print(const QString& str);
		void		hot(int x, int y, const QString& str, int colnorm, int colinv);
	public:	// ---- Cursor-Funktionen ---------------------------------------------------------------------------------
 		void		cursor(Cursors mode);
		void		setCursorPos(int x, int y);
		void		getCursorPos(int& x, int& y);
	private:
		void		showWins();
	private:
		TVio&		m_Vio;
		_win_st*	m_Win;									//! Das Fenster-Handle
		_win_st*	m_Sub;									//! Das innere Fenster wegen Rahmen
	};	

