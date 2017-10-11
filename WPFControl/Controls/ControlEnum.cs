using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JP.HCZZP.WPFApp.Infrastructure.Controls
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
}
