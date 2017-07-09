using System;
using UIKit;

namespace Pager.Sample
{
	public class SamplePageViewController : UIViewController
	{
		public SamplePageViewController(UIColor background)
		{
			this.background = background;
		}

		readonly UIColor background;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.View = new UILabel(this.View.Frame)
			{
				Text = this.Title,
				TextColor = UIColor.White,
				BackgroundColor = background,
				TextAlignment = UITextAlignment.Center,
			};
		}
	}
}
