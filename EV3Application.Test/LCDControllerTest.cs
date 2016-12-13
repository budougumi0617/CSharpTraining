using System;
using NUnit.Framework;
using MonoBrickFirmware;
using MonoBrickFirmwareWrapper;
using EV3Application;
using System.Reflection;
using System.Threading;

namespace EV3Application.Test
{
	[TestFixture]
	public class LCDControllerTest
	{
		private FieldInfo stateInfo;//<see cref="EV3Application.LCDController.state"/>の情報
		private FieldInfo sendSignalInfo;//<see cref="EV3Application.LCDController.sendSignal"/>の情報
		private MethodInfo showInfoDialogInfo;//<see cref="EV3Application.LCDController.showInfoDialog"/>の情報
		private MethodInfo showAlphanumericInfo;//<see cref="EV3Application.LCDController.showAlphanumericDisplay"/>の情報
		private FieldInfo writeTextInfo;//<see cref="MonoBrickFirmwareWrapper.Display.LcdWrapper.writeText"/>の情報
		private FieldInfo updateInfo;//<see cref="MonoBrickFirmwareWrapper.Display.LcdWrapper.update"/>の情報
		private FieldInfo clearInfo;//<see cref="MonoBrickFirmwareWrapper.Display.LcdWrapper.clear"/>の情報
		private Action<MonoBrickFirmware.Display.Font, MonoBrickFirmware.Display.Point, string, bool> originalWriteText;//元のメソッドを退避させるためのメソッド
		private Action<int> originalUpdate;//元のメソッドを退避させるためのメソッド
		private Action originalClear;//元のメソッドを退避させるためのメソッド


		[TestFixtureSetUp]
		public void InitializeTest()
		{
			stateInfo = (typeof(LCD.LCDController)).GetField("state", BindingFlags.NonPublic | BindingFlags.Instance);
			sendSignalInfo = (typeof(LCD.LCDController)).GetField("sendSignal", BindingFlags.NonPublic | BindingFlags.Instance);
			showInfoDialogInfo = (typeof(LCD.LCDController)).GetMethod("showInfoDialog", BindingFlags.NonPublic | BindingFlags.Instance);
			showAlphanumericInfo = (typeof(LCD.LCDController)).GetMethod("showAlphanumericDisplay", BindingFlags.NonPublic | BindingFlags.Instance);
			writeTextInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("writeText", BindingFlags.NonPublic | BindingFlags.Static);
			updateInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("update", BindingFlags.NonPublic | BindingFlags.Static);
			clearInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("clear", BindingFlags.NonPublic | BindingFlags.Static);
		}

		[SetUp]
		public void SetUpTest()
		{
			originalWriteText = null;
			originalUpdate = null;
			originalClear = null;
		}

		[TearDown]
		public void TearDown()
		{
			if (originalWriteText != null)
			{
				writeTextInfo.SetValue (null, originalWriteText);
				originalWriteText = null;
			}
			if(originalUpdate != null)
			{
				updateInfo.SetValue (null, originalUpdate);
				originalUpdate = null;
			}
			if(originalClear != null)
			{
				clearInfo.SetValue (null, originalClear);
				originalClear = null;
			}
		}

		[Test, Description("フィールドstateが初期化されているか")]
		public void LCDControllerTest001()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController(new ManualResetEvent (false));
			//実行、確認
			Assert.AreEqual(LCD.LCDController.State.Started, stateInfo.GetValue(controller));
		}

		[Test, Description("フィールドsendSignalが初期化されているか")]
		public void LCDControllerTest002()
		{
			//準備
			ManualResetEvent mre = new ManualResetEvent (false);
			LCD.LCDController controller = new LCD.LCDController(mre);
			//実行、確認
			Assert.AreSame(mre, sendSignalInfo.GetValue(controller));
		}

		[Test, Description("インスタンスが生成されるか")]
		public void LCDControllerTest003()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController(new ManualResetEvent(false));
			//実行、確認
			Assert.IsNotNull(controller);
		}

		//ControllLCDのテスト
		//<see cref="state"/>を参照して、LCD画面をコントロールする。

		[Test, Description("stateがStartedからClosedInfoDialogに変更されているか確認する")]
		public void ControlLCDTest001()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			//実行
			controller.ControlLCD();
			//確認
			Assert.AreEqual(LCD.LCDController.State.ClosedInfoDialog, stateInfo.GetValue(controller));
		}

		[Test, Description("InfoDialogを表示したか確認する")]
		public void ControlLCDTest002()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			//実行
			controller.ControlLCD();

		}

		[Test, Description("stateがClosedInfoDialogからClearedTextHelloに変更されているか確認する")]
		public void ControlLCDTest003()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			stateInfo.SetValue (controller, LCD.LCDController.State.ClosedInfoDialog);
			//実行
			controller.ControlLCD();
			//確認
			Assert.AreEqual(LCD.LCDController.State.ClearedTextHello, stateInfo.GetValue(controller));
		}

		[Test, Description("Helloを表示したか確認する")]
		public void ControlLCDTest004()
		{
			//オリジナルメソッドを退避
			//originalWriteText = MonoBrickFirmwareWrapper.Display.LcdWrapper.WriteTextAction;
			//originalUpdate = MonoBrickFirmwareWrapper.Display.LcdWrapper.Update;
			//originalClear = MonoBrickFirmwareWrapper.Display.LcdWrapper.Clear;

			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			stateInfo.SetValue (controller, LCD.LCDController.State.ClosedInfoDialog);
			writeTextInfo.SetValue(null, (Action<MonoBrickFirmware.Display.Font, MonoBrickFirmware.Display.Point, string, bool>)this.MyWriteText);
			updateInfo.SetValue (null, (Action<int>)this.MyUpdate);
			clearInfo.SetValue (null, (Action)this.MyClear);
			//実行、確認
			controller.ControlLCD();
		}

		[Test, Description("stateがClearedTextHelloからEndに変更されているか確認する")]
		public void ControlLCDTest005()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			stateInfo.SetValue (controller, LCD.LCDController.State.ClearedTextHello);
			//実行
			controller.ControlLCD();
			//確認
			Assert.AreEqual(LCD.LCDController.State.End, stateInfo.GetValue(controller));
		}

		[Test, Description("Good Byeを表示したか確認する")]
		public void ControlLCDTest006()
		{
			//オリジナルメソッドを退避
			originalWriteText = MonoBrickFirmwareWrapper.Display.LcdWrapper.WriteTextAction;
			originalUpdate = MonoBrickFirmwareWrapper.Display.LcdWrapper.Update;
			originalClear = MonoBrickFirmwareWrapper.Display.LcdWrapper.Clear;
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			stateInfo.SetValue (controller, LCD.LCDController.State.ClearedTextHello);
			writeTextInfo.SetValue(null, (Action<MonoBrickFirmware.Display.Font, MonoBrickFirmware.Display.Point, string, bool>)this.MyWriteText);
			updateInfo.SetValue (null, (Action<int>)this.MyUpdate);
			clearInfo.SetValue (null, (Action)this.MyClear);
			//実行、確認
			controller.ControlLCD();
		}

		[Test, Description("stateがEndの場合に処理が実行されないことを確認する")]
		public void ControlLCDTest007()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			FieldInfo stateInfo = (typeof(LCD.LCDController)).GetField("state", BindingFlags.NonPublic | BindingFlags.Instance);
			stateInfo.SetValue (controller, LCD.LCDController.State.End);
			//実行
			controller.ControlLCD();
			//確認

		}

		//showInfoDialogのテスト
		//LCD画面上に<c>InfoDialog</c>を表示する。

		[Test, Description("InfoDialogを表示したか確認する")]
		public void ShowInfoDialogTest001()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController(new ManualResetEvent(false));
			MethodInfo showDialogInfo = (typeof(LCD.LCDController)).GetMethod ("showInfoDialog", BindingFlags.NonPublic | BindingFlags.Instance);


		}

		//showAlphanumericDisplayのテスト
		//LCD画面上に文字列を表示する。



		//Test用メソッド

		public void MyWriteText(MonoBrickFirmware.Display.Font font, MonoBrickFirmware.Display.Point point, string message, bool color)
		{
			System.Console.WriteLine("MyWriteText is called, and message is " + message);
		}

		public void MyUpdate(int  yOffset = 0)
		{
			System.Console.WriteLine("MyUpdate is called");
		}

		public void MyClear()
		{
			System.Console.WriteLine ("MyClear is called");
		}
	}
}


