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
            if (t == null || string.IsNullOrWhiteSpace(t.Name) || string.IsNullOrWhiteSpace(t.Lastname))
            {
                return "";
            }
            string name = t.Name + " " + t.Lastname;
            if (!string.IsNullOrWhiteSpace(t.Title))
            {
                name = t.Title + " " + name;
            }
            if (!string.IsNullOrWhiteSpace(t.TitleSuffix))
            {
                name = name + ", " + t.TitleSuffix;
            }
            return name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
