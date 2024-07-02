using CommunityToolkit.Mvvm.Input;
using Ebd.Mobile.Services.Interfaces;
using Ebd.MobileApp.ViewModels.Home;

namespace Ebd.MobileApp.ViewModels.Welcome;

internal sealed partial class WelcomeViewModel : BasePageViewModel
{
    public WelcomeViewModel(IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService logger, IAnalyticsService analyticsService) : base(diagnosticService, dialogService, logger, analyticsService)
    {
    }

    [RelayCommand]
    Task GetStarted() => Navigate<HomeViewModel>();

    public override async Task OnAppearingAsync(object? parameter = null)
    {
        await base.OnAppearingAsync(parameter);

        await Task.Delay(1000);
        await GetStarted();
    }
}
