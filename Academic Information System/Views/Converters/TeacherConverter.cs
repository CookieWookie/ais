using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using AiS.Models;

namespace AiS.Views.Converters
{
    [ValueConversion(typeof(Teacher), typeof(string))]
    public class TeacherConverter : IValueConverter
    {
        public TeacherConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Teacher t = (Teacher)value;
            if (string.IsNullOrWhiteSpace(t.Name) || string.IsNullOrWhiteSpace(t.Lastname))
            {
                return "";
            }
            return t.Title + " " + t.Name + " " + t.Lastname + ", " + t.TitleSuffix;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
