using System;
using MonoBrickFirmware;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display;

namespace EV3Application.LCD
{
	/// <summary>
	/// InfoDialogを画面に表示するクラス。
	/// </summary>
	public class InfoDialog : AlphanumericDisplayBase
	{
		private string title = "Information"; //ダイアログのタイトル
		
		/// <summary>
		/// 表示するダイアログのメッセージを初期化し、インスタンスを生成する。
		/// </summary>
		/// <param name="message">ダイアログのメッセージ</param>
		public InfoDialog(string message)
		{
			Message = message;
		}

		/// <summary>
		/// 画面にInfoDialogを表示する。
		/// </summary>
		/// <exception cref="System.InvalidOperationException">
		/// 表示するダイアログのメッセージが半角英数字スペース以外を含むときに例外を出す。
		/// </exception>
		public override void Show()
		{
			if(!(isAlphanumeric())){
				throw new InvalidOperationException();
			}

			MonoBrickFirmware.Display.Dialogs.InfoDialog iDialog = new MonoBrickFirmware.Display.Dialogs.InfoDialog(Message,title);
			iDialog.Show();
		}

	}
}
