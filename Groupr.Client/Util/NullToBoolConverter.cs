using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Groupr.Client.Util
{
    /// <summary>
    /// Converts a null check into a boolean value. True if not null, false if null.
    /// </summary>
    class NullToBoolConverter: IValueConverter
    {
        /// <summary>
        /// Instance of the converter.
        /// </summary>
        public static IValueConverter Instance = new NullToBoolConverter();

        /// <summary>
        /// Converts a null check into a boolean value. True if not null, false if null.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
