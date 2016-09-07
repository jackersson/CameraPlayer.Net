using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Castle.MicroKernel.Registration;

namespace ViewUtils.Convertors
{
  public class ObjectCompareConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue) return false;
      var flag = (values[0].Equals(values[1]));
      return flag;
    }

    public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
    {
      return null;
    }
  }
}
