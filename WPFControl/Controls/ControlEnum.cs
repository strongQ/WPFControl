using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFControl.Controls
{
    #region IconType
    public enum EnumIconType
    {
        左翻页,
        右翻页,
      
        File,
        Picture,
        MessageArrow,
        ExclamationPoin,
        Left,
        NativePlace,
        Car,
        Phone,
        WeChart,
        QQ,
        Male,
        FeMale,
        PoliceStation,
        IDCard,
        Arrow,
        Right,
        Info,
        Error,
        Warning,
        Success,
        MacOS,
        Windows,
        Linux,
        Android,
        Star_Empty,
        Star_Half,
        Star_Full,
    }
    #endregion

    public enum EnumCompare
    {
        /// <summary>
        /// 小于
        /// </summary>
        Less,
        /// <summary>
        /// 等于
        /// </summary>
        Equal,
        /// <summary>
        /// 大于
        /// </summary>
        Large,
        None,
    }

    #region EnumLoadingType
    public enum EnumLoadingType
    {
        /// <summary>
        /// 两个环形
        /// </summary>
        DoubleArc,
        /// <summary>
        /// 两个圆
        /// </summary>
        DoubleRound,
        /// <summary>
        /// 一个圆
        /// </summary>
        SingleRound,
        /// <summary>
        /// 仿Win10加载条
        /// </summary>
        Win10,
        /// <summary>
        /// 仿Android加载条
        /// </summary>
        Android,
        /// <summary>
        /// 仿苹果加载条
        /// </summary>
        Apple,
        Cogs,
        Normal,
    }
    #endregion
}
