using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WalkmanEditor.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter, IMultiValueConverter
    {
        public Visibility VisibilityOfTrue { get; set; } = Visibility.Visible; 
        
        public Visibility VisibilityOfFalse { get; set; } = Visibility.Collapsed;

        public LogicalOperator MultiValueOperator { get; set; } = LogicalOperator.And;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToBoolean(value, CultureInfo.InvariantCulture) ? VisibilityOfTrue : VisibilityOfFalse;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            switch (MultiValueOperator)
            {
                case LogicalOperator.And:
                    return values.All(value => value is bool boolean && boolean) ? VisibilityOfTrue : VisibilityOfFalse;
                case LogicalOperator.Or:
                    return values.Any(value => value is bool boolean && boolean) ? VisibilityOfTrue : VisibilityOfFalse;
                default:
                    return VisibilityOfFalse;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Logical operators type enumeration
    /// </summary>
    public enum LogicalOperator
    {
        And, 
        Or
    }
}
