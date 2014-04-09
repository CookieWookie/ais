using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiS.Models;
using System.Windows.Data;

namespace AiS.Views.Converters
{
    [ValueConversion(typeof(Subject), typeof(string))]
    public class SubjectConverter : IValueConverter
    {
        public SubjectConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Subject s = (Subject)value;
            return s.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
