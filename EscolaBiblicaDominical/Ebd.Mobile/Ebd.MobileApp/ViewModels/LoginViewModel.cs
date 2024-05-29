using AsyncAwaitBestPractices;
using Ebd.Mobile.Services.Interfaces;
using Ebd.MobileApp.ViewModels;
using Ebd.MobileApp.ViewModels.Home;
using System.Windows.Input;

namespace Ebd.Mobile.ViewModels
{
    internal partial class LoginViewModel : BasePageViewModel
    {
        private readonly IUsuarioService usuarioService;

        public ICommand EfetuarLoginCommand { get; }

        public LoginViewModel(IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService loggerService, IUsuarioService usuarioService) : base(diagnosticService, dialogService, loggerService)
        {
            this.usuarioService = usuarioService;
            EfetuarLoginCommand = new Command(async () => await ClicouEmEfetuarLogin(), PodeEfetuarLogin);
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

            var resposta = await usuarioService.EfetuarLoginAsync(new MobileApp.Services.Requests.Usuario.EfetuarLoginRequest
            {
                Login = Login,
                Senha = Senha
            });
            if (resposta.IsSuccess)
            {
                SyncData().SafeFireAndForget();
                await Navigate<HomeViewModel>();
            }
            else
            {
                await DialogService.DisplayAlert("Oops", resposta.Exception.Message);
            }
            //await Shell.Current.GoToAsync($"{nameof(HomePage)}");

            IsBusy = false;
        }


        private async Task SyncData()
        {
            await Task.Factory.StartNew(async () =>
            {
                Console.WriteLine("Sincronizando dados...");
                await Task.Delay(5000);
                Console.WriteLine("Sincronização de dados concluída");
            }).ConfigureAwait(false);

            Console.WriteLine("DEVE aparecer antes da conclusão da sincronização...");
        }
    }
}
