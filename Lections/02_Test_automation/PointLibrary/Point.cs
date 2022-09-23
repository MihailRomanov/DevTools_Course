namespace PointLibrary
{
	public class Point
	{
		public int X
		{
			get;
			private set;
		}

		public int Y
		{
			get;
			private set;
		}

		public Point()
		{
			this.X = 0;
			this.Y = 0;
		}

		public Point(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public void Move(int dx, int dy)
		{
			this.X += dx;
			this.Y += dy;
		}
	}

}
