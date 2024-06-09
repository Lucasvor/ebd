using Ebd.MobileApp.Services.Interfaces.BottomSheets;
using Ebd.MobileApp.Views.Turma;

namespace Ebd.MobileApp.Services.Implementations.BottomSheets;

public class EscolherTurmaBottomSheetService : IEscolherTurmaBottomSheetService
{
    public async Task AbrirBottomSheetAsync()
    {
        var bottomSheet = new EscolherTurmaBottomSheet
        {
            HasHandle = true,
            IsCancelable = false
        };

        await bottomSheet.ShowAsync();
    }
}
