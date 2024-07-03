using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ebd.Mobile.Services.Interfaces;
using Ebd.Mobile.Services.Responses.Turma;
using Ebd.Mobile.ViewModels.Aluno;
using Ebd.Mobile.Views.Chamada;
using Ebd.MobileApp.Services.Interfaces.BottomSheets;
using Ebd.MobileApp.ViewModels.Perfil;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Collections.ObjectModel;

namespace Ebd.MobileApp.ViewModels.Home
{
    internal sealed partial class HomeViewModel : BasePageViewModel
    {
        private readonly ISyncService syncService;
        private readonly IEscolherTurmaBottomSheetService escolherTurmaBottomSheetService;
        private readonly ITurmaService turmaService;
        private readonly IDialogService dialogService;
        private readonly IConfiguracoesDoUsuarioService configuracoesDoUsuarioService;

        public HomeViewModel(ISyncService syncService, IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService loggerService, IEscolherTurmaBottomSheetService escolherTurmaBottomSheetService, IConfiguracoesDoUsuarioService configuracoesDoUsuarioService, IAnalyticsService analyticsService, ITurmaService turmaService) : base(diagnosticService, dialogService, loggerService, analyticsService)
        {
            this.syncService = syncService;
            this.escolherTurmaBottomSheetService = escolherTurmaBottomSheetService;
            this.configuracoesDoUsuarioService = configuracoesDoUsuarioService;
            this.turmaService = turmaService;
            this.dialogService = dialogService;

            SetupScreenName("Inicio");
            Title = "Inicio";

            GoToAlunoPageCommand = new AsyncCommand(
                execute: ExecuteGoToAlunoPageCommand,
                onException: CommandOnException);

            GoToEscolherTurmaPageCommand = new AsyncCommand(
                execute: ExecuteGoToEscolherTurmaPageCommand,
                onException: CommandOnException);

            ClicouNaAbaPerfilCommand = new AsyncCommand(
                execute: ExecutarClicouNaAbaPerfilCommand,
                onException: CommandOnException);
        }

        public AsyncCommand GoToAlunoPageCommand { get; private set; }
        public AsyncCommand ClicouNaAbaPerfilCommand { get; private set; }
        public AsyncCommand GoToEscolherTurmaPageCommand { get; }

        [ObservableProperty]
        HomeTab _currentTab;

        public ObservableCollection<TurmaResponse> Turmas { get; } = new ObservableCollection<TurmaResponse>(new List<TurmaResponse> { new TurmaResponse { TurmaId = 1, Nome = "Turma 1", IdadeMinima = 22, IdadeMaxima = 90 } });


        [RelayCommand]
        void GoToTab(HomeTab destinationTab)
        {
            if (CurrentTab == destinationTab)
                return;

            CurrentTab = destinationTab;

            switch (CurrentTab)
            {
                case HomeTab.Home:
                    InitializeHomeTab().SafeFireAndForget();
                    break;
                case HomeTab.Classroom:
                    InitializeClassroomTab().SafeFireAndForget();
                    break;
                case HomeTab.Attendance:
                    InitializeAttendanceTab().SafeFireAndForget();
                    break;
                case HomeTab.Profile:
                    InitializeProfileTab().SafeFireAndForget();
                    break;
                default:
                    break;
            }
        }

        private async Task ExecuteGoToAlunoPageCommand()
        {
            if (IsBusy) return;
            IsBusy = true;
            await Navigate<ListaAlunoViewModel>();
            //await Shell.Current.GoToAsync(PageConstant.Aluno.Lista);
            IsBusy = false;
        }

        private async Task ExecuteGoToEscolherTurmaPageCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(EscolherTurmaPage)}");
        }

        private async Task ExecutarClicouNaAbaPerfilCommand()
        {
            if (IsBusy) return;

            IsBusy = true;
            await Navigate<PerfilPageViewModel>();
            IsBusy = false;
        }

        public async override Task OnAppearingAsync(object? parameter = null)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await base.OnAppearingAsync(parameter);
            await CertificarQueTurmaFoiSelecionada();

            IsBusy = false;
        }
    }
}
