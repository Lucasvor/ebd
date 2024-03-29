using Ebd.Mobile.Services.Interfaces;
using System.Windows.Input;

namespace Ebd.Mobile.ViewModels
{
    internal class AboutViewModel : BaseViewModel
    {
        public AboutViewModel(IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService loggerService) : base(diagnosticService, dialogService, loggerService)
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public ICommand OpenWebCommand { get; }
    }
}