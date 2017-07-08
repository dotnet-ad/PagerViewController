namespace Pager
{
	using System;
	using System.Linq;
	using CoreGraphics;
	using UIKit;

	public class PagerViewController : UIViewController
	{
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Pager.PagerViewController"/> class.
		/// </summary>
		/// <param name="style">The style of the pager.</param>
		/// <param name="children">All children view controllers.</param>
		public PagerViewController(PagerStyle style, params UIViewController[] children)
		{
			this.children = children;
			this.style = new PagerStyle(style);
		}

		#endregion

		#region Fields

		private PagerStyle style;

		private UIPageViewController pager;

		private UIViewController[] children;

		private PagerBar bar;

		#endregion

		#region Events

		public event EventHandler<TabSelectedEventArgs> TabSelected;

		private void HandleTabSelected(UIViewController viewController, int index)
		{
			var handler = this.TabSelected;
			handler?.Invoke(this, new TabSelectedEventArgs(viewController, index));
		}

		#endregion

		#region Lifecycle

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.View.BackgroundColor = this.style.PagesBackgroundColor;

			this.bar = new PagerBar(this.style)
			{
				Frame = new CGRect(0, 0, this.View.Bounds.Width, this.style.BarHeight),
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleBottomMargin,
			};

			this.bar.Sections = this.children.Select(vc => vc.Title).ToArray();
			this.bar.TabSelected += OnTabSelected; ;

			this.View.AddSubview(bar);

			// Pager

			this.pager = new UIPageViewController(UIPageViewControllerTransitionStyle.Scroll, UIPageViewControllerNavigationOrientation.Horizontal);
			this.pager.DidFinishAnimating += OnPageSwiped;
			this.pager.DataSource = new PagerDataSource(children);
			this.pager.SetViewControllers(children.Take(1).ToArray(), UIPageViewControllerNavigationDirection.Forward, false, s => { });

			this.AddChildViewController(pager);
			pager.DidMoveToParentViewController(this);

			this.View.AddSubview(this.pager.View);
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			var pagerView = this.pager.View;
			pagerView.Frame = new CGRect(0, this.style.BarHeight, this.View.Bounds.Width, this.View.Bounds.Height - this.style.BarHeight);
			pagerView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleBottomMargin;
		}

		#endregion

		#region Public

		public void SelectTab(int index, bool animated)
		{
			if (index >= 0)
			{
				this.bar?.SelectAndRaise(index, animated);
			}
		}

		public void SelectTab(Type viewControllerType, bool animated)
		{
			var index = this.children.FirstOrDefault(e => e.GetType() == viewControllerType);
			SelectTab(index, animated);
		}

		public void SelectTab(UIViewController viewController, bool animated)
		{
			var index = Array.IndexOf(this.children, viewController);
			SelectTab(index, animated);
		}

		#endregion

		#region Tab selection

		private void OnPageSwiped(object sender, UIPageViewFinishedAnimationEventArgs e)
		{
			if (e.Completed)
			{
				var index = this.children.ToList().IndexOf(this.pager.ViewControllers[0]);
				this.bar.Select(index, true);
				HandleTabSelected(this.pager.ViewControllers[0], index);
			}
		}

		private void OnTabSelected(object sender, PagerBar.ValueChangedEventArgs e)
		{
			var direction = (e.OldValue < e.NewValue) ? UIPageViewControllerNavigationDirection.Forward : UIPageViewControllerNavigationDirection.Reverse;
			this.pager.SetViewControllers(new[] { children[e.NewValue] }, direction, true, s =>
			{
				HandleTabSelected(children[e.NewValue], e.NewValue);
			});
		}

		#endregion
	}
}
