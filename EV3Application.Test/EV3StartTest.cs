using System;
using NUnit.Framework;
using EV3Application;

namespace EV3Application.Test
{
	/// <summary>
	/// <see cref="EV3Application.EV3Start"/>のテストクラス。
	/// </summary>
	[TestFixture]
	public class EV3StartTest
	{
		//TODO:EV3Controllerにフィールドを作成し、コンストラクタで中身を入れるように設計を変更する
		//EV3Start.Mainの呼び出し先のControlEV3内で、いくつかのクラスをnewしているため、モックが注入できない
		//そのため、テスト環境でEV3Start.Mainを呼び出すと落ちてしまう
		//このテストケースはデバッグ確認を行うこととする
		/*
		[Test, Description("ControlEV3が呼び出し、アプリケーションが開始できているか確認する"), Category("normal")]
		public void CallControlEV3Test()
		{
			//実施、確認
			Assert.DoesNotThrow (
				() => EV3Start.Main ()
			);
		}*/
	}
}

