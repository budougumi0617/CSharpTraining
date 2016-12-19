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
		#region ConstructorTest
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

		[Test, Description("AlphanumericDisplayクラスのインスタンスが生成されるか確認する"), Category("normal")]
		public void InstanceConstructorTest()
		{
			//実行、確認
			Assert.DoesNotThrow (
				() => new LCD.AlphanumericDisplay ("Test")
			);
		}
		#endregion

		#region ShowTest
		[Test, Description("Messageが半角英数字スペースのみの時、MonoBrickFirmware.Display.Lcdクラスの各メソッド呼ばれるか確認する"), Category("normal")]
		public void CallLcdMethodTest()
		{
			//ラッパーメソッドの情報を取得
			FieldInfo writeTextInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("writeText", BindingFlags.NonPublic | BindingFlags.Static);
			FieldInfo updateInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("update", BindingFlags.NonPublic | BindingFlags.Static);
			FieldInfo clearInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("clear", BindingFlags.NonPublic | BindingFlags.Static);
			//オリジナルメソッドの情報を退避
			Action<MonoBrickFirmware.Display.Font, MonoBrickFirmware.Display.Point, string, bool> originalWriteText = MonoBrickFirmwareWrapper.Display.LcdWrapper.WriteTextAction;
			Action<int> originalUpdate = MonoBrickFirmwareWrapper.Display.LcdWrapper.Update;
			Action originalClear = MonoBrickFirmwareWrapper.Display.LcdWrapper.Clear;
			//フラッグを用意
			bool isWriteTextCalled = false; 
			bool isUpdateCalled = false; 
			bool isClearCalled = false;
			//入れ替えるためのメソッドを用意
			Action<MonoBrickFirmware.Display.Font, MonoBrickFirmware.Display.Point, string, bool> myWriteText 
			= (MonoBrickFirmware.Display.Font font, MonoBrickFirmware.Display.Point point, string message, bool color) => {isWriteTextCalled = true;};
			Action<int> myUpdate = (int yOffset) => {isUpdateCalled = true;};
			Action myClear = () => {isClearCalled = true;};
			//入れ替え
			writeTextInfo.SetValue(null, myWriteText);
			updateInfo.SetValue (null, myUpdate);
			clearInfo.SetValue (null, myClear);
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay("Test");
			//実行
			alphanumericDisplay.Show();
			//確認
			Assert.IsTrue(isWriteTextCalled && isUpdateCalled && isClearCalled);
			//オリジナルメソッドを戻す
			writeTextInfo.SetValue (null, originalWriteText);
			updateInfo.SetValue (null, originalUpdate);
			clearInfo.SetValue (null, originalClear);
		}

		[Test, Description("Messageに半角英数字スペース以外がSetされているときにInvalidOperationExceptionをthrowするか確認する"), Category("normal")]
		public void ThrowInvalidOperationExceptionTest()
		{
			//準備
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay("fdui (#) % 6");
			//実行、確認
			Assert.Throws <InvalidOperationException>(
				() => alphanumericDisplay.Show ()
			);
		}
		#endregion
	}
}

