namespace OldScool 
{
	public enum FrameType
	{
		None,
		Single,
		Double,
		Thick,
		Block
	};
	
	public interface IFrame
	{
		FrameType Type { get; }
		char HLine { get; }
		char VLine { get; }
		char TopLeft { get; }
		char TopRight { get; }
		char BottomLeft { get; }
		char BottomRight { get; }
	}
	
	public static class Frame
	{
		public static IFrame CreateFrame(FrameType frameType)
		{
			switch(frameType) {
				case FrameType.Single: return new SingleFrame();
				case FrameType.Double: return new DoubleFrame();
				case FrameType.Thick: return new ThickFrame();
				case FrameType.Block: return new BlockFrame();
			}
			return new NoFrame();
		}
	}

	public class SingleFrame : IFrame {
		public FrameType Type => FrameType.Single;
		public char HLine => '\u2500';
		public char VLine => '\u2502';
		public char TopLeft => '\u250C';
		public char TopRight => '\u2510';
		public char BottomLeft => '\u2514';
		public char BottomRight => '\u2518';
	}

	public class DoubleFrame : IFrame
	{
		public FrameType Type => FrameType.Double;
		public char HLine => '\u2550';
		public char VLine => '\u2551';
		public char TopLeft => '\u2554';
		public char TopRight => '\u2557';
		public char BottomLeft => '\u255A';
		public char BottomRight => '\u255D';
	}

	public class ThickFrame : IFrame
	{
		public FrameType Type => FrameType.Thick;
		public char HLine => '\u2501';
		public char VLine => '\u2503';
		public char TopLeft => '\u250F';
		public char TopRight => '\u2513';
		public char BottomLeft => '\u2517';
		public char BottomRight => '\u251B';
	}

	public class BlockFrame : IFrame
	{
		public FrameType Type => FrameType.Block;
		public char HLine => '\u2587';
		public char VLine => '\u2587';
		public char TopLeft => '\u2587';
		public char TopRight => '\u2587';
		public char BottomLeft => '\u2587';
		public char BottomRight => '\u2587';
	}

	public class NoFrame : IFrame
	{
		public FrameType Type => FrameType.None;
		public char HLine => ' ';
		public char VLine => ' ';
		public char TopLeft => ' ';
		public char TopRight => ' ';
		public char BottomLeft => ' ';
		public char BottomRight => ' ';
	}
}

