using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFControl.Controls
{
    public class MessageBar:ItemsControl
    {
        #region 依赖属性
        /// <summary>
        /// 获取详情模板
        /// </summary>
        [Bindable(true), Description("图片模板")]
        public DataTemplate PictureTemplate
        {
            get { return (DataTemplate)GetValue(PictureTemplateProperty); }
            set { SetValue(PictureTemplateProperty, value); }
        }

        public static readonly DependencyProperty PictureTemplateProperty =
            DependencyProperty.Register("PictureTemplate", typeof(DataTemplate), typeof(MessageBar));

        /// <summary>
        /// 尾部模板
        /// </summary>
        [Bindable(true),Description("尾部模板")]
        public DataTemplate EndTemplate
        {
            get { return (DataTemplate)GetValue(EndTemplateProperty); }
            set { SetValue(EndTemplateProperty, value); }
        }

        public static readonly DependencyProperty EndTemplateProperty =
           DependencyProperty.Register("EndTemplate", typeof(DataTemplate), typeof(MessageBar));
        /// <summary>
        /// 是否显示图标
        /// </summary>
        [Bindable(true),Description("是否显示图标")]
        public Visibility ShowICO
        {
            get { return (Visibility)GetValue(ShowICOProperty); }
            set { SetValue(ShowICOProperty, value); }
        }

        public static readonly DependencyProperty ShowICOProperty =
            DependencyProperty.Register("ShowICO", typeof(Visibility), typeof(MessageBar),new PropertyMetadata(Visibility.Collapsed));
        #endregion

        static MessageBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBar), new FrameworkPropertyMetadata(typeof(MessageBar)));
        }
        /// <summary>
        /// 初始化用
        /// </summary>
        /// <param name="element"></param>
        /// <param name="item"></param>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            
            MessageBarItem messageBarItem = element as MessageBarItem;
          
           
            base.PrepareContainerForItemOverride(messageBarItem, item);
        }
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MessageBarItem();
        }
    }
}
