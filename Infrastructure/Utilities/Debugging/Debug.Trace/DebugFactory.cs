using Controls.Configuration;

namespace Controls.Debugging
{
    public static class DebugFactory
    {
        public static IDebug Create(IConfigService configService)
        {
            var traceConfiguration = configService.Get<TraceConfiguration>("Trace", "Trace");
            traceConfiguration.Fill();
            return new Debug(traceConfiguration.GetTraceCollection(), traceConfiguration.GetDefaultTrace());
        }
    }
}