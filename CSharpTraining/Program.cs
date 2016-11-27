using System;
using MonoBrickFirmwareWrapper.Display;

namespace CSharpTraining
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			// output to lcd console of EV3
			LcdConsoleWrapper.WriteLine("Hello World!");
		}

		public static int SampleMethod()
		{
			return 10;
		}
	}
}
