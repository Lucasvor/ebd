using Ebd.CrossCutting.Common.Extensions;
using Ebd.Mobile.Services.Interfaces;

namespace Ebd.Mobile.ViewModels.Aluno
{
    internal class AdicionarResponsavelViewModel : BaseViewModel
    {
        public AdicionarResponsavelViewModel(IDiagnosticService diagnosticService, IDialogService dialogService, ILoggerService logger, IAnalyticsService analyticsService) : base(diagnosticService, dialogService, logger, analyticsService)
        {
        }

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private bool formIsValid;
        public bool FormIsValid
        {
            get => formIsValid;
            set => SetProperty(ref formIsValid, value);
        }

        private string nome;
        public string Nome
        {
            get => nome;
            set
            {
                SetProperty(ref nome, value);
                CheckFormIsValid();
            }
        }

        private string dataNascimento;
        public string DataNascimento
        {
            get => dataNascimento;
            set
            {
                SetProperty(ref dataNascimento, value);
                CheckFormIsValid();
            }
        }

        private int bairroId;
        public int BairroId
        {
            get => bairroId;
            set
            {
                SetProperty(ref bairroId, value);
                CheckFormIsValid();
            }
        }

        private string celular;
        public string Celular
        {
            get => celular;
            set
            {
                SetProperty(ref celular, value);
                CheckFormIsValid();
            }
        }

        private string cep;
        public string Cep
        {
            get => cep;
            set
            {
                SetProperty(ref cep, value);
                CheckFormIsValid();
            }
        }

        private string numero;
        public string Numero
        {
            get => numero;
            set
            {
                SetProperty(ref numero, value);
                CheckFormIsValid();
            }
        }

        private string logradouro;
        public string Logradouro
        {
            get => logradouro;
            set
            {
                SetProperty(ref logradouro, value);
                CheckFormIsValid();
            }
        }

        private void CheckFormIsValid()
        {
            FormIsValid = string.IsNullOrWhiteSpace(Nome).Not()
                && DataNascimento is not null
                && string.IsNullOrWhiteSpace(Logradouro).Not()
                && string.IsNullOrWhiteSpace(Numero).Not()
                && BairroId != 0
                && string.IsNullOrWhiteSpace(Celular).Not() && Celular.Length == 15;
        }
    }
}
