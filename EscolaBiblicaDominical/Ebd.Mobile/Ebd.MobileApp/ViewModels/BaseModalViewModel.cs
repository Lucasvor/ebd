using Ebd.Mobile.Services.Interfaces;

namespace Ebd.MobileApp.ViewModels;

internal abstract class BaseModalViewModel : BasePageViewModel
{
    protected BaseModalViewModel(IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService logger, IAnalyticsService analyticsService) : base(diagnosticService, dialogService, logger, analyticsService)
    {
    }
}
