//
// AlphanumericDisplayBaseTest.cs
//
// Author:Yojiro Nanameki
//
// Copyright (c) 2016 

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
		#region Message Test
		[Test, Description("Messageに指定文字列のSet、Getを確認する"), Category("normal")]
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
		#endregion

		#region isAlphanumeric Test
		[Test, Description("半角英数字スペースのみのMessageにtrueを返すか確認する"), Category("normal")]
		public void OnlyValidCharactersTest()
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

		[Test, Description("半角英数字スペースとそれ以外が混在するMessageにfalseを返すか確認する"), Category("normal")]
		public void  ContainsInvalidCharactersTest()
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

		[Test, Description("半角英数字スペース以外のみのMessageにfalseを返すか確認する"), Category("normal")]
		public void OnlyInvalidCharactersTest()
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

		[Test, Description("Messageが空文字の時にfalseを返すか確認する"), Category("normal")]
		public void  EmptyTest()
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

		[Test, Description("Messageがnullの時にfalseを返すか確認する"), Category("normal")]
		public void  NullTest()
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
		#endregion
	}
}
