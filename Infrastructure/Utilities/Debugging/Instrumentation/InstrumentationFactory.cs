using Controls.Configuration;

namespace Controls.Debugging
{
    public static class InstrumentationFactory
    {
        public static IInstrumentation Create(IConfigService configService)
        {
            var instrumentationConfiguration = configService.Get<InstrumentationConfiguration>("Instrumentation", "Instrumentation");
            instrumentationConfiguration.Fill();
            return new Instrumentation(instrumentationConfiguration.GetCounterCollection(), instrumentationConfiguration.GetDefaultCounter());
        }
    }
}