using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Utils.Network
{
  public class LocalAddress
  {

    public string MacAddress => GetMacAddress();

    public string IpAddress => GetLocalIpAddress();

    private static string GetMacAddress()
    {
      var nics = NetworkInterface.GetAllNetworkInterfaces();
      string[] sMacAddress = {string.Empty};
      foreach (var adapter in nics.Where(adapter => string.IsNullOrEmpty(sMacAddress[0])))
        sMacAddress[0] = adapter.GetPhysicalAddress().ToString();
      
      return sMacAddress[0];
    }

    private static string GetLocalIpAddress()
    {
      var host = Dns.GetHostEntry(Dns.GetHostName());
      foreach (var ip in host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
        return ip.ToString();
      
      return string.Empty;
    }
  }
}
