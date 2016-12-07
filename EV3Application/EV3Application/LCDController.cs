using System;
using System.Threading;
using MonoBrickFirmware;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display;

namespace EV3Application.LCD
{
    /// <summary>
    /// LCD画面を制御するクラス。
    /// 画面に表示するダイアログや文字列を制御する。
    /// </summary>
	public class LCDController
	{
        /// <summary>
        /// LCD画面の状態を表す。
        /// </summary>
		public enum State {
            STARTED, //アプリケーション開始後
            CLOSEDINFODIALOG, //InfoDialog消去後
            CLEAREDTEXTHELLO, //Helloメッセージ消去後
            END //アプリケーション終了
        }
		private State state;
		private ManualResetEvent sendSignal;
		private IDisplay currentDisplay;

        /// <summary>
        /// コンストラクタ。
        /// LCD画面の状態をアプリケーション開始後状態にする。
        /// </summary>
        /// <param name="sendSignal">スレッドの停止、開始を知らせるイベント</param>
		public LCDController (ManualResetEvent sendSignal)
		{
			state = State.STARTED;
			this.sendSignal = sendSignal;
		}

        /// <summary>
        /// テスト用コンストラクタ。
        /// LCD画面の状態をアプリケーション開始後状態にする。
        /// </summary>
        /// <param name="state">LCD画面の状態</param>
        /// <param name="sendSignal">スレッドの停止、開始を知らせるイベント</param>
        /// <param name="currentDisplay">表示しているディスプレイのインスタンス</param>
        public LCDController(State state, ManualResetEvent sendSignal, IDisplay currentDisplay)
        {
            this.state = state;
            this.sendSignal = sendSignal;
            this.currentDisplay = currentDisplay;
        }

        /// <summary>
        /// 画面に表示するダイアログや文字列を制御する。
        /// 自身の<see cref="state"/>によって表示するダイアログや文字列を変更する。
        /// </summary>
        /// <remarks>
        /// <c>STARTED</c>のとき、InfoDialogを表示する。
        /// <c>CLOSEDINFODIALOG</c>のとき、文字列Helloを表示する。
        /// <c>CLEAREDTEXTHELLO</c>のとき、文字列Good Byeを表示する。
        /// </remarks>
		public void ControlLCD(){

			while(state != State.END){
				switch(state){

				case State.STARTED:
					showInfoDialog ();
					break;

				case State.CLOSEDINFODIALOG:
					currentDisplay = new AlphanumericDisplay ("Hello");
					showAlphanumericDisplay ();
					if (state == State.END) {
						return;
					} else {
						state = State.CLEAREDTEXTHELLO;
					}
					break;

				case State.CLEAREDTEXTHELLO:
					currentDisplay.Message = "Good Bye";
					showAlphanumericDisplay ();
					state = State.END;
					break;

				default:
					state = State.END;
					break;
				}
			}
		}

        /// <summary>
        /// <see cref="EV3Application.LCD.InfoDialog"/>にInfoDialogの表示を指示する。
        /// </summary>
        /// <remarks>
        /// 例外をcatchした場合は、<see cref="state"/>を<c>END</c>に変更する。
        /// </remarks>
		private void showInfoDialog(){

			currentDisplay = new InfoDialog ("Please Push The EnterButton");
			try {
				currentDisplay.Show ();
				state = State.CLOSEDINFODIALOG;
			}catch(Exception e){
				state = State.END;
			}
		}

        /// <summary>
        /// <see cref="EV3Application.LCD.AlphanumericDisplay"/>にInfoDialogの表示を指示する。
        /// </summary>
        /// <remarks>
        /// 例外をcatchした場合は、<see cref="state"/>を<c>END</c>に変更する。
        /// </remarks>
		private void showAlphanumericDisplay(){

			try{
				currentDisplay.Show ();
				sendSignal.Reset ();
				sendSignal.WaitOne (5000);
			}catch(Exception e) {
				state = State.END;
			}
		}

        /// <summary>
        /// EnterButtonが押下された際の処理を実行する。
        /// <see cref="state"/>がSTARTEDではない時、<see cref="state"/>を<c>END</c>に変更し、処理待ち状態を解除する。
        /// </summary>
		public void EneterPressed(){

			if(state != State.STARTED){
				state = State.END;
				sendSignal.Set ();
			}
		}
	}
}

