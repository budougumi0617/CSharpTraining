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
		//テスト対象メソッド内で、ハード環境に依存するクラスのインスタンスを生成しているため、外部からモックの注入ができず、実機上でしか動かすことができない
		//そのため、テスト環境でこのケースを実施すると落ちてしまう
		//よって、このテスト対象メソッドはデバッグ確認によって、品質確認を行う
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
