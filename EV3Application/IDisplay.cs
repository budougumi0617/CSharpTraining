//
// IDisplay.cs
//
// Author:Yojiro Nanameki
//
// Copyright (c) 2016 

using System;

namespace EV3Application.LCD
{
	/// <summary>
	/// 画面表示を司るインターフェース。
	/// </summary>
	public interface IDisplay
	{
		/// <summary>
		/// 画面に表示するメッセージ。
		/// </summary>
		string Message{get; set;}

		/// <summary>
		/// 画面に表示する。
		/// </summary>
		/// <exception cref="System.InvalidOperationException">
		/// 画面に表示できない場合に例外を出す。
		/// </exception>
		void Show();
	}
}
