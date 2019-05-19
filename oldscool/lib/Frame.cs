namespace OldSchool 
{
    public interface IFrame
    {
      char HLine { get; }
      char VLine { get; }
      char TopLeft { get; }
      char TopRight { get; }
      char BottomLeft { get; }
      char BottomRight { get; }
    }

    public class SingleFrame : IFrame
    {
      private SingleFrame() {}

      static SingleFrame _instance;
      public static SingleFrame Instance 
      {
        get { return _instance ?? (_instance = new SingleFrame()); }
      }

      public char HLine { get => '\u2500'; }
      public char VLine { get => '\u2502'; }
      public char TopLeft { get => '\u250C'; }
      public char TopRight { get => '\u2510'; }
      public char BottomLeft { get => '\u2514'; }
      public char BottomRight { get => '\u2518'; }
    }

    public class DoubleFrame : IFrame
    {
      private DoubleFrame() {}

      static DoubleFrame _instance;
      public static DoubleFrame Instance 
      {
        get { return _instance ?? (_instance = new DoubleFrame()); }
      }

      public char HLine { get => '\u2550'; }
      public char VLine { get => '\u2551'; }
      public char TopLeft { get => '\u2554'; }
      public char TopRight { get => '\u2557'; }
      public char BottomLeft { get => '\u255A'; }
      public char BottomRight { get => '\u255D'; }
    }

    public class ThickFrame : IFrame
    {
      private ThickFrame() {}

      static ThickFrame _instance;
      public static ThickFrame Instance 
      {
        get { return _instance ?? (_instance = new ThickFrame()); }
      }

      public char HLine { get => '\u2501'; }
      public char VLine { get => '\u2503'; }
      public char TopLeft { get => '\u250F'; }
      public char TopRight { get => '\u2513'; }
      public char BottomLeft { get => '\u2517'; }
      public char BottomRight { get => '\u251B'; }
    }

    public class BlockFrame : IFrame
    {
      private BlockFrame() {}

      static BlockFrame _instance;
      public static BlockFrame Instance 
      {
        get { return _instance ?? (_instance = new BlockFrame()); }
      }

      public char HLine { get => '\u2587'; }
      public char VLine { get => '\u2587'; }
      public char TopLeft { get => '\u2587'; }
      public char TopRight { get => '\u2587'; }
      public char BottomLeft { get => '\u2587'; }
      public char BottomRight { get => '\u2587'; }
    }

    public class NoFrame : IFrame
    {
      private NoFrame() {}

      static NoFrame _instance;
      public static NoFrame Instance 
      {
        get { return _instance ?? (_instance = new NoFrame()); }
      }

      public char HLine { get => ' '; }
      public char VLine { get => ' '; }
      public char TopLeft { get => ' '; }
      public char TopRight { get => ' '; }
      public char BottomLeft { get => ' '; }
      public char BottomRight { get => ' '; }
    }
}

/*
			Stipple = '\u2592';
			Diamond = '\u25c6';
			LeftTee = '\u251c';
			RightTee = '\u2524';
			TopTee = '\u22a4';
			BottomTee = '\u22a5';
*/