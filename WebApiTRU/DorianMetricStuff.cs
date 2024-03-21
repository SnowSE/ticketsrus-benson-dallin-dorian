using System.Diagnostics.Metrics;
using WebApiTRU.Components;

public class DorianMetric
{
  public static int CounterNumber {get; set;} = 2;
  public const string MetricCounterName  = "dorian_counter";
  public static Meter DorianMeter = new (MetricCounterName, "1.0.0");
  public  static Counter<int> DorianCounter = DorianMeter.CreateCounter<int>(MetricCounterName);
}


public static partial class DorianLogging
{
  [LoggerMessage(Level = LogLevel.Information, Message = "Starting {appName}.")]
  public static partial void LogApplicationAccess(ILogger logger, string appName);
}


