using Controls.UserDialogs.Maui;
using Ebd.Mobile.Services.Interfaces;
using System.Diagnostics;

namespace Ebd.Mobile.Services.Implementations.Dialog
{
    internal sealed class DialogService : IDialogService
    {
        private readonly IDiagnosticService _diagnosticService;

        public DialogService(IDiagnosticService diagnosticService)
        {
            _diagnosticService = diagnosticService;
        }

        public Task DisplayAlert(string title, string message) => DisplayAlert(title, message, "Ok");

        public Task DisplayAlert(string title, string message, string cancel) => DisplayAlert(title, message, "Ok", cancel);

        public Task DisplayAlert(Exception ex)
        {
            return ex switch
            {
                ArgumentException argumentException => DisplayAlert(null, argumentException.Message, "Ok"),
                InvalidOperationException invalidOperationException => DisplayAlert(null, invalidOperationException.Message, "Ok"),
                TaskCanceledException taskCanceledException => DisplayAlert("Operação cancelada", "Operação cancelada pelo usuário", "Ok"),
                _ => Task.Factory.StartNew(() =>
                {
                    _diagnosticService.TrackError(ex);

                    return DisplayAlert("Ah não", ex.Message, "Ok");
                })
            };
        }

        private Task DisplayAlert(string? title, string message, string accept, string cancel)
            => Application.Current?.MainPage?.DisplayAlert(title, message, accept, cancel) ?? Task.Run(() => Debug.WriteLine("Application.Current?.MainPage was null when trying to DisplayAlert"));

        public void ShowLoading(string message)
            => UserDialogs.Instance.ShowLoading(
                message: message,
                maskType: MaskType.Black);

        public void HideLoading()
            => UserDialogs.Instance.HideHud();
    }
}
