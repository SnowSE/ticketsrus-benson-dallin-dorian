using System.Diagnostics.Metrics;
using WebApiTRU.Components;

public class DorianMetric
{
  public static int CounterNumber {get; set;} = 2;
  public static Meter DorianMeter = new ("DorianMeter", "1.0.0");
  public static Counter<int> TimesVisitedHomePage = DorianMeter.CreateCounter<int>("times_visited_home_page");
  public static Counter<long> TimeOnHomePageCounter = DorianMeter.CreateCounter<long>("time_spent_home_page");
  public  static Counter<int> ArianaTicketsSold = DorianMeter.CreateCounter<int>("tickets_sold_ariana");
  public  static Counter<int> OliviaTicketsSold = DorianMeter.CreateCounter<int>("tickets_sold_olivia");
  public  static Counter<int> RonnieTicketsSold = DorianMeter.CreateCounter<int>("tickets_sold_ronnie");


  public static Counter<int> TicketsScanned = DorianMeter.CreateCounter<int>("num_tickets_scanned");
}


public static partial class DorianLogging
{
  [LoggerMessage(Level = LogLevel.Information, Message = "Starting {appName}.")]
  public static partial void LogApplicationAccess(ILogger logger, string appName);
}


