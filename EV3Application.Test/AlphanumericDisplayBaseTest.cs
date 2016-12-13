using NUnit.Framework;
using System;
using EV3Application;

namespace EV3Application.Tests
{
	[TestFixture]
	public class AlphanumericDisplayBaseTest
	{
		//Messageのテスト
		//画面に表示するメッセージ。

		[Test, Description("Messageに指定文字列がSetされるか")]
		public void  MessageTest001()
		{
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			string expected = "Set";
			wrapper.Message = expected;
			string actual = wrapper.Message;
			Assert.AreEqual (expected, actual);
		}

		[Test, Description("MessageからSetされた文字列がGetできるか")]
		public void  MessageTest002()
		{
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			string expected = "Get";
			wrapper.Message = expected;
			string actual = wrapper.Message;
			Assert.AreEqual (expected, actual);
		}

		//isAlphanumericのテスト
		//メッセージが半角英数字スペースのみかどうかを判定する。

		[Test, Description("半角英数字スペースのみのMessageにtrueを返すか")]
		public void  IsAlphanumericTest001()
		{
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			wrapper.Message = "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ 0123456789";
			bool expected = true;
			bool actual = wrapper.isAphanumericWrapper();
			Assert.AreEqual (expected, actual);
		}

		[Test, Description("半角英数字スペースとそれ以外が混在するMessageにfalseを返すか")]
		public void  IsAlphanumericTest002()
		{
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			wrapper.Message = "a b 9 237#あｐ";
			bool expected = false;
			bool actual = wrapper.isAphanumericWrapper();
			Assert.AreEqual (expected, actual);
		}

		[Test, Description("半角英数字スペース以外のみのMessageにfalseを返すか")]
		public void  IsAlphanumericTest003()
		{
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			wrapper.Message = "'()%#!|^-ｔｃ";
			bool expected = false;
			bool actual = wrapper.isAphanumericWrapper();
			Assert.AreEqual (expected, actual);
		}

		[Test, Description("Messageが空文字の時にfalseを返すか")]
		public void  IsAlphanumericTest004()
		{
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			wrapper.Message = "";
			bool expected = false;
			bool actual = wrapper.isAphanumericWrapper();
			Assert.AreEqual (expected, actual);
		}

		[Test, Description("Messageがnullの時にfalseを返すか")]
		public void  IsAlphanumericTest005()
		{
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			wrapper.Message = null;
			bool expected = false;
			bool actual = wrapper.isAphanumericWrapper();
			Assert.AreEqual (expected, actual);
		}
	}
}
