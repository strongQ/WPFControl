using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WPFControl.Controls
{
    public class RowsPanel : Panel, IScrollInfo
    {
        public static readonly DependencyProperty RowHeightProperty = DependencyProperty.Register(
            "RowHeight",
            typeof(double),
            typeof(RowsPanel),
            new PropertyMetadata(30.0D));

        public bool AnimateScroll { get; set; }
        public RowsPanel()
        {
            // For use in the IScrollInfo implementation
            _trans = new TranslateTransform();
            this.RenderTransform = _trans;
        }

        public double RowHeight
        {
            get { return (double)GetValue(RowHeightProperty); }
            set { SetValue(RowHeightProperty, value); }
        }

        /// <summary>
        /// Measure the children
        /// </summary>
        /// <param name="constraint">Size available</param>
        /// <returns>Size desired</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            if (constraint.Width == double.PositiveInfinity || constraint.Height == double.PositiveInfinity)
                return Size.Empty;

            UpdateScrollInfo(constraint);

            int childCount = InternalChildren.Count;
            for (int i = 0; i < childCount; i++)
            {
                InternalChildren[i].Measure(new Size(constraint.Width, RowHeight));
            }

            return constraint;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            UpdateScrollInfo(finalSize);

            int childCount = InternalChildren.Count;
            for (int i = 0; i < childCount; i++)
            {
                UIElement child = InternalChildren[i];

                child.Arrange(new Rect(0, i * RowHeight, finalSize.Width, RowHeight));
            }

            return finalSize;
        }

        #region Layout specific code

        private Size CalculateExtent(Size availableSize, int itemCount)
        {
            return new Size(availableSize.Width, RowHeight * itemCount);
        }

        private void ArrangeChild(int itemIndex, UIElement child, Size finalSize)
        {
            child.Arrange(new Rect(0, itemIndex * RowHeight, finalSize.Width, RowHeight));
        }

        #endregion

        #region IScrollInfo implementation

        private void UpdateScrollInfo(Size availableSize)
        {
            // See how many items there are
            int itemCount = InternalChildren.Count;
            bool viewportChanged = false;
            bool extentChanged = false;

            Size extent = CalculateExtent(availableSize, itemCount);
            // Update extent
            if (extent != _extent)
            {
                _extent = extent;
                extentChanged = true;
            }

            // Update viewport
            if (availableSize != _viewport)
            {
                _viewport = availableSize;
                viewportChanged = true;
            }

            if ((extentChanged || viewportChanged) && _scrollOwner != null)
            {
                _offset.Y = CalculateVerticalOffset(VerticalOffset);
                _offset.X = CalculateHorizontalOffset(HorizontalOffset);
                _scrollOwner.InvalidateScrollInfo();

                Scroll(HorizontalOffset, VerticalOffset);
            }
        }

        public ScrollViewer ScrollOwner
        {
            get { return _scrollOwner; }
            set { _scrollOwner = value; }
        }

        public bool CanHorizontallyScroll
        {
            get { return _canHScroll; }
            set { _canHScroll = value; }
        }

        public bool CanVerticallyScroll
        {
            get { return _canVScroll; }
            set { _canVScroll = value; }
        }

        public double HorizontalOffset
        {
            get { return _offset.X; }
        }

        public double VerticalOffset
        {
            get { return _offset.Y; }
        }

        public double ExtentHeight
        {
            get { return _extent.Height; }
        }

        public double ExtentWidth
        {
            get { return _extent.Width; }
        }

        public double ViewportHeight
        {
            get { return _viewport.Height; }
        }

        public double ViewportWidth
        {
            get { return _viewport.Width; }
        }

        public void LineUp()
        {
            SetVerticalOffset(VerticalOffset - 10);
        }

        public void LineDown()
        {
            SetVerticalOffset(VerticalOffset + 10);
        }

        public void PageUp()
        {
            SetVerticalOffset(VerticalOffset - ViewportHeight);
        }

        public void PageDown()
        {
            SetVerticalOffset(VerticalOffset + ViewportHeight);
        }

        public void MouseWheelUp()
        {
            SetVerticalOffset(this.VerticalOffset - 10);
        }

        public void MouseWheelDown()
        {
            SetVerticalOffset(this.VerticalOffset + 10);
        }

        public void LineLeft()
        {
            SetHorizontalOffset(HorizontalOffset - 10);
        }

        public void LineRight()
        {
            SetHorizontalOffset(HorizontalOffset + 10);
        }

        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            return rectangle;
        }

        public void MouseWheelLeft()
        {
            SetHorizontalOffset(HorizontalOffset - 10);
        }

        public void MouseWheelRight()
        {
            SetHorizontalOffset(HorizontalOffset + 10);
        }

        public void PageLeft()
        {
            SetHorizontalOffset(HorizontalOffset - ViewportWidth);
        }

        public void PageRight()
        {
            SetHorizontalOffset(HorizontalOffset + ViewportWidth);
        }

        public void SetHorizontalOffset(double offset)
        {
            offset = CalculateHorizontalOffset(offset);

            _offset.X = offset;

            if (_scrollOwner != null)
                _scrollOwner.InvalidateScrollInfo();

            Scroll(offset, VerticalOffset);

            // Force us to realize the correct children
            InvalidateMeasure();
        }

        public void SetVerticalOffset(double offset)
        {
            offset = CalculateVerticalOffset(offset);

            _offset.Y = offset;

            if (_scrollOwner != null)
                _scrollOwner.InvalidateScrollInfo();

            Scroll(HorizontalOffset, offset);
        }

        private double CalculateVerticalOffset(double offset)
        {
            if (offset < 0 || _viewport.Height >= _extent.Height)
            {
                offset = 0;
            }
            else
            {
                if (offset + _viewport.Height >= _extent.Height)
                {
                    offset = _extent.Height - _viewport.Height;
                }
            }
            return offset;
        }

        private double CalculateHorizontalOffset(double offset)
        {
            if (offset < 0 || _viewport.Width >= _extent.Width)
            {
                offset = 0;
            }
            else
            {
                if (offset + _viewport.Width >= _extent.Width)
                {
                    offset = _extent.Width - _viewport.Width;
                }
            }
            return offset;
        }

        private void Scroll(double xOffset, double yOffset)
        {
            if (AnimateScroll)
            {
                DoubleAnimation anim = new DoubleAnimation(-yOffset, new Duration(TimeSpan.FromMilliseconds(500)));
                PropertyPath p = new PropertyPath("(0).(1)", RenderTransformProperty, TranslateTransform.YProperty);
                Storyboard.SetTargetProperty(anim, p);

                Storyboard sb = new Storyboard();
                sb.Children.Add(anim);
                EventHandler handler = null;
                handler = delegate
                {
                    sb.Completed -= handler;
                    sb.Remove(this);

                    _trans.X = -xOffset;
                    _trans.Y = -yOffset;
                };
                sb.Completed += handler;
                sb.Begin(this, true);
            }
            else
            {
                // Translate
                _trans.X = -xOffset;
                _trans.Y = -yOffset;
            }
        }

        private TranslateTransform _trans = new TranslateTransform();
        private ScrollViewer _scrollOwner;
        private bool _canHScroll = false;
        private bool _canVScroll = false;
        private Size _extent = new Size(0, 0);
        private Size _viewport = new Size(0, 0);
        private Point _offset;

        #endregion
    }
}
