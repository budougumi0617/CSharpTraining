using NUnit.Framework;
using System;
using EV3Application;

namespace EV3Application.Test
{
	/// <summary>
	/// <see cref="EV3Application.LCD.AlphanumericDisplayBase"/>のテストクラス。
	/// </summary>
	[TestFixture]
	public class AlphanumericDisplayBaseTest
	{
		[Test, Description("Messageに指定文字列のSet、Getを確認する"), Category("Message")]
		public void  MessageAccessorTest()
		{
			//準備
			AlphanumericDisplayBaseWrapper wrapper = new AlphanumericDisplayBaseWrapper ();
			string expected = "Test";
			wrapper.Message = expected;
			//実行
			string actual = wrapper.Message;
			//確認
			Assert.AreEqual (expected, actual);
		}

		[Test, Description("半角英数字スペースのみのMessageにtrueを返すか確認する"), Category("isAlphanumeric")]
		public void NormalAlphanumericSpaceTest()
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
		public void  MixedAlphanumericSpaceTest()
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
		public void AbnormalAlphanumericSpaceTest()
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
		public void  EmptyAlphanumericSpaceTest()
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
		public void  NullAlphanumericSpaceTest()
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
