using Ebd.Mobile;
using Ebd.MobileApp.ViewModels.Welcome;

namespace Ebd.MobileApp.Views.Welcome;

public partial class WelcomePage : ContentPage
{
    private readonly WelcomeViewModel viewModel;

    public WelcomePage()
    {
        InitializeComponent();
        BindingContext = viewModel ??= DependencyInjection.GetService<WelcomeViewModel>();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.OnAppearingAsync();
    }
}
