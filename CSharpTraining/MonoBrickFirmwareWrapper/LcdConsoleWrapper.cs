using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoBrickFirmware.Display;

namespace CSharpTraining.MonoBrickFirmwareWrapper
{
	/// <summary>
	/// A wrapper class of <see cref="LcdConsole"/>.
	/// </summary>
	static class LcdConsoleWrapper
	{
		/// <summary>
		/// Delegate type of <see cref="LcdConsole.WriteLine"/>.
		/// </summary>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg">An array of objects to write using format.</param>
		public delegate void WriteLineDelegate(string format, params object[] arg);

		/// <summary>
		/// A wrapper of <see cref="LcdConsole.WriteLine"/>.
		/// </summary>
		public static WriteLineDelegate WriteLine { get; } = LcdConsole.WriteLine;

		/// <summary>
		/// A wrapper of <see cref="LcdConsole.Clear"/>.
		/// </summary>
		public static Action Clear { get; } = LcdConsole.Clear;
	}
}
