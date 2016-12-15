using System;
using NUnit.Framework;
using EV3Application;

namespace EV3Application.Test
{
	[TestFixture]
	public class EV3ControllerTest
	{
		//ControlEV3のテスト
		//各コントローラーを使用して、機能を実現する。

		[Test, Description("ControlLCDを呼び出し、機能が実現できているか")]
		public void ControlEV3Test001 ()
		{
			//準備
			EV3Controller controller = new EV3Controller();
			//実施、確認
			Assert.DoesNotThrow(
				() => controller.ControlEV3()
			);
		}
	}
}
