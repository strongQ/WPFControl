using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WPFControl.Controls.Transition
{
    public enum Direction
    {
        LeftToRight,
        RightToLeft
    }

    public class SlideTransition : TransitionBase
    {
        private Direction _direction;
        private Rectangle _nextRect;
        private Rectangle _prevRect;
        private System.Windows.Controls.Grid _rectContainer;

        public SlideTransition()
            : this(Direction.LeftToRight)
        {
        }

        public SlideTransition(Direction direction)
        {
            Direction = direction;
        }

        public Direction Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public override FrameworkElement SetupVisuals(VisualBrush prevBrush, VisualBrush nextBrush)
        {
            _prevRect = new Rectangle();
            _prevRect.Fill = prevBrush;
            _prevRect.RenderTransform = new TranslateTransform();

            _nextRect = new Rectangle();
            _nextRect.Fill = nextBrush;
            _nextRect.RenderTransform = new TranslateTransform();

            _rectContainer = new System.Windows.Controls.Grid();
            _rectContainer.ClipToBounds = true;
            _rectContainer.Children.Add(_nextRect);
            _rectContainer.Children.Add(_prevRect);

            return _rectContainer;
        }

        public override Storyboard PrepareStoryboard(TransitionContainer container)
        {
            Storyboard animator = new Storyboard();

            DoubleAnimation prevAnim = new DoubleAnimation();
            Storyboard.SetTarget(prevAnim, _prevRect);// Hilight this in the chapter!
            Storyboard.SetTargetProperty(prevAnim, new PropertyPath("(0).(1)", UIElement.RenderTransformProperty, TranslateTransform.XProperty));
            prevAnim.Duration = Duration;
            prevAnim.From = 0;
            prevAnim.To = Direction == Direction.RightToLeft ? -1 * container.ActualWidth : container.ActualWidth;

            DoubleAnimation nextAnim = new DoubleAnimation();
            Storyboard.SetTarget(nextAnim, _nextRect);
            Storyboard.SetTargetProperty(nextAnim, new PropertyPath("(0).(1)", UIElement.RenderTransformProperty, TranslateTransform.XProperty));
            nextAnim.Duration = Duration;
            nextAnim.From = Direction == Direction.RightToLeft ? container.ActualWidth : -1 * container.ActualWidth;
            nextAnim.To = 0;

            animator.Children.Add(prevAnim);
            animator.Children.Add(nextAnim);

            return animator;
        }
    }
}
