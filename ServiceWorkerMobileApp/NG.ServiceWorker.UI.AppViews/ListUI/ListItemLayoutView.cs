using System;

using Xamarin.Forms;

namespace NG.ServiceWorker.UI.ListUI
{
    public class ListItemLayoutView : Layout<View>
    {
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            const double HMARGIN = 16.0;
            const double DISC_PADDING = 8.0;

            // Get views
            View mainLabelView = this.Children[0];
            View subtitleLabelView = this.Children[1];
            View attributeLabelView = this.Children[2];
            View disclosureView = this.Children[3];
            View tickView = this.Children[4];

            // Apply margin
            x += HMARGIN;
            width -= HMARGIN + HMARGIN;

            // Layout disclosure and tick
            if (disclosureView.IsVisible)
            {
                double viewWidth = disclosureView.WidthRequest;
                double viewHeight = disclosureView.HeightRequest;
                double viewTop = (height - viewHeight) / 2;
                LayoutChildIntoBoundingRegion(disclosureView, new Rectangle(x + width - viewWidth, y + viewTop, viewWidth, viewHeight));
                width -= viewWidth + DISC_PADDING;
            }
            if (tickView.IsVisible)
            {
                double viewWidth = tickView.WidthRequest;
                double viewHeight = tickView.HeightRequest;
                double viewTop = (height - viewHeight) / 2;
                LayoutChildIntoBoundingRegion(tickView, new Rectangle(x + width - viewWidth, y + viewTop, viewWidth, viewHeight));
                width -= viewWidth + DISC_PADDING;
            }

            // Check type
            if (attributeLabelView.IsVisible)
            {
                SizeRequest nameMeasure = mainLabelView.Measure(width, height);
                SizeRequest valueMeasure = attributeLabelView.Measure(width, height);
                double nameWidth = Math.Min(nameMeasure.Request.Width, width);
                double valueWidth = Math.Min(valueMeasure.Request.Width, width);
                double nameTop = (height - nameMeasure.Request.Height) / 2;
                double valueTop = (height - valueMeasure.Request.Height) / 2;
                LayoutChildIntoBoundingRegion(mainLabelView, new Rectangle(x, y + nameTop, nameWidth, nameMeasure.Request.Height));
                LayoutChildIntoBoundingRegion(attributeLabelView, new Rectangle(x + width - valueWidth, y + valueTop, valueWidth, valueMeasure.Request.Height));
            }
            else if (subtitleLabelView.IsVisible)
            {
                SizeRequest titleMeasure = mainLabelView.Measure(width, height);
                SizeRequest subtitleMeasure = subtitleLabelView.Measure(width, height);
                double titleWidth = Math.Min(titleMeasure.Request.Width, width);
                double subTitleWidth = Math.Min(subtitleMeasure.Request.Width, width);
                double labelTotalHeight = titleMeasure.Request.Height + subtitleMeasure.Request.Height;
                double titleTop = (height - labelTotalHeight) / 2;
                double subTitleTop = titleTop + titleMeasure.Request.Height;
                LayoutChildIntoBoundingRegion(mainLabelView, new Rectangle(x, y + titleTop, titleWidth, titleMeasure.Request.Height));
                LayoutChildIntoBoundingRegion(subtitleLabelView, new Rectangle(x, y + subTitleTop, subTitleWidth, subtitleMeasure.Request.Height));
            }
            else
            {
                SizeRequest mainLabelMeasure = mainLabelView.Measure(width, height);
                double viewWidth = Math.Min(mainLabelMeasure.Request.Width, width);
                double viewTop = (height - mainLabelMeasure.Request.Height) / 2;
                LayoutChildIntoBoundingRegion(mainLabelView, new Rectangle(x, y + viewTop, viewWidth, mainLabelMeasure.Request.Height));
            }
        }
    }
}
