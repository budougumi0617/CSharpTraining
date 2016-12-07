using System;
using System.Text.RegularExpressions;

namespace EV3Application.LCD
{
    /// <summary>
    /// 抽象クラス。
    /// 半角英数字のみをLCD画面上に表示するクラス。
    /// <see cref="EV3Application.LCD.IDisplay"/>を実現する。
    /// </summary>
	public abstract class AlphanumericDisplayBase : IDisplay
	{
		/// <summary>
		/// <see cref="EV3Application.IDisplay.Message"/>の実装。
		/// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 抽象メソッド。
        /// <see cref="EV3Application.IDisplay.Show"/>の実装。
        /// </summary>
		public abstract void Show();

		//if Message contains only 0-9, a-z, and A-Z, return true

        /// <summary>
        /// <c>Message</c>が半角英数字または半角スペースのみかどうかを判定する。
        /// </summary>
        /// <returns><c>Message</c>が半角英数字スペースのみで構成されているか</returns>
		protected bool isAlphanumeric(){

			return Regex.IsMatch(Message,"^[a-zA-Z0-9\\s]+$");
		}
	}
}
