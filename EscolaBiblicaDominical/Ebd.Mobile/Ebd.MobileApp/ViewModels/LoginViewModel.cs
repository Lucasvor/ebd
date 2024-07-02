using Ebd.Mobile.Services.Interfaces;
using Ebd.MobileApp.ViewModels;
using Ebd.MobileApp.ViewModels.Home;
using System.Windows.Input;

namespace Ebd.Mobile.ViewModels
{
    internal partial class LoginViewModel : BasePageViewModel
    {
        private readonly IUsuarioService usuarioService;
        private readonly ITurmaService turmaService;
        private readonly ISyncService syncService;

        public ICommand EfetuarLoginCommand { get; }

        public LoginViewModel(IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService loggerService, IUsuarioService usuarioService, ITurmaService turmaService, ISyncService syncService, IAnalyticsService analyticsService) : base(diagnosticService, dialogService, loggerService, analyticsService)
        {
            this.usuarioService = usuarioService;
            EfetuarLoginCommand = new Command(async () => await ClicouEmEfetuarLogin(), PodeEfetuarLogin);

            SetupScreenName("Login");
            Login = "admin";
            Senha = "admin";
            this.turmaService = turmaService;
            this.syncService = syncService;
        }

        private string login;
        public string Login
        {
            get => login;
            set
            {
                SetProperty(ref login, value);
                ((Command)EfetuarLoginCommand).ChangeCanExecute();
            }
        }

        private string senha;
        public string Senha
        {
            get => senha;
            set
            {
                SetProperty(ref senha, value);
                ((Command)EfetuarLoginCommand).ChangeCanExecute();
            }
        }

        private bool PodeEfetuarLogin()
        {
            return IsNotBusy && TodosCamposPreenchidosCorretamente();
        }

        private bool TodosCamposPreenchidosCorretamente()
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Senha);
        }

        private async Task ClicouEmEfetuarLogin()
        {
            if (IsBusy) return;
            IsBusy = true;
            DialogService.ShowLoading("Efetuando login...");

            var resposta = await usuarioService.EfetuarLoginAsync(new MobileApp.Services.Requests.Usuario.EfetuarLoginRequest
            {
                Login = Login,
                Senha = Senha
            });

            if (resposta.IsSuccess)
            {
                DialogService.ShowLoading("Sincronizando seus dados...");
                await SincronizarDados();
                DialogService.HideLoading();

                await Navigate<HomeViewModel>();
            }
            else
            {
                DialogService.HideLoading();
                await DialogService.DisplayAlert("Oops", resposta.Exception.Message);
            }

            IsBusy = false;
        }

        private async Task SincronizarDados()
        {
            try
            {
                Logger.LogInformation("Sincronizando dados...");
                await syncService.SyncDataAsync();
                Logger.LogInformation("Sincronização de dados concluída");
            }
            catch (Exception ex)
            {
                Logger.LogError("Erro ao sincroniar dados", ex);
                DiagnosticService.TrackError(ex, "Erro ao sincroniar dados");
            }
        }

        public override async Task OnAppearingAsync(object? parameter = null)
        {
            await base.OnAppearingAsync(parameter);

            Logger.LogInformation($"parameter: {parameter}");
        }
    }
}
