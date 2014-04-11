using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using AiS.Models;

namespace AiS.Views.Converters
{
    [ValueConversion(typeof(Student), typeof(string))]
    public class StudentConverter : IValueConverter
    {
        public StudentConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Student s = (Student)value;
            return s.Name + " " + s.Lastname;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
