using System;
using UIKit;

namespace Pager
{
	public class TabSelectedEventArgs : EventArgs
	{
		public TabSelectedEventArgs(UIViewController viewController, int index)
		{
			SelectedViewController = viewController;
			SelectedIndex = index;
		}

		public UIViewController SelectedViewController { get; private set; }

		public int SelectedIndex { get; private set; }
	}
}
