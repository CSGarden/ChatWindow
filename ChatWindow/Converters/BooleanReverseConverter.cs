using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWindow.Converters
{
    public class BooleanReverseConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            var result = (bool)value;
            return !result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
