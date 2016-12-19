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
		#region ConstructorTest
		[Test, Description("Messageが初期化されるか確認する"), Category("normal")]
		public void InitialiseMessageTest()
		{
			string expected = "Test";
			LCD.InfoDialog infoDialog = new LCD.InfoDialog (expected);
			string actual = infoDialog.Message;

			Assert.AreEqual (expected, actual); 
		}

		[Test, Description("InfoDialogクラスのインスタンスが生成されるか確認する"), Category("normal")]
		public void InstanceConstructorTest()
		{
			LCD.InfoDialog infoDialog = new LCD.InfoDialog ("Test");

			Assert.IsNotNull (infoDialog);
		}
		#endregion

		/*
		[Test, Description("Messageに半角英数字スペースのみの時、MonoBrickFirmwareのInfoDialogクラスのshowメソッド呼ばれるか確認する"), Category("normal")]
		public void CallShowTest()
		{
			LCD.InfoDialog infoDialog = new LCD.InfoDialog ("abcde");
			infoDialog.Show ();
		}
	*/
		[Test, Description("Messageに半角英数字スペース以外がSetされているときにExceptionがthrowされるか確認する"), Category("abnormal")]
		public void ThrowInvalidOperationExceptionTest()
		{
			LCD.InfoDialog infoDialog = new LCD.InfoDialog ("fdui (#) % 6");

			Assert.Throws <InvalidOperationException>(
				() => infoDialog.Show()
			);
		}
	}
}

