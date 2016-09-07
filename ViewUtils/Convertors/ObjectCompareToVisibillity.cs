using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ViewUtils.Convertors
{
  public class ObjectCompareToVisibillity : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue) return false;
      var flag = (values[0].Equals(values[1]));
      return flag ? Visibility.Visible : Visibility.Collapsed;
    }

    public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
    {
      return null;
    }
  }
}
