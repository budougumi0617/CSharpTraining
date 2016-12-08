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
		/// テスト用。
		/// LCD画面の状態をアプリケーション開始後状態に初期化し、インスタンスを生成する。
		/// </summary>
		/// <param name="state">LCD画面の状態</param>
		/// <param name="sendSignal">スレッドの停止、開始を知らせるイベント</param>
		/// <param name="currentDisplay">表示しているディスプレイ</param>
		internal LCDController(State state, ManualResetEvent sendSignal, IDisplay currentDisplay)
		{
			this.state = state;
			this.sendSignal = sendSignal;
			this.currentDisplay = currentDisplay;
		}

		/// <summary>
		/// <see cref="state"/>を参照して、LCD画面をコントロールする。
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item>
		/// <description><see cref="EV3Application.LCDController.State.Started"/>の時、InfoDialogを表示する。</description>
		/// </item>
		/// <item>
		/// <description><see cref="EV3Application.LCDController.State.ClosedInfoDialog"/>の時、文字列Helloを表示する。</description>
		/// </item>
		/// <item>
		/// <description><see cref="EV3Application.LCDController.State.ClearedTextHello"/>の時、文字列Good Byeを表示する。</description>
		/// </item>
		/// <item>
		/// <description>上記以外の時、何もせずreturnする。
		/// </item>
		/// </list>
		/// </remarks>
		public void ControlLCD()
		{

			while(state != State.End){
				switch(state){

				case State.Started:
					showInfoDialog();
					break;

				case State.ClosedInfoDialog:
					currentDisplay = new AlphanumericDisplay("Hello");
					showAlphanumericDisplay();
					if (state == State.END){
						return;
					}
					else{
						state = State.CLEAREDTEXTHELLO;
					}
					break;

				case State.ClearedTextHello:
					currentDisplay.Message = "Good Bye";
					showAlphanumericDisplay();
					state = State.END;
					break;

				default:
					state = State.END;
					break;
				}
			}
		}
									
		/// <summary>
		/// LCD画面上にInfoDialogを表示する。
		/// </summary>
		/// <remarks>
		/// <para>ユーザー入力があるまでこのメソッドから戻らない</para>
		/// <para>エラーが発生しなければ、InfoDialog表示後に<see cref="EV3Application.LCDController.state"/>を<see cref="EV3Application.LCDController.State.ClosedInfoDialog"/>に変更する。</para>
		/// <para>エラーが発生した場合は、<see cref="EV3Application.LCDController.state"/>を<see cref="EV3Application.LCDController.State.End"/>に変更する。</para>
		/// </remarks>
		private void showInfoDialog()
		{

			currentDisplay = new InfoDialog("Please Push The EnterButton");
			try {
				currentDisplay.Show();
				state = State.CLOSEDINFODIALOG;
			}
			catch(Exception e){
				state = State.END;
			}
		}

		/// <summary>
		/// LCD画面上に文字列を表示する。
		/// </summary>
		/// <remarks>
		/// <para>ユーザ入力があるか、5秒経過するまでこのメソッドから戻らない</para>
		/// <para>エラーが発生しなければ、文字列を5秒間表示する。</para>
		/// <para>エラーが発生した場合は、<see cref="EV3Application.LCDController.state"/>を<see cref="EV3Application.LCDController.State.End"/>に変更する。</para>
		/// </remarks>
		private void showAlphanumericDisplay()
		{

			try{
				currentDisplay.Show();
				sendSignal.Reset();
				sendSignal.WaitOne(5000);
			}
			catch(Exception e){
				state = State.END;
			}
		}

		/// <summary>
		/// EnterButtonが押下された際の処理を実行する。
		/// </summary>
		/// <remarks>
		/// <para><see cref="state"/>が<see cref="EV3Application.LCDController.State.Started"/>の時、何もしない。</para>
		/// <para><see cref="state"/>が<see cref="EV3Application.LCDController.State.Started"/>以外の時、<see cref="EV3Application.LCDController.State.End"/>に変更し、処理待ち状態を解除する。</para>
		/// </remarks>
		public void EnterPressed()
		{
			if(state != State.STARTED){
				state = State.END;
				sendSignal.Set();
			}
		}
	}
}

