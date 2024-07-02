﻿using Ebd.Mobile.Constants;
using Ebd.Mobile.Views;
using Ebd.Mobile.Views.Aluno;
using Ebd.Mobile.Views.Chamada;
using Ebd.MobileApp.Views.Perfil;

namespace Ebd.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(EfetuarChamadaPage), typeof(EfetuarChamadaPage));
            Routing.RegisterRoute(nameof(EscolherTurmaPage), typeof(EscolherTurmaPage));

            Routing.RegisterRoute(PageConstant.Aluno.Novo, typeof(NovoAlunoPage));
            Routing.RegisterRoute(PageConstant.Aluno.Lista, typeof(ListaAlunoPage));
            Routing.RegisterRoute(PageConstant.Aluno.ResponsavelAluno.Adicionar, typeof(AdicionarResponsavelPage));

            Routing.RegisterRoute(PageConstant.Perfil.Detalhes, typeof(PerfilPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
