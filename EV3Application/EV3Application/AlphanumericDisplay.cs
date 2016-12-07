using System;
using MonoBrickFirmware;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display;

namespace EV3Application.LCD
{
    /// <summary>
    /// 半角英数字の文字列をLCD画面上に表示するクラス。
    /// <see cref="EV3Application.LCD.AlphanumericDisplayBase"/>を継承する。
    /// </summary>
	public class AlphanumericDisplay : AlphanumericDisplayBase
	{
		private int height = 50;
		private int width = 53;
		private Font font = Font.MediumFont;

        /// <summary>
        /// コンストラクタ。
        /// 文字列の内容を設定する。
        /// </summary>
        /// <param name="message">文字列の内容</param>
		public AlphanumericDisplay (string message)
		{
			Message = message;
		}

        /// <summary>
        /// LCD画面上に半角英数字の文字列を表示する。
        /// </summary>
        /// <exception cref="System.InvalidOperationException"/>
        /// <see cref="EV3Application.LCD.AluphanumericDisplaybase.isAlphanumeic"/>の戻り値が<c>false</c>のときにthrowされる。
        /// </exception>
		public override void Show ()
		{
			if (!(isAlphanumeric())) {
				throw new InvalidOperationException();
			} else {
				Lcd.Clear ();
				Lcd.WriteText (font, new Point (height, width), Message, true);
				Lcd.Update ();
			}
		}
	}
}
