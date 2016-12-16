using System;
using NUnit.Framework;
using EV3Application;

namespace EV3Application.Test
{
	/// <summary>
	/// <see cref="EV3Application.LCD.InfoDialog"/>のテストクラス。
	/// </summary>
	[TestFixture]
	public class InfoDialogTest
	{
		[Test, Description("Messageが初期化されるか確認する"), Category("InfoDialog")]
		public void InfoDIalogTest001()
		{
			string expected = "Test";
			LCD.InfoDialog infoDialog = new LCD.InfoDialog (expected);
			string actual = infoDialog.Message;

			Assert.AreEqual (expected, actual); 
		}

		[Test, Description("InfoDialogクラスのコンストラクタが生成されるか確認する"), Category("InfoDialog")]
		public void InfoDIalogTest002()
		{
			LCD.InfoDialog infoDialog = new LCD.InfoDialog ("Test");

			Assert.IsNotNull (infoDialog);
		}

		//Showのテスト
		//画面に<c>InfoDialog</c>を表示する。
		//表示するMessageSが半角英数字スペース以外を含むときに例外(InvalidOperationException)を出す。

		/*
		[Test, Description("Messageに半角英数字スペースのみの時、MonoBrickFirmwareのInfoDialogクラスのshowメソッド呼ばれるか")]
		public void ShowTest001()
		{
			LCD.InfoDialog infoDialog = new LCD.InfoDialog ("abcde");
			infoDialog.Show ();

		}
	*/
		[Test, Description("Messageに半角英数字スペース以外がSetされているときにExceptionがthrowされるか確認する"), Category("Show")]
		public void ShowTest002()
		{
			LCD.InfoDialog infoDialog = new LCD.InfoDialog ("fdui (#) % 6");

			Assert.Throws <InvalidOperationException>(
				() => infoDialog.Show()
			);
		}
	}
}

