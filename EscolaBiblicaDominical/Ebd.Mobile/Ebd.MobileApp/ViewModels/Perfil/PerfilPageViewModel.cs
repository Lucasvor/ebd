using Ebd.Mobile.Services.Interfaces;
using Ebd.Mobile.Services.Responses.Turma;
using Ebd.Mobile.ViewModels;
using Ebd.MobileApp.Services.Interfaces.BottomSheets;
using Ebd.MobileApp.Services.Navigation;
using MvvmHelpers.Commands;

namespace Ebd.MobileApp.ViewModels.Perfil;

internal sealed partial class PerfilPageViewModel : BasePageViewModel
{
    private readonly IConfiguracoesDoUsuarioService configuracoesDoUsuarioService;
    private readonly IEscolherTurmaBottomSheetService escolherTurmaBottomSheetService;

    public PerfilPageViewModel(IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService logger, IAnalyticsService analyticsService, IConfiguracoesDoUsuarioService configuracoesDoUsuarioService, IEscolherTurmaBottomSheetService escolherTurmaBottomSheetService) : base(diagnosticService, dialogService, logger, analyticsService)
    {
        Title = "Perfil";
        SetupScreenName("Perfil");

        this.configuracoesDoUsuarioService = configuracoesDoUsuarioService;

        AlterarTurmaCommand = new AsyncCommand(
                execute: ExecutarExecutarClicouNaAbaPerfilCommand,
                onException: CommandOnException);

        LogoutCommand = new AsyncCommand(
                execute: ExecutarLogoutCommand,
                onException: CommandOnException);

        this.escolherTurmaBottomSheetService = escolherTurmaBottomSheetService;
    }

    public AsyncCommand AlterarTurmaCommand { get; set; }
    public AsyncCommand LogoutCommand { get; set; }

    private TurmaResponse? turmaSelecionada;
    public TurmaResponse? TurmaSelecionada
    {
        get => turmaSelecionada;
        set => SetProperty(ref turmaSelecionada, value);
    }


    private string? nome;
    public string? Nome
    {
        get => nome;
        set => SetProperty(ref nome, value);
    }

    private async Task ExecutarExecutarClicouNaAbaPerfilCommand()
    {
        if (IsBusy) return;

        IsBusy = true;
        await escolherTurmaBottomSheetService.AbrirBottomSheetAsync(true);
        IsBusy = false;
    }

    private async Task ExecutarLogoutCommand()
    {
        if (IsBusy) return;

        IsBusy = true;

        //ask if the wants to logout
        var querDeslogar = await DialogService.DisplayConfirmation("Atenção", "Deseja realmente sair?");

        if (querDeslogar)
        {
            configuracoesDoUsuarioService.LimparConfiguracoes();
            IsBusy = false;
            await MainThread.InvokeOnMainThreadAsync(async () => await NavigationService.Current.Navigate<LoginViewModel>("Disconnected by the user"));
        }
        IsBusy = false;
    }

    public async override Task OnAppearingAsync(object? parameter = null)
    {
        await base.OnAppearingAsync(parameter);

        if (configuracoesDoUsuarioService.SelecionouUmaTurma)
        {
            var turma = configuracoesDoUsuarioService.TurmaSelecionada;
            TurmaSelecionada = turma;
            Nome = turma!.Nome;
        }
    }
}
