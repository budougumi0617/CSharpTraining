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
		#region Constructor Test

		[Test, Description("Messageが初期化されるか確認する"), Category("normal")]
		public void InitialiseMessageTest()
		{
			string expected = "Test";
			LCD.InfoDialog infoDialog = new LCD.InfoDialog (expected);
			string actual = infoDialog.Message;

			Assert.AreEqual (expected, actual); 
		}

		[Test, Description("コンストラクタ実行時に例外が起きていないか確認する"), Category("normal")]
		public void InstanceConstructorTest()
		{
			//実行、確認
			Assert.DoesNotThrow (
				() => new LCD.InfoDialog ("Test")
			);
		}

		#endregion

		#region Show Method Test

		//TODO:InfoDialogをクラスメンバとして持つように、テスト対象クラスの設計を変更する
		//MonoBrickFrimware.Display.InfoDialogをテスト対象メソッド内でnewしているため、外部からモックの注入ができない
		//そのため、このテストケースはデバッグ確認を行うこととする
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

		#endregion
	}
}

