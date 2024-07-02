using Ebd.Mobile;
using Ebd.MobileApp.ViewModels.Perfil;

namespace Ebd.MobileApp.Views.Perfil;

public partial class PerfilPage : ContentPage
{
    private readonly PerfilPageViewModel viewModel;

    public PerfilPage()
    {
        InitializeComponent();
        BindingContext = viewModel = DependencyInjection.GetService<PerfilPageViewModel>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await viewModel.OnAppearingAsync();
    }
}