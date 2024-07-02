using Ebd.MobileApp.Services.Interfaces.BottomSheets;
using Ebd.MobileApp.Views.Turma;

namespace Ebd.MobileApp.Services.Implementations.BottomSheets;

public class EscolherTurmaBottomSheetService : IEscolherTurmaBottomSheetService
{
    private EscolherTurmaBottomSheet? EscolherTurmaBottomSheet;

    public async Task AbrirBottomSheetAsync()
    {
        EscolherTurmaBottomSheet = new EscolherTurmaBottomSheet
        {
            HasHandle = true,
            IsCancelable = false
        };

        await EscolherTurmaBottomSheet.ShowAsync();
    }

    public async Task FecharBottomSheetAsync()
    {
        if (EscolherTurmaBottomSheet is null)
            return;

        await EscolherTurmaBottomSheet.DismissAsync();
    }

    public void Dispose()
    {
        EscolherTurmaBottomSheet = null;
    }
}
