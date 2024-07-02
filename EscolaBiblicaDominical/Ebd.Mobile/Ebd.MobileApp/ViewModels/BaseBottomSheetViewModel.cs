using Ebd.CrossCutting.Common.Extensions;
using Ebd.Mobile.Services.Interfaces;
using Ebd.Mobile.ViewModels;

namespace Ebd.MobileApp.ViewModels;

internal abstract class BaseBottomSheetViewModel : BaseViewModel
{
    protected string? BottomSheetName { get; private set; }

    protected BaseBottomSheetViewModel(IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService logger, IAnalyticsService analyticsService) : base(diagnosticService, dialogService, logger, analyticsService)
    {
    }

    protected void DefinirNomeDoBottomSheet(string name)
    {
        BottomSheetName = name;
    }

    public virtual Task LoadDataAsync(object? parameter = null)
    {
        var nameWasDefined = string.IsNullOrWhiteSpace(BottomSheetName).Not();
        if (nameWasDefined)
        {
            AnalyticsService.ScreenOpened(BottomSheetName!);
        }

        return Task.CompletedTask;
    }
}