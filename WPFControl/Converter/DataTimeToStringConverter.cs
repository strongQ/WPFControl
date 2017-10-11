using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JP.HCZZP.WPFApp.Infrastructure.Converter
{
    public class DataTimeToStringConverter : System.Windows.Data.IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                return (DateTime)value == default(DateTime) ? "暂无更新" : value.ToString();
            }
            else
            {
                return "暂无更新";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
