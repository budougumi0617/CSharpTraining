using System;
using EV3Application.LCD;

namespace EV3Application.Tests
{
	public class AlphanumericDisplayBaseWrapper : AlphanumericDisplayBase
	{
		public override void Show ()
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Ises the aphanumeric wrapper.
		/// </summary>
		/// <returns><c>true</c>, if aphanumeric wrapper was ised, <c>false</c> otherwise.</returns>
		public bool isAphanumericWrapper()
		{
			return base.isAlphanumeric ();
		}
	}
}