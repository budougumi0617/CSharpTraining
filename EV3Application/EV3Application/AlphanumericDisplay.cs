using System;
using MonoBrickFirmware;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display;

namespace EV3Application.LCD
{
	/// <summary>
	/// 半角英数字スペースで構成された文字列を画面に表示するクラス。
	/// </summary>
	public class AlphanumericDisplay : AlphanumericDisplayBase
	{
		private int height = 50; //画面上のy座標
		private int width = 53; //画面上のx座標
		private Font font = Font.MediumFont; //表示する文字のフォントサイズ

		/// <summary>
		/// 表示する文字列を初期化し、インスタンスを生成する。
		/// </summary>
		/// <param name="message">表示する文字列</param>
		public AlphanumericDisplay(string message)
		{
			Message = message;
		}

		/// <summary>
		/// 画面に半角英数字スペースの文字列を表示する。
		/// </summary>
		/// <exception cref="System.InvalidOperationException">
		/// 表示する文字列が半角英数字スペース以外を含むときに例外を出す。
		/// </exception>
		public override void Show()
		{
			if(!(isAlphanumeric())){
				throw new InvalidOperationException();
			}

			Lcd.Clear();
			Lcd.WriteText(font, new Point(height, width), Message, true);
			Lcd.Update();
		}
	}
}
