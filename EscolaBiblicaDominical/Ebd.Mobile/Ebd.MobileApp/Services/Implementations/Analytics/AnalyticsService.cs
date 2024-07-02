using Ebd.Mobile.Services.Interfaces;

namespace Ebd.MobileApp.Services.Implementations.Analytics
{
    internal sealed class AnalyticsService : IAnalyticsService
    {
        private readonly ILoggerService _logger;

        public AnalyticsService(ILoggerService logger)
        {
            _logger = logger;
        }

        public void ScreenOpened(string screenName)
        {
            //TODO Implement track events
            _logger.LogInformation($"Enviando analytics TelaAberta: {screenName}");
        }

        public void ScreenClosed(string screenName)
        {
            //TODO Implement track events
            _logger.LogInformation($"Enviando analytics TelaFechada: {screenName}");
        }
    }
}
