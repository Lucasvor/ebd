using Ebd.CrossCutting.Common.Extensions;
using Ebd.Mobile.Services.Interfaces;

namespace Ebd.MobileApp.ViewModels.Home;

internal sealed partial class HomeViewModel : BasePageViewModel
{
    public HomeViewModel(IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService logger, IAnalyticsService analyticsService, ITurmaService turmaService) : base(diagnosticService, dialogService, logger, analyticsService)
    {
        this.turmaService = turmaService;
    }

    private async Task InitializeHomeTab(object? parameter = null)
    {
        await Task.CompletedTask;
    }

    private async Task CertificarQueTurmaFoiSelecionada()
    {
        if (configuracoesDoUsuarioService.SelecionouUmaTurma.Not())
        {
            try
            {
                await escolherTurmaBottomSheetService.AbrirBottomSheetAsync(false);
                //var turmaResponse = await turmaService.ObterTodasAsync();
                //if (turmaResponse.IsSuccess)
                //{
                //    if (turmaResponse.Data.Any().Not())
                //    {
                //        await dialogService.DisplayAlert("Oops", "Nenhuma turma encontrada");
                //        return;
                //    }

                //    var escolherTurmaBottomSheet = new EscolherTurmaBottomSheet(turmaResponse.Data)
                //    {
                //        HasHandle = true,
                //        IsCancelable = false
                //    };

                //    await escolherTurmaBottomSheet.LoadDataAsync();
                //    await escolherTurmaBottomSheet.ShowAsync();
                //}
            }
            catch (Exception exception)
            {
                Logger.LogError($"{nameof(HomeViewModel)}::{nameof(OnAppearingAsync)}", exception);
            }
        }
    }
}
