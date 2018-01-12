using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFControl
{
    public class ParallaxHelper : DependencyObject
    {
        public static readonly DependencyProperty ApplyParallaxProperty = DependencyProperty.RegisterAttached(
            "ApplyParallax", typeof(bool), typeof(ParallaxHelper), new PropertyMetadata(false, OnApplyParallaxChanged));

        private static void OnApplyParallaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool apply = (bool)e.NewValue;
            if (apply)
            {
                Panel panel = d as Panel;
                RoutedEventHandler handler = null;
                handler = delegate
                {
                    panel.Loaded -= handler;
                    AttachTransforms(panel);
                    panel.MouseMove += OnCanvasMouseMove;

                    ApplyParallax(panel, new Point());
                };
                panel.Loaded += handler;
            }
        }

        private static void ApplyParallax(Panel panel, Point factor)
        {
            foreach (UIElement child in panel.Children)
            {
                if (!CanMove(child, panel)) continue;

                Rect childRect = new Rect(child.RenderSize);
                Rect canvasRect = new Rect(panel.RenderSize);

                double x = (childRect.Width - canvasRect.Width) * factor.X;
                double y = (childRect.Height - canvasRect.Height) * factor.Y;

                TranslateTransform xform = child.RenderTransform as TranslateTransform;
                xform.X = -x;
                xform.Y = -y;
            }

        }

        private static bool CanMove(UIElement child, Panel panel)
        {
            Rect childRect = new Rect(child.RenderSize);

            Rect containerRect = new Rect(panel.RenderSize);

            bool isCompletelyWithin = Rect.Union(childRect, containerRect).Equals(containerRect);
            return !isCompletelyWithin;
        }


        private static void OnCanvasMouseMove(object sender, MouseEventArgs e)
        {
            Panel panel = sender as Panel;
            Point factor = e.GetPosition(panel);
            factor.X = factor.X / panel.RenderSize.Width;
            factor.Y = factor.Y / panel.RenderSize.Height;

            ApplyParallax(panel, factor);

        }

        private static void AttachTransforms(Panel panel)
        {
            foreach (UIElement child in panel.Children)
            {
                child.RenderTransform = new TranslateTransform();
                //child.RenderTransformOrigin = new Point(0.5, 0.5);
            }

        }

        public static void SetApplyParallax(Panel panel, bool apply)
        {
            panel.SetValue(ApplyParallaxProperty, apply);
        }
    }
}
