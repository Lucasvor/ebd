using Ebd.Mobile.Services.Interfaces;

namespace Ebd.MobileApp.ViewModels.Perfil;

internal class PerfilPageViewModel : BasePageViewModel
{
    public PerfilPageViewModel(IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService logger) : base(diagnosticService, dialogService, logger)
    {
    }
}
