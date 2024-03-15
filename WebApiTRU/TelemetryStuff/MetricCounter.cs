using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace WebApiTRU.TelemetryStuff
{
    public static class MetricCounter
    {
        public const string MetricCounterName = "Benson.Telemetry.Homwork.Counter";
        public static Meter MetricCounterMeter = new(MetricCounterName, "1.0.0");
    }
}
