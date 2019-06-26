using System;

namespace OldScool 
{
	public enum TitlePos
	{
		TopLeft,
		TopCenter,
		TopRight,
		BottomLeft, 
		BottomCenter,
		BottomRight
	}


	public enum CursorType
	{
		Off,
		Line,
		Block
	}

	// A window part of the screen
	public class Win
	{
		public Win(Vio vio, int x, int y, int w, int h)
		{
			_vio = vio;
			PosX = x;
			PosY = y;
			Width = w;
			Height = h;

			CursorType = CursorType.Off;
			_titleAttr = AttribRole.WinTitle;
			_frame = CreateFrame(FrameType.Single);
			_frameAttr = AttribRole.WinFrame;

			_visible = false;
			_content = new int [w, h, 2];
			_background = new int [w, h, 2];
			_textAttr = AttribRole.WinText;
		

			Clear();
		}

		public void SetBackground(AttribRole attribRole)
		{
			
		}
		
		
		public int PosX { get; } 
		public int PosY { get; } 
		public int Width { get; }
		public int Height { get; }

		int CenterCol(int len=0)
		{
			return (Width-len) / 2;
		}
		int CenterRow(int height=0)
		{
			return (Height-height) / 2;
		}








		Vio _vio;
		int _curX; // X-Position des Cursors
		int _curY; // Y-Position des Cursors
		int [,,] _content;
		int [,,] _background;
		AttribRole _textAttr;
//  INT           iTitlePos;                       /* Position des Titels     */
//  WORD          wFlags;                          /* Die Fenster-Flags       */
//  WORD          wAttrShadow;                     /* Attribut des Fensterschattens */





		public CursorType CursorType { get; set; }

		#region Title

		string _title;
		TitlePos _titlePosition;
		AttribRole _titleAttr;
		public string Title { get => _title; }
		public TitlePos TitlePosition { get => _titlePosition; }
		public void SetTitle(string title, TitlePos position, AttribRole attribute=AttribRole.WinTitle) { 
			_title = title;
			_titlePosition = position; 
			_titleAttr = attribute;
			DrawFrame(); 
			DrawTitle(); 
		}

		#endregion Title

		#region Frame

		IFrame _frame;
		AttribRole _frameAttr;

		public FrameType FrameType { get => _frame!=null ? _frame.Type : FrameType.None; }
		public void SetFrame(FrameType frameType, AttribRole attribute=AttribRole.WinFrame)
		{
			_frame = CreateFrame(frameType);
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


    public void Clear()
    {
      var line = new string(' ', Width-2);
      for(var i=0; i<Height-2; i++) {
        Ssa(0, i, line, _textAttr);  
      }
    }

    public void Sz(int col, int row, char ch)
    {
	    if( Visible )
		    _vio.Sz(PosX+col+1, PosY+row+1, ch);
	    _content[col,row,0] = ch;
    }

    public void Sza(int col, int row, char ch, AttribRole attr)
    {
      if( Visible )
        _vio.Sza(PosX+col+1, PosY+row+1, ch, attr);
      _content[col,row,0] = ch;
      _content[col,row,1] = (int) attr;
    }

    public void Ss(int col, int row, string text)
    {
	    if( Visible )
		    _vio.Ss(PosX+col+1, PosY+row+1, text);
	    for(int i=0; i<text.Length; i++) {
		    _content[col+i,row,0] = text[i];
	    }
    }

    public void Ssa(int col, int row, string text, AttribRole attr)
    {
      if( Visible )
        _vio.Ssa(PosX+col+1, PosY+row+1, text, attr);
      for(int i=0; i<text.Length; i++) {
        _content[col+i,row,0] = text[i];
        _content[col+i,row,1] = (int) attr;
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
          _vio.Sza(PosX+i+1, PosY+n+1, (char)_content[i,n,0], (AttribRole) _content[i,n,1]);
        }
      }
    }



    void RestoreWin()
    {
      for(var i=0; i<Width; i++) {
        for(var n=0; n<Height; n++) {
           _vio.Sza(i+PosX, n+PosY, (char)_background[i, n, 0], (AttribRole) _background[i, n, 1]);
        }
      }
    }

    void SaveWin()
    {
      for(var i=0; i<Width; i++) {
        for(var n=0; n<Height; n++) {
          _background[i, n, 0] = _vio.Lz(i+PosX, n+PosY);
          _background[i, n, 1] = _vio.La(i+PosX, n+PosY);
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
				case TitlePos.BottomLeft:
					row = PosY + Height -1;
					col = PosX + 1;
					break;
				case TitlePos.BottomCenter:
					row = PosY + Height -1;
					col = PosX + CenterCol(Title.Length);
					break;
				case TitlePos.BottomRight:
					row = PosY + Height -1;
					col = PosX + Width - Title.Length - 1;
					break;
				case TitlePos.TopLeft:
					col = PosX + 1;
					break;
				case TitlePos.TopCenter:
					col = PosX + CenterCol(Title.Length);
					break;
				case TitlePos.TopRight:
					col = PosX + Width - Title.Length - 1;
					break;
			}
			_vio.Ssa( col, row, Title, _titleAttr );
		}

		void DrawFrame()
		{
			if( !Visible ) return;
			var width = PosX+Width-1;
			var height = PosY+Height-1;
			_vio.Sza( PosX, PosY, _frame.TopLeft, _frameAttr );
			_vio.Sza( width, PosY, _frame.TopRight, _frameAttr );
			_vio.Sza( PosX, height, _frame.BottomLeft, _frameAttr );
			_vio.Sza( width, height, _frame.BottomRight, _frameAttr );
			for(var i=PosX+1; i<width; i++)
			{
				_vio.Sza( i, PosY, _frame.HLine, _frameAttr );
				_vio.Sza( i, height, _frame.HLine, _frameAttr );
			}
			for(var i=PosY+1; i<height; i++)
			{
				_vio.Sza( PosX, i, _frame.VLine, _frameAttr );
				_vio.Sza( width, i, _frame.VLine, _frameAttr );
			}
		}

//		void DrawShadow()
//		{
//			if( !Visible ) return;
//			var stop = PosY+Height-1;
//			for(var i=0; i<=stop; i++)
//				Vio.Sa( PosX, i, _attrShadow );
/*
  if( wStart < VioGetMaxCol() )    
  {
   wStop  = win->wPosY+win->wHeight; 
   {
    wStop  = win->wPosY+win->wHeight;
    for( i=win->wPosY+1; i<=wStop && i<VioGetMaxRow(); i++ )
    {
     VioSa( wStart, i, win->wAttrShadow );
     if( wStart<VioGetMaxCol()-1 )
      VioSa( wStart+1, i, win->wAttrShadow );
    }
    wStart = win->wPosY+win->wHeight;
    if( wStart < VioGetMaxRow() )
    {
     wStop  = win->wPosX+win->wWidth;
     for( i=win->wPosX+1; i<wStop && i< VioGetMaxCol(); i++ )
      VioSa( i, wStart, win->wAttrShadow );
    }
   }
  }
 */
		IFrame CreateFrame(FrameType type) 
		{
			if( _frame!=null ) {
				if( _frame.Type==type )
					return _frame;
			}
			return Frame.CreateFrame(type);
		}
	}
}