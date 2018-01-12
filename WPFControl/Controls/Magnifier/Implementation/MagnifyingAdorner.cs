using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFControl.Controls
{
    public class MagnifyingAdorner : Adorner
    {
        private ContentControl _container;
        private Ellipse _magView;
        private TranslateTransform _trans;
        private VisualBrush _magBrush;

        public ControlTemplate MagnifierTemplate { get; set; }
        public Size ContainerSize { get; set; }
        public double ScalingFactor { get; set; }


        //_magnifier = new MagnifyingAdorner(Container)
        //{
        //    MagnifierTemplate = (ControlTemplate)FindResource("MagnifierTemplate"),
        //        ContainerSize = new Size(300, 300),
        //        ScalingFactor = 3
        //    };
        //_magnifier.Prepare();
        //    AdornerLayer layer = AdornerLayer.GetAdornerLayer(Container);
        //layer.Add(_magnifier);

        public MagnifyingAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
        }

        public void Prepare()
        {
            _container = new ContentControl()
            {
                IsHitTestVisible = false,
                Template = MagnifierTemplate
            };

            if (MagnifierTemplate != null)
            {
                _container.ApplyTemplate();
                _container.RenderTransform = _trans = new TranslateTransform();
                _magView = (Ellipse)_container.Template.FindName("PART_MagnifyingView", _container);
                _magView.Fill = _magBrush = new VisualBrush(AdornedElement)
                {
                    // A centered transform, relative to the bounding box gets us the desired effect
                    RelativeTransform = new ScaleTransform(ScalingFactor, ScalingFactor, 0.5, 0.5),
                    Viewbox = new Rect(new Point(), ContainerSize),
                    ViewboxUnits = BrushMappingMode.Absolute
                };

                AdornedElement.MouseMove += OnMouseMove;
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            UpdateMagnifier();
        }

        private void UpdateMagnifier()
        {
            Point p = Mouse.GetPosition(AdornedElement);
            _trans.X = p.X - ContainerSize.Width / 2;
            _trans.Y = p.Y - ContainerSize.Height / 2;

            _magBrush.Viewbox = new Rect(new Point(_trans.X, _trans.Y), ContainerSize);
            InvalidateArrange();
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _container.Measure(ContainerSize);

            return ContainerSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _container.Arrange(new Rect(ContainerSize));
            return ContainerSize;
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index == 0)
            {
                return _container;
            }

            return null;
        }
    }
}
