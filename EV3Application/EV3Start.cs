//
// EV3Start.cs
//
// Author:Yojiro Nanameki
//
// Copyright (c) 2016 

using System;

namespace EV3Application
{
	/// <summary>
	/// EV3アプリケーションを開始するクラス。
	/// </summary>
	class EV3Start
	{
		/// <summary>
		/// メインメソッド。
		/// </summary>
		public static void Main()
		{
			EV3Controller controller = new EV3Controller();
			controller.ControlEV3();
		}
	}
}

