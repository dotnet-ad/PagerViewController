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
		#region Default styles

		public static PagerStyle Default => new PagerStyle();

		public static PagerStyle NotAnimated => new PagerStyle()
		{
			SelectedStripAnimation = PagerStyle.StripAnimation.None,
			SelectedStripStyle = PagerStyle.StripStyle.None,
		};

		public static PagerStyle Rounded => new PagerStyle()
		{
			SelectedStripSize = 5.0f,
			SelectedStripMargin = 3.0f,
			SelectedStripAnimation = PagerStyle.StripAnimation.Constant,
			SelectedStripStyle = PagerStyle.StripStyle.Rounded,
		};

		public static PagerStyle Dot => new PagerStyle()
		{
			SelectedStripSize = 5.0f,
			SelectedStripMargin = 3.0f,
			SelectedStripStyle = PagerStyle.StripStyle.Dot,
		};

		public static PagerStyle Stretched => new PagerStyle(Default)
		{
			SelectedStripAnimation = PagerStyle.StripAnimation.Stretched,
		};

		public static PagerStyle DotNotAnimated => new PagerStyle(Dot)
		{
			SelectedStripAnimation = PagerStyle.StripAnimation.None,
		};

		public static PagerStyle RoundedNotAnimated => new PagerStyle(Rounded)
		{
			SelectedStripAnimation = PagerStyle.StripAnimation.None,
		};

		public static PagerStyle DotStretched => new PagerStyle(Dot)
		{
			SelectedStripAnimation = PagerStyle.StripAnimation.Stretched,
		};

		public static PagerStyle RoundedStretched => new PagerStyle(Rounded)
		{
			SelectedStripAnimation = PagerStyle.StripAnimation.Stretched,
		};

		#endregion

		public enum StripStyle
		{
			None,
			Rounded,
			Dot,
		}


		public enum StripAnimation
		{
			None,
			Constant,
			Stretched,
		}

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
			this.SelectedLabelColor = other.SelectedLabelColor;
			this.UnselectedLabelColor = other.UnselectedLabelColor;
			this.SelectedStripStyle = other.SelectedStripStyle;
			this.SelectedStripMargin = other.SelectedStripMargin;
			this.SelectedStripAnimationDuration = other.SelectedStripAnimationDuration;
			this.SelectedStripAnimation = other.SelectedStripAnimation;
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
		/// Gets or sets the selected strip style
		/// </summary>
		/// <value>The size of the selected strip.</value>
		public StripStyle SelectedStripStyle { get; set; } = StripStyle.None;

		/// <summary>
		/// Gets or sets the selected strip animation mode.
		/// </summary>
		/// <value>The animation mode of the selected strip.</value>
		public StripAnimation SelectedStripAnimation { get; set; } = StripAnimation.Constant;

		/// <summary>
		/// Gets or sets the selected strip bottom margin.
		/// </summary>
		/// <value>The size of the selected strip.</value>
		public float SelectedStripMargin { get; set; } = 1.0f;

		/// <summary>
		/// Gets or sets the duration of the strip animation (in seconds).
		/// </summary>
		/// <value>The duration of the selected strip animation.</value>
		public float SelectedStripAnimationDuration { get; set; } = 0.20f;

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
