using Sega.Sonic3k.Launcher.Enums;

using System;
using System.Globalization;
using System.Windows.Data;

namespace Sega.Sonic3k.Launcher.Converters
{
    public class MusicModeToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is MusicMode & parameter is string)
            {
                return ((MusicMode)value).ToString() == (string)parameter;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter is string)
            {
                return Enum.Parse(typeof(MusicMode), (string)parameter);
            }
            //avoid red border (validation failure) around the calling UIElement
            return Binding.DoNothing;
        }
    }
}
