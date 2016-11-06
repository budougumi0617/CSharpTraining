using System;
using CSharpTraining;
using MonoBrickFirmware.Movement;

namespace XUnitSample.Tests
{
	/// <summary>
	/// Sample test class
	/// </summary>
	/// <remarks>
	/// It was described below URL how to use XUnit
	/// https://xunit.github.io/docs/comparisons.html
	/// </remarks>
	public class MainClassTests
	{
		[Xunit.Fact(DisplayName = "We can describe test summary by this attribute."), Xunit.Trait("Category", "Sample")]
		public void MainTest()
		{
			Xunit.Assert.True(true);
			Xunit.Assert.Equal(10, MainClass.SampleMethod());
			var foo = new Motor(MotorPort.OutA);
			var same = foo;
			Xunit.Assert.Same(foo, same); // Verify their variables are same object.
		}


		[Xunit.Fact(Skip="If you want to ignore test case, you set this attribute to the test case.")]
		public void IgnoreTest()
		{
			Xunit.Assert.True(false, "Expected this test case does not be executed.");
			MainClass.Main(new string[] { "" });
			Motor dummy = new Motor(MotorPort.OutA);
			dummy.GetSpeed();
		}
	}
}
