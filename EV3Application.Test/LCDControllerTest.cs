//
// LCDControllerTest.cs
//
// Author:Yojiro Nanameki
//
// Copyright (c) 2016 

using System;
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
		/// <summary>
		/// <see cref="EV3Application.LCD.LCDController.showAlphanumericDisplay"/>の情報
		/// </summary>
		private MethodInfo showAlphanumericInfo;

		[TestFixtureSetUp, Description("リフレクションによって、テスト対象メンバの情報をフィールドに代入する")]
		public void TestInitialiser()
		{
			showAlphanumericInfo = (typeof(LCD.LCDController)).GetMethod("showAlphanumericDisplay", BindingFlags.NonPublic | BindingFlags.Instance);
		}

		[SetUp, Description("Wrapperに格納されているメソッドをテスト用のメソッドに入れ替える")]
		public void ReplaceMethod()
		{
			//メソッド入れ替え
			Replacer.ReplaceWrapperMethod(typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper), "writeText", 
				(MonoBrickFirmware.Display.Font font, MonoBrickFirmware.Display.Point point, string message, bool color) => {});
			Replacer.ReplaceWrapperMethod(typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper), "update", (int yOffset) => {});
			Replacer.ReplaceWrapperMethod(typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper), "clear", () => {});
		}

		[TearDown, Description("退避させていたメソッドを戻す")]
		public void RepositMethod()
		{
			//メソッドを元に戻す
			Replacer.RestorePrivateStaticField(typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper), "writeText");
			Replacer.RestorePrivateStaticField(typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper), "update");
			Replacer.RestorePrivateStaticField(typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper), "clear");
		}

		/// <summary>
		/// <see cref="EV3Application.LCD.LCDController"/>のインスタンスを生成し、テストケースごとにセットアップする
		/// </summary>
		/// <returns>LCDControllerのインスタンス</returns>
		/// <param name="sendSignal"><see cref="EV3Application.LCD.LCDController.sendSignal"/>に設定するオブジェクト</param>
		/// <param name="state"><see cref="EV3Application.LCD.LCDController.state"/>に設定するオブジェクト</param>
		/// <param name="currentDisplay"><see cref ="EV3Application.LCD.LCDController.current display"/>に設定するオブジェクト</param>
		public LCD.LCDController SetUpTestTarget
		(ManualResetEvent sendSignal, LCD.LCDController.State state, LCD.IDisplay currentDisplay = null)
		{
			LCD.LCDController controller = new LCD.LCDController (new ManualResetEvent(false));

			Replacer.SetPrivateField<LCD.LCDController>(controller, "sendSignal", sendSignal);
			Replacer.SetPrivateField<LCD.LCDController>(controller, "state", state);
			Replacer.SetPrivateField<LCD.LCDController>(controller, "currentDisplay", currentDisplay);

			return controller;
		}

		#region Constructor Test
		[Test, Description("フィールドstateが初期化されているか確認する"), Category("normal")]
		public void InitialiseStateTest()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController(new ManualResetEvent (false));
			//実行、確認
			Assert.AreEqual(LCD.LCDController.State.Started, Replacer.GetPrivateField(controller, "state"));
		}

		[Test, Description("フィールドsendSignalが初期化されているか確認する"), Category("normal")]
		public void InitialiseSendSignalTest()
		{
			//準備
			ManualResetEvent mre = new ManualResetEvent (false);
			LCD.LCDController controller = new LCD.LCDController(mre);
			//実行、確認
			Assert.AreSame(mre, Replacer.GetPrivateField(controller, "sendSignal"));
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
		public void ChangeToEndTest()
		{
			//準備
			LCD.LCDController controller = SetUpTestTarget(new ManualResetEvent(false), LCD.LCDController.State.Started);
			//実行
			controller.ControlLCD();
			//確認
			Assert.AreEqual(LCD.LCDController.State.End, Replacer.GetPrivateField(controller, "state"));
		}

		[Test, Description("stateがStartedの時、例外なく処理が終了したか確認する"), Category("normal")]
		public void ExecuteFromStartedTest()
		{
			//準備
			LCD.LCDController controller = SetUpTestTarget(new ManualResetEvent(false), LCD.LCDController.State.Started);
			//実行、確認
			Assert.DoesNotThrow(
				() => controller.ControlLCD()
			);
		}

		[Test, Description("stateがClosedInfoDialogの時、例外なく処理が終了したか確認する"), Category("normal")]
		public void ExecuteFromClosedInfoDialogTest()
		{
			//準備
			LCD.LCDController controller = SetUpTestTarget(new ManualResetEvent(false), LCD.LCDController.State.ClosedInfoDialog);
			//実行、確認
			Assert.DoesNotThrow (
				() => controller.ControlLCD ()
			);
		}

		[Test, Description("stateがClearedTextHelloの時、例外なく処理が終了したか確認する"), Category("normal")]
		public void ExecuteFromClearedTextHelloTest()
		{
			//準備
			LCD.LCDController controller = SetUpTestTarget(new ManualResetEvent(false), LCD.LCDController.State.ClearedTextHello, new LCD.AlphanumericDisplay("Test"));
			//実行、確認
			Assert.DoesNotThrow (
				() => controller.ControlLCD ()
			);
		}

		[Test, Description("stateがEndの時、stateを変更せずに終了したか確認する"), Category("normal")]
		public void ExecuteFromEndTest()
		{
			//準備
			LCD.LCDController controller = SetUpTestTarget(new ManualResetEvent(false), LCD.LCDController.State.End);
			//実行
			controller.ControlLCD();
			//確認
			Assert.AreEqual(LCD.LCDController.State.End, Replacer.GetPrivateField(controller, "state"));
		}

		[Test, Description("stateがStarted、ClosedInfoDialog、ClearedTextHello、Endのどれでもない時、stateがEndに変更されているか確認する"), Category("normal")]
		public void ExecuteFromInvalidStateTest()
		{
			//準備
			LCD.LCDController controller = SetUpTestTarget(new ManualResetEvent(false), LCD.LCDController.State.Started);
			Replacer.SetPrivateField(controller, "state", 4);//Stateの定義はEnumの3までのため、それ以上の値を入れると強制的にEndに変更されるはず
			//実行
			controller.ControlLCD();
			//確認
			Assert.AreEqual(LCD.LCDController.State.End, Replacer.GetPrivateField(controller, "state"));
		}
		#endregion

		#region showInfoDialog Test

		//TODO:EV3Application.LCD.InfoDialogにフィールドを作成し、コンストラクタで中身を入れるように設計を変更する
		//テスト対象メソッド内で、ハード環境に依存するクラスのインスタンスを生成しているため、外部からモックの注入ができず、実機上でしか動かすことができない
		//そのため、テスト環境でこのケースを実施すると落ちてしまう
		//よって、このテスト対象メソッドはデバッグ確認によって、品質確認を行う
		/*
		[Test, Description("InfoDialogを表示したか確認する"), Category("normal")]
		public void ShowInfoDialogTest()
		{
			//準備
			LCD.LCDController controller = new LCD.LCDController(new ManualResetEvent(false));
			MethodInfo showDialogInfo = (typeof(LCD.LCDController)).GetMethod ("showInfoDialog", BindingFlags.NonPublic | BindingFlags.Instance);
		}
		*/
		#endregion

		#region showAlphanumericDisplay Test
		[Test, Description("LCD.AlphanumericDisplay.Showを例外なく呼び出したか確認する"), Category("normal")]
		public void CallAlphanumericDisplayShowTest()
		{
			//準備
			LCD.LCDController controller = SetUpTestTarget(new ManualResetEvent(false), LCD.LCDController.State.ClearedTextHello, new LCD.AlphanumericDisplay("Test"));
			//実行、確認
			Assert.DoesNotThrow(
				() => showAlphanumericInfo.Invoke(controller, new Object[0])
			);
		}

		[Test, Description("文字列を5秒間(誤差が+1秒未満)表示したか確認する"), Category("normal")]
		public void ShowMessageForFiveSecondsTest()
		{
			//準備
			LCD.LCDController controller = SetUpTestTarget(new ManualResetEvent(false), LCD.LCDController.State.ClearedTextHello, new LCD.AlphanumericDisplay("Test"));
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
			LCD.LCDController controller = SetUpTestTarget(new ManualResetEvent(false), LCD.LCDController.State.ClosedInfoDialog);
			//実行
			showAlphanumericInfo.Invoke(controller, new Object[0]);
			//確認
			Assert.AreEqual(LCD.LCDController.State.End, Replacer.GetPrivateField<LCD.LCDController>(controller, "state"));
		}
		#endregion

		#region EnterPressed Test
		[Test, Description("StateがStartedの時、stateが変更されないか確認する"),Category("normal")]
		public void StartedNotChangeToEndTest()
		{
			//準備
			LCD.LCDController controller = SetUpTestTarget(new ManualResetEvent(false), LCD.LCDController.State.Started);
			//実行
			controller.EnterPressed ();
			//確認
			Assert.AreEqual(LCD.LCDController.State.Started, Replacer.GetPrivateField<LCD.LCDController>(controller, "state"));
		}

		[Test, Description("StateがClosedInfoDialogの時、stateをEndに変更するか確認する"),Category("normal")]
		public void ClosedInfoDialogChangeToEndTest()
		{
			//準備
			LCD.LCDController controller = SetUpTestTarget(new ManualResetEvent(false), LCD.LCDController.State.ClosedInfoDialog);
			//実行
			controller.EnterPressed ();
			//確認
			Assert.AreEqual(LCD.LCDController.State.End, Replacer.GetPrivateField<LCD.LCDController>(controller, "state"));
		}

		[Test, Description("StateがClearedTextHelloの時、stateをEndに変更するか確認する"),Category("normal")]
		public void ClearedTextHelloChangeToEndTest()
		{
			//準備
			LCD.LCDController controller = SetUpTestTarget(new ManualResetEvent(false), LCD.LCDController.State.ClearedTextHello);
			//実行
			controller.EnterPressed ();
			//確認
			Assert.AreEqual(LCD.LCDController.State.End, Replacer.GetPrivateField<LCD.LCDController>(controller, "state"));
		}

		[Test, Description("StateがEndの時、stateをEndのまま変更しないか確認する")]
		public void EndNotChangeToOtherTest()
		{
			//準備
			LCD.LCDController controller = SetUpTestTarget(new ManualResetEvent(false), LCD.LCDController.State.End);
			//実行
			controller.EnterPressed ();
			//確認
			Assert.AreEqual(LCD.LCDController.State.End, Replacer.GetPrivateField<LCD.LCDController>(controller, "state"));
		}
		#endregion
	}
}
