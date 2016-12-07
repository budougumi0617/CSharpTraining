using System;

namespace EV3Application.LCD
{
    /// <summary>
    /// インターフェイス
    /// LCD画面に表示する文字列またはダイアログの内容と、表示するメソッドをメンバとして持つ。
    /// </summary>
	public interface IDisplay
	{
		/// <summary>
		/// 表示する文字列、またはダイアログの内容。
		/// </summary>
        string Message{ get; set;}

        /// <summary>
        /// LCD画面上に文字列、またはダイアログを表示する。
        /// </summary>
		void Show();
	}
}
