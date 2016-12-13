using System;
using NUnit.Framework;
using System.Reflection;
using EV3Application;
using System.Threading;

namespace EV3Application.Test
{
	[TestFixture]
	public class LCDControllerTest
	{
		[Test]
		public void ShowInfoDialogTest()
		{
			//Object instance = Activator.CreateInstance(typeof(LCD.LCDController), new ManualResetEvent(false));
			MethodInfo m = (typeof(LCD.LCDController)).GetMethod ("showInfoDialog", BindingFlags.NonPublic | BindingFlags.Instance);
			Assert.IsNotNull (m);
			//m.Invoke ();
		}
	}
}

