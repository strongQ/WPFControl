using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;


namespace WPFControl.Controls.Converter
{
    public class UrlToBitmapSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null&&!string.IsNullOrWhiteSpace(value.ToString()))
            {
                string url = value.ToString( );
                try
                {
                    Uri uri = new Uri(value.ToString( ), UriKind.Absolute);
                    //ImageExtensions.GetImageFromNet(value.ToString( ));
                    BitmapSource source = new BitmapImage(uri);
                    if(source!=null)
                    return source;
                }
                catch (Exception ex)
                {
                    //return ImageExtensions.GetImageFromNet(value.ToString( ));
                    Console.WriteLine(ex.Message );
                }
            }
            //return new BitmapImage(new Uri("ftp://172.17.99.111/Doc/qqqqq.jpg"));
            //  return Application.Current.Resources["ImageNoFind"] as BitmapImage;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
