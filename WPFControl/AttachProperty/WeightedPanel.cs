using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFControl
{
    public class WeightedPanel : Panel
    {
        public static readonly DependencyProperty WeightProperty = DependencyProperty.RegisterAttached(
            "Weight", typeof(double), typeof(WeightedPanel),
                    new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsParentMeasure |
                                                                                                    FrameworkPropertyMetadataOptions.AffectsParentArrange));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation", typeof(Orientation), typeof(WeightedPanel),
            new FrameworkPropertyMetadata(Orientation.Horizontal,
                                          FrameworkPropertyMetadataOptions.AffectsMeasure |
                                          FrameworkPropertyMetadataOptions.AffectsArrange));

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        private double[] _normalWeights;

        public static void SetWeight(DependencyObject obj, double weight)
        {
            obj.SetValue(WeightProperty, weight);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (constraint.Width == double.PositiveInfinity || constraint.Height == double.PositiveInfinity)
                return Size.Empty;

            Rect[] rects = CalculateItemRects(constraint);
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                InternalChildren[i].Measure(rects[i].Size);
            }
            return constraint;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Rect[] rects = CalculateItemRects(finalSize);
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                InternalChildren[i].Arrange(rects[i]);
            }
            return finalSize;
        }

        private Rect[] CalculateItemRects(Size panelSize)
        {
            NormalizeWeights();

            Rect[] rects = new Rect[InternalChildren.Count];
            double offset = 0;
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                if (Orientation == Orientation.Horizontal)
                {
                    double width = panelSize.Width * _normalWeights[i];
                    rects[i] = new Rect(offset, 0, width, panelSize.Height);
                    offset += width;
                }
                else if (Orientation == Orientation.Vertical)
                {
                    double height = panelSize.Height * _normalWeights[i];
                    rects[i] = new Rect(0, offset, panelSize.Width, height);
                    offset += height;
                }
            }

            return rects;
        }

        private void NormalizeWeights()
        {
            // Calculate total weight
            double weightSum = 0;
            foreach (UIElement child in InternalChildren)
            {
                weightSum += (double)child.GetValue(WeightProperty);
            }

            // Normalize each weight
            _normalWeights = new double[InternalChildren.Count];
            for (int i = 0; i < InternalChildren.Count; i++)
            {
                _normalWeights[i] = (double)InternalChildren[i].GetValue(WeightProperty) / weightSum;
            }
        }
    }
}
