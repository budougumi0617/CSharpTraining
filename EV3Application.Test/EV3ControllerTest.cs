using System;
using NUnit.Framework;
using EV3Application;

namespace EV3Application.Test
{
	/// <summary>
	/// <see cref="EV3Application.EV3Controller"/>のテストクラス。
	/// </summary>
	[TestFixture]
	public class EV3ControllerTest
	{
		//TODO:EV3Controllerにフィールドを作成し、コンストラクタで中身を入れるように設計を変更する
		//ControlEV3内で、いくつかのクラスをnewしているため、モックが注入できない
		//そのため、テスト環境でEV3Controller.ControlLcdを呼び出すと落ちてしまう
		//このテストケースはデバッグ確認を行うこととする
		/*
		[Test, Description("ControlLCDを呼び出し、機能が実現できているか"), Category("normal")]
		public void CallControlLcdTest()
		{
			//準備
			EV3Controller controller = new EV3Controller();
			//実施、確認
			Assert.DoesNotThrow(
				() => controller.ControlEV3()
			);
		}
		*/
	}
}
