﻿using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WalkmanEditor.Converters
{
    public class String2BooleanReConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
