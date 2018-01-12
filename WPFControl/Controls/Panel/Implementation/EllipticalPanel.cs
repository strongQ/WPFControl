using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace WPFControl.Controls
{
    public class EllipticalPanel : Panel
    {
        public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(
            "ItemWidth", typeof(double), typeof(EllipticalPanel), new FrameworkPropertyMetadata(0.0,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(
            "ItemHeight", typeof(double), typeof(EllipticalPanel), new FrameworkPropertyMetadata(0.0,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty UseFerrisWheelLayoutProperty = DependencyProperty.Register(
            "UseFerrisWheelLayout", typeof(bool), typeof(EllipticalPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsArrange));

        public bool UseFerrisWheelLayout
        {
            get { return (bool)GetValue(UseFerrisWheelLayoutProperty); }
            set { SetValue(UseFerrisWheelLayoutProperty, value); }
        }

        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (constraint.Width == double.PositiveInfinity || constraint.Height == double.PositiveInfinity)
                return Size.Empty;

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(new Size(ItemWidth, ItemHeight));
            }
            return constraint;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            // Calculate radius
            double radiusX = (finalSize.Width - ItemWidth) * 0.5;
            double radiusY = (finalSize.Height - ItemHeight) * 0.5;

            double count = InternalChildren.Count;

            // Sector angle between items
            double deltaAngle = 2 * Math.PI / count;

            // Center of the ellipse
            Point center = new Point(finalSize.Width / 2, finalSize.Height / 2);


            for (int i = 0; i < count; i++)
            {
                UIElement child = InternalChildren[i];

                // Calculate position
                double angle = i * deltaAngle;
                double x = center.X + radiusX * Math.Cos(angle) - ItemWidth / 2;
                double y = center.Y + radiusY * Math.Sin(angle) - ItemHeight / 2;

                if (UseFerrisWheelLayout)
                {
                    child.RenderTransform = null;
                }
                else
                {
                    child.RenderTransformOrigin = new Point(0.5, 0.5);
                    child.RenderTransform = new RotateTransform(angle * 180 / Math.PI);
                }

                child.Arrange(new Rect(x, y, ItemWidth, ItemHeight));
            }
            return finalSize;
        }
    }
}
