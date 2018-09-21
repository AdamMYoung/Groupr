using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Groupr.Client.Util
{
    /// <summary>
    ///     Converts a null check into a visibility value. Visibile if not null, Hidden if null.
    /// </summary>
    internal class NullToVisibilityConverter : IValueConverter
    {
        /// <summary>
        ///     Instance of the converter.
        /// </summary>
        public static IValueConverter Instance = new NullToVisibilityConverter();

        /// <summary>
        ///     Converts a null check into a visibility value. Visibile if not null, Hidden if null.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}