using System.Windows;
using SWCT = System.Windows.Controls.ToolTip;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace JP.HCZZP.WPFApp.Infrastructure.Controls
{
    public class ToolTip : SWCT
    {
        #region Private属性

        #endregion

        #region 依赖属性定义
        public static readonly DependencyProperty PlacementExProperty = DependencyProperty.Register("PlacementEx"
            , typeof(EnumPlacement), typeof(ToolTip), new PropertyMetadata(EnumPlacement.TopLeft));
        public static readonly DependencyProperty IsShowShadowProperty = DependencyProperty.Register("IsShowShadow"
            , typeof(bool), typeof(ToolTip), new PropertyMetadata(true));
        #endregion

        #region 依赖属性set get
        /// <summary>
        /// 鼠标按下时按钮的背景色
        /// </summary>
        public EnumPlacement PlacementEx
        {
            get { return (EnumPlacement)GetValue(PlacementExProperty); }
            set { SetValue(PlacementExProperty, value); }
        }

        /// <summary>
        /// 是否显示阴影
        /// </summary>
        public bool IsShowShadow
        {
            get { return (bool)GetValue(IsShowShadowProperty); }
            set { SetValue(IsShowShadowProperty, value); }
        }
        #endregion

        #region Constructors
        static ToolTip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolTip), new FrameworkPropertyMetadata(typeof(ToolTip)));
        }
        #endregion

        #region Override方法
        public ToolTip()
        {

        }
        #endregion

        #region Private方法

        #endregion
    }

    #region EnumPlacement
    public enum EnumPlacement
    {
        /// <summary>
        /// 左上
        /// </summary>
        LeftTop,
        /// <summary>
        /// 左中
        /// </summary>
        LeftBottom,
        /// <summary>
        /// 左下
        /// </summary>
        LeftCenter,
        /// <summary>
        /// 右上
        /// </summary>
        RightTop,
        /// <summary>
        /// 右下
        /// </summary>
        RightBottom,
        /// <summary>
        /// 右中
        /// </summary>
        RightCenter,
        /// <summary>
        /// 上左
        /// </summary>
        TopLeft,
        /// <summary>
        /// 上中
        /// </summary>
        TopCenter,
        /// <summary>
        /// 上右
        /// </summary>
        TopRight,
        /// <summary>
        /// 下左
        /// </summary>
        BottomLeft,
        /// <summary>
        /// 下中
        /// </summary>
        BottomCenter,
        /// <summary>
        /// 下右
        /// </summary>
        BottomRight,
    }
    #endregion


}
