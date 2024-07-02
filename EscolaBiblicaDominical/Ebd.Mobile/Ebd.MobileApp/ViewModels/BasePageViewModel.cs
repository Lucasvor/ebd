using Ebd.CrossCutting.Common.Extensions;
using Ebd.Mobile.Services.Interfaces;
using Ebd.Mobile.ViewModels;

namespace Ebd.MobileApp.ViewModels;

internal abstract partial class BasePageViewModel : BaseViewModel
{
    protected string? ScreenName { get; private set; }

    protected BasePageViewModel(IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService logger, IAnalyticsService analyticsService) : base(diagnosticService, dialogService, logger, analyticsService)
    {
    }


    private string? title;
    public string? Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }


    protected void SetupScreenName(string screenName)
    {
        ScreenName = screenName;
    }

    public virtual Task OnAppearingAsync(object? parameter = null)
    {
        var nomeDaTelaFoiDefinido = string.IsNullOrWhiteSpace(ScreenName).Not();
        if (nomeDaTelaFoiDefinido)
        {
            AnalyticsService.ScreenOpened(ScreenName!);
        }

        return Task.CompletedTask;
    }
}
