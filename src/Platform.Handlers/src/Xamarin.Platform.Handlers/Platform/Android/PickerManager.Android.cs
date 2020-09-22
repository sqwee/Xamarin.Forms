﻿using System.Collections.Generic;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Xamarin.Forms;
using AView = Android.Views.View;

namespace Xamarin.Platform
{
	internal static class PickerManager
	{
		readonly static HashSet<Keycode> AvailableKeys = new HashSet<Keycode>(new[] {
			Keycode.Tab, Keycode.Forward, Keycode.DpadDown, Keycode.DpadLeft, Keycode.DpadRight, Keycode.DpadUp
		});

		public static void Init(EditText editText)
		{
			editText.Focusable = true;
			editText.Clickable = true;
			editText.InputType = InputTypes.Null;
			editText.KeyPress += OnKeyPress;

			editText.SetOnClickListener(PickerListener.Instance);
		}

		public static void OnTouchEvent(EditText sender, MotionEvent e)
		{
			if (e.Action == MotionEventActions.Up && !sender.IsFocused)
			{
				sender.RequestFocus();
			}
		}

		public static void OnFocusChanged(bool gainFocus, EditText sender)
		{
			if (gainFocus)
				sender.CallOnClick();
		}

		static void OnKeyPress(object sender, AView.KeyEventArgs e)
		{
			if (!AvailableKeys.Contains(e.KeyCode))
			{
				e.Handled = false;
				return;
			}
			e.Handled = true;
			(sender as AView)?.CallOnClick();
		}

		public static void Dispose(EditText editText)
		{
			editText.KeyPress -= OnKeyPress;
			editText.SetOnClickListener(null);
		}

		public static ICharSequence GetTitle(Color titleColor, string title)
		{
			if (titleColor == Color.Default)
				return new Java.Lang.String(title);

			var spannableTitle = new SpannableString(title ?? string.Empty);
			spannableTitle.SetSpan(new ForegroundColorSpan(titleColor.ToNative()), 0, spannableTitle.Length(), SpanTypes.ExclusiveExclusive);
			return spannableTitle;
		}

		class PickerListener : Java.Lang.Object, AView.IOnClickListener
		{
			public static readonly PickerListener Instance = new PickerListener();

			public void OnClick(AView view)
			{
				// TODO: Move KeyboardManager to Xamarin.Platform
				/*
				if (view is AView picker)
					picker.HideKeyboard();
				*/
			}
		}
	}
}