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
		[Test, Description("フィールドstateが初期化されているか")]
		public void LCDControllerTest001()
		{
			LCD.LCDController controller = new LCD.LCDController(new ManualResetEvent (false));
			FieldInfo stateInfo = (typeof(LCD.LCDController)).GetField("state", BindingFlags.NonPublic | BindingFlags.Instance);
			Assert.AreEqual(LCD.LCDController.State.Started, stateInfo.GetValue(controller));
		}

		[Test, Description("フィールドsendSignalが初期化されているか")]
		public void LCDControllerTest002()
		{
			ManualResetEvent mre = new ManualResetEvent (false);
			LCD.LCDController controller = new LCD.LCDController(mre);
			FieldInfo sendSignalInfo = (typeof(LCD.LCDController)).GetField("sendSignal", BindingFlags.NonPublic | BindingFlags.Instance);
			Assert.AreSame(mre, sendSignalInfo.GetValue(controller));
		}

		[Test, Description("インスタンスが生成されるか")]
		public void LCDControllerTest003()
		{
			LCD.LCDController controller = new LCD.LCDController(new ManualResetEvent(false));
			Assert.IsNotNull(controller);
		}

		[Test]
		public void ShowInfoDialogTest()
		{
			//Object instance = Activator.CreateInstance(typeof(LCD.LCDController), new ManualResetEvent(false));
			MethodInfo showDialogInfo = (typeof(LCD.LCDController)).GetMethod ("showInfoDialog", BindingFlags.NonPublic | BindingFlags.Instance);
			Assert.IsNotNull (showDialogInfo);
			//m.Invoke ();
		}
	}
}

