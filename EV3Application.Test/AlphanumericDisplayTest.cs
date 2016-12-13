using System;
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
			string expected = "Test";
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay(expected);
			string actual = alphanumericDisplay.Message;

			Assert.AreEqual(expected, actual); 
		}

		[Test, Description("InfoDialogクラスのコンストラクタが生成されるか")]
		public void AlphanumericDisplayTest002()
		{
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay("Test");

			Assert.IsNotNull (alphanumericDisplay);
		}

		//Showのテスト
		//画面に半角英数字スペースの文字列を表示する。
		//表示するMessageが半角英数字スペース以外を含むときに例外(InvalidOperationException)を出す。

		/*
		[Test, Description("Messageに半角英数字スペースのみの時、MonoBrickFirmwareのInfoDialogクラスのshowメソッド呼ばれるか")]
		public void ShowTest001()
		{
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay("Test");
			alphanumericDisplay.Show();

		}
		*/
		[Test, Description("Messageに半角英数字スペース以外がSetされているときにExceptionがthrowされるか")]
		public void ShowTest002()
		{
			LCD.AlphanumericDisplay alphanumericDisplay = new LCD.AlphanumericDisplay("fdui (#) % 6");

			Assert.Throws <InvalidOperationException>(
				() => alphanumericDisplay.Show ()
			);
		}
	}
}

