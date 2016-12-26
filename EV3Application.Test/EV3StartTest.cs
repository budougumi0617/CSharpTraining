//
// EV3StartTest.cs
//
// Author:Yojiro Nanameki
//
// Copyright (c) 2016 

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
		//TODO:EV3Application.EV3Controllerにフィールドを作成し、コンストラクタで中身を入れるように設計を変更する
		//テスト対象メソッドの呼び出し先で、ハード環境に依存するクラスのインスタンスを生成しているため、外部からモックの注入ができず、実機上でしか動かすことができない
		//そのため、テスト環境でこのケースを実施すると落ちてしまう
		//よって、このテスト対象メソッドはデバッグ確認によって、品質確認を行う
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

