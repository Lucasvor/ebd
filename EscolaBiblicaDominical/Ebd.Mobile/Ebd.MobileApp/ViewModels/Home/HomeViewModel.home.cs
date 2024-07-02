using Ebd.Mobile.Services.Interfaces;

namespace Ebd.MobileApp.ViewModels.Home;

internal sealed partial class HomeViewModel : BasePageViewModel
{
    public HomeViewModel(IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService logger, IAnalyticsService analyticsService) : base(diagnosticService, dialogService, logger, analyticsService)
    {
    }

    private async Task InitializeHomeTab(object? parameter = null)
    {
        await Task.CompletedTask;
    }
}
