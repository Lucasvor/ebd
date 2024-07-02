﻿namespace Ebd.Mobile.Services.Interfaces
{
    public interface IDialogService
    {
        Task DisplayAlert(string title, string message);
        Task DisplayAlert(string title, string message, string cancel);
        Task<bool> DisplayConfirmation(string? title, string message, string? accept = null, string? cancel = null);
        Task DisplayAlert(Exception ex);
        void ShowLoading(string message = "Processando...");
        public void HideLoading();
    }
}
