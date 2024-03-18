using System.Diagnostics;

namespace WebApiTRU.TelemetryStuff;

public class TRUActivitySource
{
    public static string ActivitySourceName = "Bensons-Activity-Source-Name";
    public static ActivitySource ActualActivitySource = new(ActivitySourceName);
}