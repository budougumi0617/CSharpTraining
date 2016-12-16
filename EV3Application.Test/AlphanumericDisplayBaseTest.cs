using NUnit.Framework;
using System;
using EV3Application;

namespace EV3Application.Tests
{
	/// <summary>
	/// <see cref="EV3Application.LCD.AlphanumericDisplayBase"/>のテストクラス。
	/// </summary>
	[TestFixture]
	public class AlphanumericDisplayBaseTest
	{
		[Test, Description("Messageに指定文字列がSetされるか確認する"), Category("Message")]
		public void  MessageTest001()
		{
			//準備
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			string expected = "Set";
			wrapper.Message = expected;
			//実行
			string actual = wrapper.Message;
			//確認
			Assert.AreEqual (expected, actual);
		}

		[Test, Description("MessageからSetされた文字列がGetできるか確認する"), Category("Message")]
		public void  MessageTest002()
		{
			//準備
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			string expected = "Get";
			wrapper.Message = expected;
			//実行
			string actual = wrapper.Message;
			//確認
			Assert.AreEqual (expected, actual);
		}

		[Test, Description("半角英数字スペースのみのMessageにtrueを返すか確認する"), Category("isAlphanumeric")]
		public void  IsAlphanumericTest001()
		{
			//準備
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			wrapper.Message = "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ 0123456789";
			bool expected = true;
			//実行
			bool actual = wrapper.isAlphanumericWrapper();
			//確認
			Assert.AreEqual (expected, actual);
		}

		[Test, Description("半角英数字スペースとそれ以外が混在するMessageにfalseを返すか確認する"), Category("isAlphanumeric")]
		public void  IsAlphanumericTest002()
		{
			//準備
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			wrapper.Message = "a b 9 237#あｐ";
			bool expected = false;
			//実行
			bool actual = wrapper.isAlphanumericWrapper();
			//確認
			Assert.AreEqual (expected, actual);
		}

		[Test, Description("半角英数字スペース以外のみのMessageにfalseを返すか確認する"), Category("isAlphanumeric")]
		public void  IsAlphanumericTest003()
		{
			//準備
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			wrapper.Message = "'()%#!|^-ｔｃ";
			bool expected = false;
			//実行
			bool actual = wrapper.isAlphanumericWrapper();
			//確認
			Assert.AreEqual (expected, actual);
		}

		[Test, Description("Messageが空文字の時にfalseを返すか確認する"), Category("isAlphanumeric")]
		public void  IsAlphanumericTest004()
		{
			//準備
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			wrapper.Message = "";
			bool expected = false;
			//実行
			bool actual = wrapper.isAlphanumericWrapper();
			//確認
			Assert.AreEqual (expected, actual);
		}

		[Test, Description("Messageがnullの時にfalseを返すか確認する"), Category("isAlphanumeric")]
		public void  IsAlphanumericTest005()
		{
			//準備
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			wrapper.Message = null;
			bool expected = false;
			//実行
			bool actual = wrapper.isAlphanumericWrapper();
			//確認
			Assert.AreEqual (expected, actual);
		}
	}
}
