using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SWCT = System.Windows.Controls.ToolTip;

namespace WPFControl.Controls
{
    /// <summary>
    /// ToolTip2.xaml 的交互逻辑
    /// </summary>
    public class ToolTip2 : SWCT
    {
        #region 提示框阴影
        public static readonly DependencyProperty IsShowShadowProperty = DependencyProperty.Register("IsShowShadow"
            , typeof(bool), typeof(ToolTip2), new PropertyMetadata(true, new PropertyChangedCallback(IsShowShadowChanged)));
        /// <summary>
        /// 是否显示阴影
        /// </summary>
        public bool IsShowShadow
        {
            get { return (bool)GetValue(IsShowShadowProperty); }
            set { SetValue(IsShowShadowProperty, value); }
        }
        private static void IsShowShadowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uc = d as FrameworkElement;
            if (uc != null)
            {
                ToolTip2 ucToolTip2 = (ToolTip2)uc;
                ucToolTip2.HasDropShadow = ucToolTip2.IsShowShadow;
            }

        }
        #endregion

        #region 内容
        public static readonly DependencyProperty ContentElementProperty = DependencyProperty.Register("ContentElement"
            , typeof(UIElement), typeof(ToolTip2));
        /// <summary>
        /// 内容自定义
        /// </summary>
        public UIElement ContentElement
        {
            get { return (UIElement)GetValue(ContentElementProperty); }
            set { SetValue(ContentElementProperty, value); }
        }

        #endregion
        static ToolTip2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolTip2), new FrameworkPropertyMetadata(typeof(ToolTip2)));
        }

        protected override void OnOpened(RoutedEventArgs e)
        {
            e.Handled = checkContent(((ToolTip2)e.Source).Content);
            if (e.Handled)
                return;
            base.OnOpened(e);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            checkContent(newContent);

            base.OnContentChanged(oldContent, newContent);
        }

        private bool checkContent(object newContent)
        {
            if (newContent is string)
            {
                string StrContent = (string)newContent;
                if (string.IsNullOrWhiteSpace(StrContent))
                {

                    SetVisibility(false);
                    return true;
                }
                else
                {
                    SetVisibility();

                }
            }
            else if (newContent is TextBlock)
            {
                var tb = (TextBlock)newContent;
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    SetVisibility(false);
                    return true;
                }
                else
                {
                    SetVisibility();
                }
            }
            else
            {
                if (newContent == null)
                {
                    SetVisibility(false);
                    return true;
                }
                else
                {
                    SetVisibility();
                }
            }
            return false;
        }
        private void SetVisibility(bool flag = true)
        {
            if (flag)
            {
                this.Visibility = Visibility.Visible;
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
            }
        }
        public ToolTip2()
        {

            //DefaultStyleKeyProperty.OverrideMetadata(typeof(SWCT), new FrameworkPropertyMetadata(typeof(SWCT)));
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolTip2), new FrameworkPropertyMetadata(typeof(ToolTip2)));
        }
    }
}
