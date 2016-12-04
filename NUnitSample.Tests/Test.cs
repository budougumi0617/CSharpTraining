//
// NUnitSampleTests.cs
//
// Author:
//       budougumi0617 <budougumi0617@gmail.com>
//
// Copyright (c) 2016 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using CSharpTraining;
using MonoBrickFirmwareWrapper.Movement;
using NUnit.Framework;
using Moq;

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
			Assert.AreEqual(10, MainClass.SampleMethod());
		}


		[Test, Description("If We confirm that the targert method throws an exeception, we use ExpectedException attribute.")]
		[Explicit("If you want to ignore test case, you set this attribute to the test case.")]
		public void IgnoreTest()
		{
			Assert.Fail("Expected this test case does not be executed.");
			MainClass.Main(new string[] { "" });
			IMotor dummy = new Mock<IMotor>().Object;
			dummy.Off();
			Assert.Throws<InvalidOperationException>(() =>
			{
				throw new InvalidOperationException();
			});
		}
	}
}
