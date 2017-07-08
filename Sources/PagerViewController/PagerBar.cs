namespace Pager
{
	using System;
	using System.Linq;
	using CoreGraphics;
	using UIKit;

	/// <summary>
	/// Represents the tab view.
	/// </summary>
	internal class PagerBar : UIView
	{
		public PagerBar(PagerStyle style)
		{
			this.Frame = new CGRect(0, 0, 0, style.SelectedStripSize);
			this.style = style;
			this.BackgroundColor = style.BarBackgroundColor;

			const int border = 1;
			var bottomBorder = new UIView(new CGRect(0, this.Bounds.Height - border, this.Bounds.Width, border))
			{
				BackgroundColor = style.BottomBorderColor,
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleTopMargin,
			};
			this.AddSubview(bottomBorder);

			var frame = this.Bounds;
			frame.Y = this.Bounds.Height - style.SelectedStripSize - style.SelectedStripMargin;
			frame.Height = style.SelectedStripSize;

			if (style.SelectedStripStyle == PagerStyle.StripStyle.Dot)
			{
				frame.Width = style.SelectedStripSize;
			}

			frame.X = (this.Bounds.Width / 2) - (frame.Width / 2);

			this.selectedView = new UIView(frame)
			{
				BackgroundColor = GetStripColor(0),
			};

			if (style.SelectedStripStyle == PagerStyle.StripStyle.Rounded || style.SelectedStripStyle == PagerStyle.StripStyle.Dot)
			{
				this.selectedView.Layer.CornerRadius = style.SelectedStripSize * 0.5f;
			}

			if (style.SelectedStripStyle == PagerStyle.StripStyle.Dot)
			{
				this.selectedView.AutoresizingMask = UIViewAutoresizing.FlexibleTopMargin | UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleLeftMargin;
			}
			else
			{
				this.selectedView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleTopMargin | UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin;
			}

			this.AddSubview(this.selectedView);
		}

		#region Events

		public class ValueChangedEventArgs
		{
			public int OldValue { get; set; }

			public int NewValue { get; set; }
		}

		public event EventHandler<ValueChangedEventArgs> TabSelected;

		#endregion

		#region Fields

		private PagerStyle style;

		private string[] sections = { };

		private UILabel[] labels = { };

		private UIView selectedView;

		#endregion

		#region Private properties

		private int SectionCount => Math.Max(1, this.sections.Length);

		private nfloat TabWidth => this.Bounds.Size.Width / this.SectionCount;

		#endregion

		#region Property

		public int SelectedIndex { get; private set; } = 0;

		public string[] Sections
		{
			get { return sections; }
			set
			{
				if (this.sections != value)
				{
					this.sections = value;
					UpdateLabels();
					UpdateSelectedPosition(false);
				}
			}
		}

		#endregion

		#region Methods

		public void SelectAndRaise(int index, bool animated)
		{
			var oldValue = this.SelectedIndex;
			this.Select(index, animated);
			var newValue = this.SelectedIndex;
			if (oldValue != newValue)
				this.TabSelected?.Invoke(this, new ValueChangedEventArgs() { OldValue = oldValue, NewValue = newValue });
		}

		public void Select(int index, bool animated)
		{
			var newIndex = Math.Min(SectionCount - 1, Math.Max(0, index));
			if (newIndex != SelectedIndex)
			{
				this.SelectedIndex = newIndex;
				this.UpdateSelectedPosition(animated);
			}
		}

		#endregion

		#region Updating

		private void UpdateLabels()
		{
			if (this.labels.Length > 0)
			{
				foreach (var label in this.labels)
				{
					label.RemoveFromSuperview();
				}
			}

			this.labels = new UILabel[this.sections.Length];

			var tabSize = new CGSize(TabWidth, this.selectedView.Frame.Y);

			if (style.SelectedStripStyle != PagerStyle.StripStyle.Dot)
			{
				var stripFrame = this.selectedView.Frame;
				stripFrame.Width = tabSize.Width;
				this.selectedView.Frame = stripFrame;
			}

			for (int i = 0; i < this.sections.Length; i++)
			{
				var section = this.sections[i];
				var location = new CGPoint(i * tabSize.Width, 0);
				var label = new UILabel(new CGRect(location, tabSize))
				{
					Text = section,
					Font = style.LabelFont,
					TextAlignment = UITextAlignment.Center,
					TextColor = (this.SelectedIndex == i) ? this.style.SelectedLabelColor : this.style.UnselectedLabelColor,
					AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin,
				};
				this.labels[i] = label;
				this.AddSubview(label);
			}
		}

		private UIColor GetStripColor(int index) => this.style.SelectedStripColors[index % this.style.SelectedStripColors.Length];

		private void UpdateLabelColors(int touched = -1)
		{
			for (int i = 0; i < this.labels.Length; i++)
			{
				var label = this.labels[i];

				if (i == touched)
					label.TextColor = GetStripColor(i);
				else if (i == this.SelectedIndex)
					label.TextColor = this.style.SelectedLabelColor;
				else
					label.TextColor = this.style.UnselectedLabelColor;
			}
		}

		private void UpdateSelectedPosition(bool animated)
		{
			// Labels

			this.UpdateLabelColors();

			// Strip

			var y = this.selectedView.Frame.Y;
			var x = TabWidth * SelectedIndex + (TabWidth / 2);
			var height = this.selectedView.Frame.Height;
			var width = TabWidth;

			if (style.SelectedStripStyle == PagerStyle.StripStyle.Rounded)
			{
				width -= 2 * this.style.SelectedStripMargin;
			}
			else if (style.SelectedStripStyle == PagerStyle.StripStyle.Dot)
			{
				width = this.style.SelectedStripSize;
			}

			x -= width / 2;

			var destination = new CGRect(x,y,width,height);

			if (animated && this.style.SelectedStripAnimation != PagerStyle.StripAnimation.None)
			{
				if (this.style.SelectedStripAnimation == PagerStyle.StripAnimation.Stretched)
				{
					var step = CGRect.Union(selectedView.Frame, destination);
					var stepDuration = this.style.SelectedStripAnimationDuration / 2;
					UIView.Animate(stepDuration, 0, UIViewAnimationOptions.CurveEaseIn, () => this.selectedView.Frame = step, () =>
					{
						UIView.Animate(stepDuration, 0, UIViewAnimationOptions.CurveEaseOut, () => this.selectedView.Frame = destination , () => { });
					});

					UIView.Animate(this.style.SelectedStripAnimationDuration, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
					{
						this.selectedView.BackgroundColor = GetStripColor(SelectedIndex);
					}, () => { });
				}
				else if (this.style.SelectedStripAnimation == PagerStyle.StripAnimation.Constant)
				{
					UIView.Animate(this.style.SelectedStripAnimationDuration, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
					{
						this.selectedView.Frame = destination;
						this.selectedView.BackgroundColor = GetStripColor(SelectedIndex);
					}, () => { });

				}
			}
			else
			{
				this.selectedView.Frame = destination;
				this.selectedView.BackgroundColor = GetStripColor(SelectedIndex);
			}
		}

		#endregion

		#region Touch

		private UILabel GetTouchedLabel(Foundation.NSSet touches)
		{
			foreach (var item in touches)
			{
				var touch = (item as UITouch)?.LocationInView(this);
				if (touch != null && this.Bounds.Contains(touch.Value))
				{
					return this.labels.FirstOrDefault((l) => l.Frame.Contains(touch.Value));
				}
			}

			return null;
		}

		public override void TouchesCancelled(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled(touches, evt);
			this.UpdateLabelColors();
		}

		public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);
			this.TouchesMoved(touches, evt);
		}

		public override void TouchesMoved(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);

			var touchedindex = -1;
			var touchedlabel = this.GetTouchedLabel(touches);
			if (touchedlabel != null)
			{
				touchedindex = labels.ToList().IndexOf(touchedlabel);
			}

			this.UpdateLabelColors(touchedindex);
		}

		public override void TouchesEnded(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);

			this.UpdateLabelColors();

			var label = this.GetTouchedLabel(touches);
			if (label != null)
			{
				var index = labels.ToList().IndexOf(label);
				this.SelectAndRaise(index, true);
			}
		}

		#endregion
	}
}
