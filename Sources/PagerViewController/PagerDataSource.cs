namespace Pager
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UIKit;

	internal class PagerDataSource : UIPageViewControllerDataSource
	{
		private List<UIViewController> children;

		public PagerDataSource(UIViewController[] children)
		{
			this.children = children.ToList();
		}

		override public UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
		{
			var currentIndex = this.children.IndexOf(referenceViewController);
			if (currentIndex == 0)
				return null;
			currentIndex--;
			return this.children[currentIndex];
		}

		override public UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
		{
			var currentIndex = this.children.IndexOf(referenceViewController);
			if (currentIndex >= this.children.Count - 1)
				return null;
			currentIndex++;
			return this.children[currentIndex];
		}
	}
}
