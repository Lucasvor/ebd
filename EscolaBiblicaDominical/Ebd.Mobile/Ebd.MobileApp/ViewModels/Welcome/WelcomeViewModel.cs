using Ebd.Mobile.Services.Interfaces;

namespace Ebd.MobileApp.ViewModels.Welcome;

internal class WelcomeViewModel : BasePageViewModel
{
    public WelcomeViewModel(IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService logger) : base(diagnosticService, dialogService, logger)
    {
    }
}
