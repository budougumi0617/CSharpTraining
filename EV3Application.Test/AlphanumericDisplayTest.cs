using System;
using System.Reflection;
using NUnit.Framework;
using MonoBrickFirmwareWrapper.Utilities;
using EV3Application;

namespace EV3Application.Test
{
	/// <summary>
	///<see cref="EV3Application.LCD.AlphanumericDisplay"/>のテストクラス。
	/// </summary>
	[TestFixture]
	public class AlphanumericDisplayTest
	{
		//フラッグを用意
		private bool isWriteTextCalled;
		private bool isUpdateCalled; 
		private bool isClearCalled;

		[SetUp, Description("Wrapperクラスのメソッドを入れ替える")]
		public void TestSetUp()
		{
			//フラッグの初期化
			isWriteTextCalled = false; 
			isUpdateCalled = false; 
			isClearCalled = false;
			//メソッド入れ替え
			Replacer.ReplaceWrapperMethod(typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper), "writeText", 
				(MonoBrickFirmware.Display.Font font, MonoBrickFirmware.Display.Point point, string message, bool color) => {isWriteTextCalled = true;});
			Replacer.ReplaceWrapperMethod(typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper), "update", (int yOffset) => {isUpdateCalled = true;});
			Replacer.ReplaceWrapperMethod(typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper), "clear", () => {isClearCalled = true;});
		}

		[TearDown, Description("Wrapperクラスのメソッドを元に戻す")]
		public void TestTearDown()
		{
			//メソッドを元に戻す
			Replacer.RestorePrivateStaticField(typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper), "writeText");
			Replacer.RestorePrivateStaticField(typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper), "update");
			Replacer.RestorePrivateStaticField(typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper), "clear");
			//フラッグの初期化
			isWriteTextCalled = false; 
			isUpdateCalled = false; 
			isClearCalled = false;
		}

		#region Constructor Test
		[Test, Description("Messageが初期化されるか確認する"), Category("normal")]
		public void InitialiseMessageTest()
		{
			//準備
			string expected = "Test";
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay(expected);
			//実行
			string actual = alphanumericDisplay.Message;
			//確認
			Assert.AreEqual(expected, actual);
		}

		[Test, Description("コンストラクタ実行時に例外が起きていないか確認する"), Category("normal")]
		public void InstanceConstructorTest()
		{
			//実行、確認
			Assert.DoesNotThrow (
				() => new LCD.AlphanumericDisplay ("Test")
			);
		}
		#endregion

		#region Show Method Test
		[Test, Description("Messageが半角英数字スペースのみの時、MonoBrickFirmware.Display.Lcdクラスの各メソッド呼ばれるか確認する"), Category("normal")]
		public void CallLcdMethodTest()
		{
			//準備
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay("Test");
			alphanumericDisplay.Message =  "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ 0123456789";
			//実行
			alphanumericDisplay.Show();
			//確認
			Assert.IsTrue(isWriteTextCalled && isUpdateCalled && isClearCalled);
		}

		[Test, Description("Messageに半角英数字スペース以外がSetされているときにInvalidOperationExceptionをthrowするか確認する"), Category("abnormal")]
		public void ThrowInvalidOperationExceptionTest()
		{
			//準備
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay("Test");
			alphanumericDisplay.Message =  "fdui (#) % 6";
			//実行、確認
			Assert.Throws <InvalidOperationException>(
				() => alphanumericDisplay.Show ()
			);
		}
		#endregion
	}
}

