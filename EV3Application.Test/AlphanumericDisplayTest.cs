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
		private bool isWriteTextCalled = false; //<see cref="MonoBrickFirmwareWrapper.Display.LcdWrapper.writeText"/>が呼ばれたかどうかを確認するための変数
		private bool isUpdateCalled = false; //<see cref="MonoBrickFirmwareWrapper.Display.LcdWrapper.update"/>が呼ばれたかどうかを確認するための変数
		private bool isClearCalled = false; //<see cref="MonoBrickFirmwareWrapper.Display.LcdWrapper.clear"/>が呼ばれたかどうかを確認するための変数

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
			//実行
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay("Test");
			//確認
			Assert.IsNotNull (alphanumericDisplay);
		}

		[Test, Description("Messageが半角英数字スペースのみの時、MonoBrickFirmware.Display.Lcdクラスの各メソッド呼ばれるか確認する"), Category("normal")]
		public void CallLcdMethodTest()
		{
			//準備
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay("Test");
			FieldInfo writeTextInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("writeText", BindingFlags.NonPublic | BindingFlags.Static);
			FieldInfo updateInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("update", BindingFlags.NonPublic | BindingFlags.Static);
			FieldInfo clearInfo = (typeof(MonoBrickFirmwareWrapper.Display.LcdWrapper)).GetField("clear", BindingFlags.NonPublic | BindingFlags.Static);
			writeTextInfo.SetValue(null, (Action<MonoBrickFirmware.Display.Font, MonoBrickFirmware.Display.Point, string, bool>)this.MyWriteText);
			updateInfo.SetValue (null, (Action<int>)this.MyUpdate);
			clearInfo.SetValue (null, (Action)this.MyClear);
			//実行
			alphanumericDisplay.Show();
			//確認
			Assert.IsTrue(this.isWriteTextCalled && this.isUpdateCalled && this.isClearCalled);
		}

		[Test, Description("Messageに半角英数字スペース以外がSetされているときにInvalidOperationExceptionをthrowするか確認する"), Category("abnormal")]
		public void ThrowInvalidOperationExceptionTest()
		{
			//準備
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay("fdui (#) % 6");
			//実行、確認
			Assert.Throws <InvalidOperationException>(
				() => alphanumericDisplay.Show ()
			);
		}

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
			this.isWriteTextCalled = true;
		}

		/// <summary>
		/// <see cref="MonoBrickFirmware.Display.Lcd.Update"/>の代わりに置き換えるメソッド。
		/// </summary>
		/// <param name="yOffset">y座標のオフセット</param>
		public void MyUpdate(int  yOffset = 0)
		{
			this.isUpdateCalled = true;
		}

		/// <summary>
		/// <see cref="MonoBrickFirmware.Display.Lcd.Clear"/>の代わりに置き換えるメソッド。
		/// </summary>
		public void MyClear()
		{
			this.isClearCalled = true;
		}
	}
}

