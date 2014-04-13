using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using AiS.ViewModels;
using System.Windows;
using System.Collections.ObjectModel;

namespace AiS.Views.Converters
{
    [ValueConversion(typeof(ObservableCollection<IViewModel>), typeof(Visibility))]
    public class OpenWindowsMenuVisibilityConverter : IValueConverter
    {
        public OpenWindowsMenuVisibilityConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ObservableCollection<IViewModel> collection = (ObservableCollection<IViewModel>)value;
            return collection.Count > 0 ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
