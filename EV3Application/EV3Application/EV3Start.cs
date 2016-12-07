
using System;

namespace EV3Application
{
	/// <summary>
	/// EV3アプリケーションを開始と終了を管理する。
	/// </summary>
	class EV3Start
	{
		/// <summary>
		/// アプリケーションの開始点であり、開始と終了を管理する。
		/// </summary>
		public static void Main ()
		{
			EV3Controller controller = new EV3Controller ();
			controller.ControlEV3 ();
		}
	}
}

