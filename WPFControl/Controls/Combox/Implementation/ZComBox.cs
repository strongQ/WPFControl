using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JP.HCZZP.WPFApp.Infrastructure.Controls
{
    public class ZComBox : ComboBox
    {
        #region Private属性

        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius"
            , typeof(System.Windows.CornerRadius), typeof(ZComBox));
        /// <summary>
        /// 边框圆角
        /// </summary>
        public System.Windows.CornerRadius CornerRadius
        {
            get { return (System.Windows.CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark"
            , typeof(string), typeof(ZComBox));
        /// <summary>
        /// 文本输入框的水印
        /// </summary>
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }
        #endregion

        #region Constructors
        static ZComBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZComBox), new FrameworkPropertyMetadata(typeof(ZComBox)));
        }
        #endregion

        #region 依赖属性set get

        #endregion

        #region Override方法
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

        }
        #endregion

        #region Private方法

        #endregion
    }
}
