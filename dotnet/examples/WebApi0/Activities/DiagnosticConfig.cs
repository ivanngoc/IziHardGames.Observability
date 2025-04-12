
using System.Diagnostics;
using SharedConstants;

namespace WebApi0
{

    public static class DiagnosticConfig
    {
        public const string SourceName = DiagnosticNames.Izi0;
        public const string ServiceName = "MyIziServiceForZipkin";
        public const string HostName = "IziHostName";
        public static ActivitySource Source = new ActivitySource(SourceName);
    }
}
