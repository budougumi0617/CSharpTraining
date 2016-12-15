using System;
using System.Reflection;
using NUnit.Framework;
using EV3Application;

namespace EV3Application.Test
{
	/// <summary>
	///<see cref="EV3Application.LCD.AlphanumericDisplay"/>のテストクラス。
	/// </summary>
	[TestFixture]
	public class AlphanumericDisplayTest
	{
		[Test, Description("Messageが初期化されるか確認する"), Category("AlphanumericDisplay")]
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

		[Test, Description("AlphanumericDisplayクラスのコンストラクタが生成されるか確認する"), Category("AlphanumericDisplay")]
		public void AlphanumericDisplayTest002()
		{
			//実行
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay("Test");
			//確認
			Assert.IsNotNull (alphanumericDisplay);
		}

		[Test, Description("Messageに半角英数字スペースのみの時、MonoBrickFirmwareのInfoDialogクラスのshowメソッド呼ばれるか確認する"), Category("Show")]
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
			Assert.DoesNotThrow(
				() => alphanumericDisplay.Show()
			);
		}

		[Test, Description("Messageに半角英数字スペース以外がSetされているときにExceptionがthrowされるか確認する"), Category("Show")]
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

