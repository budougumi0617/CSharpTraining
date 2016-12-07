using System;
using System.Threading;
using System.Threading.Tasks;
using MonoBrickFirmware.UserInput;

namespace EV3Application
{
	/// <summary>
	/// EV3デバイスの全体を制御するクラス。
    /// 各コントローラーに処理を依頼する。
	/// </summary>
	public class EV3Controller
	{
		/// <summary>
		/// デフォルトコンストラクタ。
		/// </summary>
		public EV3Controller ()
		{
		}

		/// <summary>
		/// <see cref \= "EV3Applicationl.LCDController"/>にLCD画面の処理を指示する。
        /// EnterButtonが押下された際に<see cref \= "EV3Applicationl.LCDController"/>に知らせる。
		/// </summary>
		public void ControlEV3(){

			ManualResetEvent sendSignal = new ManualResetEvent (false);
			LCD.LCDController controller = new LCD.LCDController (sendSignal);
			ButtonEvents bevent = new ButtonEvents ();
			bevent.EnterPressed += () => {
				controller.EneterPressed();
			};
			controller.ControlLCD ();
		}
	}
}
