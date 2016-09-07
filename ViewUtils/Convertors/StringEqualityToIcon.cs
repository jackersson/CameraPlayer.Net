using System;
using System.Windows.Data;
using ApplicationResources;

namespace ViewUtils.Convertors
{
  //Convertor for checking active, desired vs selected device
  public class StringEqualityToIcon : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
    {
      if (values == null)
        return Loader.CancelIcon;

      var collectionItem = values[0]?.ToString() ?? string.Empty;
      var activeItem     = values[1]?.ToString() ?? string.Empty;
      var desiredItem    = values[2]?.ToString() ?? string.Empty;

      var activeEmpty  = string.IsNullOrEmpty(activeItem);
      var desiredEmpty = string.IsNullOrEmpty(desiredItem);

      var desiredEqualsToCollectionItem = string.Equals(desiredItem, collectionItem);
      var desiredEqualsToActiveItem     = string.Equals(desiredItem, activeItem);
      var activeEqualsToCollectionItem  = string.Equals(desiredItem, activeItem);

      if (!activeEmpty && activeEqualsToCollectionItem && desiredEqualsToActiveItem)
        return Loader.OkIcon;
      else if (!activeEmpty && !desiredEmpty && !desiredEqualsToActiveItem && desiredEqualsToCollectionItem)
        return Loader.OkIcon;
      else if (activeEmpty && !desiredEmpty && desiredEqualsToCollectionItem)
        return Loader.OkIcon;

      return Loader.CancelIcon;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
        System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
