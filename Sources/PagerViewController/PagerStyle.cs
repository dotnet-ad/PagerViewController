namespace Pager
{
	using System;
	using System.Linq;
	using UIKit;

	/// <summary>
	/// Represents the pager style.
	/// </summary>
	public class PagerStyle
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Pager.PagerStyle"/> class.
		/// </summary>
		public PagerStyle() { }

		/// <summary>
		/// Clones the given style.
		/// </summary>
		/// <param name="other">Other.</param>
		public PagerStyle(PagerStyle other)
		{
			this.BarHeight = other.BarHeight;
			this.BarBackgroundColor = other.BarBackgroundColor;
			this.PagesBackgroundColor = other.PagesBackgroundColor;
			this.SelectedStripColors = other.SelectedStripColors.ToArray();
			this.BottomBorderColor = other.BottomBorderColor;
			this.LabelFont = other.LabelFont;
			this.SelectedStripSize = other.SelectedStripSize;
			this.UnselectedLabelColor = other.UnselectedLabelColor;
		}

		/// <summary>
		/// Gets or sets the height of the tab bar.
		/// </summary>
		/// <value>The height of the bar.</value>
		public nfloat BarHeight { get; set; } = 35;

		/// <summary>
		/// Gets or sets the background color of the tab bar.
		/// </summary>
		/// <value>The background color of the tab bar.</value>
		public UIColor BarBackgroundColor { get; set; } = UIColor.White;

		/// <summary>
		/// Gets or sets the color behind the pages.
		/// </summary>
		/// <value>The color behind the pages.</value>
		public UIColor PagesBackgroundColor { get; set; } = UIColor.LightGray;

		/// <summary>
		/// Gets or sets selected strip color for each of the tabs (modulo if smaller than tabs number).
		/// </summary>
		/// <value>The selected strip colors.</value>
		public UIColor[] SelectedStripColors { get; set; } = { UIColor.Black };

		/// <summary>
		/// Gets or sets the bottom border color.
		/// </summary>
		/// <value>The color of the bottom border.</value>
		public UIColor BottomBorderColor { get; set; } = UIColor.FromWhiteAlpha(0.9f,1.0f);

		/// <summary>
		/// Gets or sets the tab label font.
		/// </summary>
		/// <value>The font.</value>
		public UIFont LabelFont { get; set; } = UIFont.SystemFontOfSize(11);

		/// <summary>
		/// Gets or sets the size of the selected strip.
		/// </summary>
		/// <value>The size of the selected strip.</value>
		public float SelectedStripSize { get; set; } = 2.0f;

		/// <summary>
		/// Gets or sets the color of selected labels.
		/// </summary>
		/// <value>The color of the selected label.</value>
		public UIColor SelectedLabelColor { get; set; } = UIColor.Black;

		/// <summary>
		/// Gets or sets the color of unselected labels.
		/// </summary>
		/// <value>The color of the unselected label.</value>
		public UIColor UnselectedLabelColor { get; set; } = UIColor.Gray;
	}
}
