using System;
using EV3Application.LCD;

namespace EV3Application.Tests
{
	/// <summary>
	/// <see cref="EV3Application.LCD.AlphanumericDisplayBase"/>のラッパークラス。
	/// </summary>
	public class AlphanumericDisplayBaseWrapper : AlphanumericDisplayBase
	{
		/// <summary>
		/// 画面に表示する。
		/// 実際に使用しないので実装していない。
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public override void Show()
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// <see cref="EV3Application.LCD.AlphanumericDisplayBase.isAlphanumeric"/>のラッパー。
		/// </summary>
		/// <returns>
		/// <see cref="EV3Application.LCD.AlphanumericDisplayBase.Message"/>が半角英数字スペースのみで構成されていれば<c>true</c>を返す。</br>
		/// <see cref="EV3Application.LCD.AlphanumericDisplayBase.Message"/>が半角英数字スペース以外が含まれていれば<c>false</c>を返す。
		/// </returns>
		public bool isAlphanumericWrapper()
		{
			return base.isAlphanumeric ();
		}
	}
}