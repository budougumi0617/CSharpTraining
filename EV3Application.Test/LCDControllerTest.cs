using System;
using NUnit.Framework;
using MonoBrickFirmwareWrapper;
using System.Reflection;
using EV3Application;
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
		private MethodInfo writeTextInfo;//<see cref="MonoBrickFirmwareWrapper.Display.LcdWrapper.writeText"/>の情報
		private MonoBrickFirmwareWrapper.Display.LcdWrapper.TextWidth originalMethod;

		[TestFixtureSetUp]
		public void InitializeTest(){
			stateInfo = (typeof(LCD.LCDController)).GetField("state", BindingFlags.NonPublic | BindingFlags.Instance);
			sendSignalInfo = (typeof(LCD.LCDController)).GetField("sendSignal", BindingFlags.NonPublic | BindingFlags.Instance);
			showInfoDialogInfo = (typeof(LCD.LCDController)).GetMethod("showInfoDialog", BindingFlags.NonPublic | BindingFlags.Instance);
			showAlphanumericInfo = (typeof(LCD.LCDController)).GetMethod("showAlphanumericDisplay", BindingFlags.NonPublic | BindingFlags.Instance);
			writeTextInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetMethod ("witeText", BindingFlags.NonPublic | BindingFlags.Static);
		}

		[TearDown]
		public void TearDown(){

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
	}
}

