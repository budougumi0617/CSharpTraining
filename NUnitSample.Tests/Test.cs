using NUnit.Framework;
using System;
using CSharpTraining;
using MonoBrickFirmware.Movement;

namespace NUnitSample.Tests
{
	/// <summary>
	/// Sample test class
	/// </summary>
	/// <remarks>You can use NUnit console too. There is in below path.
	/// .\packages\NUnit.Runners.2.6.4\tools\nunit.exe
	/// </remarks>
	[TestFixture]
	public class MainClassTests
	{
		[Test, Description("We can describe test summary by this attribute."), Category("We can set test category by this attribute.")]
		public void MainTest()
		{
			Assert.IsTrue(true);
			Assert.AreEqual(1, 1);
			var foo = new Object();
			var same = foo;
			Assert.AreSame(foo, same); // Verify their variables are same object.
		}


		[Test, Description("If We confirm that the targert method throws an exeception, we use ExpectedException attribute.")]
		[ExpectedException(typeof(InvalidOperationException))]
		[Explicit("If you want to ignore test case, you set this attribute to the test case.")]
		public void IgnoreTest()
		{
			Assert.Fail("Expected this test case does not be executed.");
			MainClass.Main(new string[] { "" });
			Motor dummy = new Motor(MotorPort.OutA);
			dummy.GetSpeed();
		}
	}
}
