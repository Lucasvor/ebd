namespace Ebd.Mobile.Services.Interfaces
{
    public interface IAnalyticsService
    {
        void ScreenOpened(string screenName);
        void ScreenClosed(string screenName);
    }
}
