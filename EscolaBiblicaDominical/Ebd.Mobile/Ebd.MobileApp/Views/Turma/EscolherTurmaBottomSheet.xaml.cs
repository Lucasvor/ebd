using Ebd.Mobile;
using Ebd.MobileApp.ViewModels.Turma;
using The49.Maui.BottomSheet;

namespace Ebd.MobileApp.Views.Turma;

public partial class EscolherTurmaBottomSheet : BottomSheet
{
    private readonly EscolherTurmaBottomSheetViewModel viewModel;

    public EscolherTurmaBottomSheet()
    {
        InitializeComponent();
        BindingContext = viewModel = DependencyInjection.GetService<EscolherTurmaBottomSheetViewModel>();
        MainThread.BeginInvokeOnMainThread(async () => await viewModel.OnAppearingAsync());
    }
}