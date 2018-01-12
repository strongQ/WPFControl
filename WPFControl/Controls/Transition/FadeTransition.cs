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
    public class FadeTransition : TransitionBase
    {
        private Rectangle _nextRect;
        private Rectangle _prevRect;
        private System.Windows.Controls.Grid _rectContainer;

        public override FrameworkElement SetupVisuals(VisualBrush prevBrush, VisualBrush nextBrush)
        {
            _prevRect = new Rectangle();
            _prevRect.Fill = prevBrush;

            _nextRect = new Rectangle();
            _nextRect.Fill = nextBrush;

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
            Storyboard.SetTarget(prevAnim, _prevRect);
            Storyboard.SetTargetProperty(prevAnim, new PropertyPath("Opacity"));
            prevAnim.Duration = Duration;
            prevAnim.From = 1;
            prevAnim.To = 0;

            DoubleAnimation nextAnim = new DoubleAnimation();
            Storyboard.SetTarget(nextAnim, _nextRect);
            Storyboard.SetTargetProperty(nextAnim, new PropertyPath("Opacity"));
            nextAnim.Duration = Duration;
            nextAnim.From = 0;
            nextAnim.To = 1;

            animator.Children.Add(prevAnim);
            animator.Children.Add(nextAnim);

            return animator;
        }
    }
}
