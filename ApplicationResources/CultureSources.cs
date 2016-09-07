
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace ApplicationResources
{
  public class CultureSources
  {
    private static readonly object SyncObject = new object();

    private static volatile CultureSources _instance;
    public static CultureSources Instance
    {
      get
      {
        if (_instance != null) return _instance;
        lock (SyncObject)
        {
          if (_instance == null)
            _instance = new CultureSources();
        }
        return _instance;
      }
    }

    private CultureSources() { }


    private string[] GetCountryNames()
    {
      var countryNameDictonary = new Dictionary<string, string>();

      foreach (var cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
      {
        var regionInfo = new RegionInfo(cultureInfo.Name);
        var nativeName = regionInfo.NativeName;
        if (!countryNameDictonary.ContainsKey(nativeName))
          countryNameDictonary.Add(nativeName, regionInfo.TwoLetterISORegionName);
      }

      var orderedNames = countryNameDictonary.OrderBy(p => p.Key);

      var countries = orderedNames.ToDictionary(val => val.Key, val => val.Value);

      return countries.Keys.ToArray();
    }

    private string[] _countriesNames;
    public string[] CountriesNames => _countriesNames ?? (_countriesNames = GetCountryNames());
  }
}
