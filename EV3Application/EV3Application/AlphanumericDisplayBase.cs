using System;
using System.Text.RegularExpressions;

namespace EV3Application.LCD
{
	/// <summary>
	/// 半角英数字スペースを画面に表示する抽象クラス。
	/// </summary>
	public abstract class AlphanumericDisplayBase : IDisplay
	{
		/// <summary>
		/// 画面に表示するメッセージ。
		/// </summary>
		public string Message{get; set;}

		/// <summary>
		/// 半角英数字スペースを画面に表示する。
		/// </summary>
		/// <exception cref="System.InvalidOperationException">
		/// ディスプレイが半角英数字スペース以外を含む場合に例外を出す。
		/// </exception>
		public abstract void Show();

		/// <summary>
		/// メッセージが半角英数字スペースのみかどうかを判定する。
		/// </summary>
		/// <returns>
		/// <see cref="EV3Application.LCD.AlphanumericDisplayBase.Message"/>が半角英数字スペースのみで構成されていれば<c>true</c>を返す。</br>
		/// <see cref="EV3Application.LCD.AlphanumericDisplayBase.Message"/>が半角英数字スペース以外が含まれていれば<c>false</c>を返す。
		/// </returns>
		protected bool isAlphanumeric()
		{
			return Regex.IsMatch(Message,"^[a-zA-Z0-9\\s]+$");
		}
	}
}
