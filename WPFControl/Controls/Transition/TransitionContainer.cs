using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WPFControl.Controls.Transition;

namespace WPFControl.Controls
{
    [ContentProperty("Children")]
    public class TransitionContainer : ContentControl
    {


        #region Test
        //_trans.Transition = new FadeTransition();
        //_trans.Transition = new SlideTransition();
        //_trans.ApplyTransition(_prev, _next);
        #endregion





        public event EventHandler TransitionCompleted;

        public static readonly DependencyProperty TransitionProperty =
            DependencyProperty.Register("Transition", typeof(TransitionBase), typeof(TransitionContainer));

        private readonly System.Windows.Controls.Grid _childContainer;
        private readonly System.Windows.Controls.Grid _rootContainer;
        private readonly System.Windows.Controls.Grid _transitionContainer;

        private UIElement _nextChild;
        private UIElement _prevChild;

        public UIElementCollection Children
        {
            get { return _childContainer.Children; }
        }

        public TransitionBase Transition
        {
            get { return (TransitionBase)GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }

        public TransitionContainer()
        {
            _childContainer = new System.Windows.Controls.Grid();
            _transitionContainer = new System.Windows.Controls.Grid();

            _rootContainer = new System.Windows.Controls.Grid();
            _rootContainer.Children.Add(_transitionContainer);
            _rootContainer.Children.Add(_childContainer);

            Content = _rootContainer;
        }


        public void ApplyTransition(string prevChildName, string nextChildName)
        {
            FrameworkElement prevChild = (FrameworkElement)FindName(prevChildName);
            FrameworkElement nextChild = (FrameworkElement)FindName(nextChildName);

            ApplyTransition(prevChild, nextChild);
        }

        public void ApplyTransition(UIElement prevChild, UIElement nextChild)
        {
            if (prevChild == null)
            {
                throw new ArgumentNullException("prevChild cannot be null");
            }

            if (nextChild == null)
            {
                throw new ArgumentNullException("nextChild cannot be null");
            }

            _prevChild = prevChild;
            _nextChild = nextChild;

            StartTransition();
        }

        private void StartTransition()
        {
            // Make the children Visible, so that the VisualBrush will not be blank
            _prevChild.Visibility = Visibility.Visible;
            _nextChild.Visibility = Visibility.Visible;

            // Switch to transition-mode
            FrameworkElement root = Transition.SetupVisuals(CreateBrush(_prevChild), CreateBrush(_nextChild));
            _transitionContainer.Children.Add(root);
            _transitionContainer.Visibility = Visibility.Visible;
            _childContainer.Visibility = Visibility.Hidden;

            // Get Storyboard to play
            Storyboard sb = Transition.PrepareStoryboard(this);
            EventHandler handler = null;
            handler = delegate
            {
                sb.Completed -= handler;
                FinishTransition();
            };
            sb.Completed += handler;
            sb.Begin(_transitionContainer);
        }

        private VisualBrush CreateBrush(UIElement elt)
        {
            VisualBrush brush = new VisualBrush(elt);
            RenderOptions.SetCachingHint(brush, CachingHint.Cache);
            brush.Viewbox = new Rect(0, 0, ActualWidth, ActualHeight);
            brush.ViewboxUnits = BrushMappingMode.Absolute;
            return brush;
        }

        public void FinishTransition()
        {
            // Bring the next-child on top
            ChangeChildrenStackOrder();

            _transitionContainer.Visibility = Visibility.Hidden;
            _childContainer.Visibility = Visibility.Visible;
            _transitionContainer.Children.Clear();

            NotifyTransitionCompleted();
        }

        private void ChangeChildrenStackOrder()
        {
            Panel.SetZIndex(_nextChild, 1);
            foreach (UIElement element in _childContainer.Children)
            {
                if (element != _nextChild)
                {
                    Panel.SetZIndex(element, 0);
                    element.Visibility = Visibility.Hidden;
                }
            }
        }

        private void NotifyTransitionCompleted()
        {
            if (TransitionCompleted != null)
            {
                TransitionCompleted(this, null);
            }
        }
    }
}
