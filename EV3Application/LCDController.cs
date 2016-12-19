using System;
using System.Threading;
using MonoBrickFirmware;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display;

namespace EV3Application.LCD
{
	/// <summary>
	/// LCD画面を制御するクラス。
	/// </summary>
	public class LCDController
	{
		/// <summary>
		/// LCD画面の状態を表す。
		/// </summary>
		public enum State
		{
			Started, //アプリケーション開始後
			ClosedInfoDialog, //InfoDialog消去後
			ClearedTextHello, //Helloメッセージ消去後
			End //アプリケーション終了
		}

		private State state; //LCD画面の状態
		private ManualResetEvent sendSignal; //スレッドの停止、開始を知らせるイベント
		private IDisplay currentDisplay; //表示しているディスプレイ

		/// <summary>
		/// LCD画面の状態をアプリケーション開始後状態に初期化し、インスタンスを生成する。
		/// </summary>
		/// <param name="sendSignal">スレッドの停止、開始を知らせるイベント</param>
		public LCDController (ManualResetEvent sendSignal)
		{
			state = State.Started;
			this.sendSignal = sendSignal;
		}

		/// <summary>
		/// <see cref="state"/>を参照して、LCD画面をコントロールする。
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item>
		/// <description><see cref="EV3Application.LCDController.State.Started"/>の時、<c>InfoDialog</c>を表示する。</description>
		/// </item>
		/// <item>
		/// <description><see cref="EV3Application.LCDController.State.ClosedInfoDialog"/>の時、文字列<c>Hello</c>を表示する。</description>
		/// </item>
		/// <item>
		/// <description><see cref="EV3Application.LCDController.State.ClearedTextHello"/>の時、文字列<c>Good Bye</c>を表示する。</description>
		/// </item>
		/// <item>
		/// <description>上記以外の時、何もせずに終了する。
		/// </item>
		/// </list>
		/// </remarks>
		public void ControlLCD()
		{
			while(state != State.End)
			{
				switch(state)
				{

				case State.Started:
					showInfoDialog ();
					state = State.ClosedInfoDialog;
					break;

				case State.ClosedInfoDialog:
					currentDisplay = new AlphanumericDisplay("Hello");
					showAlphanumericDisplay();
					if (state == State.End)
					{
						return;
					}
					else
					{
						state = State.ClearedTextHello;
					}
					break;

				case State.ClearedTextHello:
					currentDisplay.Message = "Good Bye";
					showAlphanumericDisplay();
					state = State.End;
					break;

				default:
					state = State.End;
					break;
				}
			}
		}
									
		/// <summary>
		/// LCD画面上に<c>InfoDialog</c>を表示する。
		/// </summary>
		/// <remarks>
		/// ユーザー入力があるまでこのメソッドから戻らない。</br>
		/// エラーが発生しなければ、InfoDialog表示する。</br>
		/// エラーが発生した場合は、<see cref="EV3Application.LCDController.state"/>を<see cref="EV3Application.LCDController.State.End"/>に変更する。
		/// </remarks>
		private void showInfoDialog()
		{
			currentDisplay = new InfoDialog("Please Push The EnterButton");
			try
			{
				currentDisplay.Show();
			}
			catch(Exception e)
			{
				state = State.End;
			}
		}

		/// <summary>
		/// LCD画面上に文字列を表示する。
		/// </summary>
		/// <remarks>
		/// ユーザ入力があるか、5秒経過するまでこのメソッドから戻らない。</br>
		/// エラーが発生しなければ、文字列を5秒間表示する。</br>
		/// エラーが発生した場合は、<see cref="EV3Application.LCDController.state"/>を<see cref="EV3Application.LCDController.State.End"/>に変更する。
		/// </remarks>
		private void showAlphanumericDisplay()
		{
			try
			{
				currentDisplay.Show();
				sendSignal.Reset();
				sendSignal.WaitOne(5000);
			}
			catch(Exception e)
			{
				state = State.End;
			}
		}

		/// <summary>
		/// EnterButtonが押下された際の処理を実行する。
		/// </summary>
		/// <remarks>
		/// <see cref="state"/>が<see cref="EV3Application.LCDController.State.Started"/>の時、何もしない。</br>
		/// <see cref="state"/>が<see cref="EV3Application.LCDController.State.Started"/>以外の時、<see cref="EV3Application.LCDController.State.End"/>に変更し、処理待ち状態を解除する。
		/// </remarks>
		public void EnterPressed()
		{
			if(state != State.Started)
			{
				state = State.End;
				sendSignal.Set();
			}
		}
	}
}

