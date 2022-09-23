using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointLibrary;

namespace PoitLibrary.Tests
{
	[TestClass]
	public class PointTest
	{
		[TestMethod]
		public void DefaultConstructorTest()
		{
			Point point = new Point();

			Assert.AreEqual(0, point.X);
			Assert.AreEqual(0, point.Y);
		}

		[TestMethod]
		public void ParametrizedConstructorTest()
		{
			Point point1 = new Point(0, 0);

			Assert.AreEqual(0, point1.X);
			Assert.AreEqual(0, point1.Y);

			Point point2 = new Point(1, -2);

			Assert.AreEqual(1, point2.X);
			Assert.AreEqual(-2, point2.Y);

		}

		[TestMethod]
		public void MoveTest()
		{
			Point point = new Point(1, 2);
			point.Move(0, 0);

			Assert.AreEqual(1, point.X);
			Assert.AreEqual(2, point.Y);

			point.Move(-1, -3);

			Assert.AreEqual(0, point.X);
			Assert.AreEqual(-1, point.Y);
		}
	}
}
