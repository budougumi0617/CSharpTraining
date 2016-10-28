using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoBrickFirmware.Display;

namespace CSharpTraining.MonoBrickFirmwareWrapper
{
	static class LcdConsoleWrapper
	{
		public delegate void WriteLineDelegate(string format, params object[] arg);

		public static WriteLineDelegate WriteLine { get; } = LcdConsole.WriteLine;

		public static Action Clear { get; } = LcdConsole.Clear;
	}
}
