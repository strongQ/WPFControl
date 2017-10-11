using System;
using System.Globalization;
using System.Windows;

namespace JP.HCZZP.WPFApp.Infrastructure.Converter
{
    public class BoolToValueConverter : System.Windows.Data.IValueConverter
    {
        public object TrueValue
        {
            get { return trueValue; }
            set { trueValue = value; }
        }
        object trueValue;

        public object FalseValue
        {
            get { return falseValue; }
            set { falseValue = value; }
        }
        object falseValue;


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (value == null)
            //	return DependencyProperty.UnsetValue;
            if (value.GetType().Name == "Decimal")
            {
                if ((decimal)value == 1)
                {
                    return TrueValue;
                }
                else
                {
                    return FalseValue;
                }
            }

            if (value != null && (bool)value)
                return TrueValue;
            else
                return FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

}
