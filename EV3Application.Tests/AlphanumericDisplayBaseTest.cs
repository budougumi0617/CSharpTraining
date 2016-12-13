using NUnit.Framework;
using System;
using EV3Application;
//using System.Reflection;

namespace EV3Application.Tests
{
	[TestFixture]
	public class AlphanumericDisplayBaseTest
	{
		[Test, Description("半角英数字スペースのみのメッセージにtrueを返すかどうか")]
		public void  isAlphanumericTestTrue()
		{
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			wrapper.Message = "a b 1 2 3423423                  ";
			bool expected = true;
			bool actual = wrapper.isAphanumericWrapper();
			Assert.AreEqual (expected, actual);

			/*
			Type t = Type.GetType("LCD.AlphanumericDisplayBase");
			Object o = Activator.CreateInstance(t);
			PropertyInfo p = t.GetProperty("Message");
			p.SetValue(o, "a b 1", null);
			MethodInfo m = t.GetMethod ("isAlphanumeric");
			bool actual = (bool) m.Invoke(o, null);
			bool expected = true;
			Assert.AreEqual (expected, actual);
			*/
		}
		[Test, Description("半角英数字スペース以外を含むメッセージにfalseを返すかどうか")]
		public void  isAlphanumericTestFalse()
		{
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			wrapper.Message = "あいうえお";
			bool expected = false;
			bool actual = wrapper.isAphanumericWrapper();
			Assert.AreEqual (expected, actual);
		}
	}
}

