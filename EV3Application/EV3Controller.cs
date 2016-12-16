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
		/// 各コントローラーを使用して、機能を実現する。
		/// </summary>
		public void ControlEV3()
		{
			ManualResetEvent sendSignal = new ManualResetEvent(false);
			LCD.LCDController controller = new LCD.LCDController(sendSignal);
			
			ButtonEvents bEvent = new ButtonEvents();
			bEvent.EnterPressed += () => {
				controller.EnterPressed();
			};
			
			controller.ControlLCD();
		}
	}
}
