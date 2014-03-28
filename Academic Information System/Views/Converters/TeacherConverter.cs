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
            Teacher t = value as Teacher;
            string result = t.Name + " "+ t.Lastname;
            if (!string.IsNullOrWhiteSpace(t.Title))
            {
                result = t.Title + " " + result;
            }
            if (!string.IsNullOrWhiteSpace(t.TitleSuffix))
            {
                result = result + ", " + t.TitleSuffix;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
