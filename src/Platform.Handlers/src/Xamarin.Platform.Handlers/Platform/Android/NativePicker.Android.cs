﻿using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Runtime;
using ARect = Android.Graphics.Rect;

#if __ANDROID_29__
using AndroidX.Core.Graphics.Drawable;
#else
using Android.Support.V4.Graphics.Drawable;
# endif

namespace Xamarin.Platform
{
	public class NativePicker : NativePickerBase
	{
		public bool ShowPopupOnFocus { get; set; }

		public NativePicker(Context context) : base(context)
		{
			PickerManager.Init(this);
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			PickerManager.OnTouchEvent(this, e);
			return base.OnTouchEvent(e); // Raises the OnClick event if focus is already received
		}

		protected override void OnFocusChanged(bool gainFocus, [GeneratedEnum] FocusSearchDirection direction, ARect previouslyFocusedRect)
		{
			base.OnFocusChanged(gainFocus, direction, previouslyFocusedRect);
			PickerManager.OnFocusChanged(gainFocus, this);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				PickerManager.Dispose(this);

			base.Dispose(disposing);
		}
	}

	public class NativePickerBase : EditText
	{
		public NativePickerBase(Context context) : base(context)
		{
			DrawableCompat.Wrap(Background);
		}
	}
}