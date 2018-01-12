using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace WPFControl.Controls
{
    public class DockSlidePresenter : FrameworkElement
    {
        private System.Windows.Controls.Grid _root;
        private Viewport3D _viewport;
        private ModelUIElement3D _meshModel;
        private Decorator _frontChildContainer;
        private Decorator _backChildContainer;
        private static ResourceDictionary _dictionary;


        public FrameworkElement FrontChild { get; set; }
        public FrameworkElement BackChild { get; set; }

        public static readonly DependencyProperty IsChildDockedProperty = DependencyProperty.Register(
            "IsChildDocked", typeof(bool), typeof(DockSlidePresenter),
            new PropertyMetadata(false, OnIsChildDockedChanged));

        private static void OnIsChildDockedChanged(DependencyObject d,
                                                   DependencyPropertyChangedEventArgs e)
        {
            DockSlidePresenter presenter = d as DockSlidePresenter;
            bool docked = (bool)e.NewValue;
            if (docked) presenter.DockFrontChild();
            else presenter.UndockFrontChild();
        }

        static DockSlidePresenter()
        {
            ClipToBoundsProperty.OverrideMetadata(typeof(DockSlidePresenter), new PropertyMetadata(true));
            _dictionary = Application.LoadComponent(new Uri("/WPFControl;component/Controls/3D/Themes/InternalResources.xaml", UriKind.Relative)) as
                                                        ResourceDictionary;

        }

        protected override void OnInitialized(EventArgs e)
        {
            BuildVisualTree();
            AddVisualChild(_root);
        }

        private void BuildVisualTree()
        {
            // Viewport and Models
            _root = Application.LoadComponent(new Uri("/WPFControl;component/Controls/3D/Themes/DockSlideVisualTree.xaml", UriKind.Relative)) as System.Windows.Controls.Grid;

            // Viewport
            _viewport = _root.FindName("Viewport") as Viewport3D;
            _meshModel = (_viewport.Children[0] as ContainerUIElement3D).Children[0] as ModelUIElement3D;

            // Texture for the model
            VisualBrush brush = new VisualBrush(FrontChild);
            ((_meshModel.Model as GeometryModel3D).Material as DiffuseMaterial).Brush = brush;

            // Front child
            _frontChildContainer = _root.FindName("FrontChildContainer") as Decorator;
            _frontChildContainer.Child = FrontChild;

            // Back Child
            _backChildContainer = _root.FindName("BackChildContainer") as Decorator;
            _backChildContainer.Child = BackChild;
        }

        #region Viewport3D config

        private void AdjustViewport3D()
        {
            PositionCamera();
            AdjustMeshModel();
        }

        private void AdjustMeshModel()
        {
            double aspect = ActualWidth / ActualHeight;
            Point3DCollection positions = new Point3DCollection();
            positions.Add(new Point3D(-aspect / 2, 0.5, 0));
            positions.Add(new Point3D(aspect / 2, 0.5, 0));
            positions.Add(new Point3D(aspect / 2, -0.5, 0));
            positions.Add(new Point3D(-aspect / 2, -0.5, 0));

            GeometryModel3D model = _meshModel.Model as GeometryModel3D;
            MeshGeometry3D mesh = model.Geometry as MeshGeometry3D;
            mesh.Positions = positions;

            // Rotating about the left hinge
            ((model.Transform as Transform3DGroup).Children[0] as RotateTransform3D).CenterX = -aspect / 2;
        }

        private void PositionCamera()
        {
            PerspectiveCamera camera = _viewport.Camera as PerspectiveCamera;
            double aspect = ActualWidth / ActualHeight;
            double z = aspect * 0.866025404;
            camera.Position = new Point3D(0, 0, z);
        }

        #endregion

        private void DockFrontChild()
        {
            // Hide the front child container
            _frontChildContainer.Visibility = Visibility.Hidden;

            _viewport.Visibility = Visibility.Visible;

            Storyboard dockAnim = PrepareStoryboard(new AnimationParameters
            {
                DockDirection = "Dock",
                AnimationType = "Dock"
            });
            dockAnim.Begin(_viewport, true);

            // show the back child
            Panel.SetZIndex(_backChildContainer, 1);
            Storyboard slideAnim = PrepareStoryboard(new AnimationParameters
            {
                DockDirection = "Dock",
                AnimationType = "Slide"
            });

            slideAnim.Begin(_backChildContainer, true);
        }

        private void UndockFrontChild()
        {
            Storyboard dockAnim = PrepareStoryboard(new AnimationParameters
            {
                DockDirection = "Undock",
                AnimationType = "Dock"
            });
            EventHandler handler = null;
            handler = delegate
            {
                // Hide the Viewport3D
                _viewport.Visibility = Visibility.Hidden;
                Panel.SetZIndex(_backChildContainer, 0);

                // show the FrontChild
                _frontChildContainer.Visibility = Visibility.Visible;
                dockAnim.Completed -= handler;
            };
            dockAnim.Completed += handler;
            dockAnim.Begin(_viewport, true);

            // show the back child
            Storyboard slideAnim = PrepareStoryboard(new AnimationParameters
            {
                DockDirection = "Undock",
                AnimationType = "Slide"
            });

            slideAnim.Begin(_backChildContainer, true);
        }

        private Storyboard PrepareStoryboard(AnimationParameters ap)
        {
            Storyboard anim = GetBaseStoryboard(ap.AnimationType == "Dock" ? "DockAnimation" : "SlideAnimation");

            double from = 0;
            double to = 0;
            if (ap.AnimationType == "Dock")
            {
                from = ap.DockDirection == "Dock" ? 0 : 90;
                to = ap.DockDirection == "Dock" ? 90 : 0;
            }
            else if (ap.AnimationType == "Slide")
            {
                from = ap.DockDirection == "Dock" ? -ActualWidth : 0;
                to = ap.DockDirection == "Dock" ? 0 : -ActualWidth;

                double opacity = ap.DockDirection == "Dock" ? 1 : 0;
                (anim.Children[1] as DoubleAnimation).To = opacity;
            }

            (anim.Children[0] as DoubleAnimation).From = from;
            (anim.Children[0] as DoubleAnimation).To = to;

            return anim;
        }

        private Storyboard GetBaseStoryboard(string animationName)
        {
            return _dictionary[animationName] as Storyboard;
        }

        #region Layout Overrides

        protected override Size MeasureOverride(Size availableSize)
        {
            _root.Measure(availableSize);
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _root.Arrange(new Rect(finalSize));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return _root;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            AdjustViewport3D();
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        #endregion
    }

    internal sealed class AnimationParameters
    {
        public string DockDirection { get; set; }
        public string AnimationType { get; set; }
    }
}
