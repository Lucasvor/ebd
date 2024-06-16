using Ebd.CrossCutting.Common.Extensions;
using Ebd.Mobile.Services.Interfaces;
using Ebd.Mobile.Services.Responses.Turma;
using Ebd.MobileApp.Services.Interfaces.BottomSheets;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Ebd.MobileApp.ViewModels.Turma;

internal sealed class EscolherTurmaBottomSheetViewModel : BasePageViewModel
{
    private readonly ITurmaService turmaService;
    private readonly IConfiguracoesDoUsuarioService configuracoesDoUsuarioService;
    private readonly IEscolherTurmaBottomSheetService escolherTurmaBottomSheetService;

    public EscolherTurmaBottomSheetViewModel(IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService logger, ITurmaService turmaService, IConfiguracoesDoUsuarioService configuracoesDoUsuarioService, IEscolherTurmaBottomSheetService escolherTurmaBottomSheetService) : base(diagnosticService, dialogService, logger)
    {
        this.turmaService = turmaService;
        Turmas = new ObservableCollection<TurmaResponse>();
        TurmaSelecionadaCommand = new Command<TurmaResponse>(ExecutarTurmaSelecionadaCommand);
        this.configuracoesDoUsuarioService = configuracoesDoUsuarioService;
        this.escolherTurmaBottomSheetService = escolherTurmaBottomSheetService;
    }

    private ObservableCollection<TurmaResponse> turmas = new();
    public ObservableCollection<TurmaResponse> Turmas
    {
        get => turmas;
        set => SetProperty(ref turmas, value);
    }

    public ICommand TurmaSelecionadaCommand { get; set; }

    public override async Task OnAppearingAsync(object? parameter = null)
    {
        DialogService.ShowLoading("Carregando as turmas...");
        var respostaTurmas = await turmaService.ObterTodasAsync();
        HideLoading();

        if (respostaTurmas.IsSuccess)
        {
            if (respostaTurmas.Data.Any().Not())
            {
                await DialogService.DisplayAlert("Oops", "Nenhuma turma encontrada");
                return;
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Turmas.Clear();
                foreach (var turma in respostaTurmas.Data)
                {
                    Turmas.Add(turma);
                }
            });
        }
        else
        {
            Logger.LogError("Erro ao obter as turmas", respostaTurmas.Exception);
            await DialogService.DisplayAlert("Oops", "Erro ao obter o obter as turmas");
        }
    }

    private void ExecutarTurmaSelecionadaCommand(TurmaResponse turma)
    {
        Logger.LogInformation($"Turma selecionada: {turma.Nome}");

        configuracoesDoUsuarioService.TurmaSelecionada = turma;

        escolherTurmaBottomSheetService.FecharBottomSheetAsync();
    }
}
