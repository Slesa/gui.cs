using System;

namespace OldSchool 
{
	public enum TitlePosition
	{
		TopLeft,        // oben links
		TopCenter,      // oben zentriert
		TopRight,       // oben rechts
		BottomLeft,     // unten links 
		BottomCenter,   // unten zentriert
		BottomRight     // unten rechts
	}

	public enum FrameType
	{
		None,           // Kein Rahmen
		Single,         // Einfacher Rahmen
		Double,         // Doppelter Rahmen
		Thick,          // Dicker Rahmen
		Block,          // Blockrahmen
	}

	public enum CursorType
	{
		Off,            // No cursor
		Line,           // Line cursor
		Block           // Block cursor
	}

	// A window part of the screen
	public class Win
	{
		static int DefTitleAttr = Colors.MakeAttr(ConsoleColor.White, ConsoleColor.DarkBlue);
		static int DefFrameAttr = Colors.MakeAttr(ConsoleColor.White, ConsoleColor.DarkBlue);

    int _curX; // X-Position des Cursors
    int _curY; // Y-Position des Cursors
    int [,,] _content;
    int [,,] _background;
    int _textAttr;
//  INT           iTitlePos;                       /* Position des Titels     */
//  WORD          wFlags;                          /* Die Fenster-Flags       */
//  WORD          wAttrShadow;                     /* Attribut des Fensterschattens */

	public Win(int x, int y, int w, int h)
	{
		PosX = x;
		PosY = y;
		Width = w;
		Height = h;

		CursorType = CursorType.Off;
		_titleAttr = DefTitleAttr;
		_frameType = FrameType.Single;
		_frameAttr = DefFrameAttr;

      _visible = false;
      _content = new int [w, h, 2];
      _background = new int [w, h, 2];
      _textAttr = Colors.MakeAttr(ConsoleColor.White, ConsoleColor.DarkBlue);
		

		Cls();
    }

	int CenterCol(int len=0)
	{
		return (Width-len) / 2;
	}
	int CenterRow(int height=0)
	{
		return (Height-height) / 2;
	}

	public int PosX { get; } 
	public int PosY { get; } 
	public int Width { get; }
    public int Height { get; }
	public CursorType CursorType { get; set; }

	#region Title

	string _title;
	TitlePosition _titlePosition;
    int _titleAttr;
	public string Title { get => _title; }
	public TitlePosition TitlePosition { get => _titlePosition; }
      
    void SetTitle(string title, TitlePosition position, int attribute) { 
		_title = title;
		_titlePosition = position; 
		_titleAttr = attribute;
		DrawFrame(); 
		DrawTitle(); 
	}
    
	#endregion Title

	#region Frame

	FrameType _frameType;
	int _frameAttr;

	public FrameType FrameType { get => _frameType; }
	void SetFrame(FrameType frameType, int attribute)
	{
		_frameType = frameType;
		_frameAttr = attribute;
		DrawFrame();
		DrawTitle();
	}

	#endregion Frame

    bool _visible;
    public bool Visible {
      get => _visible;
      set {
        if( _visible==value ) return; 
        _visible = value;
        if( _visible ) { 
          SaveWin();
          DrawFrame(); 
          DrawTitle(); 
          Redraw();
        } else {
          RestoreWin();
        }
      }
    }

	public void Remove() {}


    public void Cls()
    {
      var line = new string(' ', Width-2);
      for(var i=0; i<Height-2; i++) {
        Ssa(0, i, line, _textAttr);  
      }
    }

    public void Sza(int col, int row, char ch, int attr)
    {
      if( Visible )
        Vio.Sza(PosX+col+1, PosY+row+1, ch, attr);
      _content[col,row,0] = ch;
      _content[col,row,1] = attr;
    }

    public void Ssa(int col, int row, string text, int attr)
    {
      if( Visible )
        Vio.Ssa(PosX+col+1, PosY+row+1, text, attr);
      for(int i=0; i<text.Length; i++) {
        _content[col+i,row,0] = text[i];
        _content[col+i,row,1] = attr;
      }
    }

    public void Printf(string text)
    {
      var maxCol = Width-2;
      var maxRow = Height-2;
      var col = _curX;
      var row = _curY;
      for(var i=0; i<text.Length; i++)
      {
        switch( text[i] )
        {
          case '\b':
            if( col>0 ) col--;
            break;
          case '\r':
            col = 0;
            break;
          case '\n':
            if( row==maxRow ) 
              ; //ScrollUp();
            else
              row++;
              col = 0;
            break;
          default:
            Sza( col, row, (char)text[i], _textAttr );
            if( ++col == maxCol ) {
              col = 0;
              if( row==maxRow )
                ; //ScrollUp();
              else
                row++;
            }
            break;
        }
      }
      _curX = col;
      _curY =row;
    }

    void Redraw() 
    {
      for(var i=0; i<Width-2; i++) {
        for(var n=0; n<Height-2; n++) {
          Vio.Sza(PosX+i+1, PosY+n+1, (char)_content[i,n,0], _content[i,n,1]);
        }
      }
    }



    void RestoreWin()
    {
      for(var i=0; i<Width; i++) {
        for(var n=0; n<Height; n++) {
           Vio.Sza(i+PosX, n+PosY, (char)_background[i, n, 0], _background[i, n, 1]);
        }
      }
    }

    void SaveWin()
    {
      for(var i=0; i<Width; i++) {
        for(var n=0; n<Height; n++) {
          _background[i, n, 0] = Vio.Lz(i+PosX, n+PosY);
          _background[i, n, 1] = Vio.La(i+PosX, n+PosY);
        }
      }
    }


		void DrawTitle()
		{
			if( !Visible ) return;
			if( string.IsNullOrEmpty(Title) ) return;

			var col = 0;
			var row = PosY;
 
			switch(TitlePosition)
			{
				case TitlePosition.BottomLeft:
					row = PosY + Height -1;
					col = PosX + 1;
					break;
				case TitlePosition.BottomCenter:
					row = PosY + Height -1;
					col = PosX + CenterCol(Title.Length);
					break;
				case TitlePosition.BottomRight:
					row = PosY + Height -1;
			        col = PosX + Width - Title.Length - 1;
					break;
				case TitlePosition.TopLeft:
					col = PosX + 1;
					break;
				case TitlePosition.TopCenter:
					col = PosX + CenterCol(Title.Length);
					break;
				case TitlePosition.TopRight:
			        col = PosX + Width - Title.Length - 1;
					break;
			}
			Vio.Ssa( row, col, Title, _titleAttr );
		}

		void DrawFrame()
		{
			if( !Visible ) return;
			var width = PosX+Width-1;
			var height = PosY+Height-1;
			var chars = GetFrameChars() ;
			Vio.Sza( PosX, PosY, chars.TopLeft, _frameAttr );
			Vio.Sza( width, PosY, chars.TopRight, _frameAttr );
			Vio.Sza( PosX, height, chars.BottomLeft, _frameAttr );
			Vio.Sza( width, height, chars.BottomRight, _frameAttr );
			for(var i=PosX+1; i<width; i++)
			{
				Vio.Sza( i, PosY, chars.HLine, _frameAttr );
				Vio.Sza( i, height, chars.HLine, _frameAttr );
			}
			for(var i=PosY+1; i<height; i++)
			{
				Vio.Sza( PosX, i, chars.VLine, _frameAttr );
				Vio.Sza( width, i, chars.VLine, _frameAttr );
			}
		}

		IFrame GetFrameChars() 
		{
			switch(Frame)
			{
				case Frame.None: return NoFrame.Instance;
				case Frame.Single: return SingleFrame.Instance;
				case Frame.Double: return DoubleFrame.Instance;
				case Frame.Thick: return ThickFrame.Instance;
				case Frame.Block: return BlockFrame.Instance;
			}
			return SingleFrame.Instance;
		}
	}
}