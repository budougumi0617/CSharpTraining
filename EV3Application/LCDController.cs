//
// LCDController.cs
//
// Author:Yojiro Nanameki
//
// Copyright (c) 2016 

using System;
using System.Threading;
using MonoBrickFirmware;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display;

namespace EV3Application.LCD
{
	/// <summary>
	/// LCDを制御するクラス。
	/// </summary>
	public class LCDController
	{
		/// <summary>
		/// LCDの状態を表す。
		/// </summary>
		public enum State
		{
			/// <summary>
			/// アプリケーション開始後。
			/// </summary>
			Started,

			/// <summary>
			/// InfoDialog消去後。
			/// </summary>
			ClosedInfoDialog, 

			/// <summary>
			/// Helloメッセージ消去後。
			/// </summary>
			ClearedTextHello,

			/// <summary>
			/// アプリケーション終了。
			/// </summary>
			End
		}

		private State state; //LCDの状態
		private ManualResetEvent sendSignal; //スレッドの停止、開始を知らせるイベント
		private IDisplay currentDisplay; //表示しているディスプレイ
		private const int waitAlphanumericDisplay = 5000;

		/// <summary>
		/// LCDの状態をアプリケーション開始後状態に初期化し、インスタンスを生成する。
		/// </summary>
		/// <param name="sendSignal">スレッドの停止、開始を知らせるイベント</param>
		public LCDController(ManualResetEvent sendSignal)
		{
			state = State.Started;
			this.sendSignal = sendSignal;
		}

		/// <summary>
		/// <see cref="state"/>を参照して、LCDをコントロールする。
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
					showInfoDialog();
					state = State.ClosedInfoDialog;
					break;

				case State.ClosedInfoDialog:
					currentDisplay = new AlphanumericDisplay("Hello");
					showAlphanumericDisplay(waitAlphanumericDisplay);
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
					showAlphanumericDisplay(waitAlphanumericDisplay);
					state = State.End;
					break;

				default: // このケースに入ることはない
					state = State.End;
					break;
				}
			}
		}
									
		/// <summary>
		/// LCD上に<c>InfoDialog</c>を表示する。
		/// </summary>
		/// <remarks>
		/// ユーザー入力があるまでこのメソッドは終了しない。</br>
		/// エラーが発生しなければ、InfoDialogを表示する。</br>
		/// エラーが発生した場合は、アプリケーションを終了する。
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
		/// LCD上に文字列を表示する。
		/// </summary>
		/// <remarks>
		/// ユーザ入力があるか、5秒経過するまでこのメソッドは終了しない。</br>
		/// エラーが発生しなければ、文字列を5秒間表示する。</br>
		/// エラーが発生した場合は、アプリケーションを終了する。
		/// </remarks>
		private void showAlphanumericDisplay(int waitAlphanumericDisplay)
		{
			try
			{
				currentDisplay.Show();
				sendSignal.Reset();
				sendSignal.WaitOne(waitAlphanumericDisplay);
			}
			catch(Exception e)
			{
				state = State.End;
			}
		}

		/// <summary>
		/// EnterButtonが押下された際に、アプリケーション終了処理を実行する。
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

