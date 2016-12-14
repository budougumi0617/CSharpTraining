using System;
using System.Reflection;
using NUnit.Framework;
using EV3Application;

namespace EV3Application.Test
{
	[TestFixture]
	public class AlphanumericDisplayTest
	{
		//AlphanumericDisplayのテスト
		//表示するダイアログのメッセージを初期化し、インスタンスを生成する。

		[Test, Description("Messageが初期化されるか")]
		public void AlphanumericDisplayTest001()
		{
			//準備
			string expected = "Test";
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay(expected);
			//実行
			string actual = alphanumericDisplay.Message;
			//確認
			Assert.AreEqual(expected, actual); 
		}

		[Test, Description("AlphanumericDisplayクラスのコンストラクタが生成されるか")]
		public void AlphanumericDisplayTest002()
		{
			//実行
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay("Test");
			//確認
			Assert.IsNotNull (alphanumericDisplay);
		}

		//Showのテスト
		//画面に半角英数字スペースの文字列を表示する。
		//表示するMessageが半角英数字スペース以外を含むときに例外(InvalidOperationException)を出す。

		[Test, Description("Messageに半角英数字スペースのみの時、MonoBrickFirmwareのInfoDialogクラスのshowメソッド呼ばれるか")]
		public void ShowTest001()
		{
			//準備
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay("Test");
			FieldInfo writeTextInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("writeText", BindingFlags.NonPublic | BindingFlags.Static);
			FieldInfo updateInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("update", BindingFlags.NonPublic | BindingFlags.Static);
			FieldInfo clearInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("clear", BindingFlags.NonPublic | BindingFlags.Static);
			LCDControllerTest lcdControllerTest = new LCDControllerTest ();
			writeTextInfo.SetValue(null, (Action<MonoBrickFirmware.Display.Font, MonoBrickFirmware.Display.Point, string, bool>)lcdControllerTest.MyWriteText);
			updateInfo.SetValue (null, (Action<int>)lcdControllerTest.MyUpdate);
			clearInfo.SetValue (null, (Action)lcdControllerTest.MyClear);
			//実行、確認
			alphanumericDisplay.Show();
		}

		[Test, Description("Messageに半角英数字スペース以外がSetされているときにExceptionがthrowされるか")]
		public void ShowTest002()
		{
			//準備
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay("fdui (#) % 6");
			//実行、確認
			Assert.Throws <InvalidOperationException>(
				() => alphanumericDisplay.Show ()
			);
		}
	}
}

