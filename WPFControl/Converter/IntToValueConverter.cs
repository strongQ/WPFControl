using System;
using System.Globalization;
using System.Windows;

namespace JP.HCZZP.WPFApp.Infrastructure.Converter
{
    public class IntToValueConverter : System.Windows.Data.IValueConverter
    {
        public object ShowValue
        {
            get { return showValue; }
            set { showValue = value; }
        }
        object showValue;

        public object KeyValue
        {
            get { return keyValue; }
            set { keyValue = value; }
        }
        object keyValue;


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] keys = keyValue.ToString().Split('|');
            if (value != null && keys.Length > 0)
            {
                return showValue.ToString().Split('|')[Array.IndexOf(keys, value.ToString())];
            }
            else
            {
                return "error";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
