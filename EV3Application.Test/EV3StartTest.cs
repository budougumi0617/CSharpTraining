using System;
using NUnit.Framework;
using EV3Application;

namespace EV3Application.Test
{
	[TestFixture]
	public class EV3StartTest
	{
		
		[Test, Description("ControlEV3が呼び出し、アプリケーションが開始できているか")]
		public void MainTest001 ()
		{
			//実施、確認
			Assert.DoesNotThrow (
				() => EV3Start.Main ()
			);
		}
	}
}

