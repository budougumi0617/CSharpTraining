using System;
using NUnit.Framework;
using EV3Application;

namespace EV3Application.Test
{
	[TestFixture]
	public class InfoDialogTest
	{
		//InfoDialogのテスト
		//表示するダイアログのメッセージを初期化し、インスタンスを生成する。

		[Test, Description("Messageが初期化されるか")]
		public void InfoDIalogTest001()
		{
			string expected = "Test";
			LCD.InfoDialog infoDialog = new LCD.InfoDialog (expected);
			string actual = infoDialog.Message;
			Assert.AreEqual (expected, actual); 
		}

		[Test, Description("InfoDialogクラスのコンストラクタが生成されるか")]
		public void InfoDIalogTest002()
		{
			//LCD.InfoDialog infoDialog = new LCD.InfoDialog ("Test");
			//Assert.IsNotNull (infoDialog);
			LCD.InfoDialog info = new LCD.InfoDialog ();
			Assert.IsNull (info);
		}
		/*
		[Test, Description("Messageに半角英数字スペース以外がSetされているときにExceptionがthrowされるか")]
		public void ShowTestThrowException ()
		{
			LCD.InfoDialog infoDialog = new LCD.InfoDialog ("324jij3　");
			infoDialog.Show ();
			Assert.Throws (InvalidOperationException);
		}

		[Test, Description()]
		public void InfoDIalogTest ()
		{
			string expected = "Test";
			LCD.InfoDialog infoDialog = new LCD.InfoDialog (expected);
			string actual = infoDialog.Message;
			Assert.AreEqual (expected, actual); 
		}*/
	}
}

