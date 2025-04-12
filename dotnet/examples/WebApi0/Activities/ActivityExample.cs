
using System.Diagnostics;

namespace WebApi0
{
    public static class ActivityExample
    {
        public const string SourceName = "MyActivitySource";
        public static ActivitySource Source = new ActivitySource(SourceName);
        public static string Example1 = "My Custom activity1";
    }
}
