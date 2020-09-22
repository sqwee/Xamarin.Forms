﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Platform;

namespace Sample
{
	public class Picker : View, IPicker
	{
		public Picker()
		{
			((INotifyCollectionChanged)Items).CollectionChanged += OnItemsCollectionChanged;
		}

		public string Title { get; set; }

		public Color TitleColor { get; set; }

		public IList<string> Items { get; } = new LockableObservableListWrapper();

		public IList ItemsSource { get; set; }

		public int SelectedIndex { get; set; } = -1;

		public object SelectedItem { get; set; }

		public string Text { get; set; }

		public Color Color { get; set; }

		public Font Font { get; set; }

		public TextTransform TextTransform { get; set; }

		public double CharacterSpacing { get; set; }

		public FontAttributes FontAttributes { get; set; }

		public string FontFamily { get; set; }

		public double FontSize => throw new System.NotImplementedException();

		public TextAlignment HorizontalTextAlignment { get; set; }

		public TextAlignment VerticalTextAlignment { get; set; }


		void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			var oldIndex = SelectedIndex;
			var newIndex = SelectedIndex = SelectedIndex.Clamp(-1, Items.Count - 1);

			// If the index has not changed, still need to change the selected item
			if (newIndex == oldIndex)
				UpdateSelectedItem(newIndex);
		}

		void UpdateSelectedItem(int index)
		{
			if (index == -1)
			{
				SelectedItem = null;
				return;
			}

			if (ItemsSource != null)
			{
				SelectedItem = ItemsSource[index];
				return;
			}

			SelectedItem = Items[index];
		}
	}
}