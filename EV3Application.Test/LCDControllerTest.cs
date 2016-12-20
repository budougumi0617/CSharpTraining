﻿using System;
using NUnit.Framework;
using MonoBrickFirmware;
using MonoBrickFirmwareWrapper.Utilities;
using EV3Application;
using System.Reflection;
using System.Threading;

namespace EV3Application.Test
{
	/// <summary>
	/// <see cref="EV3Application.LCD.LCDController"/>のテストクラス。
	/// </summary>
	[TestFixture]
	public class LCDControllerTest
	{
		private FieldInfo stateInfo;//<see cref="EV3Application.LCD.LCDCotroller.state"/>の情報
		private FieldInfo sendSignalInfo;//<see cref="EV3Application.LCD.LCDController.sendSignal"/>の情報
		private FieldInfo currentDisplayInfo;//<see cref="EV3Application.LCD.LCDController.currentDisplay"/>の情報
		//private MethodInfo showInfoDialogInfo;//<see cref="EV3Application.LCD.LCDController.showInfoDialog"/>の情報
		private MethodInfo showAlphanumericInfo;//<see cref="EV3Application.LCD.LCDController.showAlphanumericDisplay"/>の情報
		private FieldInfo writeTextInfo;//<see cref="MonoBrickFirmwareWrapper.Display.LcdWrapper.writeText"/>の情報
		private FieldInfo updateInfo;//<see cref="MonoBrickFirmwareWrapper.Display.LcdWrapper.update"/>の情報
		private FieldInfo clearInfo;//<see cref="MonoBrickFirmwareWrapper.Display.LcdWrapper.clear"/>の情報
		private Action<MonoBrickFirmware.Display.Font, MonoBrickFirmware.Display.Point, string, bool> originalWriteText;//元のメソッドを退避させるためのメソッド
		private Action<int> originalUpdate;//元のメソッドを退避させるためのメソッド
		private Action originalClear;//元のメソッドを退避させるためのメソッド

		[TestFixtureSetUp, Description("リフレクションによって、テスト対象メンバの情報をフィールドに代入する")]
		public void TestInitialiser()
		{
			stateInfo = (typeof(LCD.LCDController)).GetField("state", BindingFlags.NonPublic | BindingFlags.Instance);
			sendSignalInfo = (typeof(LCD.LCDController)).GetField("sendSignal", BindingFlags.NonPublic | BindingFlags.Instance);
			currentDisplayInfo = (typeof(LCD.LCDController)).GetField("currentDisplay", BindingFlags.NonPublic | BindingFlags.Instance);
			//showInfoDialogInfo = (typeof(LCD.LCDController)).GetMethod("showInfoDialog", BindingFlags.NonPublic | BindingFlags.Instance);
			showAlphanumericInfo = (typeof(LCD.LCDController)).GetMethod("showAlphanumericDisplay", BindingFlags.NonPublic | BindingFlags.Instance);
			writeTextInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("writeText", BindingFlags.NonPublic | BindingFlags.Static);
			updateInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("update", BindingFlags.NonPublic | BindingFlags.Static);
			clearInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("clear", BindingFlags.NonPublic | BindingFlags.Static);
		}

		[SetUp, Description("オリジナルメソッド格納用の変数を初期化する")]
		public void TestSetUp()
		{
			
			originalWriteText = null;
			originalUpdate = null;
			originalClear = null;
		}

		[TearDown, Description("退避させていたメソッドを戻す")]
		public void TestTearDown()
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

		#region Constructor Test
		[Test, Description("フィールドstateが初期化されているか確認する"), Category("normal")]
		public void InitialiseStateTest()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController(new ManualResetEvent (false));
			//実行、確認
			Assert.AreEqual(LCD.LCDController.State.Started, stateInfo.GetValue(controller));
		}

		[Test, Description("フィールドsendSignalが初期化されているか確認する"), Category("normal")]
		public void InitialiseSendSignalTest()
		{
			//準備
			ManualResetEvent mre = new ManualResetEvent (false);
			LCD.LCDController controller = new LCD.LCDController(mre);
			//実行、確認
			Assert.AreSame(mre, sendSignalInfo.GetValue(controller));
		}

		[Test, Description("コンストラクタ実行時に例外が起きていないか確認する"), Category("normal")]
		public void InstanceConstructorTest()
		{
			//実行、確認
			Assert.DoesNotThrow (
				() => new LCD.LCDController(new ManualResetEvent(false))
			);
		}
		#endregion

		#region ControlLCD Test
		[Test, Description("処理が終了した時、stateがStartedからEndに変更されているか確認する"), Category("normal")]
		public void ChangedToEndTest()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			//実行
			controller.ControlLCD();
			//確認
			Assert.AreEqual(LCD.LCDController.State.End, stateInfo.GetValue(controller));
		}

		[Test, Description("stateがStartedの時、例外なく処理が終了したか確認する"), Category("normal")]
		public void ShowAllTest()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			//実行、確認
			Assert.DoesNotThrow(
				() => controller.ControlLCD()
			);
		}

		[Test, Description("stateがClosedInfoDialogの時、Hello、GoodByeを表示したか確認する"), Category("normal")]
		public void ShowHelloAndGoodByeTest()
		{
			//オリジナルメソッドを退避
			originalWriteText = MonoBrickFirmwareWrapper.Display.LcdWrapper.WriteTextAction;
			originalUpdate = MonoBrickFirmwareWrapper.Display.LcdWrapper.Update;
			originalClear = MonoBrickFirmwareWrapper.Display.LcdWrapper.Clear;
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			stateInfo.SetValue (controller, LCD.LCDController.State.ClosedInfoDialog);
			writeTextInfo.SetValue(null, (Action<MonoBrickFirmware.Display.Font, MonoBrickFirmware.Display.Point, string, bool>)this.MyWriteText);
			updateInfo.SetValue (null, (Action<int>)this.MyUpdate);
			clearInfo.SetValue (null, (Action)this.MyClear);
			//実行、確認
			Assert.DoesNotThrow (
				() => controller.ControlLCD ()
			);
		}

		[Test, Description("stateがClearedTextHelloの時、Good Byeを表示したか確認する"), Category("normal")]
		public void ShowGoodByeTest()
		{
			//オリジナルメソッドを退避
			originalWriteText = MonoBrickFirmwareWrapper.Display.LcdWrapper.WriteTextAction;
			originalUpdate = MonoBrickFirmwareWrapper.Display.LcdWrapper.Update;
			originalClear = MonoBrickFirmwareWrapper.Display.LcdWrapper.Clear;
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			currentDisplayInfo.SetValue (controller, new LCD.AlphanumericDisplay("Test"));
			stateInfo.SetValue (controller, LCD.LCDController.State.ClearedTextHello);
			writeTextInfo.SetValue(null, (Action<MonoBrickFirmware.Display.Font, MonoBrickFirmware.Display.Point, string, bool>)this.MyWriteText);
			updateInfo.SetValue (null, (Action<int>)this.MyUpdate);
			clearInfo.SetValue (null, (Action)this.MyClear);
			//実行、確認
			Assert.DoesNotThrow (
				() => controller.ControlLCD ()
			);
		}
		[Test, Description(""), Category("normal")]
		public void Test()
		{

		}
		#endregion
		/*
		[Test, Description("InfoDialogを表示したか確認する"), Category("normal")]
		public void ShowInfoDialogTest()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController(new ManualResetEvent(false));
			MethodInfo showDialogInfo = (typeof(LCD.LCDController)).GetMethod ("showInfoDialog", BindingFlags.NonPublic | BindingFlags.Instance);
		}
		*/
		[Test, Description("文字列を表示したか確認する"), Category("normal")]
		public void ShowAlphanumericDisplayTest()
		{
			//オリジナルメソッドを退避
			originalWriteText = MonoBrickFirmwareWrapper.Display.LcdWrapper.WriteTextAction;
			originalUpdate = MonoBrickFirmwareWrapper.Display.LcdWrapper.Update;
			originalClear = MonoBrickFirmwareWrapper.Display.LcdWrapper.Clear;
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			currentDisplayInfo.SetValue (controller, new LCD.AlphanumericDisplay("Test"));
			writeTextInfo.SetValue(null, (Action<MonoBrickFirmware.Display.Font, MonoBrickFirmware.Display.Point, string, bool>)this.MyWriteText);
			updateInfo.SetValue (null, (Action<int>)this.MyUpdate);
			clearInfo.SetValue (null, (Action)this.MyClear);
			//実行、確認
			Assert.DoesNotThrow(
				() => showAlphanumericInfo.Invoke(controller, new Object[0])
			);
		}

		[Test, Description("文字列を5秒間(誤差が+1秒未満)表示したか確認する"), Category("normal")]
		public void ShowMessageForFiveSecondsTest()
		{
			//オリジナルメソッドを退避
			originalWriteText = MonoBrickFirmwareWrapper.Display.LcdWrapper.WriteTextAction;
			originalUpdate = MonoBrickFirmwareWrapper.Display.LcdWrapper.Update;
			originalClear = MonoBrickFirmwareWrapper.Display.LcdWrapper.Clear;
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			currentDisplayInfo.SetValue (controller, new LCD.AlphanumericDisplay("Test"));
			writeTextInfo.SetValue(null, (Action<MonoBrickFirmware.Display.Font, MonoBrickFirmware.Display.Point, string, bool>)this.MyWriteText);
			updateInfo.SetValue (null, (Action<int>)this.MyUpdate);
			clearInfo.SetValue (null, (Action)this.MyClear);
			TimeSpan expected = new TimeSpan(0,0,5);//予想表示時間(5秒)
			//実行
			DateTime before = DateTime.Now;//メソッド呼び出し前の時刻
			showAlphanumericInfo.Invoke(controller, new Object[0]);
			DateTime after = DateTime.Now;//メソッド呼出し後の時刻
			TimeSpan actual = after - before;
			//確認
			Assert.IsTrue (1 > (actual - expected).TotalSeconds &&  (actual - expected).TotalSeconds >=0);
		}

		[Test, Description("文字列表示に失敗した際に、Exceptionをcatchするかどうか確認する"), Category("abnormal")]
		public void CatchExceptionTest()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			Replacer.SetPrivateField<LCD.LCDController>(controller, "sendSignal", new ManualResetEvent (false));
			Replacer.SetPrivateField<LCD.LCDController>(controller, "currentDisplay", null);
			//実行
			showAlphanumericInfo.Invoke(controller, new Object[0]);
			//確認
			Assert.AreEqual(LCD.LCDController.State.End, Replacer.GetPrivateField<LCD.LCDController>(controller, "state"));
		}

		#region EnterPressed Test
		[Test, Description("StateがStartedの時、stateが変更されないか確認する"),Category("normal")]
		public void StartedNotChangeToEndTest()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			Replacer.SetPrivateField<LCD.LCDController>(controller, "sendSignal", new ManualResetEvent (false));
			Replacer.SetPrivateField<LCD.LCDController> (controller, "state", LCD.LCDController.State.Started);
			stateInfo.SetValue (controller, LCD.LCDController.State.Started);
			//実行
			controller.EnterPressed ();
			//確認
			Assert.AreEqual(LCD.LCDController.State.Started, Replacer.GetPrivateField<LCD.LCDController>(controller, "state"));
		}

		[Test, Description("StateがClosedInfoDialogの時、stateをEndに変更するか確認する"),Category("normal")]
		public void ClosedInfoDialogChangeToEndTest()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			Replacer.SetPrivateField<LCD.LCDController>(controller, "sendSignal", new ManualResetEvent (false));
			Replacer.SetPrivateField<LCD.LCDController> (controller, "state", LCD.LCDController.State.ClosedInfoDialog);
			//実行
			controller.EnterPressed ();
			//確認
			Assert.AreEqual(LCD.LCDController.State.End, Replacer.GetPrivateField<LCD.LCDController>(controller, "state"));
		}

		[Test, Description("StateがClearedTextHelloの時、stateをEndに変更するか確認する"),Category("normal")]
		public void ClearedTextHelloChangeToEndTest()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			Replacer.SetPrivateField<LCD.LCDController>(controller, "sendSignal", new ManualResetEvent (false));
			Replacer.SetPrivateField<LCD.LCDController> (controller, "state", LCD.LCDController.State.ClearedTextHello);
			//実行
			controller.EnterPressed ();
			//確認
			Assert.AreEqual(LCD.LCDController.State.End, Replacer.GetPrivateField<LCD.LCDController>(controller, "state"));
		}

		[Test, Description("StateがEndの時、stateをEndのまま変更しないか確認する")]
		public void EndNotChangeToOtherTest()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));
			Replacer.SetPrivateField<LCD.LCDController>(controller, "sendSignal", new ManualResetEvent (false));
			Replacer.SetPrivateField<LCD.LCDController> (controller, "state", LCD.LCDController.State.End);
			//実行
			controller.EnterPressed ();
			//確認
			Assert.AreEqual(LCD.LCDController.State.End, Replacer.GetPrivateField<LCD.LCDController>(controller, "state"));
		}
		#endregion
		//AlphanumericDisplay表示テスト用メソッド

		/// <summary>
		/// <see cref="MonoBrickFirmware.Display.Lcd.WriteText"/>の代わりに置き換えるメソッド。
		/// </summary>
		/// <param name="font">表示する文字のフォントサイズ</param>
		/// <param name="point">画面の座標</param>
		/// <param name="message">表示メッセージ</param>
		/// <param name="color">表示文字色、<c>true</c>なら黒、<c>false</c>なら白で表示する</param>
		public void MyWriteText(MonoBrickFirmware.Display.Font font, MonoBrickFirmware.Display.Point point, string message, bool color)
		{
		}

		/// <summary>
		/// <see cref="MonoBrickFirmware.Display.Lcd.Update"/>の代わりに置き換えるメソッド。
		/// </summary>
		/// <param name="yOffset">y座標のオフセット</param>
		public void MyUpdate(int  yOffset = 0)
		{
		}

		/// <summary>
		/// <see cref="MonoBrickFirmware.Display.Lcd.Clear"/>の代わりに置き換えるメソッド。
		/// </summary>
		public void MyClear()
		{
		}
	}
}


