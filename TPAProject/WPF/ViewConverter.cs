using Library.TreeView;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Presentation.WPF
{
    [ValueConversion(typeof(ItemTypeEnum), typeof(string))]
    class ViewConverter : IValueConverter
    {
        public static ViewConverter Converter = new ViewConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "[" + (ItemTypeEnum)value + "]";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }


}
