using System;
using MonoBrickFirmware;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display;

namespace EV3Application.LCD
{
	/// <summary>
    /// InfoDialogをLCD画面上に表示するクラス。
    /// <see cref="EV3Application.LCD.AlphanumericDisplayBase"/>を継承する。
	/// </summary>
    public class InfoDialog : AlphanumericDisplayBase
	{
		private string title = "Information";
		
        /// <summary>
        /// コンストラクタ。
        /// ダイアログの内容を設定する。
        /// </summary>
        /// <param name="message">ダイアログの内容</param>
        public InfoDialog (string message)
		{
			Message = message;
		}

        /// <summary>
        /// LCD画面上にInfoDialogを表示する。
        /// </summary>
        /// <exception cref="System.InvalidOperationException"/>
        /// <see cref="EV3Application.LCD.AluphanumericDisplaybase.isAlphanumeic"/>の戻り値が<c>false</c>のときにthrowされる。
        /// </exception>
		public override void Show ()
		{
			if (!(isAlphanumeric())) {
				throw new InvalidOperationException();
			} else {
				MonoBrickFirmware.Display.Dialogs.InfoDialog iDialog = new MonoBrickFirmware.Display.Dialogs.InfoDialog (Message,title);
				iDialog.Show ();
			}
		}

	}
}
